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
using System.Windows.Forms;
using Y2C_I2C_Libraries;

namespace DIO_8_4RE_UBC {

    public partial class Form1 : Form {

        private I2C_FT4222 i2c;
        private DIO_8_4 dio84re;    // DIO-8/4RE-UBC
        private DIO_8_4 dio84rd;    // DIO-8/4RD-IRC
        private DIO_0_16 dio016rc;  // DIO-0/16RC-IRC
        private AIO_32_0 aio320ra;  // AIO-32/0RA-IRC

        public Form1() {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            I2C_Close();
        }

        public void Print(string text) {
            printTextBox.AppendText(text);
            Application.DoEvents();
        }

        public void Println(string text) {
            Print(text + Environment.NewLine);
        }

        private ResultCode I2C_Open() {
            if (i2c != null)
                return ResultCode.OK;
            i2c = new I2C_FT4222();
            ResultCode result = i2c.Open();
            if (result != ResultCode.OK) {
                MessageBox.Show(result.ToString());
                I2C_Close();
            }
            return result;
        }

        private ResultCode I2C_Close() {
            if (i2c == null)
                return ResultCode.OK;
            ResultCode result = i2c.Close();
            if (result != ResultCode.OK) {
                Println("I2C Close NG: " + result.ToString());
            } else {
                //Println("Close OK");
            }
            i2c = null;
            return result;
        }

        // *****************************************************
        // DIO-8/4RE-UBC
        // *****************************************************

        private ResultCode Dio84reInitialize() {
            ResultCode result;
            if (i2c == null) {
                result = I2C_Open();
                if (result != ResultCode.OK)
                    return result;
            }
            if (dio84re == null) {
                dio84re = new DIO_8_4(i2c, Convert.ToByte(dio84reAddressTextBox.Text, 16));
            }
            result = dio84re.Initialize();
            if (result != ResultCode.OK) {
                MessageBox.Show(result.ToString());
            } else {
                Println("DIO-8/4RE-UBC: Initialize OK");
            }
            return result;
        }

        private void dio84reInitializeButton_Click(object sender, EventArgs e) {
            Dio84reInitialize();
        }

        private void dio84reReadButton_Click(object sender, EventArgs e) {
            ResultCode result;
            if ((dio84re == null) || !dio84re.IsInitialized) {
                result = Dio84reInitialize();
                if (result != ResultCode.OK)
                    return;
            }
            byte value;
            result = dio84re.ReadPort(out value);
            if (result != ResultCode.OK) {
                MessageBox.Show(result.ToString());
                return;
            }
            dio84reReadTextBox.Text = "0x" + value.ToString("x2");
            Println("DIO-8/4RE-UBC: Read OK");
        }

        private void dio84reWriteButton_Click(object sender, EventArgs e) {
            ResultCode result;
            if ((dio84re == null) || !dio84re.IsInitialized) {
                result = Dio84reInitialize();
                if (result != ResultCode.OK)
                    return;
            }
            if (string.IsNullOrEmpty(dio84reWriteTextBox.Text))
                dio84reWriteTextBox.Text = "0";
            byte value = Convert.ToByte(dio84reWriteTextBox.Text, 16);
            dio84reWriteTextBox.Text = "0x" + value.ToString("x2");
            result = dio84re.WritePort(value);
            if (result != ResultCode.OK) {
                MessageBox.Show(result.ToString());
            } else {
                Println("DIO-8/4RE-UBC: Write OK");
            }
        }

        // *****************************************************
        // DIO-8/4RD-IRC
        // *****************************************************

        private ResultCode Dio84rdInitialize() {
            ResultCode result;
            if (i2c == null) {
                result = I2C_Open();
                if (result != ResultCode.OK)
                    return result;
            }
            if (dio84rd == null) {
                dio84rd = new DIO_8_4(i2c, Convert.ToByte(dio84rdAddressTextBox.Text, 16));
            }
            result = dio84rd.Initialize();
            if (result != ResultCode.OK) {
                MessageBox.Show(result.ToString());
            } else {
                Println("DIO-8/4RD-IRC: Initialize OK");
            }
            return result;
        }

        private void dio84rdInitializeButton_Click(object sender, EventArgs e) {
            Dio84rdInitialize();
        }

