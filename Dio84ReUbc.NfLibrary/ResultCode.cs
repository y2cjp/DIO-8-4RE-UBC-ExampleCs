// MIT License
//
// Copyright (c) Y2 Corporation

namespace Dio84ReUbc.NfLibrary
{
    public enum ResultCode
    {
        Ok,
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
        IsNotI2CMode,
        IsNotSpiSingleMode,
        IsNotSpiMultiMode,
        WrongI2CAddr,
        InvalidFunction,
        InvalidPointer,
        ExceededMaxTransferSize,
        FailedToReadDevice,
        I2CNotSupportedInThisMode,
        GpioNotSupportedInThisMode,
        GpioExceededMaxPortNum,
        GpioWriteNotSupported,
        GpioPullupInvalidInInputmode,
        GpioPulldownInvalidInInputmode,
        GpioOpendrainInvalidInOutputmode,
        InterruptNotSupported,
        GpioInputNotSupported,
        EventNotSupported,
        FunNotSupport,

        I2CmControllerBusy = 2000,
        I2CmDataNack,
        I2CmAddressNack,
        I2CmArbLost,
        I2CmBusBusy,
        I2CmTimeout,

        InvalidChipVersion,
        InvalidDllVersion,
        FatalError
    }
}
