// MIT License
//
// Copyright (c) Y2 Corporation

using System.Threading.Tasks;
using Y2.Ft4222.Core;

namespace Dio84ReUbc.CoreSample
{
    public class TestAdafruit2348
    {
        private readonly IFt4222I2cMaster _i2c;

        public TestAdafruit2348(IFt4222I2cMaster i2c)
        {
            _i2c = i2c;
        }

        /// <summary>
        /// DCモーター駆動テスト
        /// </summary>
        /// <param name="channel">チャネル</param>
        /// <param name="speedMax">速度</param>
        /// <param name="speedStep">加減速度</param>
        public async Task TestDcMotor(int channel = 1, int speedMax = 4095, int speedStep = 2)
        {
            var motorHat = new Adafruit2348(_i2c);

            motorHat.SetFrequency(1500);

            // CW方向へ回転開始
            motorHat.SetDriveMode(channel, Adafruit2348.DriveMode.CW);

            // 加速
            for (var speed = 0; speed < speedMax; speed += speedStep)
            {
                motorHat.SetSpeed(channel, speed);
            }

            // 減速
            for (var speed = speedMax; speed > 0; speed -= speedStep)
            {
                motorHat.SetSpeed(channel, speed);
            }

            motorHat.SetSpeed(channel, 0);
            await Task.Delay(1000);

            // CCW方向へ回転開始
            motorHat.SetDriveMode(channel, Adafruit2348.DriveMode.CCW);

            // 加速
            for (var speed = 0; speed < speedMax; speed += speedStep)
            {
                motorHat.SetSpeed(channel, speed);
            }

            // 減速
            for (var speed = speedMax; speed > 0; speed -= speedStep)
            {
                motorHat.SetSpeed(channel, speed);
            }

            motorHat.SetSpeed(channel, 0);
            motorHat.SetDriveMode(channel, Adafruit2348.DriveMode.Stop);
            await Task.Delay(1000);
        }
    }
}
