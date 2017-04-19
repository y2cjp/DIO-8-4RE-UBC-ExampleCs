// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Y2C Corporation">
//   Y2 Corporation
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;

namespace DIO_8_4RE_UBC
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
