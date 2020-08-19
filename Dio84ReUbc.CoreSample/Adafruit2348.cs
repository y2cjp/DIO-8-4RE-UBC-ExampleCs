// MIT License
//
// Copyright (c) Y2 Corporation

using System;
using Y2.Ft4222.Core;

namespace Dio84ReUbc.CoreSample
{
    public class Adafruit2348
    {
        private readonly Pca9685 _pca9685;

        public Adafruit2348(IFt4222I2cMaster i2c, byte slaveAddress = 0x60)
        {
            _pca9685 = new Pca9685(i2c, slaveAddress);
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

        public void SetDriveMode(int channel, DriveMode driveMode)
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
                    throw new ArgumentOutOfRangeException(nameof(channel));
            }

            switch (driveMode)
            {
                case DriveMode.CW:
                    SetPin(in1, PinState.On);
                    SetPin(in2, PinState.Off);
                    return;
                case DriveMode.CCW:
                    SetPin(in1, PinState.Off);
                    SetPin(in2, PinState.On);
                    return;
                case DriveMode.Brake:
                    SetPin(in1, PinState.On);
                    SetPin(in2, PinState.On);
                    return;
                // case DriveMode.Stop:
                default:
                    SetPin(in1, PinState.Off);
                    SetPin(in2, PinState.Off);
                    return;
            }
        }

        public void SetPin(Pin pwmChannel, PinState pinState)
        {
            _pca9685.SetPwm((byte)pwmChannel, pinState == PinState.Off ? (ushort)0 : (ushort)4096, 0);
        }

        public void SetSpeed(int motorChannel, int speed)
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
                    throw new ArgumentOutOfRangeException(nameof(motorChannel));
            }

            _pca9685.SetPwm(pwmChannel, 0, (ushort)speed);
        }

        public void SetFrequency(float frequency)
        {
            _pca9685.SetPwmFreq(frequency);
        }
    }
}
