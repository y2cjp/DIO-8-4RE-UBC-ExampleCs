// --------------------------------------------------------------------------------------------------------------------
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
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;

namespace DIO_8_4RE_UBC
{
    public partial class Form1 : Form
    {
        private I2CFt4222 i2C;
        private Dio84 dio84Re;    // DIO-8/4RE-UBC
        private Dio84 dio84Rd;    // DIO-8/4RD-IRC
        private Dio016 dio016Rc;  // DIO-0/16RC-IRC
        private Aio320 aio320Ra;  // AIO-32/0RA-IRC

        public Form1()
        {
            InitializeComponent();
        }

        public void Print(string text)
        {
            if (printTextBox == null || text == null)
                return;
            printTextBox.AppendText(text);
            Application.DoEvents();
        }

        public void Println(string text)
        {
            Print(text + Environment.NewLine);
        }

        private ResultCode I2COpen()
        {
            if (i2C != null)
                return ResultCode.Ok;
            i2C = new I2CFt4222();
            ResultCode result = i2C.Open();
            if (result == ResultCode.Ok)
                return result;
            MessageBox.Show(result.ToString());
            I2CClose();
            return result;
        }

        private void I2CClose()
        {
            if (i2C == null)
                return;
            ResultCode result = i2C.Close();
            if (result != ResultCode.Ok)
                Println("I2C Close NG: " + result);
            i2C = null;
        }

        //// *****************************************************
        //// DIO-8/4RE-UBC
        //// *****************************************************

        private ResultCode Dio84ReInitialize()
        {
            ResultCode result;
            if (i2C == null)
            {
                result = I2COpen();
                if (result != ResultCode.Ok)
                    return result;
            }

            if (dio84Re == null)
                dio84Re = new Dio84(i2C, Convert.ToByte(dio84reAddressTextBox.Text, 16));
            result = dio84Re.Initialize();
            if (result != ResultCode.Ok)
                MessageBox.Show(result.ToString());
            else
                Println("DIO-8/4RE-UBC: Initialize OK");
            return result;
        }

        private void dio84reInitializeButton_Click(object sender, EventArgs e)
        {
            Dio84ReInitialize();
        }

        private void dio84reReadButton_Click(object sender, EventArgs e)
        {
            ResultCode result;

            // if (!dio84Re?.IsInitialized != true)         // C# 6.0 or later
            if (dio84Re == null || !dio84Re.IsInitialized)
            {
                result = Dio84ReInitialize();
                if (result != ResultCode.Ok)
                    return;
            }

            byte value;
            result = dio84Re.ReadPort(out value);
            if (result != ResultCode.Ok)
            {
                MessageBox.Show(result.ToString());
                return;
            }

            dio84reReadTextBox.Text = @"0x" + value.ToString("x2");
            Println("DIO-8/4RE-UBC: Read OK");
        }

        private void dio84reWriteButton_Click(object sender, EventArgs e)
        {
            ResultCode result;

            // if (!dio84Re?.IsInitialized != true)         // C# 6.0 or later
            if (dio84Re == null || !dio84Re.IsInitialized)
            {
                result = Dio84ReInitialize();
                if (result != ResultCode.Ok)
                    return;
            }

            if (string.IsNullOrEmpty(dio84reWriteTextBox.Text))
                dio84reWriteTextBox.Text = @"0";
            byte value = Convert.ToByte(dio84reWriteTextBox.Text, 16);
            dio84reWriteTextBox.Text = @"0x" + value.ToString("x2");
            result = dio84Re.WritePort(value);
            if (result != ResultCode.Ok)
                MessageBox.Show(result.ToString());
            else
                Println("DIO-8/4RE-UBC: Write OK");
        }

        //// *****************************************************
        //// DIO-8/4RD-IRC
        //// *****************************************************

        private ResultCode Dio84RdInitialize()
        {
            ResultCode result;
            if (i2C == null)
            {
                result = I2COpen();
                if (result != ResultCode.Ok)
                    return result;
            }

            if (dio84Rd == null)
                dio84Rd = new Dio84(i2C, Convert.ToByte(dio84rdAddressTextBox.Text, 16));

            result = dio84Rd.Initialize();
            if (result != ResultCode.Ok)
                MessageBox.Show(result.ToString());
            else
                Println("DIO-8/4RD-IRC: Initialize OK");

            return result;
        }

        private void dio84rdInitializeButton_Click(object sender, EventArgs e)
        {
            Dio84RdInitialize();
        }

