using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learn.SDK.Redis
{
    public class RedisTrans
    {
        public void InitTrans()
        {
            using (var redisConsumer = RedisManager.GetClient())
            {
                using (var trans = redisConsumer.CreateTransaction())
                {
                    try
                    {
                        trans.QueueCommand(r => r.Set("keyA", 20));
                        trans.QueueCommand(r => r.Increment("keyA", 1));

                        //注意,如果有下面这条语句,提交事务会报错，这说明启动事务后的  
                        //Client操作都必须在trans内调用.  
                        //redisConsumer.Set<string>("KeyABC", "sadfsdffdsa");  

                        //throw new Exception("数据库操作错误!");  
                        // 提交事务
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                    }

                    //上面报异常后，keyA不存在，但KeyABC的值存在.  
                    string theV1 = redisConsumer.Get<int>("keyA").ToString();
                    string theV2 = redisConsumer.Get<string>("KeyABC");

                    //并发锁  
                    redisConsumer.Add("mykey", 1);
                    // 支持IRedisTypedClient和IRedisClient  
                    //注意这里是Form调用，测试不出效果，如果是网页程序，则可获得锁效果.  
                    using (redisConsumer.AcquireLock("testlock"))
                    {
                        var counter = redisConsumer.Get<int>("mykey");
                        Thread.Sleep(100);
                        redisConsumer.Set("mykey", counter + 1);
                    }
                }
            }
    }
}
