// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Ssd1306.cs" company="Y2 Corporation">
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

namespace DIO_8_4RE_UBC
{
    public class Ssd1306
    {
        protected const int DrawingAreaWidth = 128;

        private readonly I2CFt4222 i2C;
        private readonly byte slaveAddress;
        private readonly int displayAreaWidth;
        private readonly int displayAreaHeight;

        public Ssd1306(I2CFt4222 i2C, byte slaveAddress, int displayAreaWidth, int displayAreaHeight)
        {
            this.i2C = i2C;
            this.slaveAddress = slaveAddress;
            this.displayAreaWidth = displayAreaWidth;
            this.displayAreaHeight = displayAreaHeight;
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
        public ResultCode Initialize()
        {
            ResultCode result = WriteCommand(Command.DisplayOff);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(Command.SetDisplayClockDivideRatio);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(0x80);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(Command.SetMultiplexRatio);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(displayAreaHeight - 1);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(Command.SetDisplayOffset);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(0x00);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(Command.SetDisplayStartLine);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(Command.ChargePump);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(0x14);  // Enable charge pump
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand((byte)Command.SetSegmentReMap | 0x01);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(Command.SetComScanDirectionDec);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(Command.SetComPins);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(0x12);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(Command.SetContrast);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(0xAF);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(Command.SetPreChargePeriod);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(0x25);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(Command.SetVcomhDeselect);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(0x20);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(Command.EntireDisplayOn);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(Command.SetNormalDisplay);
            if (result != ResultCode.Ok)
                return result;
            result = ClearDisplay();
            if (result != ResultCode.Ok)
                return result;
            return WriteCommand(Command.DisplayOn);
        }

        /// <summary>
        /// 画像の描画
        /// </summary>
        public ResultCode DrawPicture(byte[] picture)
        {
            int index = 0;
            for (int v = 0; v < displayAreaHeight / 8 + 1; v++)
            {
                ResultCode result = SetPageAddress(v);
                if (result != ResultCode.Ok)
                    return result;
                result = SetColumnAddress(0);
                if (result != ResultCode.Ok)
                    return result;
                for (int h = 0; h < displayAreaWidth; h++)
                {
                    if (index >= picture.Length)
                        break;
                    result = WriteData(picture[v * displayAreaWidth + h]);
                    if (result != ResultCode.Ok)
                        return result;
                    index++;
                }
            }
            return ResultCode.Ok;
        }

        /// <summary>
        /// 画面クリア
        /// </summary>
        public ResultCode ClearDisplay()
        {
            for (int v = 0; v < displayAreaHeight / 8 + 1; v++)
            {
                ResultCode result = SetPageAddress(v);
                if (result != ResultCode.Ok)
                    return result;
                result = SetColumnAddress(0);
                if (result != ResultCode.Ok)
                    return result;
                for (int j = 0; j < DrawingAreaWidth; j++)
                {
                    result = WriteData(0);
                    if (result != ResultCode.Ok)
                        return result;
                }
            }
            return ResultCode.Ok;
        }

        /// <summary>
        /// 表示のオン・オフ
        /// </summary>
        public ResultCode VisibleDisplay(bool visible)
        {
            if (visible)
                return WriteCommand(Command.DisplayOn);
            return WriteCommand(Command.DisplayOff);
        }

        /// <summary>
        /// コントラストの設定
        /// </summary>
        public ResultCode SetContrast(int contrast)
        {
            ResultCode result = WriteCommand(Command.SetContrast);
            if (result != ResultCode.Ok)
                return result;
            return WriteCommand(contrast);
        }

        /// <summary>
        /// スクロールの開始
        /// </summary>
        public ResultCode StartScroll(ScrollDirection direction, int startPage, int endPage, ScrollInterval interval)
        {
            ResultCode result = WriteCommand((byte)Command.RightHorizontalScroll + (byte)direction);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(0);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(startPage);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand((byte)interval);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(endPage);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(0);
            if (result != ResultCode.Ok)
                return result;
            result = WriteCommand(0xff);
            if (result != ResultCode.Ok)
                return result;
            return WriteCommand(Command.ActivateScroll);
        }

        /// <summary>
        /// スクロールの停止
        /// </summary>
        public ResultCode StopScroll()
        {
            return WriteCommand(Command.DeactivateScroll);
        }

        /// <summary>
        /// 表示の反転
        /// </summary>
        public ResultCode SetInverseDisplay()
        {
            return WriteCommand(Command.SetInverseDisplay);
        }

        /// <summary>
        /// 通常表示
        /// </summary>
        public ResultCode SetNormalDisplay()
        {
            return WriteCommand(Command.SetNormalDisplay);
        }

        /// <summary>
        /// データの書き込み
        /// </summary>
        public ResultCode WriteData(int data)
        {
            byte[] writeBuffer = { 0x60, (byte)data };
            return i2C.Write(slaveAddress, ref writeBuffer[0], (ushort)writeBuffer.Length);
        }

        protected ResultCode SetPageAddress(int add)
        {
            add = 0xb0 | add;
            return WriteCommand(add);
        }

        protected ResultCode SetColumnAddress(byte add)
        {
            add += 32;
            ResultCode result = WriteCommand(0x10 | (add >> 4));
            if (result != ResultCode.Ok)
                return result;
            return WriteCommand(0x0f & add);
        }

        private ResultCode WriteCommand(byte command)
        {
            byte[] writeBuffer = { 0, command };
            return i2C.Write(slaveAddress, ref writeBuffer[0], (ushort)writeBuffer.Length);
        }

        private ResultCode WriteCommand(int command)
        {
            return WriteCommand((byte)command);
        }

        private ResultCode WriteCommand(Command command)
        {
            return WriteCommand((byte)command);
        }
    }
}
