// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Y2C Corporation">
//   Y2 Corporation
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;

namespace DIO_8_4RE_UBC
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
