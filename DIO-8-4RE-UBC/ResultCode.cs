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

namespace Y2C_I2C_Libraries {

    public enum ResultCode {
        OK,
        InvalidHandle,
        DeviceNotFound,
        DeviceNotOpened,
        IoError,
        InsufficientResources,
        InvalidParameter,
        InvalidBaudRate,
        DeviceNotOpenedForErase,
        DeviceNotOpenedForWrite,
        FailedToWriteDevice,
        EepromReadFailed,
        EepromWriteFailed,
        EepromEraseFailed,
        EepromNotPresent,
        EepromNotProgrammed,
        InvalidArgs,
        NotSupported,
        OtherError,
        DeviceListNotReady,

        DeviceNotSupported = 1000,  // FT_STATUS extending message
        ClkNotSupported,            // spi master do not support 80MHz/CLK_2
        VenderCmdNotSupported,
        IsNotSpiMode,
        IsNotI2cMode,
        IsNotSpiSingleMode,
        IsNotSpiMultiMode,
        WrongI2cAddr,
        InvaildFunction,
        InvalidPointer,
        ExceededMaxTransferSize,
        FailedToReadDevice,
        I2cNotSupportedInThisMode,
        GpioNotSupportedInThisMode,
        GpioExceededMaxPortnum,
        GpioWriteNotSupported,
        GpioPullupInvalidInInputmode,
        GpioPulldownInvalidInInputmode,
        GpioOpendrainInvalidInOutputmode,
        InterruptNotSupported,
        GpioInputNotSupported,
        EventNotSupported,
        FunNotSupport,

        I2cmControllerBusy = 2000,
        I2cmDataNack,
        I2cmAddressNack,
        I2cmArbLost,
        I2cmBusBusy,
        I2cmTimeout,

        InvalidChipVersion,
        InvalidDllVersion,
        FatalError,
    }

}
