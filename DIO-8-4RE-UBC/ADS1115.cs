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
using System;
using System.Threading;

namespace Y2C_I2C_Libraries {
    class ADS1115 {

        private enum Register : byte {
            Conversion = 0x00,
            Config = 0x01,
            LoThresh = 0x02,
            HiThresh = 0x03
        }

        private enum SingleShotCoversion : byte {
            NoEffect,
            Begin,
            //Begin = 0x80,
        }

        public enum Mux : byte {
            Ain0_Ain1,
            Ain0_Ain3,
            Ain1_Ain3,
            Ain2_Ain3,
            Ain0_Gnd,
            Ain1_Gnd,
            Ain2_Gnd,
            Ain3_Gnd
        }

        public enum Pga : byte {
            FS6_144V,
            FS4_096V,
            FS2_048V,
            FS1_024V,
            FS0_512V,
            FS0_256V
        }

        private enum Mode : byte {
            Continuous,
            PowerDown_SingleShot
        }

        public enum DataRate : byte {
            SPS8,
            SPS16,
            SPS32,
            SPS64,
            SPS128,  // Default
            SPS250,
            SPS475,
            SPS860,
        }

        private enum ComparatorQueue : byte {
            Disable = 0x03
        }

        private byte slaveAddress;
        private I2C_FT4222 i2c;

        public ADS1115(I2C_FT4222 i2c, byte slaveAddress) {
            this.i2c = i2c;
            this.slaveAddress = slaveAddress;
        }

        public ResultCode ReadRaw(Mux mux, out int value, DataRate dataRate = DataRate.SPS128, Pga pga = Pga.FS2_048V) {
            value = 0;
            byte[] config = new byte[] { (byte)Register.Config, (byte)((byte)SingleShotCoversion.Begin << 7 | (byte)mux << 4 | (byte)pga << 1 | (byte)Mode.PowerDown_SingleShot), (byte)((byte)dataRate << 5 | (byte)ComparatorQueue.Disable) };
            ResultCode result = i2c.Write(slaveAddress, ref config[0], 3);
            if (result != ResultCode.OK)
                return result;

            byte[] conversion = new byte[] { (byte)Register.Conversion };
            result = i2c.WriteEx(slaveAddress, (byte)LibFT4222.I2cMasterFlag.Start, ref conversion[0], 1);
            if (result != ResultCode.OK)
                return result;
            switch (dataRate) {
                case DataRate.SPS8:
                    Thread.Sleep(126);
                    break;
                case DataRate.SPS16:
                    Thread.Sleep(64);
                    break;
                case DataRate.SPS32:
                    Thread.Sleep(33);
                    break;
                case DataRate.SPS64:
                    Thread.Sleep(17);
                    break;
                case DataRate.SPS128:
                    Thread.Sleep(9);
                    break;
                case DataRate.SPS250:
                    Thread.Sleep(5);
                    break;
                case DataRate.SPS475:
                    Thread.Sleep(4);
                    break;
                case DataRate.SPS860:
                    Thread.Sleep(3);
                    break;
            }
            byte[] readBuffer = new byte[2];
            result = i2c.ReadEx(slaveAddress, (byte)(LibFT4222.I2cMasterFlag.RepeatedStart | LibFT4222.I2cMasterFlag.Stop), ref readBuffer[0], (ushort)readBuffer.Length);
            if (result != ResultCode.OK)
                return result;

            value = (Int16)((readBuffer[0] << 8) | readBuffer[1]);
            return result;
        }

    }
}
