# DIO-8/4RE-UBC サンプル

[USB-I2C変換ボード（絶縁デジタル入出力付） DIO-8/4RE-UBC](https://www.y2c.co.jp/i2c-r/dio-8-4re-ubc.html) のサンプルです。  

### 注意事項

.NET Core対応にともない2020/8/19にプロジェクト名などを変更しました。  
Dio84ReUbc.Nfで始まるプロジェクトが以前のプロジェクトです。
古いバージョンが必要な場合は以前のバージョンを使用してください。

* [.NET Core対応前の最終バージョン（1.1.0）](https://github.com/y2cjp/DIO-8-4RE-UBC-ExampleCs/tree/1.1.0)

### フォルダ構成

プロジェクト名|フレームワーク|UI|概要|言語|注記
---|---|---|---|---|---
Dio84ReUbc.CoreSample|.NET  Core|-|サンプルアプリケーション<br>（UI非依存部分）|C#|※1
Dio84ReUbc.CoreSampleWpf|.NET  Core|WPF|サンプルアプリケーション|C#|※2
Dio84ReUbc.NfLibrary|.NET  Framework|-|ライブラリ|C#|※3
Dio84ReUbc.NfSample|.NET  Framework|-|サンプルアプリケーション<br>（UI非依存部分）|C#|-
Dio84ReUbc.NfSampleWinform|.NET  Framework|WinForms|サンプルアプリケーション|C#|-
Dio84ReUbc.NfSampleWinformVb|.NET  Framework|WinForms|サンプルアプリケーション|Visual Basic|-

※1

ライブラリは [Y2.Dio84ReUbc.Core](https://github.com/y2cjp/Y2.Dio84ReUbc.Core)（NuGetライブラリ）を使用しています。  
.NET Core（クロスプラットフォーム）に対応しています。  
Windows・Linux・MacOSで使用できます。   
各OS固有のライブラリ（デバイスドライバ含む）のインストール方法は、[Y2.Dio84ReUbc.Core](https://github.com/y2cjp/Y2.Dio84ReUbc.Core) のセットアップ方法を参照してください。

※2  

.NET Coreに対応しています。（UIはWindows専用）  
今後の機能拡張などはこちらを優先しておこないます。  
フレームワークに制約がないのであれば、こちらをお使いいただくのがおすすめです。

※3  

.NET Frameworkに対応したライブラリです。  
C#の古いバージョンや他の言語にも移植しやすいように、C#特有の機能（例外・継承・var・null演算子など）は極力使用しないようにしています。

### 使用方法

* [.NET Core](https://www.y2c.co.jp/i2c-r/dio-8-4re-ubc/netcore/)  

* [.NET Framework](https://www.y2c.co.jp/i2c-r/dio-8-4re-ubc/windows/)  

使用例

* [パソコンからアナログ値を測定する（アナログ入力32点を増設）](https://www.y2c.co.jp/i2c-r/aio-32-0ra-irc/windows/)

* [パソコンから絶縁デジタル入出力を制御する（絶縁デジタル入力8点・絶縁デジタル出力4点を増設）](https://www.y2c.co.jp/i2c-r/dio-8-4rd-irc/windows/)

* [パソコンからDCモータを制御する](https://www.y2c.co.jp/i2c-r/dio-8-4re-ubc/adafruit2348/)

* [パソコンからOLEDディスプレイを制御する](https://www.y2c.co.jp/i2c-r/dio-8-4re-ubc/mikroe1649/)