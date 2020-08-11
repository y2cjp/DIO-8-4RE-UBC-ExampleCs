// MIT License
//
// Copyright (c) Y2 Corporation

using System;
using System.Runtime.InteropServices;
using FTD2XX_NET;

namespace Dio84ReUbc.NfLibrary
{
    public class LibFt4222
    {
        // FT4222 Device status
        public enum Ft4222Status
        {
            Ft4222Ok,
            Ft4222InvalidHandle,
            Ft4222DeviceNotFound,
            Ft4222DeviceNotOpened,
            Ft4222IoError,
            Ft4222InsufficientResources,
            Ft4222InvalidParameter,
            Ft4222InvalidBaudRate,
            Ft4222DeviceNotOpenedForErase,
            Ft4222DeviceNotOpenedForWrite,
            Ft4222FailedToWriteDevice,
            Ft4222EepromReadFailed,
            Ft4222EepromWriteFailed,
            Ft4222EepromEraseFailed,
            Ft4222EepromNotPresent,
            Ft4222EepromNotProgrammed,
            Ft4222InvalidArgs,
            Ft4222NotSupported,
            Ft4222OtherError,
            Ft4222DeviceListNotReady,

            Ft4222DeviceNotSupported = 1000,    // FT_STATUS extending message
            Ft4222ClkNotSupported,              // spi master do not support 80MHz/CLK_2
            Ft4222VenderCmdNotSupported,
            Ft4222IsNotSpiMode,
            Ft4222IsNotI2CMode,
            Ft4222IsNotSpiSingleMode,
            Ft4222IsNotSpiMultiMode,
            Ft4222WrongI2CAddr,
            Ft4222InvalidFunction,
            Ft4222InvalidPointer,
            Ft4222ExceededMaxTransferSize,
            Ft4222FailedToReadDevice,
            Ft4222I2CNotSupportedInThisMode,
            Ft4222GpioNotSupportedInThisMode,
            Ft4222GpioExceededMaxPortNum,
            Ft4222GpioWriteNotSupported,
            Ft4222GpioPullupInvalidInInputmode,
            Ft4222GpioPulldownInvalidInInputmode,
            Ft4222GpioOpendrainInvalidInOutputmode,
            Ft4222InterruptNotSupported,
            Ft4222GpioInputNotSupported,
            Ft4222EventNotSupported,
            Ft4222FunNotSupport,
        }

        public enum Ft4222ClockRate
        {
            SysClk60 = 0,
            SysClk24,
            SysClk48,
            SysClk80,
        }

        public enum Ft4222Function
        {
            Ft4222I2CMaster = 1,
            Ft4222I2CSlave,
            Ft4222SpiMaster,
            Ft4222SpiSlave,
        }

        public enum Ft4222SpiMode
        {
            SpiIoNone = 0,
            SpiIoSingle = 1,
            SpiIoDual = 2,
            SpiIoQuad = 4,
        }

        public enum Ft4222SpiClock
        {
            ClkNone = 0,
            ClkDiv2,    // 1/2   System Clock
            ClkDiv4,    // 1/4   System Clock
            ClkDiv8,    // 1/8   System Clock
            ClkDiv16,   // 1/16  System Clock
            ClkDiv32,   // 1/32  System Clock
            ClkDiv64,   // 1/64  System Clock
            ClkDiv128,  // 1/128 System Clock
            ClkDiv256,  // 1/256 System Clock
            ClkDiv512,  // 1/512 System Clock
        }

        public enum Ft4222SpiCpol
        {
            ClkIdleLow = 0,
            ClkIdleHigh = 1,
        }

        public enum Ft4222SpiCpha
        {
            ClkLeading = 0,
            ClkTrailing = 1,
        }

        public enum SpiDrivingStrength
        {
            Ds4Ma = 0,
            Ds8Ma,
            Ds12Ma,
            Ds16Ma,
        }

        public enum GpioPort
        {
            GpioPort0 = 0,
            GpioPort1,
            GpioPort2,
            GpioPort3
        }

        public enum GpioDir
        {
            GpioOutput = 0,
            GpioInput,
        }

        public enum GpioTrigger
        {
            TriggerRising = 0x01,
            TriggerFalling = 0x02,
            TriggerLevelHigh = 0x04,
            TriggerLevelLow = 0X08
        }

        public enum GpioOutput
        {
            GpioOutputLow,
            GpioOutputHigh
        }

        [Flags]
        public enum I2CMasterFlag
        {
            None = 0x80,
            Start = 0x02,
            RepeatedStart = 0x03, // Repeated_START will not send master code in HS mode
            Stop = 0x04,
            StartAndStop = 0x06, // START condition followed by SEND and STOP condition
        }

        public enum SpiSlaveProtocol
        {
            SpiSlaveWithProtocol = 0,
            SpiSlaveNoProtocol,
            SpiSlaveNoAck,
        }

        public enum ChipRevision
        {
            A = 0x42220100,
            B = 0x42220200,
            C = 0x42220300,
            D = 0x42220400,
            Max = 0x4222ffff,
        }

        public struct Ft4222Version
        {
            public uint ChipVersion;
            public uint DllVersion;
        }

        internal static class NativeMethods
        {
            //// **************************************************************************
            ////
            //// FUNCTION IMPORTS FROM FTD2XX DLL
            ////
            //// **************************************************************************

