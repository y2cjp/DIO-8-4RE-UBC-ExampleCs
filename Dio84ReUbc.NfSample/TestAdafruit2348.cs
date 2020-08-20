// MIT License
//
// Copyright (c) Y2 Corporation

using System.Threading;

namespace Dio84ReUbc.NfLibrary
{
    public class TestAdafruit2348
    {
        private readonly I2CFt4222 _i2C;

        public TestAdafruit2348(I2CFt4222 i2c)
        {
            _i2C = i2c;
        }

        /// <summary>
        /// DCモーター駆動テスト
        /// </summary>
        /// <param name="channel">チャネル</param>
        /// <param name="speedMax">速度</param>
        /// <param name="speedStep">加減速度</param>
        public ResultCode TestDcMotor(int channel = 1, int speedMax = 4095, int speedStep = 2)
        {
            Adafruit2348 motorHat = new Adafruit2348(_i2C);

            motorHat.SetFrequency(1500);

            // CW方向へ回転開始
            motorHat.SetDriveMode(channel, Adafruit2348.DriveMode.CW);

            // 加速
            for (int speed = 0; speed < speedMax; speed += speedStep)
            {
                motorHat.SetSpeed(channel, speed);
            }

            // 減速
            for (int speed = speedMax; speed > 0; speed -= speedStep)
            {
                motorHat.SetSpeed(channel, speed);
            }

            motorHat.SetSpeed(channel, 0);
            Thread.Sleep(1000);

            // CCW方向へ回転開始
            motorHat.SetDriveMode(channel, Adafruit2348.DriveMode.CCW);

            // 加速
            for (int speed = 0; speed < speedMax; speed += speedStep)
            {
                motorHat.SetSpeed(channel, speed);
            }

            // 減速
            for (int speed = speedMax; speed > 0; speed -= speedStep)
            {
                motorHat.SetSpeed(channel, speed);
            }

            motorHat.SetSpeed(channel, 0);
            motorHat.SetDriveMode(channel, Adafruit2348.DriveMode.Stop);
            Thread.Sleep(1000);
            return ResultCode.Ok;
        }
    }
}
