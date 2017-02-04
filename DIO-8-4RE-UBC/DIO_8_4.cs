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
    public class DIO_8_4 {

        private PCA9535 ioExpander;
        public bool IsInitialized { get; private set; }
        private PinState mikroBusResetPin;

        public enum PinState {
            Off,
            On
        }

        public DIO_8_4(I2C_FT4222 i2c, byte slaveAddress) {
            IsInitialized = false;
            ioExpander = new PCA9535(i2c, slaveAddress);
        }

        public ResultCode Initialize() {
            ResultCode result = ioExpander.WritePort(0, 0xff);
            if (result != ResultCode.OK)
                return result;
            result = ioExpander.SetPortDirection(0, 0x00);
            if (result != ResultCode.OK)
                return result;
            byte value;
            result = ioExpander.ReadPort(0, out value);
            if (result != ResultCode.OK)
                return result;
            if ((value & 0x80) != 0)
                mikroBusResetPin = PinState.Off;
            else
                mikroBusResetPin = PinState.On;
            IsInitialized = true;
            return result;
        }

        public ResultCode ReadPort(out byte value) {
            ResultCode result = ioExpander.ReadPort(1, out value);
            value = (byte)~value;
            return result;
        }

        public ResultCode ReadPin(int pin, out PinState pinState) {
            pinState = PinState.Off;
            if ((pin < 0) || (7 < pin))
                return ResultCode.InvalidParameter;
            bool state;
            ResultCode result = ioExpander.ReadPin(1, (byte)pin, out state);
            pinState = (state == false) ? PinState.On : PinState.Off;
            return result;
        }

        public ResultCode WritePort(byte value) {
            if (!IsInitialized) {
                ResultCode result = Initialize();
                if (result != ResultCode.OK)
                    return result;
            }
            if (mikroBusResetPin == PinState.On)
                value |= 0x80;
            else
                value &= 0x7f;
            return ioExpander.WritePort(0, (byte)~value);
        }

        public ResultCode WritePin(int pin, PinState pinState) {
            if ((pin < 0) || (3 < pin))
                return ResultCode.InvalidParameter;
            if (!IsInitialized) {
                ResultCode result = Initialize();
                if (result != ResultCode.OK)
                    return result;
            }
            bool state = (pinState == PinState.Off) ? true : false;
            return ioExpander.WritePin(0, (byte)pin, state);
        }

        public ResultCode SetMikroBusResetPin(PinState pinState) {
            ResultCode result;
            if (!IsInitialized) {
                result = Initialize();
                if (result != ResultCode.OK)
                    return result;
            }
            bool state = (pinState == PinState.Off) ? true : false;
            result = ioExpander.WritePin(0, 7, state);   // ResetOff(/RST=H), ResetOn(/RST=L)
            if (result != ResultCode.OK)
                return result;
            mikroBusResetPin = pinState;
            return ResultCode.OK;
        }

    }
}