        private void dio84rdReadButton_Click(object sender, EventArgs e)
        {
            ResultCode result;

            // if (!dio84Rd?.IsInitialized != true)         // C# 6.0 or later
            if (dio84Rd == null || !dio84Rd.IsInitialized)
                {
                result = Dio84RdInitialize();
                if (result != ResultCode.Ok)
                    return;
            }

            byte value;
            result = dio84Rd.ReadPort(out value);
            if (result != ResultCode.Ok)
            {
                MessageBox.Show(result.ToString());
                return;
            }

            dio84rdReadTextBox.Text = @"0x" + value.ToString("x2");
            Println("DIO-8/4RD-IRC: Read OK");
        }

        private void dio84rdWriteButton_Click(object sender, EventArgs e)
        {
            ResultCode result;

            // if (!dio84Rd?.IsInitialized != true)         // C# 6.0 or later
            if (dio84Rd == null || !dio84Rd.IsInitialized)
            {
                result = Dio84RdInitialize();
                if (result != ResultCode.Ok)
                    return;
            }

            if (string.IsNullOrEmpty(dio84rdWriteTextBox.Text))
                dio84rdWriteTextBox.Text = @"0";
            byte value = Convert.ToByte(dio84rdWriteTextBox.Text, 16);
            dio84rdWriteTextBox.Text = @"0x" + value.ToString("x2");
            result = dio84Rd.WritePort(value);
            if (result != ResultCode.Ok)
                MessageBox.Show(result.ToString());
            else
                Println("DIO-8/4RD-IRC: Write OK");
        }

        //// *****************************************************
        //// DIO-0/16RC-IRC
        //// *****************************************************

        private ResultCode Dio016Initialize()
        {
            ResultCode result;
            if (i2C == null)
            {
                result = I2COpen();
                if (result != ResultCode.Ok)
                    return result;
            }

            if (dio016Rc == null)
                dio016Rc = new Dio016(i2C, Convert.ToByte(dio016rcAddressTextBox.Text, 16));
            result = dio016Rc.Initialize();
            if (result != ResultCode.Ok)
                MessageBox.Show(result.ToString());
            else
                Println("DIO-0/16RC-IRC: Initialize OK");
            return result;
        }

        private void dio016rcInitializeButton_Click(object sender, EventArgs e)
        {
            Dio016Initialize();
        }

        private void dio016rcWriteButton_Click(object sender, EventArgs e)
        {
            ResultCode result;

            // if (!dio016Rc?.IsInitialized != true)            // C# 6.0 or later
            if (dio016Rc == null || !dio016Rc.IsInitialized)
            {
                result = Dio016Initialize();
                if (result != ResultCode.Ok)
                    return;
            }

            if (string.IsNullOrEmpty(dio016rcWriteTextBox.Text))
                dio016rcWriteTextBox.Text = @"0";
            uint value = Convert.ToUInt32(dio016rcWriteTextBox.Text, 16);
            dio016rcWriteTextBox.Text = @"0x" + value.ToString("x4");
            result = dio016Rc.WriteAll(value);
            if (result != ResultCode.Ok)
                MessageBox.Show(result.ToString());
            else
                Println("DIO-0/16RC-IRC: Write OK");
        }

        //// *****************************************************
        //// AIO-32/0RA-IRC
        //// *****************************************************

        private ResultCode Aio320RaInitialize()
        {
            ResultCode result;
            if (i2C == null)
            {
                result = I2COpen();
                if (result != ResultCode.Ok)
                    return result;
            }

            if (aio320Ra == null)
                aio320Ra = new Aio320(i2C, Convert.ToByte(aio320raAdcAddressTextBox.Text, 16), Convert.ToByte(aio320raMuxAddressTextBox.Text, 16));
            result = aio320Ra.Initialize();
            if (result != ResultCode.Ok)
                MessageBox.Show(result.ToString());
            else
                Println("AIO-32/0RA-IRC: Initialize OK");
            return result;
        }

        private void aio320raReadButton_Click(object sender, EventArgs e)
        {
            ResultCode result;

            // if (!aio320Ra?.IsInitialized != true)            // C# 6.0 or later
            if (aio320Ra == null || !aio320Ra.IsInitialized)
            {
                result = Aio320RaInitialize();
                if (result != ResultCode.Ok)
                    return;
            }

            float[] volt = new float[32];
            result = aio320Ra.ReadVoltage(0, volt, 32);
            if (result != ResultCode.Ok)
            {
                MessageBox.Show(result.ToString());
                return;
            }

            Println("AIO-32/0RA-IRC: Read");
            for (int ch = 0; ch < 32; ch++)
                Println(" AIN" + ch + ": " + volt[ch].ToString("F3") + "V");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            I2CClose();
        }
    }
}
