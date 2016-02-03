using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learn.SDK.ThreadLock
{
    /// <summary>
    /// 经典的双检锁技术
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FirSingleton<T> where T : class
    {
        private static Object s_lock = new Object();

        public static T s_value = null;

        public static T GetSingleton() {
            if (s_value != null) return s_value;

            //在CLR中，对任何锁方法的调用都构成一个完整的内存栅栏
            //在栅栏之前【写入】的的任何变量都必须在栅栏这前完成。
            //在栅栏之后的任何变量的【读取】都必须在栅栏之后开始。
            Monitor.Enter(s_lock);
            if (s_value == null) {
                //编译器有可能在new一对象时，分配好内存，先把引用返回，再调用构造函数。
                T temp = default(T);
                Interlocked.Exchange(ref s_value, temp);
            }
            Monitor.Exit(s_lock);

            return s_value;
        }
    }

    /// <summary>
    /// Lazy与LazyInitializer两个类封闭了这两种单例模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SecSingleton<T> where T : class
    {
        public static T s_value = null;

        public static T GetSingleton()
        {
            if (s_value != null) return s_value;

            //编译器有可能在new一对象时，分配好内存，先把引用返回，再调用构造函数。
            T temp = default(T);
            Interlocked.CompareExchange(ref s_value, temp, null);

            return s_value;
        }
    }
}
