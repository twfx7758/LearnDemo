using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Learn.CSharpForCLR.Learn
{
    public class LearnGCHandle
    {
        //public void Test1()
        //{
        //    GCHandle.Alloc()
        //}

        unsafe public static void Go()
        {
            //分配一系列立马变成垃圾的对象。
            for (Int32 i = 0; i < 10000; i++) new object();
            IntPtr originalMemoryAddress;
            Byte[] bytes = new Byte[1000];
            //获取Byte[]在内存中的地址
            fixed (Byte* pbytes = bytes) { originalMemoryAddress = (IntPtr)pbytes; };
            //强迫一次垃圾回收，垃圾对象会被回收，Byte[]可能被压缩
            GC.Collect();

            fixed (Byte* pbytes = bytes) { 
                Console.WriteLine("The Byte[] did{0} move during the GC",
                    (originalMemoryAddress == (IntPtr)pbytes) ? "not" : "");
            }
        }
    }
}
