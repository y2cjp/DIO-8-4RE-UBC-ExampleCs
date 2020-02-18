Imports System.Threading
Imports DIO_8_4RE_UBC

Public Class FormVb

    Private i2C As I2CFt4222
    Private dio84Re As Dio84
    Private dio84Rd As Dio84
    Private dio016Rc As Dio016
    Private aio320Ra As Aio320

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub Print(ByVal text As String)
        If printTextBox Is Nothing OrElse text Is Nothing Then Return
        printTextBox.AppendText(text)
        Application.DoEvents()
    End Sub

    Public Sub Println(ByVal text As String)
        Print(text & Environment.NewLine)
    End Sub

    Private Function I2COpen() As ResultCode
        If i2C IsNot Nothing Then Return ResultCode.Ok
        i2C = New I2CFt4222()
        Dim result As ResultCode = i2C.Open()
        If result = ResultCode.Ok Then Return result
        MessageBox.Show(result.ToString())
        I2CClose()
        Return result
    End Function

    Private Sub I2CClose()
        If i2C Is Nothing Then Return
        Dim result As ResultCode = i2C.Close()
        If result <> ResultCode.Ok Then Println("I2C Close NG: " & result)
        i2C = Nothing
    End Sub

    Private Function Dio84ReInitialize() As ResultCode
        Dim result As ResultCode

        If i2C Is Nothing Then
            result = I2COpen()
            If result <> ResultCode.Ok Then Return result
        End If

        If dio84Re Is Nothing Then dio84Re = New Dio84(i2C, Convert.ToByte(dio84reAddressTextBox.Text, 16))
        result = dio84Re.Initialize()

        If result <> ResultCode.Ok Then
            MessageBox.Show(result.ToString())
        Else
            Println("DIO-8/4RE-UBC: Initialize OK")
        End If

        Return result
    End Function

    Private Sub dio84reInitializeButton_Click(sender As Object, e As EventArgs) Handles dio84reInitializeButton.Click
        Dio84ReInitialize()
    End Sub

    Private Sub dio84reReadButton_Click(sender As Object, e As EventArgs) Handles dio84reReadButton.Click
        Dim result As ResultCode

        If dio84Re Is Nothing OrElse Not dio84Re.IsInitialized Then
            result = Dio84ReInitialize()
            If result <> ResultCode.Ok Then Return
        End If

        Dim value As Byte
        result = dio84Re.ReadPort(value)

        If result <> ResultCode.Ok Then
            MessageBox.Show(result.ToString())
            Return
        End If

        dio84reReadTextBox.Text = "0x" & value.ToString("x2")
        Println("DIO-8/4RE-UBC: Read OK")
    End Sub

    Private Sub dio84reWriteButton_Click(sender As Object, e As EventArgs) Handles dio84reWriteButton.Click
        Dim result As ResultCode

        If dio84Re Is Nothing OrElse Not dio84Re.IsInitialized Then
            result = Dio84ReInitialize()
            If result <> ResultCode.Ok Then Return
        End If

        If String.IsNullOrEmpty(dio84reWriteTextBox.Text) Then dio84reWriteTextBox.Text = "0"
        Dim value As Byte = Convert.ToByte(dio84reWriteTextBox.Text, 16)
        dio84reWriteTextBox.Text = "0x" & value.ToString("x2")
        result = dio84Re.WritePort(value)

        If result <> ResultCode.Ok Then
            MessageBox.Show(result.ToString())
        Else
            Println("DIO-8/4RE-UBC: Write OK")
        End If
    End Sub

    Private Function Dio84RdInitialize() As ResultCode
        Dim result As ResultCode

        If i2C Is Nothing Then
            result = I2COpen()
            If result <> ResultCode.Ok Then Return result
        End If

        If dio84Rd Is Nothing Then dio84Rd = New Dio84(i2C, Convert.ToByte(dio84rdAddressTextBox.Text, 16))
        result = dio84Rd.Initialize()

        If result <> ResultCode.Ok Then
            MessageBox.Show(result.ToString())
        Else
            Println("DIO-8/4RD-IRC: Initialize OK")
        End If

        Return result
    End Function

    Private Sub dio84rdInitializeButton_Click(sender As Object, e As EventArgs) Handles dio84rdInitializeButton.Click
        Dio84RdInitialize()
    End Sub

    Private Sub dio84rdReadButton_Click(sender As Object, e As EventArgs) Handles dio84rdReadButton.Click
        Dim result As ResultCode

        If dio84Rd Is Nothing OrElse Not dio84Rd.IsInitialized Then
            result = Dio84RdInitialize()
            If result <> ResultCode.Ok Then Return
        End If

        Dim value As Byte
        result = dio84Rd.ReadPort(value)

        If result <> ResultCode.Ok Then
            MessageBox.Show(result.ToString())
            Return
        End If

        dio84rdReadTextBox.Text = "0x" & value.ToString("x2")
        Println("DIO-8/4RD-IRC: Read OK")
    End Sub

    Private Sub dio84rdWriteButton_Click(sender As Object, e As EventArgs) Handles dio84rdWriteButton.Click
        Dim result As ResultCode

        If dio84Rd Is Nothing OrElse Not dio84Rd.IsInitialized Then
            result = Dio84RdInitialize()
            If result <> ResultCode.Ok Then Return
        End If

        If String.IsNullOrEmpty(dio84rdWriteTextBox.Text) Then dio84rdWriteTextBox.Text = "0"
        Dim value As Byte = Convert.ToByte(dio84rdWriteTextBox.Text, 16)
        dio84rdWriteTextBox.Text = "0x" & value.ToString("x2")
        result = dio84Rd.WritePort(value)

        If result <> ResultCode.Ok Then
            MessageBox.Show(result.ToString())
        Else
            Println("DIO-8/4RD-IRC: Write OK")
        End If
    End Sub

    Private Function Dio016Initialize() As ResultCode
        Dim result As ResultCode

        If i2C Is Nothing Then
            result = I2COpen()
            If result <> ResultCode.Ok Then Return result
        End If

        If dio016Rc Is Nothing Then dio016Rc = New Dio016(i2C, Convert.ToByte(dio016rcAddressTextBox.Text, 16))
        result = dio016Rc.Initialize()

        If result <> ResultCode.Ok Then
            MessageBox.Show(result.ToString())
        Else
            Println("DIO-0/16RC-IRC: Initialize OK")
        End If

        Return result
    End Function

    Private Sub dio016rcInitializeButton_Click(sender As Object, e As EventArgs) Handles dio016rcInitializeButton.Click
        Dio016Initialize()
    End Sub

    Private Sub dio016rcWriteButton_Click(sender As Object, e As EventArgs) Handles dio016rcWriteButton.Click
        Dim result As ResultCode

        If dio016Rc Is Nothing OrElse Not dio016Rc.IsInitialized Then
            result = Dio016Initialize()
            If result <> ResultCode.Ok Then Return
        End If

        If String.IsNullOrEmpty(dio016rcWriteTextBox.Text) Then dio016rcWriteTextBox.Text = "0"
        Dim value As UInteger = Convert.ToUInt32(dio016rcWriteTextBox.Text, 16)
        dio016rcWriteTextBox.Text = "0x" & value.ToString("x4")
        result = dio016Rc.WriteAll(value)

        If result <> ResultCode.Ok Then
            MessageBox.Show(result.ToString())
        Else
            Println("DIO-0/16RC-IRC: Write OK")
        End If
    End Sub

    Private Function Aio320RaInitialize() As ResultCode
        Dim result As ResultCode

        If i2C Is Nothing Then
            result = I2COpen()
            If result <> ResultCode.Ok Then Return result
        End If

        If aio320Ra Is Nothing Then aio320Ra = New Aio320(i2C, Convert.ToByte(aio320raAdcAddressTextBox.Text, 16), Convert.ToByte(aio320raMuxAddressTextBox.Text, 16))
        result = aio320Ra.Initialize()

        If result <> ResultCode.Ok Then
            MessageBox.Show(result.ToString())
        Else
            Println("AIO-32/0RA-IRC: Initialize OK")
        End If

        Return result
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub aio320raReadButton_Click(sender As Object, e As EventArgs) Handles aio320raReadButton.Click
        Dim result As ResultCode

        If aio320Ra Is Nothing OrElse Not aio320Ra.IsInitialized Then
            result = Aio320RaInitialize()
            If result <> ResultCode.Ok Then Return
        End If

        Dim volt As Single() = New Single(31) {}
        result = aio320Ra.ReadVoltage(0, volt, 32)

        If result <> ResultCode.Ok Then
            MessageBox.Show(result.ToString())
            Return
        End If

        Println("AIO-32/0RA-IRC: Read")

        For ch As Integer = 0 To 32 - 1
            Println(" AIN" & ch & ": " & volt(ch).ToString("F3") & "V")
        Next
    End Sub

    Private Sub adafruit2348TestButton_Click(sender As Object, e As EventArgs) Handles adafruit2348TestButton.Click
        Dim result As ResultCode

        If i2C Is Nothing Then
            result = I2COpen()
            If result <> ResultCode.Ok Then Return
        End If

        Println("DC Motor Test: Start")
        Dim test As TestAdafruit2348 = New TestAdafruit2348(i2C)
        result = test.TestDcMotor()

        If result <> ResultCode.Ok Then
            MessageBox.Show(result.ToString())
        Else
            Println("DC Motor Test: Finish")
        End If
    End Sub

    Private Sub mikroe1649TestButton_Click(sender As Object, e As EventArgs) Handles mikroe1649TestButton.Click
        Dim result As ResultCode

        If dio84Re Is Nothing OrElse Not dio84Re.IsInitialized Then
            result = Dio84ReInitialize()
            If result <> ResultCode.Ok Then Return
        End If

        result = dio84Re.SetMikroBusResetPin(Dio84.PinState.[On])

        If result <> ResultCode.Ok Then
            MessageBox.Show(result.ToString())
            Return
        End If

        Thread.Sleep(1)
        result = dio84Re.SetMikroBusResetPin(Dio84.PinState.Off)

        If result <> ResultCode.Ok Then
            MessageBox.Show(result.ToString())
            Return
        End If

        Println("OLED Module Reset: OK")
        Thread.Sleep(100)
        Println("OLED Test: Start")
        Dim test As TestMikroe1649 = New TestMikroe1649(i2C, &H3C)
        result = test.TestPicture()

        If result <> ResultCode.Ok Then
            MessageBox.Show(result.ToString())
        Else
            Println("OLED Test: Finish")
        End If
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        I2CClose()
    End Sub

End Class