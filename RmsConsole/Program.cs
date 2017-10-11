using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sdt.RMS;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace RmsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            UInt32 errorCode = RmsAdapter.GetSerializedLicenseFromFile(@"C:\Temp\p.docx");
            string message = RmsAdapter.GetErrorMessage(errorCode);
            Console.WriteLine(message);
            Console.ReadLine();
        }
    }
}
