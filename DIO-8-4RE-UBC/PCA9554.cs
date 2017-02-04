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

namespace Y2C_I2C_Libraries {
    class PCA9554 {

        private const byte PortMax = 1;

        public enum Register : byte {
            InputPort,
            OutputPort,
            PolarityInversion,
            IoConfiguration
        }

        public enum PinDirection {
            Output,
            Input
        }

        private byte slaveAddress;
        private byte outputValue = 0xff;
        private byte portDirection = 0xff;

        private I2C_FT4222 i2c;

        public PCA9554(I2C_FT4222 i2c, byte slaveAddress) {
            this.i2c = i2c;
            this.slaveAddress = slaveAddress;
        }

        public ResultCode SetPortDirection(byte value) {
            byte[] writeBuffer = new byte[] { (byte)Register.IoConfiguration, value };
            ResultCode result = i2c.Write(slaveAddress, ref writeBuffer[0], (ushort)writeBuffer.Length);
            return ResultCode.OK;
        }

        public ResultCode SetPinDirection(int pin, PinDirection pinDir) {
            if ((pin < 0) || (7 < pin))
                return ResultCode.InvalidParameter;
            byte value;
            if (pinDir == PinDirection.Input)
                value = (byte)(portDirection | (1 << pin));
            else
                value = (byte)(portDirection & ~(1 << pin));
            return SetPortDirection(portDirection);
        }

        public ResultCode ReadRegister(Register register, out byte value) {
            value = 0;
            byte[] writeBuffer = new byte[] { (byte)register };
            ResultCode result = i2c.WriteEx(slaveAddress, (byte)LibFT4222.I2cMasterFlag.Start, ref writeBuffer[0], (ushort)writeBuffer.Length);
            if (result != ResultCode.OK)
                return result;
            return i2c.ReadEx(slaveAddress, (byte)(LibFT4222.I2cMasterFlag.RepeatedStart | LibFT4222.I2cMasterFlag.Stop), ref value, 1);

        }

        public ResultCode ReadPort(out byte value) {
            return ReadRegister(Register.InputPort, out value);
        }

        public ResultCode ReadPin(int pin, out bool state) {
            state = true;
            if ((pin < 0) || (7 < pin))
                return ResultCode.InvalidParameter;
            byte ip;
            ResultCode result = ReadPort(out ip);
            state = (ip & (1 << pin)) != 0;
            return result;
        }

        public ResultCode WritePort(byte value) {
            byte[] writeBuffer = new byte[] { (byte)Register.OutputPort, value };
            ResultCode result = i2c.Write(slaveAddress, ref writeBuffer[0], (ushort)writeBuffer.Length);
            if (result != ResultCode.OK)
                return result;
            return ResultCode.OK;
        }

        public ResultCode WritePin(int pin, bool state) {
            if ((pin < 0) || (7 < pin))
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
