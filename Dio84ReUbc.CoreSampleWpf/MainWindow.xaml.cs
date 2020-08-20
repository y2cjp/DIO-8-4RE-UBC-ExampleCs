using System;
using System.Threading.Tasks;
using System.Windows;
using Dio84ReUbc.CoreSample;
using Y2.Dio84ReUbc.Core;

namespace Dio84ReUbc.CoreSampleWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDio84Re _dio84Re;    // DIO-8/4RE-UBC
        private IDio84 _dio84Rd;      // DIO-8/4RD-IRC
        private IDio016 _dio016Rc;    // DIO-0/16RC-IRC
        private IAio320 _aio320Ra;    // AIO-32/0RA-IRC

        public MainWindow()
        {
            InitializeComponent();

            Dio84ReAddress.Text = "0x26";
            Dio84ReWriteData.Text = "0x00";
            Dio84RdAddress.Text = "0x23";
            Dio84RdWriteData.Text = "0x00";
            Dio016RcAddress.Text = "0x24";
            Dio016RcWriteData.Text = "0x0000";
            Aio320RaAdcAddress.Text = "0x49";
            Aio320RaMuxAddress.Text = "0x3e";
        }

        private void AppendStatus(string message)
        {
            Status.AppendText($"{message}{Environment.NewLine}");
        }

        //// *****************************************************
        //// DIO-8/4RE-UBC
        //// *****************************************************

        private void Dio84ReInitialize()
        {
            _dio84Re = new Dio84Re(Convert.ToByte(Dio84ReAddress.Text, 16));
            _dio84Re.Initialize();
            AppendStatus("DIO-8/4RE-UBC: Initialize OK");
        }

        private void Dio84ReInitializeButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Dio84ReInitialize();
            }
            catch (Exception ex)
            {
                AppendStatus(ex.Message);
            }
        }

        private void Dio84ReReadButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_dio84Re?.IsInitialized != true)
                    Dio84ReInitialize();

                var value = _dio84Re.ReadPort();
                Dio84ReReadData.Text = $"0x{value:x2}";
                AppendStatus("DIO-8/4RE-UBC: Read OK");
            }
            catch (Exception ex)
            {
                AppendStatus(ex.Message);
            }
        }

        private void Dio84ReWriteButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_dio84Re?.IsInitialized != true)
                    Dio84ReInitialize();

                var value = Convert.ToByte(Dio84ReWriteData.Text, 16);
                Dio84ReWriteData.Text = $"0x{value:x2}";
                _dio84Re.WritePort(value);
                AppendStatus("DIO-8/4RE-UBC: Write OK");
            }
            catch (Exception ex)
            {
                AppendStatus(ex.Message);
            }
        }

        //// *****************************************************
        //// DIO-8/4RD-IRC
        //// *****************************************************

        private void Dio84RdInitialize()
        {
            if (_dio84Re?.IsInitialized != true)
                Dio84ReInitialize();

            _dio84Rd = new Dio84(_dio84Re, Convert.ToByte(Dio84RdAddress.Text, 16));
            _dio84Rd.Initialize();
            AppendStatus("DIO-8/4RD-IRC: Initialize OK");
        }

        private void Dio84RdInitializeButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Dio84RdInitialize();
            }
            catch (Exception ex)
            {
                AppendStatus(ex.Message);
            }
        }

        private void Dio84RdReadButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_dio84Rd?.IsInitialized != true)
                    Dio84RdInitialize();

                var value = _dio84Rd.ReadPort();
                Dio84RdReadData.Text = $"0x{value:x2}";
                AppendStatus("DIO-8/4RD-IRC: Read OK");
            }
            catch (Exception ex)
            {
                AppendStatus(ex.Message);
            }
        }

        private void Dio84RdWriteButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_dio84Rd?.IsInitialized != true)
                    Dio84RdInitialize();

                var value = Convert.ToByte(Dio84RdWriteData.Text, 16);
                Dio84RdWriteData.Text = $"0x{value:x2}";
                _dio84Rd.WritePort(value);
                AppendStatus("DIO-8/4RD-IRC: Write OK");
            }
            catch (Exception ex)
            {
                AppendStatus(ex.Message);
            }
        }

        //// *****************************************************
        //// DIO-0/16RC-IRC
        //// *****************************************************

        private void Dio016Initialize()
        {
            try
            {
                if (_dio84Re?.IsInitialized != true)
                    Dio84ReInitialize();

                _dio016Rc = new Dio016(_dio84Re, Convert.ToByte(Dio016RcAddress.Text, 16));
                _dio016Rc.Initialize();
                AppendStatus("DIO-0/16RC-IRC: Initialize OK");
            }
            catch (Exception ex)
            {
                AppendStatus(ex.Message);
            }
        }

        private void Dio016RcInitializeButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Dio016Initialize();
            }
            catch (Exception ex)
            {
                AppendStatus(ex.Message);
            }
        }

        private void Dio016RcWriteButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_dio016Rc?.IsInitialized != true)
                    Dio016Initialize();

                if (string.IsNullOrEmpty(Dio016RcWriteData.Text))
                    Dio016RcWriteData.Text = @"0";

                var value = Convert.ToUInt32(Dio016RcWriteData.Text, 16);
                Dio016RcWriteData.Text = $"0x{value:x4}";
                _dio016Rc.WriteAll(value);
                AppendStatus("DIO-0/16RC-IRC: Write OK");
            }
            catch (Exception ex)
            {
                AppendStatus(ex.Message);
            }
        }

        //// *****************************************************
        //// AIO-32/0RA-IRC
        //// *****************************************************

        private void Aio320RaInitialize()
        {
            if(_dio84Re?.IsInitialized != true)
                Dio84ReInitialize();

            var adc = Convert.ToByte(Aio320RaAdcAddress.Text, 16);
            var mux = Convert.ToByte(Aio320RaMuxAddress.Text, 16);
            _aio320Ra = new Aio320(_dio84Re, adc, mux);
            _aio320Ra.Initialize();
            AppendStatus("AIO-32/0RA-IRC: Initialize OK");
        }

        private void Aio320RaInitializeButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Aio320RaInitialize();
            }
            catch (Exception ex)
            {
                AppendStatus(ex.Message);
            }
        }

        private void Aio320RaReadButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_aio320Ra?.IsInitialized != true)
                    Aio320RaInitialize();

                var volt = _aio320Ra.ReadVoltage(0, 32);
                AppendStatus("AIO-32/0RA-IRC: Read");
                for (var ch = 0; ch < 32; ch++)
                    AppendStatus(" AIN" + ch + ": " + volt[ch].ToString("F3") + "V");
            }
            catch (Exception ex)
            {
                AppendStatus(ex.Message);
            }
        }

        /// <summary>
        /// DCモーター駆動テスト
        /// </summary>
        private async void Adafruit2348TestButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_dio84Re?.IsInitialized != true)
                    Dio84ReInitialize();

                AppendStatus("DC Motor Test: Start");
                await Task.Delay(10);
                var test = new TestAdafruit2348(_dio84Re);
                await test.TestDcMotor();
                AppendStatus("DC Motor Test: Finish");
            }
            catch (Exception ex)
            {
                AppendStatus(ex.Message);
            }
        }

        /// <summary>
        /// OLEDディスプレイ表示テスト
        /// </summary>
        private async void Mikroe1649TestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_dio84Re?.IsInitialized != true)
                    Dio84ReInitialize();

                _dio84Re.SetMikroBusResetPin(PinState.On);
                await Task.Delay(1);

                _dio84Re.SetMikroBusResetPin(PinState.Off);

                AppendStatus("OLED Module Reset: OK");
                await Task.Delay(100);

                AppendStatus("OLED Test: Start");
                var test = new TestMikroe1649(_dio84Re, 0x3c);
                test.TestPicture();
                AppendStatus("OLED Test: Finish");
            }
            catch (Exception ex)
            {
                AppendStatus(ex.Message);
            }
        }
    }
}
