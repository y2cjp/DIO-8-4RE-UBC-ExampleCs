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
using Y2C_I2C_Libraries;

namespace Y2C_I2C_Libraries {
    class DIO_0_16 {

        private PCA9535 ioExpander;
        public bool IsInitialized { get; private set; }

        public enum PinState {
            Off,
            On
        }

        public DIO_0_16(I2C_FT4222 i2c, byte slaveAddress) {
            IsInitialized = false;
            ioExpander = new PCA9535(i2c, slaveAddress);
        }

        public ResultCode ReadRegister(PCA9535.Register register, byte[] value, int index, int length) {
            return ioExpander.ReadRegister(register, value, index, length);
        }

        public ResultCode Initialize() {
            // 接続状態確認の為、ダミーリード
            byte value;
            ResultCode result = ioExpander.ReadPort(0, out value);
            if (result != ResultCode.OK)
                return result;

            byte[] buffer = new byte[] { 0xff, 0xff };
            result = ioExpander.WritePort(0, buffer, 0, 2);
            if (result != ResultCode.OK)
                return result;
            buffer[0] = 0x00;
            buffer[1] = 0x00;
            result = ioExpander.SetPortDirection(0, buffer, 0x00, 2);
            if (result == ResultCode.OK)
                IsInitialized = true;
            return result;
        }

        private void PinToDevicePin(int pin, out int devicePort, out int devicePin) {
            if (pin < 8)
                devicePort = 0;
            else if (pin < 16)
                devicePort = 1;
            else
                for (;;) ;
            devicePin = pin % 8;
        }

        public ResultCode WriteAll(uint value) {
            if (!IsInitialized) {
                ResultCode result = Initialize();
                if (result != ResultCode.OK)
                    return result;
            }
            byte[] valueByte = new byte[] {
                (byte)(~value & 0xff),
                (byte)(~(value >> 8) & 0xff) };
            return ioExpander.WritePort(0, valueByte, 0, valueByte.Length);
        }

        public ResultCode WritePort(int port, byte value) {
            if ((port < 0) || (1 < port))
                return ResultCode.InvalidParameter;
            if (!IsInitialized) {
                ResultCode result = Initialize();
                if (result != ResultCode.OK)
                    return result;
            }
            value = (byte)~value;
            return ioExpander.WritePort(port, value);
        }

        public ResultCode WritePin(int pin, PinState pinState) {
            if ((pin < 0) || (15 < pin))
                return ResultCode.InvalidParameter;
            if (!IsInitialized) {
                ResultCode result = Initialize();
                if (result != ResultCode.OK)
                    return result;
            }
            bool state;
            state = (pinState == PinState.Off) ? true : false;
            int devicePort, devicePin;
            PinToDevicePin(pin, out devicePort, out devicePin);
            return ioExpander.WritePin(devicePort, devicePin, state);
        }

    }
}
