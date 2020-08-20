// MIT License
//
// Copyright (c) Y2 Corporation

namespace Dio84ReUbc.NfLibrary
{
    public class Dio84
    {
        private readonly Pca9535 _ioExpander;
        private PinState _mikroBusResetPin;

        public Dio84(I2CFt4222 i2C, byte slaveAddress)
        {
            IsInitialized = false;
            _ioExpander = new Pca9535(i2C, slaveAddress);
        }

        public enum PinState
        {
            Off,
            On
        }

        public bool IsInitialized { get; private set; }

        public ResultCode Initialize()
        {
            ResultCode result = _ioExpander.WritePort(0, 0xff);
            if (result != ResultCode.Ok)
                return result;

            result = _ioExpander.SetPortDirection(0, 0x00);
            if (result != ResultCode.Ok)
                return result;

            byte value;
            result = _ioExpander.ReadPort(0, out value);
            if (result != ResultCode.Ok)
                return result;

            _mikroBusResetPin = (value & 0x80) != 0 ? PinState.Off : PinState.On;
            IsInitialized = true;
            return result;
        }

        public ResultCode ReadPort(out byte value)
        {
            value = 0;
            if (_ioExpander == null)
                return ResultCode.FatalError;

            ResultCode result = _ioExpander.ReadPort(1, out value);
            value = (byte)~value;
            return result;
        }

        public ResultCode ReadPin(int pin, out PinState pinState)
        {
            pinState = PinState.Off;
            if (pin < 0 || 7 < pin)
                return ResultCode.InvalidParameter;

            bool state;
            if (_ioExpander == null)
                return ResultCode.FatalError;

            ResultCode result = _ioExpander.ReadPin(1, (byte)pin, out state);
            pinState = state == false ? PinState.On : PinState.Off;
            return result;
        }

        public ResultCode WritePort(byte value)
        {
            if (!IsInitialized)
            {
                ResultCode result = Initialize();
                if (result != ResultCode.Ok)
                    return result;
            }

            if (_mikroBusResetPin == PinState.On)
                value |= 0x80;
            else
                value &= 0x7f;
            return IoExpanderWritePort(0, (byte)~value);
        }

        public ResultCode WritePin(int pin, PinState pinState)
        {
            if (pin < 0 || 3 < pin)
                return ResultCode.InvalidParameter;
            if (!IsInitialized)
            {
                ResultCode result = Initialize();
                if (result != ResultCode.Ok)
                    return result;
            }

            bool state = pinState == PinState.Off;
            return IoExpanderWritePin(0, (byte)pin, state);
        }

        public ResultCode SetMikroBusResetPin(PinState pinState)
        {
            ResultCode result;
            if (!IsInitialized)
            {
                result = Initialize();
                if (result != ResultCode.Ok)
                    return result;
            }

            bool state = pinState == PinState.Off;
            result = IoExpanderWritePin(0, 7, state);   // ResetOff(/RST=H), ResetOn(/RST=L)
            if (result != ResultCode.Ok)
                return result;
            _mikroBusResetPin = pinState;
            return ResultCode.Ok;
        }

        private ResultCode IoExpanderWritePort(int port, byte value)
        {
            return _ioExpander.WritePort(port, value);
        }

        private ResultCode IoExpanderWritePin(int port, int pin, bool state)
        {
            return _ioExpander.WritePin(port, pin, state);
        }
    }
}
