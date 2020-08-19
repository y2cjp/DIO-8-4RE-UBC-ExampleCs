// MIT License
//
// Copyright (c) Y2 Corporation

using Y2.Ft4222.Core;

namespace Dio84ReUbc.CoreSample
{
    public class Ssd1306 : Ft4222I2cSlaveDevice
    {
        protected const int DrawingAreaWidth = 128;

        private readonly int _displayAreaWidth;
        private readonly int _displayAreaHeight;

        public Ssd1306(IFt4222I2cMaster i2c, byte slaveAddress, int displayAreaWidth, int displayAreaHeight) :
            base(i2c, slaveAddress)
        {
            _displayAreaWidth = displayAreaWidth;
            _displayAreaHeight = displayAreaHeight;
        }

        public enum ScrollInterval : byte
        {
            Frame5,
            Frame64,
            Frame128,
            Frame256,
            Frame3,
            Frame4,
            Frame25,
            Frame2
        }

        public enum ScrollDirection : byte
        {
            Right,
            Left
        }

        private enum Command : byte
        {
            SetLowerColumn = 0x00,
            SetHigherColumn = 0x10,
            SetMemoryMode = 0x20,
            SetColumnAddress = 0x21,
            SetPageAddress = 0x22,
            RightHorizontalScroll = 0x26,
            LeftHorizontalScroll = 0x27,
            VerticalAndRightHorizontalScroll = 0x29,
            VerticalAndLeftHorizontalScroll = 0x2a,
            DeactivateScroll = 0x2e,
            ActivateScroll = 0x2f,
            SetDisplayStartLine = 0x40,
            SetContrast = 0x81,
            ChargePump = 0x8d,
            SetSegmentReMap = 0xa0,
            SetVerticalScrollArea = 0xa3,
            EntireDisplayOn = 0xa4,
            SetNormalDisplay = 0xa6,
            SetInverseDisplay = 0xa7,
            SetMultiplexRatio = 0xa8,
            DisplayOff = 0xae,
            DisplayOn = 0xaf,
            SetComScanDirectionInc = 0xc0,
            SetComScanDirectionDec = 0xc8,
            SetDisplayOffset = 0xd3,
            SetDisplayClockDivideRatio = 0xd5,
            SetPreChargePeriod = 0xd9,
            SetComPins = 0xda,
            SetVcomhDeselect = 0xdb
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            WriteCommand(Command.DisplayOff);
            WriteCommand(Command.SetDisplayClockDivideRatio);
            WriteCommand(0x80);
            WriteCommand(Command.SetMultiplexRatio);
            WriteCommand(_displayAreaHeight - 1);
            WriteCommand(Command.SetDisplayOffset);
            WriteCommand(0x00);
            WriteCommand(Command.SetDisplayStartLine);
            WriteCommand(Command.ChargePump);
            WriteCommand(0x14);  // Enable charge pump
            WriteCommand((byte)Command.SetSegmentReMap | 0x01);
            WriteCommand(Command.SetComScanDirectionDec);
            WriteCommand(Command.SetComPins);
            WriteCommand(0x12);
            WriteCommand(Command.SetContrast);
            WriteCommand(0xAF);
            WriteCommand(Command.SetPreChargePeriod);
            WriteCommand(0x25);
            WriteCommand(Command.SetVcomhDeselect);
            WriteCommand(0x20);
            WriteCommand(Command.EntireDisplayOn);
            WriteCommand(Command.SetNormalDisplay);
            ClearDisplay();
            WriteCommand(Command.DisplayOn);
        }

        /// <summary>
        /// 画像の描画
        /// </summary>
        public void DrawPicture(byte[] picture)
        {
            var index = 0;
            for (var v = 0; v < _displayAreaHeight / 8 + 1; v++)
            {
                SetPageAddress(v);
                SetColumnAddress(0);
                for (var h = 0; h < _displayAreaWidth; h++)
                {
                    if (index >= picture.Length)
                        break;
                    WriteData(picture[v * _displayAreaWidth + h]);
                    index++;
                }
            }
        }

        /// <summary>
        /// 画面クリア
        /// </summary>
        public void ClearDisplay()
        {
            for (var v = 0; v < _displayAreaHeight / 8 + 1; v++)
            {
                SetPageAddress(v);
                SetColumnAddress(0);
                for (var j = 0; j < DrawingAreaWidth; j++)
                {
                    WriteData(0);
                }
            }
        }

        /// <summary>
        /// 表示のオン・オフ
        /// </summary>
        public void VisibleDisplay(bool visible)
        {
            WriteCommand(visible ? Command.DisplayOn : Command.DisplayOff);
        }

        /// <summary>
        /// コントラストの設定
        /// </summary>
        public void SetContrast(int contrast)
        {
            WriteCommand(Command.SetContrast);
            WriteCommand(contrast);
        }

        /// <summary>
        /// スクロールの開始
        /// </summary>
        public void StartScroll(ScrollDirection direction, int startPage, int endPage, ScrollInterval interval)
        {
            WriteCommand((byte)Command.RightHorizontalScroll + (byte)direction);
            WriteCommand(0);
            WriteCommand(startPage);
            WriteCommand((byte)interval);
            WriteCommand(endPage);
            WriteCommand(0);
            WriteCommand(0xff);
            WriteCommand(Command.ActivateScroll);
        }

        /// <summary>
        /// スクロールの停止
        /// </summary>
        public void StopScroll()
        {
            WriteCommand(Command.DeactivateScroll);
        }

        /// <summary>
        /// 表示の反転
        /// </summary>
        public void SetInverseDisplay()
        {
            WriteCommand(Command.SetInverseDisplay);
        }

        /// <summary>
        /// 通常表示
        /// </summary>
        public void SetNormalDisplay()
        {
            WriteCommand(Command.SetNormalDisplay);
        }

        /// <summary>
        /// データの書き込み
        /// </summary>
        public void WriteData(int data)
        {
            byte[] writeBuffer = { 0x60, (byte)data };
            Write(writeBuffer);
        }

        protected void SetPageAddress(int address)
        {
            WriteCommand(0xb0 | address);
        }

        protected void SetColumnAddress(byte address)
        {
            address += 32;
            WriteCommand(0x10 | (address >> 4));
            WriteCommand(0x0f & address);
        }

        private void WriteCommand(int command)
        {
            WriteCommand((byte)command);
        }

        private void WriteCommand(Command command)
        {
            WriteCommand((byte)command);
        }

        private void WriteCommand(byte command)
        {
            byte[] writeBuffer = { 0, command };
            Write(writeBuffer);
        }
    }
}
