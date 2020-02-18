// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Pca9685.cs" company="Y2 Corporation">
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

using System;
using System.Threading;

namespace DIO_8_4RE_UBC
{
    public class Pca9685
    {
        private readonly I2CFt4222 i2C;
        private readonly byte slaveAddress;

        public Pca9685(I2CFt4222 i2C, byte slaveAddress)
        {
            this.i2C = i2C;
            this.slaveAddress = slaveAddress;
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
            var result = i2C.Write(slaveAddress, ref writeBuffer[0], (ushort)writeBuffer.Length);
            return result;
        }

        public ResultCode ReadRegister(Register register, byte[] value, int index, int length)
        {
            byte[] writeBuffer = { (byte)register };
            ResultCode result = I2CWriteEx(slaveAddress, (byte)LibFt4222.I2CMasterFlag.Start, ref writeBuffer[0], (ushort)writeBuffer.Length);
            if (value == null)
                return ResultCode.FatalError;

            if (result != ResultCode.Ok)
                return result;
            return I2CReadEx(slaveAddress, (byte)(LibFt4222.I2CMasterFlag.RepeatedStart | LibFt4222.I2CMasterFlag.Stop), ref value[index], length);
        }

        public ResultCode WriteRegister(Register register, byte[] value, int index, int length)
        {
            var writeBuffer = new byte[length + 1];
            writeBuffer[0] = (byte)register;

            if (value == null)
                return ResultCode.FatalError;
            Array.Copy(value, index, writeBuffer, 1, length);
            return I2CWrite(slaveAddress, ref writeBuffer[0], (ushort)writeBuffer.Length);
        }

        public ResultCode WriteRegister(Register register, byte value)
        {
            var writeBuffer = new[] { (byte)register, value };
            return I2CWrite(slaveAddress, ref writeBuffer[0], (ushort)writeBuffer.Length);
        }

        public ResultCode I2CWriteEx(ushort deviceAddress, byte flag, ref byte buffer, int bytesToWrite)
        {
            // return i2C?.WriteEx(deviceAddress, flag, ref buffer, bytesToWrite) ?? ResultCode.FatalError; // C# 6.0 or later
            if (i2C == null)
                return ResultCode.FatalError;
            return i2C.WriteEx(deviceAddress, flag, ref buffer, bytesToWrite);
        }

        public ResultCode I2CWrite(ushort deviceAddress, ref byte buffer, int bytesToWrite)
        {
            // return i2C?.Write(deviceAddress, ref buffer, bytesToWrite) ?? ResultCode.FatalError; // C# 6.0 or later
            if (i2C == null)
                return ResultCode.FatalError;
            return i2C.Write(deviceAddress, ref buffer, bytesToWrite);
        }

        public ResultCode I2CReadEx(ushort deviceAddress, byte flag, ref byte buffer, int bytesToRead)
        {
            // return i2C?.ReadEx(deviceAddress, flag, ref buffer, bytesToRead) ?? ResultCode.FatalError;   // C# 6.0 or later
            if (i2C == null)
                return ResultCode.FatalError;
            return i2C.ReadEx(deviceAddress, flag, ref buffer, bytesToRead);
        }
    }
}
