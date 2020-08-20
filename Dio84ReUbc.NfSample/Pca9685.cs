// MIT License
//
// Copyright (c) Y2 Corporation

using System;
using System.Threading;

namespace Dio84ReUbc.NfLibrary
{
    public class Pca9685
    {
        private readonly I2CFt4222 _i2C;
        private readonly byte _slaveAddress;

        public Pca9685(I2CFt4222 i2C, byte slaveAddress)
        {
            _i2C = i2C;
            _slaveAddress = slaveAddress;
        }

        public enum Register : byte
        {
            Mode1,
            Mode2,
            SubAdr1,
            SubAdr2,
            SubAdr3,
            AllCallAdr,
            Led0OnL,
            Led0OnH,
            Led0OffL,
            Led0OffH,
            AllLedOnL = 250,
            AllLedOnH,
            AllLedOffL,
            AllLedOffH,
            PreScale,
            TestMode
        }

        public ResultCode SetPwmFreq(float frequency)
        {
            if (frequency < 24)
                frequency = 24;

            byte prescale = (byte)((float)25000000 / 4096 / frequency - 1);
            if (prescale < 3)
                prescale = 3;

            ResultCode result = WriteRegister(Register.Mode1, 0x31); // Oscillator Off
            if (result != ResultCode.Ok)
                return result;
            result = WriteRegister(Register.PreScale, prescale);
            if (result != ResultCode.Ok)
                return result;
            result = WriteRegister(Register.Mode1, 0x21);   // Oscillator On
            if (result != ResultCode.Ok)
                return result;
            Thread.Sleep(5);
            result = WriteRegister(Register.Mode1, 0xa1);
            return result;
        }

        public ResultCode SetPwm(byte channel, ushort on, ushort off)
        {
            if (on > 4096)
                on = 4096;
            if (off > 4096)
                off = 4096;

            byte[] writeBuffer = { (byte)(Register.Led0OnL + (byte)(4 * channel)), (byte)on, (byte)(on >> 8), (byte)off, (byte)(off >> 8) };
            var result = _i2C.Write(_slaveAddress, ref writeBuffer[0], (ushort)writeBuffer.Length);
            return result;
        }

        public ResultCode ReadRegister(Register register, byte[] values, int index, int length)
        {
            byte[] writeBuffer = { (byte)register };
            ResultCode result = I2CWriteEx((byte)LibFt4222.I2CMasterFlag.Start, ref writeBuffer[0], (ushort)writeBuffer.Length);
            if (values == null)
                return ResultCode.FatalError;

            if (result != ResultCode.Ok)
                return result;
            return I2CReadEx((byte)(LibFt4222.I2CMasterFlag.RepeatedStart | LibFt4222.I2CMasterFlag.Stop), ref values[index], length);
        }

        public ResultCode WriteRegister(Register register, byte[] values, int index, int length)
        {
            var writeBuffer = new byte[length + 1];
            writeBuffer[0] = (byte)register;

            if (values == null)
                return ResultCode.FatalError;
            Array.Copy(values, index, writeBuffer, 1, length);
            return I2CWrite(ref writeBuffer[0], (ushort)writeBuffer.Length);
        }

        public ResultCode WriteRegister(Register register, byte value)
        {
            var writeBuffer = new[] { (byte)register, value };
            return I2CWrite(ref writeBuffer[0], (ushort)writeBuffer.Length);
        }

        private ResultCode I2CWriteEx(byte flag, ref byte buffer, int bytesToWrite)
        {
            return _i2C.WriteEx(_slaveAddress, flag, ref buffer, bytesToWrite);
        }

        private ResultCode I2CWrite(ref byte buffer, int bytesToWrite)
        {
            return _i2C.Write(_slaveAddress, ref buffer, bytesToWrite);
        }

        private ResultCode I2CReadEx(byte flag, ref byte buffer, int bytesToRead)
        {
            return _i2C.ReadEx(_slaveAddress, flag, ref buffer, bytesToRead);
        }
    }
}
