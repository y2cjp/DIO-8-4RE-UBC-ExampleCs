// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestAdafruit2348.cs" company="Y2 Corporation">
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

using System.Threading;

namespace DIO_8_4RE_UBC
{
    public class TestAdafruit2348
    {
        private readonly I2CFt4222 i2c;

        public TestAdafruit2348(I2CFt4222 i2c)
        {
            this.i2c = i2c;
        }

        /// <summary>
        /// DCモーター駆動テスト
        /// </summary>
        /// <param name="channel">チャネル</param>
        /// <param name="speedMax">速度</param>
        /// <param name="speedStep">加減速度</param>
        public ResultCode TestDcMotor(int channel = 1, int speedMax = 4095, int speedStep = 2)
        {
            Adafruit2348 motorHat = new Adafruit2348(i2c);

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
