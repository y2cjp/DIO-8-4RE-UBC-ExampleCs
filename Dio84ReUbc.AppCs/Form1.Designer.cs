namespace Dio84ReUbc.AppCs
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                i2C.Dispose();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.dio84reInitializeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dio84reAddressTextBox = new System.Windows.Forms.TextBox();
            this.dio84reReadTextBox = new System.Windows.Forms.TextBox();
            this.dio84reReadButton = new System.Windows.Forms.Button();
            this.dio84reWriteButton = new System.Windows.Forms.Button();
            this.dio84reWriteTextBox = new System.Windows.Forms.TextBox();
            this.printTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dio016rcInitializeButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dio016rcAddressTextBox = new System.Windows.Forms.TextBox();
            this.dio016rcWriteTextBox = new System.Windows.Forms.TextBox();
            this.dio016rcWriteButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.aio320raMuxAddressTextBox = new System.Windows.Forms.TextBox();
            this.aio320raReadButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.aio320raAdcAddressTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dio84rdAddressTextBox = new System.Windows.Forms.TextBox();
            this.dio84rdReadTextBox = new System.Windows.Forms.TextBox();
            this.dio84rdInitializeButton = new System.Windows.Forms.Button();
            this.dio84rdReadButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dio84rdWriteTextBox = new System.Windows.Forms.TextBox();
            this.dio84rdWriteButton = new System.Windows.Forms.Button();
            this.adafruit2348TestButton = new System.Windows.Forms.Button();
            this.mikroe1649TestButton = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // dio84reInitializeButton
            // 
            this.dio84reInitializeButton.Location = new System.Drawing.Point(72, 56);
            this.dio84reInitializeButton.Name = "dio84reInitializeButton";
            this.dio84reInitializeButton.Size = new System.Drawing.Size(72, 24);
            this.dio84reInitializeButton.TabIndex = 2;
            this.dio84reInitializeButton.Text = "Initialize";
            this.dio84reInitializeButton.UseVisualStyleBackColor = true;
            this.dio84reInitializeButton.Click += new System.EventHandler(this.dio84reInitializeButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Address:";
            // 
            // dio84reAddressTextBox
            // 
            this.dio84reAddressTextBox.Enabled = false;
            this.dio84reAddressTextBox.Location = new System.Drawing.Point(72, 24);
            this.dio84reAddressTextBox.Name = "dio84reAddressTextBox";
            this.dio84reAddressTextBox.Size = new System.Drawing.Size(48, 19);
            this.dio84reAddressTextBox.TabIndex = 1;
            this.dio84reAddressTextBox.Text = "0x26";
            // 
            // dio84reReadTextBox
            // 
            this.dio84reReadTextBox.Enabled = false;
            this.dio84reReadTextBox.Location = new System.Drawing.Point(16, 88);
            this.dio84reReadTextBox.Name = "dio84reReadTextBox";
            this.dio84reReadTextBox.Size = new System.Drawing.Size(48, 19);
            this.dio84reReadTextBox.TabIndex = 3;
            // 
            // dio84reReadButton
            // 
            this.dio84reReadButton.Location = new System.Drawing.Point(72, 88);
            this.dio84reReadButton.Name = "dio84reReadButton";
            this.dio84reReadButton.Size = new System.Drawing.Size(72, 24);
            this.dio84reReadButton.TabIndex = 4;
            this.dio84reReadButton.Text = "Read";
            this.dio84reReadButton.UseVisualStyleBackColor = true;
            this.dio84reReadButton.Click += new System.EventHandler(this.dio84reReadButton_Click);
            // 
            // dio84reWriteButton
            // 
            this.dio84reWriteButton.Location = new System.Drawing.Point(72, 120);
            this.dio84reWriteButton.Name = "dio84reWriteButton";
            this.dio84reWriteButton.Size = new System.Drawing.Size(72, 24);
            this.dio84reWriteButton.TabIndex = 6;
            this.dio84reWriteButton.Text = "Write";
            this.dio84reWriteButton.UseVisualStyleBackColor = true;
            this.dio84reWriteButton.Click += new System.EventHandler(this.dio84reWriteButton_Click);
            // 
            // dio84reWriteTextBox
            // 
            this.dio84reWriteTextBox.Location = new System.Drawing.Point(16, 120);
            this.dio84reWriteTextBox.Name = "dio84reWriteTextBox";
            this.dio84reWriteTextBox.Size = new System.Drawing.Size(48, 19);
            this.dio84reWriteTextBox.TabIndex = 5;
            this.dio84reWriteTextBox.Text = "0x00";
            // 
            // printTextBox
            // 
            this.printTextBox.Location = new System.Drawing.Point(8, 8);
            this.printTextBox.Multiline = true;
            this.printTextBox.Name = "printTextBox";
            this.printTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.printTextBox.Size = new System.Drawing.Size(208, 440);
            this.printTextBox.TabIndex = 20;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dio84reAddressTextBox);
            this.groupBox1.Controls.Add(this.dio84reReadTextBox);
            this.groupBox1.Controls.Add(this.dio84reInitializeButton);
            this.groupBox1.Controls.Add(this.dio84reReadButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dio84reWriteTextBox);
            this.groupBox1.Controls.Add(this.dio84reWriteButton);
            this.groupBox1.Location = new System.Drawing.Point(232, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 152);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DIO-8/4RE-UBC";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dio016rcInitializeButton);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.dio016rcAddressTextBox);
            this.groupBox3.Controls.Add(this.dio016rcWriteTextBox);
            this.groupBox3.Controls.Add(this.dio016rcWriteButton);
            this.groupBox3.Location = new System.Drawing.Point(392, 168);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(152, 120);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DIO-0/16RC-IRC";
            // 
            // dio016rcInitializeButton
            // 
            this.dio016rcInitializeButton.Location = new System.Drawing.Point(72, 56);
            this.dio016rcInitializeButton.Name = "dio016rcInitializeButton";
            this.dio016rcInitializeButton.Size = new System.Drawing.Size(72, 24);
            this.dio016rcInitializeButton.TabIndex = 14;
            this.dio016rcInitializeButton.Text = "Initialize";
            this.dio016rcInitializeButton.UseVisualStyleBackColor = true;
            this.dio016rcInitializeButton.Click += new System.EventHandler(this.dio016rcInitializeButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Address:";
            // 
            // dio016rcAddressTextBox
            // 
            this.dio016rcAddressTextBox.Enabled = false;
            this.dio016rcAddressTextBox.Location = new System.Drawing.Point(72, 24);
            this.dio016rcAddressTextBox.Name = "dio016rcAddressTextBox";
            this.dio016rcAddressTextBox.Size = new System.Drawing.Size(48, 19);
            this.dio016rcAddressTextBox.TabIndex = 13;
            this.dio016rcAddressTextBox.Text = "0x24";
            // 
            // dio016rcWriteTextBox
            // 
            this.dio016rcWriteTextBox.Location = new System.Drawing.Point(16, 88);
            this.dio016rcWriteTextBox.Name = "dio016rcWriteTextBox";
            this.dio016rcWriteTextBox.Size = new System.Drawing.Size(48, 19);
            this.dio016rcWriteTextBox.TabIndex = 15;
            this.dio016rcWriteTextBox.Text = "0x0000";
            // 
            // dio016rcWriteButton
            // 
            this.dio016rcWriteButton.Location = new System.Drawing.Point(72, 88);
            this.dio016rcWriteButton.Name = "dio016rcWriteButton";
            this.dio016rcWriteButton.Size = new System.Drawing.Size(72, 24);
            this.dio016rcWriteButton.TabIndex = 16;
            this.dio016rcWriteButton.Text = "Write";
            this.dio016rcWriteButton.UseVisualStyleBackColor = true;
            this.dio016rcWriteButton.Click += new System.EventHandler(this.dio016rcWriteButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.aio320raMuxAddressTextBox);
            this.groupBox4.Controls.Add(this.aio320raReadButton);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.aio320raAdcAddressTextBox);
            this.groupBox4.Location = new System.Drawing.Point(392, 296);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(152, 112);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "AIO-32/0RA-IRC";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "MUX Address:";
            // 
            // aio320raMuxAddressTextBox
            // 
            this.aio320raMuxAddressTextBox.Enabled = false;
            this.aio320raMuxAddressTextBox.Location = new System.Drawing.Point(88, 48);
            this.aio320raMuxAddressTextBox.Name = "aio320raMuxAddressTextBox";
            this.aio320raMuxAddressTextBox.Size = new System.Drawing.Size(48, 19);
            this.aio320raMuxAddressTextBox.TabIndex = 18;
            this.aio320raMuxAddressTextBox.Text = "0x3e";
            // 
            // aio320raReadButton
            // 
            this.aio320raReadButton.Location = new System.Drawing.Point(72, 80);
            this.aio320raReadButton.Name = "aio320raReadButton";
            this.aio320raReadButton.Size = new System.Drawing.Size(72, 24);
            this.aio320raReadButton.TabIndex = 19;
            this.aio320raReadButton.Text = "Read";
            this.aio320raReadButton.UseVisualStyleBackColor = true;
            this.aio320raReadButton.Click += new System.EventHandler(this.aio320raReadButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "ADC Address:";
            // 
            // aio320raAdcAddressTextBox
            // 
            this.aio320raAdcAddressTextBox.Enabled = false;
            this.aio320raAdcAddressTextBox.Location = new System.Drawing.Point(88, 24);
            this.aio320raAdcAddressTextBox.Name = "aio320raAdcAddressTextBox";
            this.aio320raAdcAddressTextBox.Size = new System.Drawing.Size(48, 19);
            this.aio320raAdcAddressTextBox.TabIndex = 17;
            this.aio320raAdcAddressTextBox.Text = "0x49";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dio84rdAddressTextBox);
            this.groupBox2.Controls.Add(this.dio84rdReadTextBox);
            this.groupBox2.Controls.Add(this.dio84rdInitializeButton);
            this.groupBox2.Controls.Add(this.dio84rdReadButton);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dio84rdWriteTextBox);
            this.groupBox2.Controls.Add(this.dio84rdWriteButton);
            this.groupBox2.Location = new System.Drawing.Point(392, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(152, 152);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DIO-8/4RD-IRC";
            // 
            // dio84rdAddressTextBox
            // 
            this.dio84rdAddressTextBox.Enabled = false;
            this.dio84rdAddressTextBox.Location = new System.Drawing.Point(72, 24);
            this.dio84rdAddressTextBox.Name = "dio84rdAddressTextBox";
            this.dio84rdAddressTextBox.Size = new System.Drawing.Size(48, 19);
            this.dio84rdAddressTextBox.TabIndex = 7;
            this.dio84rdAddressTextBox.Text = "0x23";
            // 
            // dio84rdReadTextBox
            // 
            this.dio84rdReadTextBox.Enabled = false;
            this.dio84rdReadTextBox.Location = new System.Drawing.Point(16, 88);
            this.dio84rdReadTextBox.Name = "dio84rdReadTextBox";
            this.dio84rdReadTextBox.Size = new System.Drawing.Size(48, 19);
            this.dio84rdReadTextBox.TabIndex = 9;
            // 
            // dio84rdInitializeButton
            // 
            this.dio84rdInitializeButton.Location = new System.Drawing.Point(72, 56);
            this.dio84rdInitializeButton.Name = "dio84rdInitializeButton";
            this.dio84rdInitializeButton.Size = new System.Drawing.Size(72, 24);
            this.dio84rdInitializeButton.TabIndex = 8;
            this.dio84rdInitializeButton.Text = "Initialize";
            this.dio84rdInitializeButton.UseVisualStyleBackColor = true;
            this.dio84rdInitializeButton.Click += new System.EventHandler(this.dio84rdInitializeButton_Click);
            // 
            // dio84rdReadButton
            // 
            this.dio84rdReadButton.Location = new System.Drawing.Point(72, 88);
            this.dio84rdReadButton.Name = "dio84rdReadButton";
            this.dio84rdReadButton.Size = new System.Drawing.Size(72, 24);
            this.dio84rdReadButton.TabIndex = 10;
            this.dio84rdReadButton.Text = "Read";
            this.dio84rdReadButton.UseVisualStyleBackColor = true;
            this.dio84rdReadButton.Click += new System.EventHandler(this.dio84rdReadButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Address:";
            // 
            // dio84rdWriteTextBox
            // 
            this.dio84rdWriteTextBox.Location = new System.Drawing.Point(16, 120);
            this.dio84rdWriteTextBox.Name = "dio84rdWriteTextBox";
            this.dio84rdWriteTextBox.Size = new System.Drawing.Size(48, 19);
            this.dio84rdWriteTextBox.TabIndex = 11;
            this.dio84rdWriteTextBox.Text = "0x00";
            // 
            // dio84rdWriteButton
            // 
            this.dio84rdWriteButton.Location = new System.Drawing.Point(72, 120);
            this.dio84rdWriteButton.Name = "dio84rdWriteButton";
            this.dio84rdWriteButton.Size = new System.Drawing.Size(72, 24);
            this.dio84rdWriteButton.TabIndex = 12;
            this.dio84rdWriteButton.Text = "Write";
            this.dio84rdWriteButton.UseVisualStyleBackColor = true;
            this.dio84rdWriteButton.Click += new System.EventHandler(this.dio84rdWriteButton_Click);
            // 
            // adafruit2348TestButton
            // 
            this.adafruit2348TestButton.Location = new System.Drawing.Point(16, 24);
            this.adafruit2348TestButton.Name = "adafruit2348TestButton";
            this.adafruit2348TestButton.Size = new System.Drawing.Size(128, 24);
            this.adafruit2348TestButton.TabIndex = 1;
            this.adafruit2348TestButton.Text = "DCモーター制御テスト";
            this.adafruit2348TestButton.UseVisualStyleBackColor = true;
            this.adafruit2348TestButton.Click += new System.EventHandler(this.adafruit2348TestButton_Click);
            // 
            // mikroe1649TestButton
            // 
            this.mikroe1649TestButton.Location = new System.Drawing.Point(16, 24);
            this.mikroe1649TestButton.Name = "mikroe1649TestButton";
            this.mikroe1649TestButton.Size = new System.Drawing.Size(128, 24);
            this.mikroe1649TestButton.TabIndex = 1;
            this.mikroe1649TestButton.Text = "ディスプレイ表示テスト";
            this.mikroe1649TestButton.UseVisualStyleBackColor = true;
            this.mikroe1649TestButton.Click += new System.EventHandler(this.mikroe1649TestButton_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.adafruit2348TestButton);
            this.groupBox5.Location = new System.Drawing.Point(232, 328);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(152, 56);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "DC Motor HAT [Adafruit]";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.mikroe1649TestButton);
            this.groupBox6.Location = new System.Drawing.Point(232, 392);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(152, 56);
            this.groupBox6.TabIndex = 11;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "OLED W click [MikroE]";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 457);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.printTextBox);
            this.Name = "Form1";
            this.Text = "DIO-8/4RE-UBC Sample";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button dio84reInitializeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox dio84reAddressTextBox;
        private System.Windows.Forms.TextBox dio84reReadTextBox;
        private System.Windows.Forms.Button dio84reReadButton;
        private System.Windows.Forms.Button dio84reWriteButton;
        private System.Windows.Forms.TextBox dio84reWriteTextBox;
        private System.Windows.Forms.TextBox printTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button dio016rcInitializeButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox dio016rcAddressTextBox;
        private System.Windows.Forms.TextBox dio016rcWriteTextBox;
        private System.Windows.Forms.Button dio016rcWriteButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox aio320raMuxAddressTextBox;
        private System.Windows.Forms.Button aio320raReadButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox aio320raAdcAddressTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox dio84rdAddressTextBox;
        private System.Windows.Forms.TextBox dio84rdReadTextBox;
        private System.Windows.Forms.Button dio84rdInitializeButton;
        private System.Windows.Forms.Button dio84rdReadButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dio84rdWriteTextBox;
        private System.Windows.Forms.Button dio84rdWriteButton;
        private System.Windows.Forms.Button adafruit2348TestButton;
        private System.Windows.Forms.Button mikroe1649TestButton;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
    }
}

