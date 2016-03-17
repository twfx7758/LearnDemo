using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.SDK.Redis
{
    public class RedisConsumer
    {
        /// <summary>
        /// redis订阅消费者
        /// Redis实现完整的发布订阅范式，
        /// 就是说任何一台redis服务器，
        /// 启动后都可以当做发布订阅服务器。
        /// </summary>
        /// <param name="channel_name">信道名称</param>
        /// <returns></returns>
        public bool ListenInit(string channel_name)
        {
            using (var redisConsumer = RedisManager.GetClient())
            {
                using (var subscription = redisConsumer.CreateSubscription())
                {
                    subscription.OnSubscribe = channel => {
                        Console.WriteLine("subscription.OnSubscribe");
                    };

                    subscription.OnUnSubscribe = channel => {
                        Console.WriteLine("subscription.OnUnSubscribe");
                    };

                    subscription.OnMessage = (channel, msg) => {
                        Console.WriteLine("subscription.OnMessage,Received '{0}' from channel '{1}'", msg, channel);
                    };

                    Console.WriteLine(string.Format("Started Listening On '{0}'", channel_name));

                    subscription.SubscribeToChannels(channel_name);
                }
            }

            return true;
        }
    }
}
