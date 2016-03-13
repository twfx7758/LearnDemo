using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Learn.SDK.Attribute
{
    [type:StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class OSVERSIONINFO
    {
        public OSVERSIONINFO() {
            OSVersionInfoSize = (UInt32)Marshal.SizeOf(this);
        }

        public UInt32 OSVersionInfoSize = 0;
        public UInt32 MajorVersion = 0;
        public UInt32 MinorVersion = 0;
        public UInt32 BuildNumber = 0;
        public UInt32 PlatformId = 0;


        [field:MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public String CSDVersion = null;
    }

    public sealed class MyClass {
        [method:DllImport("Kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean GetVersion([In, Out] OSVERSIONINFO ver);
    }
}
