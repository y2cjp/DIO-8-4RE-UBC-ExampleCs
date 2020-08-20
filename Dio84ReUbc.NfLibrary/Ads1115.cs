// MIT License
//
// Copyright (c) Y2 Corporation

using System.Threading;

namespace Dio84ReUbc.NfLibrary
{
    public class Ads1115
    {
        private readonly I2CFt4222 _i2C;
        private readonly byte _slaveAddress;

        public Ads1115(I2CFt4222 i2C, byte slaveAddress)
        {
            _i2C = i2C;
            _slaveAddress = slaveAddress;
        }

        public enum Mux : byte
        {
            Ain0Ain1,
            Ain0Ain3,
            Ain1Ain3,
            Ain2Ain3,
            Ain0Gnd,
            Ain1Gnd,
            Ain2Gnd,
            Ain3Gnd
        }

        public enum Pga : byte
        {
            /// <summary>
            /// Full Scale: 6.144V
            /// </summary>
            Fs6144mV,

            /// <summary>
            /// Full Scale: 4.096V
            /// </summary>
            Fs4096mV,

            /// <summary>
            /// Full Scale: 2.048V
            /// </summary>
            Fs2048mV,

            /// <summary>
            /// Full Scale: 1.024V
            /// </summary>
            Fs1024mV,

            /// <summary>
            /// Full Scale: 0.512V
            /// </summary>
            Fs512mV,

            /// <summary>
            /// Full Scale: 0.256V
            /// </summary>
            Fs256mV
        }

        public enum DataRate : byte
        {
            Sps8,
            Sps16,
            Sps32,
            Sps64,
            Sps128,  // Default
            Sps250,
            Sps475,
            Sps860
        }

        private enum Register : byte
        {
            Conversion = 0x00,
            Config = 0x01,
            //// LoThresh = 0x02,
            //// HiThresh = 0x03
        }

        private enum SingleShotCoversion : byte
        {
            //// NoEffect,
            Begin = 1
        }

        private enum Mode : byte
        {
            //// Continuous,
            PowerDownSingleShot = 1
        }

        private enum ComparatorQueue : byte
        {
            Disable = 0x03
        }

        public ResultCode ReadRaw(Mux mux, out int value, DataRate dataRate = DataRate.Sps128, Pga pga = Pga.Fs2048mV)
        {
            value = 0;
            byte[] config = { (byte)Register.Config, (byte)(((byte)SingleShotCoversion.Begin << 7) | ((byte)mux << 4) | ((byte)pga << 1) | (byte)Mode.PowerDownSingleShot), (byte)(((byte)dataRate << 5) | (byte)ComparatorQueue.Disable) };
            if (_i2C == null)
                return ResultCode.FatalError;

            ResultCode result = _i2C.Write(_slaveAddress, ref config[0], 3);
            if (result != ResultCode.Ok)
                return result;

            byte[] conversion = { (byte)Register.Conversion };
            result = _i2C.WriteEx(_slaveAddress, (byte)LibFt4222.I2CMasterFlag.Start, ref conversion[0], 1);
            if (result != ResultCode.Ok)
                return result;

            switch (dataRate)
            {
                case DataRate.Sps8:
                    Thread.Sleep(126);
                    break;
                case DataRate.Sps16:
                    Thread.Sleep(64);
                    break;
                case DataRate.Sps32:
                    Thread.Sleep(33);
                    break;
                case DataRate.Sps64:
                    Thread.Sleep(17);
                    break;
                case DataRate.Sps128:
                    Thread.Sleep(9);
                    break;
                case DataRate.Sps250:
                    Thread.Sleep(5);
                    break;
                case DataRate.Sps475:
                    Thread.Sleep(4);
                    break;
                case DataRate.Sps860:
                    Thread.Sleep(3);
                    break;
                default:
                    return ResultCode.FatalError;
            }

            byte[] readBuffer = new byte[2];
            result = _i2C.ReadEx(_slaveAddress, (byte)(LibFt4222.I2CMasterFlag.RepeatedStart | LibFt4222.I2CMasterFlag.Stop), ref readBuffer[0], (ushort)readBuffer.Length);
            if (result != ResultCode.Ok)
                return result;

            value = (short)((readBuffer[0] << 8) | readBuffer[1]);
            return result;
        }
    }
}
