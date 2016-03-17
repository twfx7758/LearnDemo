using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Common
{
    public class RabbitMQConsumer
    {
        public RabbitMQClientContext Context { get; set; }

        public Action<EventMessage> ActionMessage = null;

        public void OnListening()
        {
            Task.Run(() => ListenInit());
        }

        private void ListenInit()
        {
            try
            {
                //获取连接
                Context.ListenConnection = RabbitMQClientFactory.CreateConnectionForSumer();

                //添加连接断开日志
                Context.ListenConnection.ConnectionShutdown += (s, e) =>
                {
                    if (LogLocation.Log != null)
                    {
                        LogLocation.Log.WriteInfo("RabbitMQClient", "connection shutdown" + e.ReplyText);
                    }
                };

                //获取通道
                Context.ListenChannel = RabbitMQClientFactory.CreateModel(Context.ListenConnection);

                //创建事件驱动的消费者模型
                var consumer = new EventingBasicConsumer(Context.ListenChannel);
                consumer.Received += Consumer_Received;

                //一次只获取一条消息进行消费
                Context.ListenChannel.BasicQos(0, 1, false);
                Context.ListenChannel.BasicConsume(Context.ListenQueueName, false, consumer);
            }
            catch (Exception ex)
            {
                if (LogLocation.Log != null)
                {
                    LogLocation.Log.WriteInfo("RabbitMQClient", "ListenInit()方法报错：" + ex.Message);
                }
            }
        }

        private void Consumer_Received(Client.IBasicConsumer sender, BasicDeliverEventArgs args)
        {
            try
            {
                var result = EventMessage.BuildEventMessageResult(args.Body);

                if (ActionMessage == null)
                    ActionMessage(result);//触发外部监听事件，处理此消息

                if (!result.IsOperationOk)
                {
                    //未能消费此消息，重新放入队列头
                    Context.ListenChannel.BasicReject(args.DeliveryTag, true);
                }
                else if (!Context.ListenChannel.IsClosed)
                {
                    Context.ListenChannel.BasicAck(args.DeliveryTag, false);
                }

            }
            catch (Exception ex)
            {
            }
        }
    }
}
