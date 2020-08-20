// MIT License
//
// Copyright (c) Y2 Corporation

using System;

namespace Dio84ReUbc.NfLibrary
{
    public class Pca9535
    {
        private const byte PortMax = 1;

        private readonly I2CFt4222 _i2C;
        private readonly byte _slaveAddress;
        private readonly byte[] _outputValue = { 0xff, 0xff };
        private readonly byte[] _portDirection = { 0xff, 0xff };

        public Pca9535(I2CFt4222 i2C, byte slaveAddress)
        {
            _i2C = i2C;
            _slaveAddress = slaveAddress;
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

        public ResultCode SetPortDirection(int port, byte[] value, int index, int length)
        {
            if (port < 0 || PortMax < port || length < 1 || PortMax + 1 < port + length)
                return ResultCode.InvalidParameter;
            byte[] writeBuffer = new byte[length + 1];
            writeBuffer[0] = (byte)(Register.IoConfiguration0 + (byte)port);
            if (value == null)
                return ResultCode.FatalError;

            Array.Copy(value, index, writeBuffer, 1, length);
            if (_i2C == null)
                return ResultCode.FatalError;

            ResultCode result = _i2C.Write(_slaveAddress, ref writeBuffer[0], (ushort)writeBuffer.Length);
            if (result != ResultCode.Ok)
                return result;
            if (_portDirection == null)
                return ResultCode.FatalError;

            Array.Copy(value, index, _portDirection, port, length);
            return ResultCode.Ok;
        }

        public ResultCode SetPortDirection(int port, byte value)
        {
            byte[] buffer = { value };
            return SetPortDirection(port, buffer, 0, 1);
        }

        public ResultCode SetPinDirection(int port, int pin, PinDirection pinDir)
        {
            if (port < 0 || PortMax < port || pin < 0 || 7 < pin)
                return ResultCode.InvalidParameter;

            if (_portDirection == null)
                return ResultCode.FatalError;

            byte value;
            if (pinDir == PinDirection.Input)
                value = (byte)(_portDirection[port] | (1 << pin));
            else
                value = (byte)(_portDirection[port] & ~(1 << pin));
            return SetPortDirection(port, value);
        }

        public ResultCode ReadRegister(Register register, byte[] value, int index, int length)
        {
            byte[] writeBuffer = { (byte)register };
            ResultCode result = I2CWriteEx(_slaveAddress, (byte)LibFt4222.I2CMasterFlag.Start, ref writeBuffer[0], (ushort)writeBuffer.Length);
            if (value == null)
                return ResultCode.FatalError;

            if (result != ResultCode.Ok)
                return result;
            return I2CReadEx(_slaveAddress, (byte)(LibFt4222.I2CMasterFlag.RepeatedStart | LibFt4222.I2CMasterFlag.Stop), ref value[index], length);
        }

        public ResultCode I2CWriteEx(ushort deviceAddress, byte flag, ref byte buffer, int bytesToWrite)
        {
            // return i2C?.WriteEx(deviceAddress, flag, ref buffer, bytesToWrite) ?? ResultCode.FatalError; // C# 6.0 or later
            if (_i2C == null)
                return ResultCode.FatalError;
            return _i2C.WriteEx(deviceAddress, flag, ref buffer, bytesToWrite);
        }

        public ResultCode I2CReadEx(ushort deviceAddress, byte flag, ref byte buffer, int bytesToRead)
        {
            // return i2C?.ReadEx(deviceAddress, flag, ref buffer, bytesToRead) ?? ResultCode.FatalError;   // C# 6.0 or later
            if (_i2C == null)
                return ResultCode.FatalError;
            return _i2C.ReadEx(deviceAddress, flag, ref buffer, bytesToRead);
        }

        public ResultCode ReadPort(int port, byte[] value, int index, int length)
        {
            if (port < 0 || PortMax < port || length < 1 || PortMax + 1 < port + length)
                return ResultCode.InvalidParameter;
            return ReadRegister(Register.InputPort0 + (byte)port, value, index, (ushort)length);
        }

        public ResultCode ReadPort(int port, out byte value)
        {
            byte[] buffer = new byte[1];
            ResultCode result = ReadPort(port, buffer, 0, 1);
            value = buffer[0];
            return result;
        }

        public ResultCode ReadPin(int port, int pin, out bool state)
        {
            state = true;
            if (port < 0 || PortMax < port || pin < 0 || 7 < pin)
                return ResultCode.InvalidParameter;
            byte ip;
            ResultCode result = ReadPort(port, out ip);
            state = (ip & (1 << pin)) != 0;
            return result;
        }

        public ResultCode WritePort(int port, byte[] value, int index, int length)
        {
            if (port < 0 || PortMax < port || length < 1 || PortMax + 1 < port + length)
                return ResultCode.InvalidParameter;
            byte[] writeBuffer = new byte[length + 1];
            writeBuffer[0] = (byte)(Register.OutputPort0 + (byte)port);
            if (value == null)
                return ResultCode.FatalError;

            Array.Copy(value, index, writeBuffer, 1, length);
            ResultCode result = I2CWrite(_slaveAddress, ref writeBuffer[0], (ushort)writeBuffer.Length);
            if (result != ResultCode.Ok)
                return result;
            if (_outputValue == null)
                return ResultCode.FatalError;

            Array.Copy(value, index, _outputValue, port, length);
            return ResultCode.Ok;
        }

        public ResultCode I2CWrite(ushort slaveAddr, ref byte buffer, int bytesToWrite)
        {
            // return i2C?.Write(slaveAddr, ref buffer, bytesToWrite) ?? ResultCode.FatalError; // C# 6.0 or later
            if (_i2C == null)
                return ResultCode.FatalError;
            return _i2C.Write(slaveAddr, ref buffer, bytesToWrite);
        }

        public ResultCode WritePort(int port, byte value)
        {
            byte[] buffer = { value };
            return WritePort(port, buffer, 0, 1);
        }

        public ResultCode WritePin(int port, int pin, bool state)
        {
            if (port < 0 || PortMax < port || pin < 0 || 7 < pin)
                return ResultCode.InvalidParameter;
            if (_outputValue == null)
                return ResultCode.FatalError;

            byte value;
            if (state)
                value = (byte)(_outputValue[port] | (1 << pin));
            else
                value = (byte)(_outputValue[port] & ~(1 << pin));
            return WritePort(port, value);
        }
    }
}
