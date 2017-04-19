// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Dio016.cs" company="Y2 Corporation">
//   Copyright (c) 2017 Y2 Corporation and Future Technology Devices International Limited. All rights reserved.
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
            var result = IoExpanderReadPort(0, out value);
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
                var result = Initialize();
                if (result != ResultCode.Ok)
                    return result;
            }

            var valueByte = new[]
            {
                (byte)(~value & 0xff),
                (byte)(~(value >> 8) & 0xff)
            };
            return IoExpanderWritePort(0, valueByte, 0, valueByte.Length);
        }

        public ResultCode WritePort(int port, byte value)
        {
            if (port < 0 || port > 1)
                return ResultCode.InvalidParameter;
            if (!IsInitialized)
            {
                var result = Initialize();
                if (result != ResultCode.Ok)
                    return result;
            }

            value = (byte)~value;
            return ioExpander?.WritePort(port, value) ?? ResultCode.FatalError;
        }

        public ResultCode WritePin(int pin, PinState pinState)
        {
            if (pin < 0 || pin > 15)
                return ResultCode.InvalidParameter;
            if (!IsInitialized)
            {
                var result = Initialize();
                if (result != ResultCode.Ok)
                    return result;
            }

            var state = pinState == PinState.Off;
            int devicePort, devicePin;
            PinToDevicePin(pin, out devicePort, out devicePin);

            return IoExpanderWritePin(devicePort, devicePin, state);
        }

        public ResultCode ReadRegister(Pca9535.Register register, byte[] value, int index, int length)
        {
            return ioExpander?.ReadRegister(register, value, index, length) ?? ResultCode.FatalError;
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
            return ioExpander?.SetPortDirection(port, value, index, length) ?? ResultCode.FatalError;
        }

        private ResultCode IoExpanderReadPort(int port, out byte value)
        {
            value = 0;
            return ioExpander == null ? ResultCode.FatalError : ioExpander.ReadPort(port, out value);
        }

        private ResultCode IoExpanderWritePort(int port, byte[] value, int index, int length)
        {
            return ioExpander?.WritePort(port, value, index, length) ?? ResultCode.FatalError;
        }

        private ResultCode IoExpanderWritePin(int port, int pin, bool state)
        {
            return ioExpander?.WritePin(port, pin, state) ?? ResultCode.FatalError;
        }
    }
}
