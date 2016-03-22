﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Common
{
    public class RabbitMQSender<T>
    {
        public RabbitMQClientContext Context { get; set; }

        public IEventMessage<T> message { get; set; }

        //客户端发送消息的时候要标记上消息的持久化状态
        //可以在创建队列的时候设置此队列是持久化的，但是队列中的消息要在我们发送某个消息的时候打上需要持久化的状态标记。
        public void TriggerEventMessage()
        {
            Context.SendConnection = RabbitMQClientFactory.CreateConnectionForSend();//获取连接
            using (Context.SendConnection)
            {
                //获取发送通道
                Context.SendChannel = RabbitMQClientFactory.CreateModel(Context.SendConnection);

                using (Context.SendChannel)
                {
                    //序列化消息器
                    var messageSerializer = MessageSerializerFactory.CreateMessageSerializerInstance();

                    //消息持久化
                    var properties = Context.SendChannel.CreateBasicProperties();
                    properties.DeliveryMode = message.deliveryMode;

                    //推送消息
                    byte[] sMessage = messageSerializer.SerializerBytes(message);
                    Context.SendChannel.BasicPublish(Context.SendExchange, Context.SendQueueName, properties, sMessage);
                }
            }
        }
    }
}
