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
using FTD2XX_NET;
using System;
using System.Runtime.InteropServices;

namespace Y2C_I2C_Libraries {
    public class LibFT4222 {

        //**************************************************************************
        //
        // FUNCTION IMPORTS FROM FTD2XX DLL
        //
        //**************************************************************************

        [DllImport("ftd2xx.dll")]
        public static extern FTDI.FT_STATUS FT_CreateDeviceInfoList(ref UInt32 numdevs);

        [DllImport("ftd2xx.dll")]
        public static extern FTDI.FT_STATUS FT_GetDeviceInfoDetail(UInt32 index, ref UInt32 flags, ref FTDI.FT_DEVICE chiptype, ref UInt32 id, ref UInt32 locid, byte[] serialnumber, byte[] description, ref IntPtr ftHandle);

        //[DllImportAttribute("ftd2xx.dll", CallingConvention = CallingConvention.Cdecl)]
        [DllImport("ftd2xx.dll")]
        public static extern FTDI.FT_STATUS FT_OpenEx(uint pvArg1, int dwFlags, ref IntPtr ftHandle);

        //[DllImportAttribute("ftd2xx.dll", CallingConvention = CallingConvention.Cdecl)]
        [DllImport("ftd2xx.dll")]
        public static extern FTDI.FT_STATUS FT_Close(IntPtr ftHandle);

        public const byte FT_OPEN_BY_SERIAL_NUMBER = 1;
        public const byte FT_OPEN_BY_DESCRIPTION = 2;
        public const byte FT_OPEN_BY_LOCATION = 4;

        //**************************************************************************
        //
        // FUNCTION IMPORTS FROM LIBFT4222 DLL
        //
        //**************************************************************************

        // FT4222 General Functions

