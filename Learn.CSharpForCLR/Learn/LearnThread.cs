using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

namespace Learn.CSharpForCLR.Learn
{
    #region 工作者线程
    public class LearnThread
    {
        #region 异步调用使用 .Result，同步调用使用 .Result
        public static string ThreadMain()
        {
            Console.WriteLine("Thread.CurrentThread.ManagedThreadId1:" + Thread.CurrentThread.ManagedThreadId);
            var result = test();
            Console.WriteLine("Thread.CurrentThread.ManagedThreadId4:" + Thread.CurrentThread.ManagedThreadId);
            return result;
        }
        static string test()
        {
            Console.WriteLine("Thread.CurrentThread.ManagedThreadId2:" + Thread.CurrentThread.ManagedThreadId);
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("http://www.baidu.com/").Result;
                Console.WriteLine("Thread.CurrentThread.ManagedThreadId3:" + Thread.CurrentThread.ManagedThreadId);
                return response.Content.ReadAsStringAsync().Result;
            }
        }
        #endregion

        #region 异步调用使用 await，同步调用使用 Task.Run
        public static string ThreadMain2()
        {
            Console.WriteLine("Thread.CurrentThread.ManagedThreadId1:" + Thread.CurrentThread.ManagedThreadId);
            var result = Task.Run(() => test2()).Result;
            Console.WriteLine("Thread.CurrentThread.ManagedThreadId4:" + Thread.CurrentThread.ManagedThreadId);
            return result;
        }
        static async Task<string> test2()
        {
            Console.WriteLine("Thread.CurrentThread.ManagedThreadId2:" + Thread.CurrentThread.ManagedThreadId);
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.baidu.com/");
                Console.WriteLine("Thread.CurrentThread.ManagedThreadId3:" + Thread.CurrentThread.ManagedThreadId);
                return await response.Content.ReadAsStringAsync();
            }
        }
        #endregion

        #region 异步调用使用 await，同步调用使用 .Result
        public static string ThreadMain3()
        {
            Console.WriteLine("Thread.CurrentThread.ManagedThreadId1:" + Thread.CurrentThread.ManagedThreadId);
            var result = test3().Result;
            Console.WriteLine("Thread.CurrentThread.ManagedThreadId4:" + Thread.CurrentThread.ManagedThreadId);
            return result;
        }

        static async Task<string> test3()
        {
            Console.WriteLine("Thread.CurrentThread.ManagedThreadId2:" + Thread.CurrentThread.ManagedThreadId);
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.baidu.com/");
                Console.WriteLine("Thread.CurrentThread.ManagedThreadId3:" + Thread.CurrentThread.ManagedThreadId);
                return await response.Content.ReadAsStringAsync();
            }
        }
        #endregion

        #region 异步调用使用 Task.Run，同步调用使用 .Result
        public static string ThreadMain4()
        {
            Console.WriteLine("Thread.CurrentThread.ManagedThreadId1:" + Thread.CurrentThread.ManagedThreadId);
            var result = test4().Result;
            Console.WriteLine("Thread.CurrentThread.ManagedThreadId4:" + Thread.CurrentThread.ManagedThreadId);
            return result;
        }

        static async Task<string> test4()
        {
            Console.WriteLine("Thread.CurrentThread.ManagedThreadId2:" + Thread.CurrentThread.ManagedThreadId);
            return await Task.Run(() => {
                Thread.Sleep(10000);
                Console.WriteLine("Thread.CurrentThread.ManagedThreadId3:" + Thread.CurrentThread.ManagedThreadId);
                return "quwenbin";
            });
        }
        #endregion

        #region 线程的上下文流动
        /// <summary>
        /// 每个CLR都有一个线程池，线程池的线程执行完成以后不会立即销毁，而是在一段空闲时间后再由GC回收
        /// </summary>
        public static void TreadDemo()
        {
            CallContext.LogicalSetData("name", "wenbin");//设置流动的上下文
            Console.WriteLine("The Current ThreadId:{0}", Thread.CurrentThread.ManagedThreadId);
            ThreadPool.QueueUserWorkItem(TreadPoolTest);
            ExecutionContext.SuppressFlow();//阻止上下文流动
            ThreadPool.QueueUserWorkItem(TreadPoolTest);
            ExecutionContext.RestoreFlow();
        }

        static void TreadPoolTest(object obj)
        {
            object name = CallContext.LogicalGetData("name");
            Console.WriteLine("The Current ThreadId:{0}, PreThreadContext:{1}", Thread.CurrentThread.ManagedThreadId, name);
        }
        #endregion

        #region 协助式取消
        public static void Go()
        {
            Console.WriteLine("Go Runing Thread{0}", Thread.CurrentThread.ManagedThreadId);
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.Token.Register(() => Console.WriteLine("Thread{0} is cancel", Thread.CurrentThread.ManagedThreadId));
            ThreadPool.QueueUserWorkItem(o => Count(cts.Token, 1000));
            Console.WriteLine("Press <Enter> to cancel the operatioin.");
            Console.ReadLine();
            cts.Cancel();
        }

        private static void Count(CancellationToken token, int countTo)
        {
            Console.WriteLine("Count Runing Thread{0}", Thread.CurrentThread.ManagedThreadId);
            for (int count = 0; count < countTo; count++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Count is cancelled");
                    break;
                }

                Console.WriteLine(count);
                Thread.Sleep(200);
            }

            Console.WriteLine("Count is done");
        }
        #endregion 

        #region 任务--Task
        /// <summary>
        /// 子任务有异常时，异常处理
        /// </summary>
        public static void TaskTest()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task<int> t = new Task<int>(n => Sum(cts.Token, 10000), cts.Token);
            t.Start();
            cts.Cancel();

            try
            {
                //t.Wait();//还未执行完程序，会造成线程阻塞。
                Console.WriteLine("The Sum is: " + t.Result);
            }
            catch (AggregateException ex)
            {
                ex.Handle(e => e is OperationCanceledException);
                Console.WriteLine("Sum is Canceled");
            }
        }

        /// <summary>
        /// 任务启动子任务
        /// </summary>
        public static void TaskTest2()
        {
            Task<int[]> parent = new Task<int[]>(() =>
            {
                var result = new int[3];//创建一个数组来存储结果

                //从这个任务创建并启动三个子任务
                new Task(() => result[0] = Sum(10000), TaskCreationOptions.AttachedToParent).Start();
                new Task(() => result[1] = Sum(20000), TaskCreationOptions.AttachedToParent).Start();
                new Task(() => result[2] = Sum(30000), TaskCreationOptions.AttachedToParent).Start();

                return result;
            });

            var cwt = parent.ContinueWith(parentTask => Array.ForEach(parentTask.Result, Console.WriteLine));
            //parent.Id;
            parent.Start();
        }

        /// <summary>
        /// 任务工厂
        /// </summary>
        public static void TaskTest3()
        {
            Task parent = new Task(() =>
            {
                var cts = new CancellationTokenSource();
                var tf = new TaskFactory<int>(cts.Token, TaskCreationOptions.AttachedToParent,
                    TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                
                //这个任务创建并启动三个子任务
                var childTasks = new[] { 
                    tf.StartNew(()=>Sum(cts.Token, 10000)),
                    tf.StartNew(()=>Sum(cts.Token, 10000)),
                    tf.StartNew(()=>Sum(cts.Token, int.MaxValue))
                };

                //任何子任务抛出异常，就取消子任务
                for (int task = 0; task < childTasks.Length; task++)
                {
                    childTasks[task].ContinueWith(t => cts.Cancel(), TaskContinuationOptions.OnlyOnFaulted);
                }

                //所有子任务完成以后，从未出错/未取消的的任务获取返回的最大值
                //然后将最大值传给另一个任务来显示最大结果
                tf.ContinueWhenAll(childTasks, completedTasks => completedTasks.Where(t=>!t.IsFaulted && !t.IsCanceled).Max(t=>t.Result), 
                    CancellationToken.None).ContinueWith(t=>Console.WriteLine("The maximum is: " + t.Result),
                    TaskContinuationOptions.ExecuteSynchronously);
            });

            parent.ContinueWith(p => {
                StringBuilder sb = new StringBuilder("The following exception(s) occurred: " + Environment.NewLine);
                foreach (var e in p.Exception.Flatten().InnerExceptions)
                {
                    sb.AppendLine(" " + e.GetType().ToString());
                    Console.WriteLine(sb.ToString());
                }
            }, TaskContinuationOptions.OnlyOnFaulted);

            parent.Start();
        }

        private static Int32 Sum(int n)
        {
            int sum = 0;
            for (; n > 0; n--)
            {
                checked { sum += n; }
            }

            return sum;
        }

        private static Int32 Sum(CancellationToken token, int n)
        {
            int sum = 0;
            for(; n>0; n--)
            {
                token.ThrowIfCancellationRequested();
                checked { sum += n; }
            }

            return sum;
        }
        #endregion

        #region 并行执行
        public static void ObsoleteMethods()
        {
            string assemId = "System.Data, version=4.0.0.0, culture=neutral, PublicKeyToken=b77a5c561934e089";
            Assembly a = Assembly.Load(assemId);
            var query = from type in a.GetExportedTypes().AsParallel()
                        from method in type.GetMethods(BindingFlags.Public |
                         BindingFlags.Instance | BindingFlags.Static)
                        let obsoleteAttrType = typeof(ObsoleteAttribute)
                        where Attribute.IsDefined(method, obsoleteAttrType)
                        orderby type.FullName

                        let obsoleteAttrObj = (ObsoleteAttribute)
                        Attribute.GetCustomAttribute(method, obsoleteAttrType)

                        select String.Format("Type={0}\nMethod={1}\nMessage={2}\n",
                        type.FullName, method.ToString(), obsoleteAttrObj.Message);

            //显示结果
            foreach (var result in query) Console.WriteLine(result);
        }
        #endregion

        #region 缓存线与伪共享
        [StructLayout(LayoutKind.Explicit)]
        private class Data {
            //这两个字段是相邻的，并（极有可能）在相同的缓存行中
            //高速缓存的缓存行（64bety）,如果获取int值，就会获取更多的字节，
            //就把相邻的数据加载到高速缓存中
            [FieldOffset(0)]public int field1;
            [FieldOffset(64)]public int field2;
        }
        private const Int32 iterations = 100000000;
        private static Int32 s_operations = 2;
        private static Int64 s_startTime;
        public static void FalseSharingTest()
        {
            Data data = new Data();
            s_startTime = Stopwatch.GetTimestamp();

            //让2个线程访问在对象中它们自己的字段
            ThreadPool.QueueUserWorkItem(o => AccessData(data, 0));
            ThreadPool.QueueUserWorkItem(o => AccessData(data, 1));
        }

        private static void AccessData(Data data, Int32 field)
        {
            //这里的线程各自访问它们在Data对象中自己的字段
            for (Int32 x = 0; x < iterations; x++)
                if (field == 0) data.field1++; else data.field2++;

            //不管哪个线程最后结束，都显示它花的时间
            if (Interlocked.Decrement(ref s_operations) == 0)
                Console.WriteLine("Access time: {0:n0} ",
                    (Stopwatch.GetTimestamp() - s_startTime) /
                    (Stopwatch.Frequency / 1000));
        }
        #endregion
    }
    #endregion 

    #region I/O线程
    public class LearnIOThread
    {
        public static async void IOTest1()
        {
            string fname = @"E:\testStr.txt";
            byte[] result;
            using (FileStream sourceStream = new FileStream(fname, FileMode.Open))
            {
                result = new byte[sourceStream.Length];
                await sourceStream.ReadAsync(result, 0, (int)sourceStream.Length);
            }
            Console.WriteLine(Encoding.UTF8.GetString(result));
        }
    }
    #endregion

    #region async&await
    public class MyClass
    {
        public MyClass()
        {
            DisplayValue();
            System.Diagnostics.Debug.WriteLine("MyClass() End.");
        }

        public Task<double> GetValueAsync(double num1, double num2)
        {
            return Task.Run(() => {
                for (int i = 0; i < 100000; i++)
                {
                    num1 = num1 / num2;
                }
                return num1;
            });
        }

        public async void DisplayValue()
        {
            System.Diagnostics.Debug.WriteLine("ThreadId is:" + Thread.CurrentThread.ManagedThreadId);
            double result = await GetValueAsync(1234.5, 1.01);
            System.Diagnostics.Debug.WriteLine("ThreadId is:" + Thread.CurrentThread.ManagedThreadId);
            System.Diagnostics.Debug.WriteLine("Value is:" + result);
        }
    }
    #endregion
}
