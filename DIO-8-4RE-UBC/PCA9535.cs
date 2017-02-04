﻿// 
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
using System;

namespace Y2C_I2C_Libraries {
    class PCA9535 {

        private const byte PortMax = 1;

        public enum Register : byte {
            InputPort0 = 0x00,
            OutputPort0 = 0x02,
            PolarityInversion0 = 0x04,
            IoConfiguration0 = 0x06
        }

        public enum PinDirection {
            Output,
            Input
        }

        private I2C_FT4222 i2c;
        private byte slaveAddress;
        private byte[] outputValue = new byte[] { 0xff, 0xff };
        private byte[] portDirection = new byte[] { 0xff, 0xff };

        public PCA9535(I2C_FT4222 i2c, byte slaveAddress) {
            this.i2c = i2c;
            this.slaveAddress = slaveAddress;
        }

        public ResultCode SetPortDirection(int port, byte[] value, int index, int length) {
            if ((port < 0) || (PortMax < port) || (length < 1) || (PortMax + 1 < port + length))
                return ResultCode.InvalidParameter;
            byte[] writeBuffer = new byte[length + 1];
            writeBuffer[0] = (byte)(Register.IoConfiguration0 + (byte)port);
            Array.Copy(value, index, writeBuffer, 1, length);
            ResultCode result = i2c.Write(slaveAddress, ref writeBuffer[0], (ushort)writeBuffer.Length);
            if (result != ResultCode.OK)
                return result;
            Array.Copy(value, index, portDirection, port, length);
            return ResultCode.OK;
        }

        public ResultCode SetPortDirection(int port, byte value) {
            byte[] buffer = new byte[] { value };
            return SetPortDirection(port, buffer, 0, 1);
        }

        public ResultCode SetDirectionPin(int port, int pin, PinDirection pinDir) {
            if ((port < 0) || (PortMax < port) || (pin < 0) || (7 < pin))
                return ResultCode.InvalidParameter;
            byte value;
            if (pinDir == PinDirection.Input)
                value = (byte)(portDirection[port] | (1 << pin));
            else
                value = (byte)(portDirection[port] & ~(1 << pin));
            return SetPortDirection(port, portDirection[port]);
        }

        public ResultCode ReadRegister(Register register, byte[] value, int index, int length) {
            byte[] writeBuffer = new byte[] { (byte)register };
            ResultCode result = i2c.WriteEx(slaveAddress, (byte)LibFT4222.I2cMasterFlag.Start, ref writeBuffer[0], (ushort)writeBuffer.Length);
            if (result != ResultCode.OK)
                return result;
            return i2c.ReadEx(slaveAddress, (byte)(LibFT4222.I2cMasterFlag.RepeatedStart | LibFT4222.I2cMasterFlag.Stop), ref value[index], length);
        }

        public ResultCode ReadPort(int port, byte[] value, int index, int length) {
            if ((port < 0) || (PortMax < port) || (length < 1) || (PortMax + 1 < port + length))
                return ResultCode.InvalidParameter;
            return ReadRegister(Register.InputPort0 + (byte)port, value, index, (ushort)length);
        }

        public ResultCode ReadPort(int port, out byte value) {
            byte[] buffer = new byte[1];
            ResultCode result = ReadPort(port, buffer, 0, 1);
            value = buffer[0];
            return result;
        }

        public ResultCode ReadPin(int port, int pin, out bool state) {
            state = true;
            if ((port < 0) || (PortMax < port) || (pin < 0) || (7 < pin))
                return ResultCode.InvalidParameter;
            byte ip;
            ResultCode result = ReadPort(port, out ip);
            state = (ip & (1 << pin)) != 0;
            return result;
        }

        public ResultCode WritePort(int port, byte[] value, int index, int length) {
            if ((port < 0) || (PortMax < port) || (length < 1) || (PortMax + 1 < port + length))
                return ResultCode.InvalidParameter;
            byte[] writeBuffer = new byte[length + 1];
            writeBuffer[0] = (byte)(Register.OutputPort0 + (byte)port);
            Array.Copy(value, index, writeBuffer, 1, length);
            ResultCode result = i2c.Write(slaveAddress, ref writeBuffer[0], (ushort)writeBuffer.Length);
            if (result != ResultCode.OK)
                return result;
            Array.Copy(value, index, outputValue, port, length);
            return ResultCode.OK;
        }

        public ResultCode WritePort(int port, byte value) {
            byte[] buffer = new byte[] { value };
            return WritePort(port, buffer, 0, 1);
        }

        public ResultCode WritePin(int port, int pin, bool state) {
            if ((port < 0) || (PortMax < port) || (pin < 0) || (7 < pin))
                return ResultCode.InvalidParameter;
            byte value;
            if (state)
                value = (byte)(outputValue[port] | (1 << pin));
            else
                value = (byte)(outputValue[port] & ~(1 << pin));
            return WritePort(port, value);
        }

    }
}