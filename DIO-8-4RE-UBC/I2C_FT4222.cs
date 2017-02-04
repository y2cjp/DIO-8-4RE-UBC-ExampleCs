// 
// Copyright (c) 2017 Y2 Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 

#define waitWriteBusy

using FTD2XX_NET;
using System;
using System.Diagnostics;
using System.Text;
using static Y2C_I2C_Libraries.LibFT4222;

namespace Y2C_I2C_Libraries {
    public class I2C_FT4222 {

        private IntPtr ftHandle;

        public I2C_FT4222() {
        }

        public ResultCode Open() {
            // variable
            FTDI.FT_DEVICE_INFO_NODE devInfo = new FTDI.FT_DEVICE_INFO_NODE();
            ftHandle = new IntPtr();
            FTDI.FT_STATUS ftStatus = 0;
            ResultCode result;

            // Check device
            UInt32 numOfDevices = 0;
            ftStatus = LibFT4222.FT_CreateDeviceInfoList(ref numOfDevices);

            if (numOfDevices > 0) {
                byte[] sernum = new byte[16];
                byte[] desc = new byte[64];

                ftStatus = FT_GetDeviceInfoDetail(0, ref devInfo.Flags, ref devInfo.Type, ref devInfo.ID, ref devInfo.LocId,
                                            sernum, desc, ref devInfo.ftHandle);

                devInfo.SerialNumber = Encoding.ASCII.GetString(sernum, 0, 16);
                devInfo.Description = Encoding.ASCII.GetString(desc, 0, 64);
                devInfo.SerialNumber = devInfo.SerialNumber.Substring(0, devInfo.SerialNumber.IndexOf("\0"));
                devInfo.Description = devInfo.Description.Substring(0, devInfo.Description.IndexOf("\0"));

                Debug.Print("Device Number: " + numOfDevices.ToString());
            } else {
                Debug.Print("No FTDI device");
                return ResultCode.DeviceNotFound;
            }

            // Open device
            ftStatus = FT_OpenEx(devInfo.LocId, FT_OPEN_BY_LOCATION, ref ftHandle);
            if (ftStatus != FTDI.FT_STATUS.FT_OK) {
                Debug.Print("Open NG: " + ftStatus + " ***");
                return ResultCode.DeviceNotOpened;
            }

            // Set FT4222 clock
            LibFT4222.FT4222_ClockRate ft4222_Clock = LibFT4222.FT4222_ClockRate.SYS_CLK_24;
            result = (ResultCode)LibFT4222.FT4222_SetClock(ftHandle, LibFT4222.FT4222_ClockRate.SYS_CLK_24);
            if (result != ResultCode.OK) {
                Debug.Print("SetClock NG: " + result.ToString() + " ***");
                return result;
            } else {
                Debug.Print("SetClock OK");

                result = (ResultCode)LibFT4222.FT4222_GetClock(ftHandle, ref ft4222_Clock);
                if (result != ResultCode.OK) {
                    Debug.Print("GetClock NG: " + result.ToString() + " ***");
                    return result;
                } else {
                    Debug.Print("GetClock OK: " + ft4222_Clock);
                }
            }

            // Init FT4222 I2C Master
            result = (ResultCode)LibFT4222.FT4222_I2CMaster_Init(ftHandle, 100);   // 100kHz
            if (result != ResultCode.OK) {
                Debug.Print("Open NG: " + result.ToString() + " ***");
                return ResultCode.DeviceNotOpened;
            } else {
                Debug.Print("Open OK");
            }
            return ResultCode.OK;
        }

        public ResultCode Close() {
            if (ftHandle == IntPtr.Zero)
                return ResultCode.OK;
            FT4222_STATUS status = FT4222_UnInitialize(ftHandle);
            if (status != LibFT4222.FT4222_STATUS.FT4222_OK) {
                Debug.Print(status.ToString());
            }
            FTDI.FT_STATUS ftStatus = FT_Close(ftHandle);
            if (ftStatus != FTDI.FT_STATUS.FT_OK) {
                Debug.Print(ftStatus.ToString());
            }
            ftHandle = IntPtr.Zero;
            Debug.Print("Close");
            return ResultCode.OK;
        }

        public FT4222_STATUS SetClock(FT4222_ClockRate clk) {
            return FT4222_SetClock(ftHandle, clk);
        }

