using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Sdt.RMS
{
    public class RmsAdapter
    {
        const string dllPath = @"C:\Program Files\Cliente 2.1 de Active Directory Rights Management Services\msipc.Dll";
        [DllImportAttribute(dllPath, EntryPoint = "IpcfGetSerializedLicenseFromFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 IpcfGetSerializedLicenseFromFile(IntPtr wszInputFilePath, [Out] out IntPtr ppvLicense);
        [DllImportAttribute(dllPath, EntryPoint = "IpcFreeMemory", CallingConvention = CallingConvention.Cdecl)]
        public static extern void IpcFreeMemory( IntPtr pointer);
        [DllImportAttribute(dllPath, EntryPoint = "IpcGetErrorMessageText", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 IpcGetErrorMessageText(UInt32 hrError, Int32 dwLanguageId, [Out] out IntPtr ppwszErrorMessageText);

        private static IntPtr GetIntPtrFromString (string str)
        {
            return Marshal.StringToHGlobalUni(str);
        }
        private static String GetStringFromIntPtr(IntPtr ptr)
        {
            return Marshal.PtrToStringUni(ptr);
        }

        public static UInt32 GetSerializedLicenseFromFile (string fileName)
        {
            IntPtr buffer = new IntPtr();
            IntPtr ptrFileName = GetIntPtrFromString(fileName);
            string muestra = GetStringFromIntPtr(ptrFileName);
            UInt32 result =  IpcfGetSerializedLicenseFromFile(ptrFileName, out buffer);
            IpcFreeMemory(buffer);
            return result;
        }
        public static string GetErrorMessage (UInt32 errorCode)
        {
            IntPtr buffer = new IntPtr();
            IpcGetErrorMessageText(errorCode,1033,out buffer);
            string errorMessage = GetStringFromIntPtr(buffer);
            if (errorMessage == null)
                errorMessage = new Win32Exception((int) errorCode).Message;
                return errorMessage; 
        }
    }
}
