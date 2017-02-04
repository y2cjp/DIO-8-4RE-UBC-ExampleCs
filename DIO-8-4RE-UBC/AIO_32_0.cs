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
using System.Threading;

namespace Y2C_I2C_Libraries {
    class AIO_32_0 {

        public bool IsInitialized { get; private set; }
        private PCA9554 pca9554;    // Multiplexer
        private ADS1115 ads1115;    // ADC
        private byte mux = 0xff;

        public enum Pga : byte {
            FS10_0352V = ADS1115.Pga.FS2_048V,
            FS5_0176V = ADS1115.Pga.FS1_024V,
            FS2_5088V = ADS1115.Pga.FS0_512V,
            FS1_2544V = ADS1115.Pga.FS0_256V
        }

        public enum DataRate : byte {
            SPS8 = ADS1115.DataRate.SPS8,
            SPS16 = ADS1115.DataRate.SPS16,
            SPS32 = ADS1115.DataRate.SPS32,
            SPS64 = ADS1115.DataRate.SPS64,
            SPS128 = ADS1115.DataRate.SPS128,
            SPS250 = ADS1115.DataRate.SPS250,
            SPS475 = ADS1115.DataRate.SPS475,
            SPS860 = ADS1115.DataRate.SPS860,
        }

        public AIO_32_0(I2C_FT4222 i2c, byte adcAddress, byte muxAddress) {
            pca9554 = new PCA9554(i2c, muxAddress);
            ads1115 = new ADS1115(i2c, adcAddress);
        }

        public ResultCode Initialize() {
            ResultCode result = pca9554.SetPortDirection(0x00);
            if (result == ResultCode.OK)
                IsInitialized = true;
            return result;
        }

        private void ChToDeviceChannel(int ch, out int adcChannel, out int mux) {
            adcChannel = ch / 16;
            mux = ch % 16;
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="channel">
        /// 0: AIN0-GND ... 31: AIN31-GND (SingleEnded)
        /// 32: AIN0-ADS1115_AIN3 ... 63: AIN31-ADS1115_AIN3 (SingleEnded)
        /// 256: AIN0-AIN16 ... 271: AIN15-AIN16 (Differential)
        /// 272: AIN0-AIN17 ... 287: AIN15-AIN17 (Differential)
        /// 496: AIN0-AIN31 ... 511: AIN15-AIN31 (Differential)
        /// </param>
        public ResultCode ReadRaw(int channel, out int value, DataRate dataRate = DataRate.SPS128, Pga pga = Pga.FS10_0352V) {
            value = 0;
            ResultCode result = ResultCode.OK;
            if (!IsInitialized) {
                Initialize();
            }

            byte extMux;
            ADS1115.Mux adcMux;
            if (channel < 16) {
                extMux = (byte)(mux & 0xf0 | channel);
                adcMux = ADS1115.Mux.Ain0_Gnd;
            } else if (channel < 32) {
                extMux = (byte)(mux & 0x0f | (channel & 0x0f) << 4);
                adcMux = ADS1115.Mux.Ain1_Gnd;
            } else if (channel < 48) {
                extMux = (byte)(mux & 0xf0 | channel);
                adcMux = ADS1115.Mux.Ain0_Ain3;
            } else if (channel < 64) {
                extMux = (byte)(mux & 0x0f | (channel & 0x0f) << 4);
                adcMux = ADS1115.Mux.Ain1_Ain3;
            } else if (channel < 256) {
                return ResultCode.InvalidParameter;
            } else {
                extMux = (byte)(channel & 0xff);
                adcMux = ADS1115.Mux.Ain0_Ain1;
            }

            result = pca9554.WritePort(extMux);
            if (result != ResultCode.OK)
                return result;
            mux = extMux;
            Thread.Sleep(1);

            result = ads1115.ReadRaw(adcMux, out value, (ADS1115.DataRate)dataRate, (ADS1115.Pga)pga);
            return result;
        }

        public ResultCode ReadRaw(int startChannel, int[] value, int length, DataRate dataRate = DataRate.SPS128, Pga pga = Pga.FS10_0352V) {
            ResultCode result = ResultCode.OK;
            for (int ch = startChannel; ch < startChannel + length; ch++) {
                int tmp;
                result = ReadRaw(ch, out tmp, dataRate, pga);
                if (result != ResultCode.OK)
                    return result;
                value[ch - startChannel] = tmp;
            }
            return ResultCode.OK;
        }

        public ResultCode ReadVoltage(int startChannel, float[] volt, int length, DataRate dataRate = DataRate.SPS128, Pga pga = Pga.FS10_0352V) {
            int[] value = new int[length];
            ResultCode result = ReadRaw(startChannel, value, length, dataRate, pga);
            if (result != ResultCode.OK)
                return result;
            for (int ch = 0; ch < length; ch++) {
                volt[ch] = ToVolt(value[ch], pga);
            }
            return ResultCode.OK;
        }

        private float ToVolt(int value, Pga pga) {
            switch (pga) {
                case Pga.FS1_2544V:
                    return 1.2544F * value / 32767;
                case Pga.FS2_5088V:
                    return 2.5088F * value / 32767;
                case Pga.FS5_0176V:
                    return 5.0176F * value / 32767;
                default:
                    return 10.0352F * value / 32767;
            }
        }

    }
}