            public const byte FtOpenBySerialNumber = 1;
            public const byte FtOpenByDescription = 2;
            public const byte FtOpenByLocation = 4;

            [DllImport("ftd2xx.dll")]
            public static extern FTDI.FT_STATUS FT_CreateDeviceInfoList(ref uint numDevs);

            [DllImport("ftd2xx.dll")]
            public static extern FTDI.FT_STATUS FT_GetDeviceInfoDetail(
                uint index,
                ref uint flags,
                ref FTDI.FT_DEVICE chiptype,
                ref uint id,
                ref uint locid,
                byte[] serialnumber,
                byte[] description,
                ref IntPtr ftHandle);

            // [DllImportAttribute("ftd2xx.dll", CallingConvention = CallingConvention.Cdecl)]
            [DllImport("ftd2xx.dll")]
            public static extern FTDI.FT_STATUS FT_OpenEx(uint pvArg1, int dwFlags, ref IntPtr ftHandle);

            // [DllImportAttribute("ftd2xx.dll", CallingConvention = CallingConvention.Cdecl)]
            [DllImport("ftd2xx.dll")]
            public static extern FTDI.FT_STATUS FT_Close(IntPtr ftHandle);

            //// **************************************************************************
            ////
            //// FUNCTION IMPORTS FROM LIBFT4222 DLL
            ////
            //// **************************************************************************

            //// FT4222 General Functions

            [DllImport("LibFt4222.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Ft4222Status FT4222_UnInitialize(IntPtr ftHandle);

            [DllImport("LibFt4222.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Ft4222Status FT4222_SetClock(IntPtr ftHandle, Ft4222ClockRate clk);

            [DllImport("LibFt4222.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Ft4222Status FT4222_GetClock(IntPtr ftHandle, ref Ft4222ClockRate clk);

            [DllImport("LibFt4222.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Ft4222Status FT4222_SetWakeUpInterrupt(IntPtr ftHandle, bool enable);

            [DllImport("LibFt4222.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Ft4222Status FT4222_SetInterruptTrigger(IntPtr ftHandle, GpioTrigger trigger);

            [DllImport("LibFt4222.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Ft4222Status FT4222_SetSuspendOut(IntPtr ftHandle, bool enable);

            [DllImport("LibFt4222.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Ft4222Status FT4222_GetMaxTransferSize(IntPtr ftHandle, ref ushort pMaxSize);

            [DllImport("LibFt4222.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Ft4222Status FT4222_SetEventNotification(IntPtr ftHandle, uint mask, IntPtr param);

            [DllImport("LibFt4222.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Ft4222Status FT4222_GetVersion(IntPtr ftHandle, ref Ft4222Version pVersion);

            //// FT4222 SPI Functions

            [DllImport("LibFt4222.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Ft4222Status FT4222_SPIMaster_Init(
                IntPtr ftHandle,
                Ft4222SpiMode ioLine,
                Ft4222SpiClock clock,
                Ft4222SpiCpol cpol,
                Ft4222SpiCpha cpha,
                byte ssoMap);

            [DllImport("LibFt4222.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Ft4222Status FT4222_SPI_SetDrivingStrength(
                IntPtr ftHandle,
                SpiDrivingStrength clkStrength,
                SpiDrivingStrength ioStrength,
                SpiDrivingStrength ssoStrength);

            [DllImport("LibFt4222.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Ft4222Status FT4222_SPIMaster_SingleReadWrite(
                IntPtr ftHandle,
                ref byte readBuffer,
                ref byte writeBuffer,
                ushort bufferSize,
                ref ushort sizeTransferred,
                bool isEndTransaction);

            //// FT4222 I2C Functions

            [DllImport("LibFt4222.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Ft4222Status FT4222_I2CMaster_Init(IntPtr ftHandle, uint kbps);

            //// [DllImport("LibFt4222.dll", CallingConvention = CallingConvention.Cdecl)]
            //// public static extern FT4222_STATUS FT4222_I2CMaster_Read(IntPtr ftHandle, UInt16 slaveAddress, ref byte buffer, UInt16 bytesToRead, ref UInt16 sizeTransferred);

            //// [DllImport("LibFt4222.dll", CallingConvention = CallingConvention.Cdecl)]
            //// public static extern FT4222_STATUS FT4222_I2CMaster_Write(IntPtr ftHandle, UInt16 slaveAddress, ref byte buffer, UInt16 bytesToWrite, ref UInt16 sizeTransferred);

            [DllImport("LibFt4222.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Ft4222Status FT4222_I2CMaster_ReadEx(
                IntPtr ftHandle,
                ushort deviceAddress,
                byte flag,
                ref byte buffer,
                ushort bytesToRead,
                ref ushort sizeTransferred);

            [DllImport("LibFt4222.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Ft4222Status FT4222_I2CMaster_WriteEx(
                IntPtr ftHandle,
                ushort deviceAddress,
                byte flag,
                ref byte buffer,
                ushort bytesToWrite,
                ref ushort sizeTransferred);

            [DllImport("LibFt4222.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Ft4222Status FT4222_I2CMaster_Reset(IntPtr ftHandle);

            [DllImport("LibFt4222.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Ft4222Status FT4222_I2CMaster_GetStatus(IntPtr ftHandle, ref byte controllerStatus);
        }
    }
}
