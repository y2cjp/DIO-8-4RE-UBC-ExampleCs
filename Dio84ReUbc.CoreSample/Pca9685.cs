// MIT License
//
// Copyright (c) Y2 Corporation

using System;
using System.Linq;
using System.Threading;
using Iot.Device.Ft4222;
using Y2.Ft4222.Core;

namespace Dio84ReUbc.CoreSample
{
    public class Pca9685 : Ft4222I2cSlaveDevice
    {
        public Pca9685(IFt4222I2cMaster i2c, byte slaveAddress) :  base(i2c, slaveAddress)
        {
        }

        public enum Register : byte
        {
            Mode1,
            Mode2,
            SubAdr1,
            SubAdr2,
            SubAdr3,
            AllCallAdr,
            Led0OnL,
            Led0OnH,
            Led0OffL,
            Led0OffH,
            AllLedOnL = 250,
            AllLedOnH,
            AllLedOffL,
            AllLedOffH,
            PreScale,
            TestMode
        }

        public void SetPwmFreq(float frequency)
        {
            if (frequency < 24)
                frequency = 24;

            byte prescale = (byte)((float)25000000 / 4096 / frequency - 1);
            if (prescale < 3)
                prescale = 3;

            WriteRegister(Register.Mode1, 0x31);        // Oscillator Off
            WriteRegister(Register.PreScale, prescale);
            WriteRegister(Register.Mode1, 0x21);        // Oscillator On
            Thread.Sleep(5);
            WriteRegister(Register.Mode1, 0xa1);
        }

        public void SetPwm(byte channel, ushort on, ushort off)
        {
            if (on > 4096)
                on = 4096;
            if (off > 4096)
                off = 4096;

            byte[] writeBuffer = { (byte)(Register.Led0OnL + (byte)(4 * channel)), (byte)on, (byte)(on >> 8), (byte)off, (byte)(off >> 8) };
            Write(writeBuffer);
        }

        public void ReadRegister(Register register, byte[] values, int index, int length)
        {
            ReadOnlySpan<byte> writeBuffer = stackalloc byte[] { (byte)register };
            WriteEx(I2cMasterFlag.Start, writeBuffer);
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            ReadEx(I2cMasterFlag.RepeatedStart | I2cMasterFlag.Stop, values);
        }

        public void WriteRegister(Register register, byte[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            ReadOnlySpan<byte> writeBuffer = new [] { (byte)register }.Concat(values.ToArray()).ToArray();
            Write(writeBuffer);
        }

        public void WriteRegister(Register register, byte value)
        {
            var writeBuffer = new[] { (byte)register, value };
            Write(writeBuffer);
        }
    }
}
