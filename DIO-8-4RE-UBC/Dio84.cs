// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Dio84.cs" company="Y2 Corporation">
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
    public class Dio84
    {
        private readonly Pca9535 ioExpander;
        private PinState mikroBusResetPin;

        public Dio84(I2CFt4222 i2C, byte slaveAddress)
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
            if (ioExpander == null)
                return ResultCode.FatalError;

            var result = ioExpander.WritePort(0, 0xff);
            if (result != ResultCode.Ok)
                return result;

            result = ioExpander.SetPortDirection(0, 0x00);
            if (result != ResultCode.Ok)
                return result;

            byte value;
            result = ioExpander.ReadPort(0, out value);
            if (result != ResultCode.Ok)
                return result;

            mikroBusResetPin = (value & 0x80) != 0 ? PinState.Off : PinState.On;
            IsInitialized = true;
            return result;
        }

        public ResultCode ReadPort(out byte value)
        {
            value = 0;
            if (ioExpander == null)
                return ResultCode.FatalError;

            var result = ioExpander.ReadPort(1, out value);
            value = (byte)~value;
            return result;
        }

        public ResultCode ReadPin(int pin, out PinState pinState)
        {
            pinState = PinState.Off;
            if (pin < 0 || pin > 7)
                return ResultCode.InvalidParameter;

            bool state;
            if (ioExpander == null)
                return ResultCode.FatalError;

            var result = ioExpander.ReadPin(1, (byte)pin, out state);
            pinState = state == false ? PinState.On : PinState.Off;
            return result;
        }

        public ResultCode WritePort(byte value)
        {
            if (!IsInitialized)
            {
                var result = Initialize();
                if (result != ResultCode.Ok)
                    return result;
            }

            if (mikroBusResetPin == PinState.On)
                value |= 0x80;
            else
                value &= 0x7f;
            return IoExpanderWritePort(0, (byte)~value);
        }

        public ResultCode WritePin(int pin, PinState pinState)
        {
            if (pin < 0 || pin > 3)
                return ResultCode.InvalidParameter;
            if (!IsInitialized)
            {
                var result = Initialize();
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
            mikroBusResetPin = pinState;
            return ResultCode.Ok;
        }

        private ResultCode IoExpanderWritePort(int port, byte value)
        {
            return ioExpander?.WritePort(port, value) ?? ResultCode.FatalError;
        }

        private ResultCode IoExpanderWritePin(int port, int pin, bool state)
        {
            return ioExpander?.WritePin(port, pin, state) ?? ResultCode.FatalError;
        }
    }
}
