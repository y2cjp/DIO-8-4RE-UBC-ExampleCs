// MIT License
//
// Copyright (c) Y2 Corporation

namespace Dio84ReUbc.NfLibrary
{
    public class Dio016
    {
        private readonly Pca9535 ioExpander;

        public Dio016(I2CFt4222 i2C, byte slaveAddress)
        {
            IsInitialized = false;
            ioExpander = new Pca9535(i2C, slaveAddress);
        }

        public enum PinState
        {
            Off,
            On
        }

        public bool IsInitialized { get; private set; }

        public ResultCode Initialize()
        {
            // 接続状態確認の為、ダミーリード
            byte value;
            ResultCode result = IoExpanderReadPort(0, out value);
            if (result != ResultCode.Ok)
                return result;

            byte[] buffer = { 0xff, 0xff };
            result = IoExpanderWritePort(0, buffer, 0, 2);
            if (result != ResultCode.Ok)
                return result;
            buffer[0] = 0x00;
            buffer[1] = 0x00;
            result = IoExpanderSetPortDirection(0, buffer, 0x00, 2);
            if (result == ResultCode.Ok)
                IsInitialized = true;
            return result;
        }

        public ResultCode WriteAll(uint value)
        {
            if (!IsInitialized)
            {
                ResultCode result = Initialize();
                if (result != ResultCode.Ok)
                    return result;
            }

            byte[] valueByte = { (byte)(~value & 0xff), (byte)(~(value >> 8) & 0xff) };
            return IoExpanderWritePort(0, valueByte, 0, valueByte.Length);
        }

        public ResultCode WritePort(int port, byte value)
        {
            if (port < 0 || 1 < port)
                return ResultCode.InvalidParameter;
            if (!IsInitialized)
            {
                ResultCode result = Initialize();
                if (result != ResultCode.Ok)
                    return result;
            }

            value = (byte)~value;
            return IoExpanderWritePort(port, value);
        }

        public ResultCode WritePin(int pin, PinState pinState)
        {
            if (pin < 0 || 15 < pin)
                return ResultCode.InvalidParameter;
            if (!IsInitialized)
            {
                ResultCode result = Initialize();
                if (result != ResultCode.Ok)
                    return result;
            }

            bool state = pinState == PinState.Off;
            int devicePort, devicePin;
            PinToDevicePin(pin, out devicePort, out devicePin);

            return IoExpanderWritePin(devicePort, devicePin, state);
        }

        public ResultCode ReadRegister(Pca9535.Register register, byte[] value, int index, int length)
        {
            // return ioExpander?.ReadRegister(register, value, index, length) ?? ResultCode.FatalError;    // C# 6.0 or later
            if (ioExpander == null)
                return ResultCode.FatalError;
            return ioExpander.ReadRegister(register, value, index, length);
        }

        private static void PinToDevicePin(int pin, out int devicePort, out int devicePin)
        {
            if (pin < 8)
                devicePort = 0;
            else if (pin < 16)
                devicePort = 1;
            else
                devicePort = 2;

            devicePin = pin % 8;
        }

        private ResultCode IoExpanderSetPortDirection(int port, byte[] value, int index, int length)
        {
            // return ioExpander?.SetPortDirection(port, value, index, length) ?? ResultCode.FatalError;    // C# 6.0 or later
            if (ioExpander == null)
                return ResultCode.FatalError;
            return ioExpander.SetPortDirection(port, value, index, length);
        }

        private ResultCode IoExpanderReadPort(int port, out byte value)
        {
            value = 0;
            return ioExpander == null ? ResultCode.FatalError : ioExpander.ReadPort(port, out value);
        }

        private ResultCode IoExpanderWritePort(int port, byte[] value, int index, int length)
        {
            // return ioExpander?.WritePort(port, value, index, length) ?? ResultCode.FatalError;   // C# 6.0 or later
            if (ioExpander == null)
                return ResultCode.FatalError;
            return ioExpander.WritePort(port, value, index, length);
        }

        private ResultCode IoExpanderWritePort(int port, byte value)
        {
            // return ioExpander?.WritePort(port, value) ?? ResultCode.FatalError;  // C# 6.0 or later
            if (ioExpander == null)
                return ResultCode.FatalError;
            return ioExpander.WritePort(port, value);
        }

        private ResultCode IoExpanderWritePin(int port, int pin, bool state)
        {
            // return ioExpander?.WritePin(port, pin, state) ?? ResultCode.FatalError;  // C# 6.0 or later
            if (ioExpander == null)
                return ResultCode.FatalError;
            return ioExpander.WritePin(port, pin, state);
        }
    }
}
