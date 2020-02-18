// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Pca9554.cs" company="Y2 Corporation">
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

namespace DIO_8_4RE_UBC
{
    public class Pca9554
    {
        private readonly I2CFt4222 i2C;
        private readonly byte slaveAddress;
        private byte outputValue = 0xff;
        private byte portDirection = 0xff;

        public Pca9554(I2CFt4222 i2C, byte slaveAddress)
        {
            this.i2C = i2C;
            this.slaveAddress = slaveAddress;
        }

        public enum Register : byte
        {
            InputPort,
            OutputPort,
            PolarityInversion,
            IoConfiguration
        }

        public enum PinDirection
        {
            Output,
            Input
        }

        public ResultCode SetPortDirection(byte value)
        {
            byte[] writeBuffer = { (byte)Register.IoConfiguration, value };
            if (i2C == null)
                return ResultCode.FatalError;

            ResultCode result = i2C.Write(slaveAddress, ref writeBuffer[0], (ushort)writeBuffer.Length);
            if (result != ResultCode.Ok)
                return result;

            portDirection = value;
            return ResultCode.Ok;
        }

        public ResultCode SetPinDirection(int pin, PinDirection pinDir)
        {
            if (pin < 0 || 7 < pin)
                return ResultCode.InvalidParameter;

            byte value;
            if (pinDir == PinDirection.Input)
                value = (byte)(portDirection | (1 << pin));
            else
                value = (byte)(portDirection & ~(1 << pin));
            return SetPortDirection(value);
        }

        public ResultCode ReadRegister(Register register, out byte value)
        {
            value = 0;
            byte[] writeBuffer = { (byte)register };
            if (i2C == null)
                return ResultCode.FatalError;

            ResultCode result = i2C.WriteEx(slaveAddress, (byte)LibFt4222.I2CMasterFlag.Start, ref writeBuffer[0], (ushort)writeBuffer.Length);
            if (result != ResultCode.Ok)
                return result;

            return i2C.ReadEx(slaveAddress, (byte)(LibFt4222.I2CMasterFlag.RepeatedStart | LibFt4222.I2CMasterFlag.Stop), ref value, 1);
        }

        public ResultCode ReadPort(out byte value)
        {
            return ReadRegister(Register.InputPort, out value);
        }

        public ResultCode ReadPin(int pin, out bool state)
        {
            state = true;
            if (pin < 0 || 7 < pin)
                return ResultCode.InvalidParameter;
            byte ip;
            ResultCode result = ReadPort(out ip);
            state = (ip & (1 << pin)) != 0;
            return result;
        }

        public ResultCode WritePort(byte value)
        {
            byte[] writeBuffer = { (byte)Register.OutputPort, value };
            if (i2C == null)
                return ResultCode.FatalError;

            ResultCode result = i2C.Write(slaveAddress, ref writeBuffer[0], (ushort)writeBuffer.Length);
            if (result != ResultCode.Ok)
                return result;
            outputValue = value;
            return ResultCode.Ok;
        }

        public ResultCode WritePin(int pin, bool state)
        {
            if (pin < 0 || 7 < pin)
                return ResultCode.InvalidParameter;
            byte value;
            if (state)
                value = (byte)(outputValue | (1 << pin));
            else
                value = (byte)(outputValue & ~(1 << pin));
            return WritePort(value);
        }
    }
}
