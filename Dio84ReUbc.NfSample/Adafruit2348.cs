// MIT License
//
// Copyright (c) Y2 Corporation

namespace Dio84ReUbc.NfLibrary
{
    public class Adafruit2348
    {
        private readonly Pca9685 _pca9685;

        public Adafruit2348(I2CFt4222 i2C, byte slaveAddress = 0x60)
        {
            _pca9685 = new Pca9685(i2C, slaveAddress);
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
                return _pca9685.SetPwm((byte)pwmChannel, 0, 0);
            return _pca9685.SetPwm((byte)pwmChannel, 4096, 0);
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
            return _pca9685.SetPwm(pwmChannel, 0, (ushort)speed);
        }

        public ResultCode SetFrequency(float frequency)
        {
            return _pca9685.SetPwmFreq(frequency);
        }
    }
}
