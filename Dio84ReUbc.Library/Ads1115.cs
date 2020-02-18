// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Ads1115.cs" company="Y2 Corporation">
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
    public class Ads1115
    {
        private readonly I2CFt4222 i2C;
        private readonly byte slaveAddress;

        public Ads1115(I2CFt4222 i2C, byte slaveAddress)
        {
            this.i2C = i2C;
            this.slaveAddress = slaveAddress;
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
            if (i2C == null)
                return ResultCode.FatalError;

            ResultCode result = i2C.Write(slaveAddress, ref config[0], 3);
            if (result != ResultCode.Ok)
                return result;

            byte[] conversion = { (byte)Register.Conversion };
            result = i2C.WriteEx(slaveAddress, (byte)LibFt4222.I2CMasterFlag.Start, ref conversion[0], 1);
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
            result = i2C.ReadEx(slaveAddress, (byte)(LibFt4222.I2CMasterFlag.RepeatedStart | LibFt4222.I2CMasterFlag.Stop), ref readBuffer[0], (ushort)readBuffer.Length);
            if (result != ResultCode.Ok)
                return result;

            value = (short)((readBuffer[0] << 8) | readBuffer[1]);
            return result;
        }
    }
}
