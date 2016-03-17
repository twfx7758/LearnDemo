using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Common
{
    public class RabbitMQSender
    {
        public RabbitMQClientContext Context { get; set; }

        //客户端发送消息的时候要标记上消息的持久化状态
        //可以在创建队列的时候设置此队列是持久化的，但是队列中的消息要在我们发送某个消息的时候打上需要持久化的状态标记。
        public void TriggerEventMessage(IEventMessage eventMessage)
        {
            Context.SendConnection = RabbitMQClientFactory.CreateConnectionForSend();//获取连接
            using (Context.SendConnection)
            {
                //获取发送通道
                Context.SendChannel = RabbitMQClientFactory.CreateModel(Context.SendConnection);

                const byte deliveryMode = 2;//消息交换模式，1、短暂的，2、持久的

                using (Context.SendChannel)
                {
                    //序列化消息器
                    var messageSerializer = MessageSerializerFactory.CreateMessageSerializerInstance();

                    //消息持久化
                    var properties = Context.SendChannel.CreateBasicProperties();
                    properties.DeliveryMode = deliveryMode; 

                    //推送消息
                    byte[] message = messageSerializer.SerializerBytes(eventMessage);
                    Context.SendChannel.BasicPublish(Context.SendExchange, Context.SendQueueName, properties, message);
                }
            }
        }
    }
}
