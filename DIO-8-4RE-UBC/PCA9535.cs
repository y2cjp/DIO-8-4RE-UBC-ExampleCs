// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Pca9535.cs" company="Y2 Corporation">
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

namespace DIO_8_4RE_UBC
{
    public class Pca9535
    {
        private const byte PortMax = 1;

        private readonly byte[] outputValue = { 0xff, 0xff };

        private readonly byte[] portDirection = { 0xff, 0xff };

        public Pca9535(I2CFt4222 i2C, byte slaveAddress)
        {
            I2C = i2C;
            SlaveAddress = slaveAddress;
        }

        public enum Register : byte
        {
            InputPort0 = 0x00,
            OutputPort0 = 0x02,
            PolarityInversion0 = 0x04,
            IoConfiguration0 = 0x06
        }

        public enum PinDirection
        {
            Output,
            Input
        }

        private I2CFt4222 I2C { get; }

        private byte SlaveAddress { get; }

        public ResultCode SetPortDirection(int port, byte[] value, int index, int length)
        {
            if (port < 0 || PortMax < port || length < 1 || PortMax + 1 < port + length)
                return ResultCode.InvalidParameter;
            var writeBuffer = new byte[length + 1];
            writeBuffer[0] = (byte)(Register.IoConfiguration0 + (byte)port);
            if (value == null)
                return ResultCode.FatalError;

            Array.Copy(value, index, writeBuffer, 1, length);
            if (I2C == null)
                return ResultCode.FatalError;

            var result = I2C.Write(SlaveAddress, ref writeBuffer[0], (ushort)writeBuffer.Length);
            if (result != ResultCode.Ok)
                return result;
            if (this.portDirection == null)
                return ResultCode.FatalError;

            Array.Copy(value, index, this.portDirection, port, length);
            return ResultCode.Ok;
        }

        public ResultCode SetPortDirection(int port, byte value)
        {
            byte[] buffer = { value };
            return SetPortDirection(port, buffer, 0, 1);
        }

        public ResultCode SetPinDirection(int port, int pin, PinDirection pinDir)
        {
            if (port < 0 || PortMax < port || pin < 0 || pin > 7)
                return ResultCode.InvalidParameter;

            if (portDirection == null)
                return ResultCode.FatalError;

            byte value;
            if (pinDir == PinDirection.Input)
                value = (byte)(portDirection[port] | (1 << pin));
            else
                value = (byte)(portDirection[port] & ~(1 << pin));
            return SetPortDirection(port, value);
        }

        public ResultCode ReadRegister(Register register, byte[] value, int index, int length)
        {
            byte[] writeBuffer = { (byte)register };
            var result = I2CWriteEx(SlaveAddress, (byte)LibFt4222.I2CMasterFlag.Start, ref writeBuffer[0], (ushort)writeBuffer.Length);
            if (value == null)
                return ResultCode.FatalError;

            return result != ResultCode.Ok ? result : I2CReadEx(SlaveAddress, (byte)(LibFt4222.I2CMasterFlag.RepeatedStart | LibFt4222.I2CMasterFlag.Stop), ref value[index], length);
        }

        public ResultCode I2CWriteEx(ushort deviceAddress, byte flag, ref byte buffer, int bytesToWrite)
        {
            return I2C?.WriteEx(deviceAddress, flag, ref buffer, bytesToWrite) ?? ResultCode.FatalError;
        }

        public ResultCode I2CReadEx(ushort deviceAddress, byte flag, ref byte buffer, int bytesToRead)
        {
            return I2C?.ReadEx(deviceAddress, flag, ref buffer, bytesToRead) ?? ResultCode.FatalError;
        }

        public ResultCode ReadPort(int port, byte[] value, int index, int length)
        {
            if (port < 0 || PortMax < port || length < 1 || PortMax + 1 < port + length)
                return ResultCode.InvalidParameter;
            return ReadRegister(Register.InputPort0 + (byte)port, value, index, (ushort)length);
        }

        public ResultCode ReadPort(int port, out byte value)
        {
            var buffer = new byte[1];
            var result = ReadPort(port, buffer, 0, 1);
            value = buffer[0];
            return result;
        }

        public ResultCode ReadPin(int port, int pin, out bool state)
        {
            state = true;
            if (port < 0 || PortMax < port || pin < 0 || pin > 7)
                return ResultCode.InvalidParameter;
            byte ip;
            var result = ReadPort(port, out ip);
            state = (ip & (1 << pin)) != 0;
            return result;
        }

        public ResultCode WritePort(int port, byte[] value, int index, int length)
        {
            if (port < 0 || PortMax < port || length < 1 || PortMax + 1 < port + length)
                return ResultCode.InvalidParameter;
            var writeBuffer = new byte[length + 1];
            writeBuffer[0] = (byte)(Register.OutputPort0 + (byte)port);
            if (value == null)
                return ResultCode.FatalError;

            Array.Copy(value, index, writeBuffer, 1, length);
            var result = I2CWrite(SlaveAddress, ref writeBuffer[0], (ushort)writeBuffer.Length);
            if (result != ResultCode.Ok)
                return result;
            if (this.outputValue == null)
                return ResultCode.FatalError;

            Array.Copy(value, index, this.outputValue, port, length);
            return ResultCode.Ok;
        }

        public ResultCode I2CWrite(ushort slaveAddress, ref byte buffer, int bytesToWrite)
        {
            return I2C?.Write(slaveAddress, ref buffer, bytesToWrite) ?? ResultCode.FatalError;
        }

        public ResultCode WritePort(int port, byte value)
        {
            byte[] buffer = { value };
            return WritePort(port, buffer, 0, 1);
        }

        public ResultCode WritePin(int port, int pin, bool state)
        {
            if (port < 0 || PortMax < port || pin < 0 || pin > 7)
                return ResultCode.InvalidParameter;
            if (this.outputValue == null)
                return ResultCode.FatalError;

            byte value;
            if (state)
                value = (byte)(this.outputValue[port] | (1 << pin));
            else
                value = (byte)(this.outputValue[port] & ~(1 << pin));
            return WritePort(port, value);
        }
    }
}