        public ResultCode ReadEx(ushort deviceAddress, byte flag, ref byte buffer, int bytesToRead) {
            ushort sizeTransferred = 0;
            FT4222_STATUS result = FT4222_I2CMaster_ReadEx(ftHandle, deviceAddress, flag, ref buffer, (ushort)bytesToRead, ref sizeTransferred);
            if (result != FT4222_STATUS.FT4222_OK)
                return (ResultCode)result;
            //System.Threading.Thread.Sleep(1);
            byte status = 0;
            result = FT4222_I2CMaster_GetStatus(ftHandle, ref status);
            if (result != FT4222_STATUS.FT4222_OK)
                return (ResultCode)result;
            if ((status & 0x01) != 0)
                return ResultCode.I2cmControllerBusy;
            else if ((status & 0x0a) != 0)
                return ResultCode.I2cmDataNack;
            else if ((status & 0x06) != 0)
                return ResultCode.I2cmAddressNack;
            else if ((status & 0x12) != 0)
                return ResultCode.I2cmArbLost;
            else if ((status & 0x40) != 0)
                return ResultCode.I2cmBusBusy;
            return (ResultCode)FT4222_STATUS.FT4222_OK;
        }

        public ResultCode Read(ushort slaveAddress, ref byte buffer, int bytesToRead) {
            return ReadEx(slaveAddress, (byte)I2cMasterFlag.StartAndStop, ref buffer, bytesToRead);
        }

        public ResultCode WriteEx(ushort deviceAddress, byte flag, ref byte buffer, int bytesToWrite) {
            FT4222_STATUS result;
            byte status = 0;

            for (int wait = 0; wait < int.MaxValue; wait++) {
                result = FT4222_I2CMaster_GetStatus(ftHandle, ref status);
                if (result != FT4222_STATUS.FT4222_OK)
                    return (ResultCode)result;
                else if ((status & 0x20) != 0)
                    break;
                else if (wait == 10000)
                    return ResultCode.I2cmTimeout;
            }

            ushort sizeTransferred = 0;
            result = FT4222_I2CMaster_WriteEx(ftHandle, deviceAddress, flag, ref buffer, (ushort)bytesToWrite, ref sizeTransferred);
            if (result != FT4222_STATUS.FT4222_OK)
                return (ResultCode)result;
#if waitWriteBusy
            ResultCode lastResult = ResultCode.OK;
            for (int wait = 0; wait < int.MaxValue; wait++) {
                result = FT4222_I2CMaster_GetStatus(ftHandle, ref status);
                if (result != FT4222_STATUS.FT4222_OK)
                    return (ResultCode)result;
                else if (status == 0x20)
                    return ResultCode.OK;
                else if (((flag & (byte)I2cMasterFlag.Stop) == 0) && (status == 0x40))
                    return ResultCode.OK;
                else if ((status & 0x01) != 0)
                    lastResult = ResultCode.I2cmControllerBusy;
                else if ((status & 0x0a) != 0)
                    return ResultCode.I2cmDataNack;
                else if ((status & 0x06) != 0)
                    return ResultCode.I2cmAddressNack;
                else if ((status & 0x12) != 0)
                    return ResultCode.I2cmArbLost;
                else if ((status & 0x40) != 0)
                    lastResult = ResultCode.I2cmBusBusy;
                else if (wait == 1000000)
                    return ResultCode.I2cmTimeout;
                //System.Threading.Thread.Sleep(1);
            }
            return lastResult;
#else
            result = FT4222_I2CMaster_GetStatus(ftHandle, ref status);
            if (result != FT4222_STATUS.FT4222_OK)
                return (ResultCode)result;
            else if ((status & 0x0a) != 0)
                return ResultCode.I2cmDataNack;
            else if ((status & 0x06) != 0)
                return ResultCode.I2cmAddressNack;
            else if ((status & 0x12) != 0)
                return ResultCode.I2cmArbLost;
            else
                return ResultCode.OK;
#endif
        }

        public ResultCode Write(ushort slaveAddress, ref byte buffer, int bytesToWrite) {
            return WriteEx(slaveAddress, (byte)LibFT4222.I2cMasterFlag.StartAndStop, ref buffer, bytesToWrite);
        }

        public ResultCode GetStatus(ref byte controllerStatus) {
            return (ResultCode)FT4222_I2CMaster_GetStatus(ftHandle, ref controllerStatus);
        }

        public ResultCode GetVersion(ref FT4222_Version version) {
            return (ResultCode)FT4222_GetVersion(ftHandle, ref version);
        }


    }
}
