// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Adafruit2348.cs" company="Y2 Corporation">
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
    public class Adafruit2348
    {
        private readonly Pca9685 pca9685;

        public Adafruit2348(I2CFt4222 i2C, byte slaveAddress)
        {
            pca9685 = new Pca9685(i2C, slaveAddress);
        }

        public Adafruit2348(I2CFt4222 i2C)
        {
            pca9685 = new Pca9685(i2C, 0x60);
        }

        public enum Pin : byte
        {
            M3Pwm = 2,
            M3In2,
            M3In1,
            M4In1,
            M4In2,
            M4Pwm,
            M1Pwm,
            M1In2,
            M1In1,
            M2In1,
            M2In2,
            M2Pwm
        }

        public enum PinState
        {
            Off,
            On
        }

        public enum DriveMode : byte
        {
            CW,
            CCW,
            Brake,
            Stop
        }

        public ResultCode SetDriveMode(int channel, DriveMode driveMode)
        {
            Pin in1;
            Pin in2;
            switch (channel)
            {
                case 1:
                    in1 = Pin.M1In1;
                    in2 = Pin.M1In2;
                    break;
                case 2:
                    in1 = Pin.M2In1;
                    in2 = Pin.M2In2;
                    break;
                case 3:
                    in1 = Pin.M3In1;
                    in2 = Pin.M3In2;
                    break;
                case 4:
                    in1 = Pin.M4In1;
                    in2 = Pin.M4In2;
                    break;
                default:
                    return ResultCode.FatalError;
            }

            ResultCode result;
            switch (driveMode)
            {
                case DriveMode.CW:
                    result = SetPin(in1, PinState.On);
                    if (result != ResultCode.Ok)
                        return result;
                    return SetPin(in2, PinState.Off);
                case DriveMode.CCW:
                    result = SetPin(in1, PinState.Off);
                    if (result != ResultCode.Ok)
                        return result;
                    return SetPin(in2, PinState.On);
                case DriveMode.Brake:
                    result = SetPin(in1, PinState.On);
                    if (result != ResultCode.Ok)
                        return result;
                    return SetPin(in2, PinState.On);

                // case DriveMode.Stop:
                default:
                    result = SetPin(in1, PinState.Off);
                    if (result != ResultCode.Ok)
                        return result;
                    return SetPin(in2, PinState.Off);
            }
        }

        public ResultCode SetPin(Pin pwmChannel, PinState pinState)
        {
            if (pinState == PinState.Off)
                return pca9685.SetPwm((byte)pwmChannel, 0, 0);
            return pca9685.SetPwm((byte)pwmChannel, 4096, 0);
        }

        public ResultCode SetSpeed(int motorChannel, int speed)
        {
            byte pwmChannel;
            switch (motorChannel)
            {
                case 1:
                    pwmChannel = (byte)Pin.M1Pwm;
                    break;
                case 2:
                    pwmChannel = (byte)Pin.M2Pwm;
                    break;
                case 3:
                    pwmChannel = (byte)Pin.M3Pwm;
                    break;
                case 4:
                    pwmChannel = (byte)Pin.M4Pwm;
                    break;
                default:
                    return ResultCode.FatalError;
            }
            return pca9685.SetPwm(pwmChannel, 0, (ushort)speed);
        }

        public ResultCode SetFrequency(float frequency)
        {
            return pca9685.SetPwmFreq(frequency);
        }
    }
}
