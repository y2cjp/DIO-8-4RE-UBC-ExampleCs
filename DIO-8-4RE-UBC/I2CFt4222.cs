// --------------------------------------------------------------------------------------------------------------------
// <copyright file="I2CFt4222.cs" company="Y2 Corporation">
//   Copyright (c) 2017 Y2 Corporation. All rights reserved.
// </copyright>
// <license>
//   MIT License
// 
//   Permission is hereby granted, free of charge, to any person obtaining a copy of
//   this software and associated documentation files (the "Software"), to deal in
//   the Software without restriction, including without limitation the rights to
//   use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
//   the Software, and to permit persons to whom the Software is furnished to do so,
//   subject to the following conditions:
//   
//   The above copyright notice and this permission notice shall be included in all
//   copies or substantial portions of the Software.
//   
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
//   FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
//   COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
//   IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//   CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </license>
// --------------------------------------------------------------------------------------------------------------------

#define waitWriteBusy

using System;
using System.Diagnostics;
using System.Text;

using FTD2XX_NET;

namespace DIO_8_4RE_UBC
{
    public class I2CFt4222 : IDisposable
    {
        private IntPtr ftHandle;

        ~I2CFt4222()
        {
            Dispose(false);
        }

        public ResultCode Open()
        {
            // variable
            FTDI.FT_DEVICE_INFO_NODE devInfo = new FTDI.FT_DEVICE_INFO_NODE();
            ftHandle = new IntPtr();

            // Check device
            uint numOfDevices = 0;
            LibFt4222.NativeMethods.FT_CreateDeviceInfoList(ref numOfDevices);

            if (numOfDevices > 0)
            {
                byte[] serialNumber = new byte[16];
                byte[] desc = new byte[64];

                LibFt4222.NativeMethods.FT_GetDeviceInfoDetail(0, ref devInfo.Flags, ref devInfo.Type, ref devInfo.ID, ref devInfo.LocId, serialNumber, desc, ref devInfo.ftHandle);

                devInfo.SerialNumber = Encoding.ASCII.GetString(serialNumber, 0, 16);
                devInfo.Description = Encoding.ASCII.GetString(desc, 0, 64);
                devInfo.SerialNumber = devInfo.SerialNumber.Substring(0, devInfo.SerialNumber.IndexOf("\0", StringComparison.Ordinal));
                devInfo.Description = devInfo.Description.Substring(0, devInfo.Description.IndexOf("\0", StringComparison.Ordinal));

                Debug.Print("Device Number: " + numOfDevices);
            }
            else
            {
                Debug.Print("No FTDI device");
                return ResultCode.DeviceNotFound;
            }

            // Open device
            FTDI.FT_STATUS ftStatus = LibFt4222.NativeMethods.FT_OpenEx(devInfo.LocId, LibFt4222.NativeMethods.FtOpenByLocation, ref ftHandle);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                Debug.Print("Open NG: " + ftStatus + " ***");
                return ResultCode.DeviceNotOpened;
            }

            // Set FT4222 clock
            LibFt4222.Ft4222ClockRate ft4222Clock = LibFt4222.Ft4222ClockRate.SysClk24;
            ResultCode result = (ResultCode)LibFt4222.NativeMethods.FT4222_SetClock(ftHandle, LibFt4222.Ft4222ClockRate.SysClk24);
            if (result != ResultCode.Ok)
            {
                Debug.Print("SetClock NG: " + result + " ***");
                return result;
            }

            Debug.Print("SetClock OK");

            result = (ResultCode)LibFt4222.NativeMethods.FT4222_GetClock(ftHandle, ref ft4222Clock);
            if (result != ResultCode.Ok)
            {
                Debug.Print("GetClock NG: " + result + " ***");
                return result;
            }

            Debug.Print("GetClock OK: " + ft4222Clock);

            // Init FT4222 I2C Master
            result = (ResultCode)LibFt4222.NativeMethods.FT4222_I2CMaster_Init(ftHandle, 100);   // 100kHz
            if (result != ResultCode.Ok)
            {
                Debug.Print("Open NG: " + result + " ***");
                return ResultCode.DeviceNotOpened;
            }

            Debug.Print("Open OK");

            return ResultCode.Ok;
        }

        public ResultCode Close()
        {
            if (ftHandle == IntPtr.Zero)
                return ResultCode.Ok;
            LibFt4222.Ft4222Status status = LibFt4222.NativeMethods.FT4222_UnInitialize(ftHandle);
            if (status != LibFt4222.Ft4222Status.Ft4222Ok)
                Debug.Print(status.ToString());
            FTDI.FT_STATUS ftStatus = LibFt4222.NativeMethods.FT_Close(ftHandle);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
                Debug.Print(ftStatus.ToString());
            ftHandle = IntPtr.Zero;
            Debug.Print("Close");
            return ResultCode.Ok;
        }