        [DllImport("LibFT4222.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern FT4222_STATUS FT4222_UnInitialize(IntPtr ftHandle);

        [DllImport("LibFT4222.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern FT4222_STATUS FT4222_SetClock(IntPtr ftHandle, FT4222_ClockRate clk);

        [DllImport("LibFT4222.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern FT4222_STATUS FT4222_GetClock(IntPtr ftHandle, ref FT4222_ClockRate clk);

        [DllImport("LibFT4222.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern FT4222_STATUS FT4222_SetWakeUpInterrupt(IntPtr ftHandle, bool enable);

        [DllImport("LibFT4222.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern FT4222_STATUS FT4222_SetInterruptTrigger(IntPtr ftHandle, GpioTrigger trigger);

        [DllImport("LibFT4222.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern FT4222_STATUS FT4222_SetSuspendOut(IntPtr ftHandle, bool enable);

        [DllImport("LibFT4222.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern FT4222_STATUS FT4222_GetMaxTransferSize(IntPtr ftHandle, ref UInt16 pMaxSize);

        [DllImport("LibFT4222.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern FT4222_STATUS FT4222_SetEventNotification(IntPtr ftHandle, UInt32 mask, IntPtr param);

        [DllImport("LibFT4222.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern FT4222_STATUS FT4222_GetVersion(IntPtr ftHandle, ref FT4222_Version pVersion);

        // FT4222 SPI Functions

        [DllImport("LibFT4222.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern FT4222_STATUS FT4222_SPIMaster_Init(IntPtr ftHandle, FT4222_SPIMode ioLine, FT4222_SPIClock clock, FT4222_SPICPOL cpol, FT4222_SPICPHA cpha, Byte ssoMap);

        [DllImport("LibFT4222.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern FT4222_STATUS FT4222_SPI_SetDrivingStrength(IntPtr ftHandle, SPI_DrivingStrength clkStrength, SPI_DrivingStrength ioStrength, SPI_DrivingStrength ssoStregth);

        [DllImport("LibFT4222.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern FT4222_STATUS FT4222_SPIMaster_SingleReadWrite(IntPtr ftHandle, ref byte readBuffer, ref byte writeBuffer, ushort bufferSize, ref ushort sizeTransferred, bool isEndTransaction);

        // FT4222 I2C Functions

        [DllImport("LibFT4222.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern FT4222_STATUS FT4222_I2CMaster_Init(IntPtr ftHandle, UInt32 kbps);

        //[DllImport("LibFT4222.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern FT4222_STATUS FT4222_I2CMaster_Read(IntPtr ftHandle, UInt16 slaveAddress, ref byte buffer, UInt16 bytesToRead, ref UInt16 sizeTransferred);

        //[DllImport("LibFT4222.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern FT4222_STATUS FT4222_I2CMaster_Write(IntPtr ftHandle, UInt16 slaveAddress, ref byte buffer, UInt16 bytesToWrite, ref UInt16 sizeTransferred);

        [DllImport("LibFT4222.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern FT4222_STATUS FT4222_I2CMaster_ReadEx(IntPtr ftHandle, UInt16 deviceAddress, byte flag, ref byte buffer, UInt16 bytesToRead, ref UInt16 sizeTransferred);

        [DllImport("LibFT4222.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern FT4222_STATUS FT4222_I2CMaster_WriteEx(IntPtr ftHandle, UInt16 deviceAddress, byte flag, ref byte buffer, UInt16 bytesToWrite, ref UInt16 sizeTransferred);

        [DllImport("LibFT4222.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern FT4222_STATUS FT4222_I2CMaster_Reset(IntPtr ftHandle);

        [DllImport("LibFT4222.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern FT4222_STATUS FT4222_I2CMaster_GetStatus(IntPtr ftHandle, ref byte controllerStatus);

        // FT4222 Device status
        public enum FT4222_STATUS {
            FT4222_OK,
            FT4222_INVALID_HANDLE,
            FT4222_DEVICE_NOT_FOUND,
            FT4222_DEVICE_NOT_OPENED,
            FT4222_IO_ERROR,
            FT4222_INSUFFICIENT_RESOURCES,
            FT4222_INVALID_PARAMETER,
            FT4222_INVALID_BAUD_RATE,
            FT4222_DEVICE_NOT_OPENED_FOR_ERASE,
            FT4222_DEVICE_NOT_OPENED_FOR_WRITE,
            FT4222_FAILED_TO_WRITE_DEVICE,
            FT4222_EEPROM_READ_FAILED,
            FT4222_EEPROM_WRITE_FAILED,
            FT4222_EEPROM_ERASE_FAILED,
            FT4222_EEPROM_NOT_PRESENT,
            FT4222_EEPROM_NOT_PROGRAMMED,
            FT4222_INVALID_ARGS,
            FT4222_NOT_SUPPORTED,
            FT4222_OTHER_ERROR,
            FT4222_DEVICE_LIST_NOT_READY,

            FT4222_DEVICE_NOT_SUPPORTED = 1000,        // FT_STATUS extending message
            FT4222_CLK_NOT_SUPPORTED,     // spi master do not support 80MHz/CLK_2
            FT4222_VENDER_CMD_NOT_SUPPORTED,
            FT4222_IS_NOT_SPI_MODE,
            FT4222_IS_NOT_I2C_MODE,
            FT4222_IS_NOT_SPI_SINGLE_MODE,
            FT4222_IS_NOT_SPI_MULTI_MODE,
            FT4222_WRONG_I2C_ADDR,
            FT4222_INVAILD_FUNCTION,
            FT4222_INVALID_POINTER,
            FT4222_EXCEEDED_MAX_TRANSFER_SIZE,
            FT4222_FAILED_TO_READ_DEVICE,
            FT4222_I2C_NOT_SUPPORTED_IN_THIS_MODE,
            FT4222_GPIO_NOT_SUPPORTED_IN_THIS_MODE,
            FT4222_GPIO_EXCEEDED_MAX_PORTNUM,
            FT4222_GPIO_WRITE_NOT_SUPPORTED,
            FT4222_GPIO_PULLUP_INVALID_IN_INPUTMODE,
            FT4222_GPIO_PULLDOWN_INVALID_IN_INPUTMODE,
            FT4222_GPIO_OPENDRAIN_INVALID_IN_OUTPUTMODE,
            FT4222_INTERRUPT_NOT_SUPPORTED,
            FT4222_GPIO_INPUT_NOT_SUPPORTED,
            FT4222_EVENT_NOT_SUPPORTED,
            FT4222_FUN_NOT_SUPPORT,
        };

        public enum FT4222_ClockRate {
            SYS_CLK_60 = 0,
            SYS_CLK_24,
            SYS_CLK_48,
            SYS_CLK_80,
        };

        public enum FT4222_FUNCTION {
            FT4222_I2C_MASTER = 1,
            FT4222_I2C_SLAVE,
            FT4222_SPI_MASTER,
            FT4222_SPI_SLAVE,

        };

        public enum FT4222_SPIMode {
            SPI_IO_NONE = 0,
            SPI_IO_SINGLE = 1,
            SPI_IO_DUAL = 2,
            SPI_IO_QUAD = 4,
        };

        public enum FT4222_SPIClock {
            CLK_NONE = 0,
            CLK_DIV_2,      // 1/2   System Clock
            CLK_DIV_4,      // 1/4   System Clock
            CLK_DIV_8,      // 1/8   System Clock
            CLK_DIV_16,     // 1/16  System Clock
            CLK_DIV_32,     // 1/32  System Clock
            CLK_DIV_64,     // 1/64  System Clock
            CLK_DIV_128,    // 1/128 System Clock
            CLK_DIV_256,    // 1/256 System Clock
            CLK_DIV_512,    // 1/512 System Clock
        };

        public enum FT4222_SPICPOL {
            CLK_IDLE_LOW = 0,
            CLK_IDLE_HIGH = 1,
        };

        public enum FT4222_SPICPHA {
            CLK_LEADING = 0,
            CLK_TRAILING = 1,
        };

        public enum SPI_DrivingStrength {
            DS_4MA = 0,
            DS_8MA,
            DS_12MA,
            DS_16MA,
        };

        public enum GPIO_Port {
            GPIO_PORT0 = 0,
            GPIO_PORT1,
            GPIO_PORT2,
            GPIO_PORT3
        };

        public enum GPIO_Dir {
            GPIO_OUTPUT = 0,
            GPIO_INPUT,
        };

        public enum GpioTrigger {
            TriggerRising = 0x01,
            TriggerFalling = 0x02,
            TriggerLevelHigh = 0x04,
            TriggerLevelLow = 0X08
        };

        public enum GPIO_Output {
            GPIO_OUTPUT_LOW,
            GPIO_OUTPUT_HIGH
        };

        public enum I2cMasterFlag {
            None = 0x80,
            Start = 0x02,
            RepeatedStart = 0x03,     // Repeated_START will not send master code in HS mode
            Stop = 0x04,
            StartAndStop = 0x06,      // START condition followed by SEND and STOP condition
        };

        public enum SPI_SlaveProtocol {
            SPI_SLAVE_WITH_PROTOCOL = 0,
            SPI_SLAVE_NO_PROTOCOL,
            SPI_SLAVE_NO_ACK,
        };

        public enum ChipVersion {
            A = 0x42220100,
            B = 0x42220200,
            C = 0x42220300,
            Max = 0x4222ffff,
        }

        public struct FT4222_Version {
            public UInt32 chipVersion;
            public UInt32 dllVersion;
        };






    }
}
