// MIT License
//
// Copyright (c) Y2 Corporation

using System.Threading;

namespace Dio84ReUbc.NfLibrary
{
    public class Aio320
    {
        private readonly Pca9554 _pca9554;   // Multiplexer
        private readonly Ads1115 _ads1115;   // ADC
        private byte _mux;

        public Aio320(I2CFt4222 i2C, byte adcAddress, byte muxAddress)
        {
            _pca9554 = new Pca9554(i2C, muxAddress);
            _ads1115 = new Ads1115(i2C, adcAddress);
            _mux = 0xff;
        }

        public enum Pga : byte
        {
            /// <summary>
            /// Full Scale: 10.0352V
            /// </summary>
            Fs10035mV = Ads1115.Pga.Fs2048mV,

            /// <summary>
            /// Full Scale: 5.0176V
            /// </summary>
            Fs5018mV = Ads1115.Pga.Fs1024mV,

            /// <summary>
            /// Full Scale: 2.5088V
            /// </summary>
            Fs2509mV = Ads1115.Pga.Fs512mV,

            /// <summary>
            /// Full Scale: 1.2544V
            /// </summary>
            Fs1254mV = Ads1115.Pga.Fs256mV
        }

        public enum DataRate : byte
        {
            Sps8 = Ads1115.DataRate.Sps8,
            Sps16 = Ads1115.DataRate.Sps16,
            Sps32 = Ads1115.DataRate.Sps32,
            Sps64 = Ads1115.DataRate.Sps64,
            Sps128 = Ads1115.DataRate.Sps128,
            Sps250 = Ads1115.DataRate.Sps250,
            Sps475 = Ads1115.DataRate.Sps475,
            Sps860 = Ads1115.DataRate.Sps860
        }

        public bool IsInitialized { get; private set; }

        public ResultCode Initialize()
        {
            ResultCode result = _pca9554.SetPortDirection(0x00);
            if (result == ResultCode.Ok)
                IsInitialized = true;
            return result;
        }

        public ResultCode ReadRaw(int channel, out int value, DataRate dataRate = DataRate.Sps128, Pga pga = Pga.Fs10035mV)
        {
            value = 0;
            if (!IsInitialized)
                Initialize();

            byte extMux;
            Ads1115.Mux adcMux;
            if (channel < 16)
            {
                extMux = (byte)((_mux & 0xf0) | channel);
                adcMux = Ads1115.Mux.Ain0Gnd;
            }
            else if (channel < 32)
            {
                extMux = (byte)((_mux & 0x0f) | ((channel & 0x0f) << 4));
                adcMux = Ads1115.Mux.Ain1Gnd;
            }
            else if (channel < 48)
            {
                extMux = (byte)((_mux & 0xf0) | channel);
                adcMux = Ads1115.Mux.Ain0Ain3;
            }
            else if (channel < 64)
            {
                extMux = (byte)((_mux & 0x0f) | ((channel & 0x0f) << 4));
                adcMux = Ads1115.Mux.Ain1Ain3;
            }
            else if (channel < 256)
            {
                return ResultCode.InvalidParameter;
            }
            else
            {
                extMux = (byte)(channel & 0xff);
                adcMux = Ads1115.Mux.Ain0Ain1;
            }

            ResultCode result = _pca9554.WritePort(extMux);
            if (result != ResultCode.Ok)
                return result;

            _mux = extMux;
            Thread.Sleep(1);

            result = _ads1115.ReadRaw(adcMux, out value, (Ads1115.DataRate)dataRate, (Ads1115.Pga)pga);
            return result;
        }

        public ResultCode ReadRaw(int startChannel, int[] value, int length, DataRate dataRate = DataRate.Sps128, Pga pga = Pga.Fs10035mV)
        {
            if (value == null)
                return ResultCode.FatalError;

            for (int ch = startChannel; ch < startChannel + length; ch++)
            {
                int tmp;
                ResultCode result = ReadRaw(ch, out tmp, dataRate, pga);
                if (result != ResultCode.Ok)
                    return result;
                value[ch - startChannel] = tmp;
            }

            return ResultCode.Ok;
        }

        public ResultCode ReadVoltage(int startChannel, float[] volt, int length, DataRate dataRate = DataRate.Sps128, Pga pga = Pga.Fs10035mV)
        {
            if (volt == null)
                return ResultCode.FatalError;

            int[] value = new int[length];
            ResultCode result = ReadRaw(startChannel, value, length, dataRate, pga);
            if (result != ResultCode.Ok)
                return result;
            for (int ch = 0; ch < length; ch++)
                volt[ch] = ToVolt(value[ch], pga);

            return ResultCode.Ok;
        }

        private static float ToVolt(int value, Pga pga)
        {
            switch (pga)
            {
                case Pga.Fs1254mV:
                    return 1.2544F * value / 32767;
                case Pga.Fs2509mV:
                    return 2.5088F * value / 32767;
                case Pga.Fs5018mV:
                    return 5.0176F * value / 32767;
                case Pga.Fs10035mV:
                    return 10.0352F * value / 32767;
                default:
                    return 0;
            }
        }
    }
}
