<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormVb
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.groupBox6 = New System.Windows.Forms.GroupBox()
        Me.mikroe1649TestButton = New System.Windows.Forms.Button()
        Me.adafruit2348TestButton = New System.Windows.Forms.Button()
        Me.dio84rdAddressTextBox = New System.Windows.Forms.TextBox()
        Me.dio84rdReadTextBox = New System.Windows.Forms.TextBox()
        Me.dio84rdInitializeButton = New System.Windows.Forms.Button()
        Me.dio84rdReadButton = New System.Windows.Forms.Button()
        Me.label2 = New System.Windows.Forms.Label()
        Me.dio84rdWriteTextBox = New System.Windows.Forms.TextBox()
        Me.dio84rdWriteButton = New System.Windows.Forms.Button()
        Me.groupBox5 = New System.Windows.Forms.GroupBox()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.groupBox4 = New System.Windows.Forms.GroupBox()
        Me.label5 = New System.Windows.Forms.Label()
        Me.aio320raMuxAddressTextBox = New System.Windows.Forms.TextBox()
        Me.aio320raReadButton = New System.Windows.Forms.Button()
        Me.label4 = New System.Windows.Forms.Label()
        Me.aio320raAdcAddressTextBox = New System.Windows.Forms.TextBox()
        Me.dio016rcWriteButton = New System.Windows.Forms.Button()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.dio84reAddressTextBox = New System.Windows.Forms.TextBox()
        Me.dio84reReadTextBox = New System.Windows.Forms.TextBox()
        Me.dio84reInitializeButton = New System.Windows.Forms.Button()
        Me.dio84reReadButton = New System.Windows.Forms.Button()
        Me.label1 = New System.Windows.Forms.Label()
        Me.dio84reWriteTextBox = New System.Windows.Forms.TextBox()
        Me.dio84reWriteButton = New System.Windows.Forms.Button()
        Me.groupBox3 = New System.Windows.Forms.GroupBox()
        Me.dio016rcInitializeButton = New System.Windows.Forms.Button()
        Me.label3 = New System.Windows.Forms.Label()
        Me.dio016rcAddressTextBox = New System.Windows.Forms.TextBox()
        Me.dio016rcWriteTextBox = New System.Windows.Forms.TextBox()
        Me.printTextBox = New System.Windows.Forms.TextBox()
        Me.groupBox6.SuspendLayout()
        Me.groupBox5.SuspendLayout()
        Me.groupBox2.SuspendLayout()
        Me.groupBox4.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        Me.groupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'groupBox6
        '
        Me.groupBox6.Controls.Add(Me.mikroe1649TestButton)
        Me.groupBox6.Location = New System.Drawing.Point(232, 392)
        Me.groupBox6.Name = "groupBox6"
        Me.groupBox6.Size = New System.Drawing.Size(152, 56)
        Me.groupBox6.TabIndex = 26
        Me.groupBox6.TabStop = False
        Me.groupBox6.Text = "OLED W click [MikroE]"
        '
        'mikroe1649TestButton
        '
        Me.mikroe1649TestButton.Location = New System.Drawing.Point(16, 24)
        Me.mikroe1649TestButton.Name = "mikroe1649TestButton"
        Me.mikroe1649TestButton.Size = New System.Drawing.Size(128, 24)
        Me.mikroe1649TestButton.TabIndex = 1
        Me.mikroe1649TestButton.Text = "ディスプレイ表示テスト"
        Me.mikroe1649TestButton.UseVisualStyleBackColor = True
        '
        'adafruit2348TestButton
        '
        Me.adafruit2348TestButton.Location = New System.Drawing.Point(16, 24)
        Me.adafruit2348TestButton.Name = "adafruit2348TestButton"
        Me.adafruit2348TestButton.Size = New System.Drawing.Size(128, 24)
        Me.adafruit2348TestButton.TabIndex = 1
        Me.adafruit2348TestButton.Text = "DCモーター制御テスト"
        Me.adafruit2348TestButton.UseVisualStyleBackColor = True
        '
        'dio84rdAddressTextBox
        '
        Me.dio84rdAddressTextBox.Enabled = False
        Me.dio84rdAddressTextBox.Location = New System.Drawing.Point(72, 24)
        Me.dio84rdAddressTextBox.Name = "dio84rdAddressTextBox"
        Me.dio84rdAddressTextBox.Size = New System.Drawing.Size(48, 19)
        Me.dio84rdAddressTextBox.TabIndex = 7
        Me.dio84rdAddressTextBox.Text = "0x23"
        '
        'dio84rdReadTextBox
        '
        Me.dio84rdReadTextBox.Enabled = False
        Me.dio84rdReadTextBox.Location = New System.Drawing.Point(16, 88)
        Me.dio84rdReadTextBox.Name = "dio84rdReadTextBox"
        Me.dio84rdReadTextBox.Size = New System.Drawing.Size(48, 19)
        Me.dio84rdReadTextBox.TabIndex = 9
        '
        'dio84rdInitializeButton
        '
        Me.dio84rdInitializeButton.Location = New System.Drawing.Point(72, 56)
        Me.dio84rdInitializeButton.Name = "dio84rdInitializeButton"
        Me.dio84rdInitializeButton.Size = New System.Drawing.Size(72, 24)
        Me.dio84rdInitializeButton.TabIndex = 8
        Me.dio84rdInitializeButton.Text = "Initialize"
        Me.dio84rdInitializeButton.UseVisualStyleBackColor = True
        '
        'dio84rdReadButton
        '
        Me.dio84rdReadButton.Location = New System.Drawing.Point(72, 88)
        Me.dio84rdReadButton.Name = "dio84rdReadButton"
        Me.dio84rdReadButton.Size = New System.Drawing.Size(72, 24)
        Me.dio84rdReadButton.TabIndex = 10
        Me.dio84rdReadButton.Text = "Read"
        Me.dio84rdReadButton.UseVisualStyleBackColor = True
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(16, 32)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(49, 12)
        Me.label2.TabIndex = 5
        Me.label2.Text = "Address:"
        '
        'dio84rdWriteTextBox
        '
        Me.dio84rdWriteTextBox.Location = New System.Drawing.Point(16, 120)
        Me.dio84rdWriteTextBox.Name = "dio84rdWriteTextBox"
        Me.dio84rdWriteTextBox.Size = New System.Drawing.Size(48, 19)
        Me.dio84rdWriteTextBox.TabIndex = 11
        Me.dio84rdWriteTextBox.Text = "0x00"
        '
        'dio84rdWriteButton
        '
        Me.dio84rdWriteButton.Location = New System.Drawing.Point(72, 120)
        Me.dio84rdWriteButton.Name = "dio84rdWriteButton"
        Me.dio84rdWriteButton.Size = New System.Drawing.Size(72, 24)
        Me.dio84rdWriteButton.TabIndex = 12
        Me.dio84rdWriteButton.Text = "Write"
        Me.dio84rdWriteButton.UseVisualStyleBackColor = True
        '
        'groupBox5
        '
        Me.groupBox5.Controls.Add(Me.adafruit2348TestButton)
        Me.groupBox5.Location = New System.Drawing.Point(232, 328)
        Me.groupBox5.Name = "groupBox5"
        Me.groupBox5.Size = New System.Drawing.Size(152, 56)
        Me.groupBox5.TabIndex = 25
        Me.groupBox5.TabStop = False
        Me.groupBox5.Text = "DC Motor HAT [Adafruit]"
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.dio84rdAddressTextBox)
        Me.groupBox2.Controls.Add(Me.dio84rdReadTextBox)
        Me.groupBox2.Controls.Add(Me.dio84rdInitializeButton)
        Me.groupBox2.Controls.Add(Me.dio84rdReadButton)
        Me.groupBox2.Controls.Add(Me.label2)
        Me.groupBox2.Controls.Add(Me.dio84rdWriteTextBox)
        Me.groupBox2.Controls.Add(Me.dio84rdWriteButton)
        Me.groupBox2.Location = New System.Drawing.Point(392, 8)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(152, 152)
        Me.groupBox2.TabIndex = 22
        Me.groupBox2.TabStop = False
        Me.groupBox2.Text = "DIO-8/4RD-IRC"
        '
        'groupBox4
        '
        Me.groupBox4.Controls.Add(Me.label5)
        Me.groupBox4.Controls.Add(Me.aio320raMuxAddressTextBox)
        Me.groupBox4.Controls.Add(Me.aio320raReadButton)
        Me.groupBox4.Controls.Add(Me.label4)
        Me.groupBox4.Controls.Add(Me.aio320raAdcAddressTextBox)
        Me.groupBox4.Location = New System.Drawing.Point(392, 296)
        Me.groupBox4.Name = "groupBox4"
        Me.groupBox4.Size = New System.Drawing.Size(152, 112)
        Me.groupBox4.TabIndex = 24
        Me.groupBox4.TabStop = False
        Me.groupBox4.Text = "AIO-32/0RA-IRC"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(8, 56)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(77, 12)
        Me.label5.TabIndex = 18
        Me.label5.Text = "MUX Address:"
        '
        'aio320raMuxAddressTextBox
        '
        Me.aio320raMuxAddressTextBox.Enabled = False
        Me.aio320raMuxAddressTextBox.Location = New System.Drawing.Point(88, 48)
        Me.aio320raMuxAddressTextBox.Name = "aio320raMuxAddressTextBox"
        Me.aio320raMuxAddressTextBox.Size = New System.Drawing.Size(48, 19)
        Me.aio320raMuxAddressTextBox.TabIndex = 18
        Me.aio320raMuxAddressTextBox.Text = "0x3e"
        '
        'aio320raReadButton
        '
        Me.aio320raReadButton.Location = New System.Drawing.Point(72, 80)
        Me.aio320raReadButton.Name = "aio320raReadButton"
        Me.aio320raReadButton.Size = New System.Drawing.Size(72, 24)
        Me.aio320raReadButton.TabIndex = 19
        Me.aio320raReadButton.Text = "Read"
        Me.aio320raReadButton.UseVisualStyleBackColor = True
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(8, 32)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(77, 12)
        Me.label4.TabIndex = 14
        Me.label4.Text = "ADC Address:"
        '
        'aio320raAdcAddressTextBox
        '
        Me.aio320raAdcAddressTextBox.Enabled = False
        Me.aio320raAdcAddressTextBox.Location = New System.Drawing.Point(88, 24)
        Me.aio320raAdcAddressTextBox.Name = "aio320raAdcAddressTextBox"
        Me.aio320raAdcAddressTextBox.Size = New System.Drawing.Size(48, 19)
        Me.aio320raAdcAddressTextBox.TabIndex = 17
        Me.aio320raAdcAddressTextBox.Text = "0x49"
        '
        'dio016rcWriteButton
        '
        Me.dio016rcWriteButton.Location = New System.Drawing.Point(72, 88)
        Me.dio016rcWriteButton.Name = "dio016rcWriteButton"
        Me.dio016rcWriteButton.Size = New System.Drawing.Size(72, 24)
        Me.dio016rcWriteButton.TabIndex = 16
        Me.dio016rcWriteButton.Text = "Write"
        Me.dio016rcWriteButton.UseVisualStyleBackColor = True
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.dio84reAddressTextBox)
        Me.groupBox1.Controls.Add(Me.dio84reReadTextBox)
        Me.groupBox1.Controls.Add(Me.dio84reInitializeButton)
        Me.groupBox1.Controls.Add(Me.dio84reReadButton)
        Me.groupBox1.Controls.Add(Me.label1)
        Me.groupBox1.Controls.Add(Me.dio84reWriteTextBox)
        Me.groupBox1.Controls.Add(Me.dio84reWriteButton)
        Me.groupBox1.Location = New System.Drawing.Point(232, 8)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(152, 152)
        Me.groupBox1.TabIndex = 21
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "DIO-8/4RE-UBC"
        '
        'dio84reAddressTextBox
        '
        Me.dio84reAddressTextBox.Enabled = False
        Me.dio84reAddressTextBox.Location = New System.Drawing.Point(72, 24)
        Me.dio84reAddressTextBox.Name = "dio84reAddressTextBox"
        Me.dio84reAddressTextBox.Size = New System.Drawing.Size(48, 19)
        Me.dio84reAddressTextBox.TabIndex = 1
        Me.dio84reAddressTextBox.Text = "0x26"
        '
        'dio84reReadTextBox
        '
        Me.dio84reReadTextBox.Enabled = False
        Me.dio84reReadTextBox.Location = New System.Drawing.Point(16, 88)
        Me.dio84reReadTextBox.Name = "dio84reReadTextBox"
        Me.dio84reReadTextBox.Size = New System.Drawing.Size(48, 19)
        Me.dio84reReadTextBox.TabIndex = 3
        '
        'dio84reInitializeButton
        '
        Me.dio84reInitializeButton.Location = New System.Drawing.Point(72, 56)
        Me.dio84reInitializeButton.Name = "dio84reInitializeButton"
        Me.dio84reInitializeButton.Size = New System.Drawing.Size(72, 24)
        Me.dio84reInitializeButton.TabIndex = 2
        Me.dio84reInitializeButton.Text = "Initialize"
        Me.dio84reInitializeButton.UseVisualStyleBackColor = True
        '
        'dio84reReadButton
        '
        Me.dio84reReadButton.Location = New System.Drawing.Point(72, 88)
        Me.dio84reReadButton.Name = "dio84reReadButton"
        Me.dio84reReadButton.Size = New System.Drawing.Size(72, 24)
        Me.dio84reReadButton.TabIndex = 4
        Me.dio84reReadButton.Text = "Read"
        Me.dio84reReadButton.UseVisualStyleBackColor = True
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(16, 32)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(49, 12)
        Me.label1.TabIndex = 5
        Me.label1.Text = "Address:"
        '
        'dio84reWriteTextBox
        '
        Me.dio84reWriteTextBox.Location = New System.Drawing.Point(16, 120)
        Me.dio84reWriteTextBox.Name = "dio84reWriteTextBox"
        Me.dio84reWriteTextBox.Size = New System.Drawing.Size(48, 19)
        Me.dio84reWriteTextBox.TabIndex = 5
        Me.dio84reWriteTextBox.Text = "0x00"
        '
        'dio84reWriteButton
        '
        Me.dio84reWriteButton.Location = New System.Drawing.Point(72, 120)
        Me.dio84reWriteButton.Name = "dio84reWriteButton"
        Me.dio84reWriteButton.Size = New System.Drawing.Size(72, 24)
        Me.dio84reWriteButton.TabIndex = 6
        Me.dio84reWriteButton.Text = "Write"
        Me.dio84reWriteButton.UseVisualStyleBackColor = True
        '
        'groupBox3
        '
        Me.groupBox3.Controls.Add(Me.dio016rcInitializeButton)
        Me.groupBox3.Controls.Add(Me.label3)
        Me.groupBox3.Controls.Add(Me.dio016rcAddressTextBox)
        Me.groupBox3.Controls.Add(Me.dio016rcWriteTextBox)
        Me.groupBox3.Controls.Add(Me.dio016rcWriteButton)
        Me.groupBox3.Location = New System.Drawing.Point(392, 168)
        Me.groupBox3.Name = "groupBox3"
        Me.groupBox3.Size = New System.Drawing.Size(152, 120)
        Me.groupBox3.TabIndex = 23
        Me.groupBox3.TabStop = False
        Me.groupBox3.Text = "DIO-0/16RC-IRC"
        '
        'dio016rcInitializeButton
        '
        Me.dio016rcInitializeButton.Location = New System.Drawing.Point(72, 56)
        Me.dio016rcInitializeButton.Name = "dio016rcInitializeButton"
        Me.dio016rcInitializeButton.Size = New System.Drawing.Size(72, 24)
        Me.dio016rcInitializeButton.TabIndex = 14
        Me.dio016rcInitializeButton.Text = "Initialize"
        Me.dio016rcInitializeButton.UseVisualStyleBackColor = True
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(16, 32)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(49, 12)
        Me.label3.TabIndex = 5
        Me.label3.Text = "Address:"
        '
        'dio016rcAddressTextBox
        '
        Me.dio016rcAddressTextBox.Enabled = False
        Me.dio016rcAddressTextBox.Location = New System.Drawing.Point(72, 24)
        Me.dio016rcAddressTextBox.Name = "dio016rcAddressTextBox"
        Me.dio016rcAddressTextBox.Size = New System.Drawing.Size(48, 19)
        Me.dio016rcAddressTextBox.TabIndex = 13
        Me.dio016rcAddressTextBox.Text = "0x24"
        '
        'dio016rcWriteTextBox
        '
        Me.dio016rcWriteTextBox.Location = New System.Drawing.Point(16, 88)
        Me.dio016rcWriteTextBox.Name = "dio016rcWriteTextBox"
        Me.dio016rcWriteTextBox.Size = New System.Drawing.Size(48, 19)
        Me.dio016rcWriteTextBox.TabIndex = 15
        Me.dio016rcWriteTextBox.Text = "0x0000"
        '
        'printTextBox
        '
        Me.printTextBox.Location = New System.Drawing.Point(8, 8)
        Me.printTextBox.Multiline = True
        Me.printTextBox.Name = "printTextBox"
        Me.printTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.printTextBox.Size = New System.Drawing.Size(208, 440)
        Me.printTextBox.TabIndex = 27
        '
        'FormVb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(553, 459)
        Me.Controls.Add(Me.groupBox6)
        Me.Controls.Add(Me.groupBox5)
        Me.Controls.Add(Me.groupBox2)
        Me.Controls.Add(Me.groupBox4)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.groupBox3)
        Me.Controls.Add(Me.printTextBox)
        Me.Name = "FormVb"
        Me.Text = "DIO-8/4RE-UBC Sample (VB)"
        Me.groupBox6.ResumeLayout(False)
        Me.groupBox5.ResumeLayout(False)
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox2.PerformLayout()
        Me.groupBox4.ResumeLayout(False)
        Me.groupBox4.PerformLayout()
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.groupBox3.ResumeLayout(False)
        Me.groupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents groupBox6 As GroupBox
    Private WithEvents mikroe1649TestButton As Button
    Private WithEvents adafruit2348TestButton As Button
    Private WithEvents dio84rdAddressTextBox As TextBox
    Private WithEvents dio84rdReadTextBox As TextBox
    Private WithEvents dio84rdInitializeButton As Button
    Private WithEvents dio84rdReadButton As Button
    Private WithEvents label2 As Label
    Private WithEvents dio84rdWriteTextBox As TextBox
    Private WithEvents dio84rdWriteButton As Button
    Private WithEvents groupBox5 As GroupBox
    Private WithEvents groupBox2 As GroupBox
    Private WithEvents groupBox4 As GroupBox
    Private WithEvents label5 As Label
    Private WithEvents aio320raMuxAddressTextBox As TextBox
    Private WithEvents aio320raReadButton As Button
    Private WithEvents label4 As Label
    Private WithEvents aio320raAdcAddressTextBox As TextBox
    Private WithEvents dio016rcWriteButton As Button
    Private WithEvents groupBox1 As GroupBox
    Private WithEvents dio84reAddressTextBox As TextBox
    Private WithEvents dio84reReadTextBox As TextBox
    Private WithEvents dio84reInitializeButton As Button
    Private WithEvents dio84reReadButton As Button
    Private WithEvents label1 As Label
    Private WithEvents dio84reWriteTextBox As TextBox
    Private WithEvents dio84reWriteButton As Button
    Private WithEvents groupBox3 As GroupBox
    Private WithEvents dio016rcInitializeButton As Button
    Private WithEvents label3 As Label
    Private WithEvents dio016rcAddressTextBox As TextBox
    Private WithEvents dio016rcWriteTextBox As TextBox
    Private WithEvents printTextBox As TextBox
End Class
