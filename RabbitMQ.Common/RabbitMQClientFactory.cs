using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Common
{
    public class RabbitMQClientFactory
    {
        public static IConnection CreateConnection()
        {
            var mqConfigCom = MqConfigComFactory.CreateConfigDomInstance(); //获取MQ的配置
            const ushort heartbeat = 60;
            var factory = new ConnectionFactory()
            {
                HostName = mqConfigCom.MqHost,
                UserName = mqConfigCom.MqUserName,
                Password = mqConfigCom.MqPassword,
                //心跳超时时间，如果是单节点，不设置这个值是没有问题的
                //但如果连接的是类似HAProxy虚拟节点的时候就会出现TCP被断开的可能性
                RequestedHeartbeat = heartbeat,
                AutomaticRecoveryEnabled = true //自动重连
            };

            return factory.CreateConnection();//创建连接对你
        }

        public static IModel CreateModel(IConnection connection)
        {
            return connection.CreateModel();
        }


    }
}
