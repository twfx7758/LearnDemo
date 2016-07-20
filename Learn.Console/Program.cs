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

namespace Learn.ConsolePlat
{
    class Program
    {
        static void Main(string[] args)
        {
            //AttributeDemo();

            new NSynchronizationContext().TestMethod();

            Console.ReadLine();
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