        public LibFt4222.Ft4222Status SetClock(LibFt4222.Ft4222ClockRate clk)
        {
            return LibFt4222.NativeMethods.FT4222_SetClock(ftHandle, clk);
        }

        public ResultCode ReadEx(ushort deviceAddress, byte flag, ref byte buffer, int bytesToRead)
        {
            ushort sizeTransferred = 0;
            LibFt4222.Ft4222Status result = LibFt4222.NativeMethods.FT4222_I2CMaster_ReadEx(ftHandle, deviceAddress, flag, ref buffer, (ushort)bytesToRead, ref sizeTransferred);
            if (result != LibFt4222.Ft4222Status.Ft4222Ok)
                return (ResultCode)result;

            // System.Threading.Thread.Sleep(1);
            byte status = 0;
            result = LibFt4222.NativeMethods.FT4222_I2CMaster_GetStatus(ftHandle, ref status);
            if (result != LibFt4222.Ft4222Status.Ft4222Ok)
                return (ResultCode)result;
            if ((status & 0x01) != 0)
                return ResultCode.I2CmControllerBusy;
            if ((status & 0x0a) != 0)
                return ResultCode.I2CmDataNack;
            if ((status & 0x06) != 0)
                return ResultCode.I2CmAddressNack;
            if ((status & 0x12) != 0)
                return ResultCode.I2CmArbLost;
            if ((status & 0x40) != 0)
                return ResultCode.I2CmBusBusy;
            return (ResultCode)LibFt4222.Ft4222Status.Ft4222Ok;
        }

        public ResultCode Read(ushort slaveAddress, ref byte buffer, int bytesToRead)
        {
            return ReadEx(slaveAddress, (byte)LibFt4222.I2CMasterFlag.StartAndStop, ref buffer, bytesToRead);
        }

        public ResultCode WriteEx(ushort deviceAddress, byte flag, ref byte buffer, int bytesToWrite)
        {
            LibFt4222.Ft4222Status result;
            byte status = 0;

            for (int wait = 0; wait < int.MaxValue; wait++)
            {
                result = LibFt4222.NativeMethods.FT4222_I2CMaster_GetStatus(ftHandle, ref status);
                if (result != LibFt4222.Ft4222Status.Ft4222Ok)
                    return (ResultCode)result;
                if ((status & 0x20) != 0)
                    break;
                if (wait == 10000)
                    return ResultCode.I2CmTimeout;
            }

            ushort sizeTransferred = 0;
            result = LibFt4222.NativeMethods.FT4222_I2CMaster_WriteEx(ftHandle, deviceAddress, flag, ref buffer, (ushort)bytesToWrite, ref sizeTransferred);
            if (result != LibFt4222.Ft4222Status.Ft4222Ok)
                return (ResultCode)result;
#if waitWriteBusy
            ResultCode lastResult = ResultCode.Ok;
            for (int wait = 0; wait < int.MaxValue; wait++)
            {
                result = LibFt4222.NativeMethods.FT4222_I2CMaster_GetStatus(ftHandle, ref status);
                if (result != LibFt4222.Ft4222Status.Ft4222Ok)
                    return (ResultCode)result;
                if (status == 0x20)
                    return ResultCode.Ok;
                if ((flag & (byte)LibFt4222.I2CMasterFlag.Stop) == 0 && status == 0x40)
                    return ResultCode.Ok;
                if ((status & 0x01) != 0)
                    lastResult = ResultCode.I2CmControllerBusy;
                else if ((status & 0x0a) != 0)
                    return ResultCode.I2CmDataNack;
                else if ((status & 0x06) != 0)
                    return ResultCode.I2CmAddressNack;
                else if ((status & 0x12) != 0)
                    return ResultCode.I2CmArbLost;
                else if ((status & 0x40) != 0)
                    lastResult = ResultCode.I2CmBusBusy;
                else if (wait == 1000000)
                    return ResultCode.I2CmTimeout;

                // System.Threading.Thread.Sleep(1);
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

        public ResultCode Write(ushort slaveAddress, ref byte buffer, int bytesToWrite)
        {
            return WriteEx(slaveAddress, (byte)LibFt4222.I2CMasterFlag.StartAndStop, ref buffer, bytesToWrite);
        }

        public ResultCode GetStatus(ref byte controllerStatus)
        {
            return (ResultCode)LibFt4222.NativeMethods.FT4222_I2CMaster_GetStatus(ftHandle, ref controllerStatus);
        }

        public ResultCode GetVersion(ref LibFt4222.Ft4222Version version)
        {
            return (ResultCode)LibFt4222.NativeMethods.FT4222_GetVersion(ftHandle, ref version);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Close();
            ftHandle = IntPtr.Zero;
        }
    }
}
