using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Learn.SDK.Redis;
using Learn.SDK.Attribute;
using Learn.SDK;
using System.Threading;
using Learn.SDK.MPump;
using System.Diagnostics;

namespace Learn.ConsolePlat
{
    public class Params
    {
        public string Output { get; set; }
        public int CallCounter { get; set; }
        public int OriginalThread { get; set; }
    }
    class Program
    {
        private static int mCount = 0;
        private static StaSynchronizationContext mStaSyncContext = null;
        static void Main(string[] args)
        {
            //AttributeDemo();
            mStaSyncContext = new StaSynchronizationContext();
            for (int i = 0; i < 100; i++)
            {
                ThreadPool.QueueUserWorkItem(NonStaThread);
            }
            Console.WriteLine("Processing");
            Console.WriteLine("Press any key to dispose SyncContext");
            Console.ReadLine();
            mStaSyncContext.Dispose();

        }

        private static void NonStaThread(object state)
        {
            int id = Thread.CurrentThread.ManagedThreadId;

            for (int i = 0; i < 10; i++)
            {
                var param = new Params { OriginalThread = id, CallCounter = i };
                mStaSyncContext.Send(RunOnStaThread, param);
                Debug.Assert(param.Output == "Processed", "Unexpected behavior by STA thread");
            }
        }

        private static void RunOnStaThread(object state)
        {
            mCount++;
            Console.WriteLine(mCount);
            int id = Thread.CurrentThread.ManagedThreadId;
            var args = (Params)state;
            Trace.WriteLine("STA id " + id + " original thread " +
                            args.OriginalThread + " call count " + args.CallCounter);
            args.Output = "Processed";

        }

        //Attribute
        static void AttributeDemo()
        {
            var info = new OSVERSIONINFO();
            MyClass.GetVersion(info);
            Console.WriteLine("Version:{0}", info.CSDVersion);
        }

        //Redis示例
        static void RedisDemo()
        {
            RedisClient client = new RedisClient();
            client.RedisTest();

            Console.WriteLine("插入数据结束");
        }

        //Hash示例
        static void HashDemo()
        {
            HashTest test = new HashTest();
            Dictionary<int, string> dic = test.DictionaryTst();
            Console.WriteLine("=======Dictionary Test:=======");
            foreach (int key in dic.Keys)
            {
                Console.WriteLine("Key:{0},Value:{1}", key, dic[key]);
            }

            Console.WriteLine("=======HashTable Test=======");
            Hashtable ht = test.HashtableTest();
            foreach (int key in ht.Keys)
            {
                Console.WriteLine("Key:{0},Value:{1}", key, ht[key]);
            }


            Console.WriteLine("=======HashSet Test=======");
            HashSet<string> hs = test.HashSet();
            foreach (string key in hs)
            {
                Console.WriteLine("Key:{0}", key);
            }
        }

        //冒泡算法测试
        static void TestMethod()
        {
            int[] x = { 6, 2, 4, 1, 5, 9 };
            BaseAlgorithm.Bubble_Sort(x);
            foreach (var item in x)
            {
                Console.WriteLine(item);
            }
        }
    }
}