        private void dio84rdReadButton_Click(object sender, EventArgs e) {
            ResultCode result;
            if ((dio84rd == null) || !dio84rd.IsInitialized) {
                result = Dio84rdInitialize();
                if (result != ResultCode.OK)
                    return;
            }
            byte value;
            result = dio84rd.ReadPort(out value);
            if (result != ResultCode.OK) {
                MessageBox.Show(result.ToString());
                return;
            }
            dio84rdReadTextBox.Text = "0x" + value.ToString("x2");
            Println("DIO-8/4RD-IRC: Read OK");
        }

        private void dio84rdWriteButton_Click(object sender, EventArgs e) {
            ResultCode result;
            if ((dio84rd == null) || !dio84rd.IsInitialized) {
                result = Dio84rdInitialize();
                if (result != ResultCode.OK)
                    return;
            }
            if (string.IsNullOrEmpty(dio84rdWriteTextBox.Text))
                dio84rdWriteTextBox.Text = "0";
            byte value = Convert.ToByte(dio84rdWriteTextBox.Text, 16);
            dio84rdWriteTextBox.Text = "0x" + value.ToString("x2");
            result = dio84rd.WritePort(value);
            if (result != ResultCode.OK) {
                MessageBox.Show(result.ToString());
            } else {
                Println("DIO-8/4RD-IRC: Write OK");
            }
        }

        // *****************************************************
        // DIO-0/16RC-IRC
        // *****************************************************

        private ResultCode Dio016Initialize() {
            ResultCode result;
            if (i2c == null) {
                result = I2C_Open();
                if (result != ResultCode.OK)
                    return result;
            }
            if (dio016rc == null)
                dio016rc = new DIO_0_16(i2c, Convert.ToByte(dio016rcAddressTextBox.Text, 16));
            result = dio016rc.Initialize();
            if (result != ResultCode.OK) {
                MessageBox.Show(result.ToString());
            } else {
                Println("DIO-0/16RC-IRC: Initialize OK");
            }
            return result;
        }

        private void dio016rcInitializeButton_Click(object sender, EventArgs e) {
            Dio016Initialize();
        }

        private void dio016rcWriteButton_Click(object sender, EventArgs e) {
            ResultCode result;
            if ((dio016rc == null) || !dio016rc.IsInitialized) {
                result = Dio016Initialize();
                if (result != ResultCode.OK)
                    return;
            }
            if (string.IsNullOrEmpty(dio016rcWriteTextBox.Text))
                dio016rcWriteTextBox.Text = "0";
            uint value = Convert.ToUInt32(dio016rcWriteTextBox.Text, 16);
            dio016rcWriteTextBox.Text = "0x" + value.ToString("x4");
            result = dio016rc.WriteAll(value);
            if (result != ResultCode.OK) {
                MessageBox.Show(result.ToString());
            } else {
                Println("DIO-0/16RC-IRC: Write OK");
            }
        }

        // *****************************************************
        // AIO-32/0RA-IRC
        // *****************************************************

        private ResultCode Aio320raInitialize() {
            ResultCode result;
            if (i2c == null) {
                result = I2C_Open();
                if (result != ResultCode.OK)
                    return result;
            }
            if (aio320ra == null)
                aio320ra = new AIO_32_0(i2c, Convert.ToByte(aio320raAdcAddressTextBox.Text, 16), Convert.ToByte(aio320raMuxAddressTextBox.Text, 16));
            result = aio320ra.Initialize();
            if (result != ResultCode.OK) {
                MessageBox.Show(result.ToString());
            } else {
                Println("AIO-32/0RA-IRC: Initialize OK");
            }
            return result;
        }

        private void aio320raReadButton_Click(object sender, EventArgs e) {
            ResultCode result;
            if ((aio320ra == null) || !aio320ra.IsInitialized) {
                result = Aio320raInitialize();
                if (result != ResultCode.OK)
                    return;
            }
            float[] volt = new float[32];
            result = aio320ra.ReadVoltage(0, volt, 32);
            if (result != ResultCode.OK) {
                MessageBox.Show(result.ToString());
                return;
            }
            Println("AIO-32/0RA-IRC: Read");
            for (int ch = 0; ch < 32; ch++) {
                Println(" AIN" + ch.ToString() + ": " + volt[ch].ToString("F3") + "V");
            }
        }

    }
}
