using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WCF.Client2
{
    public class WMessage
    {
        public static void MainMethod()
        {
            Order order = new Order {
                ID = Guid.NewGuid(),
                Date = DateTime.Today,
                Customer = "屈文斌",
                ShipAddress = "北京市 沙河镇 巩华家园"
            };
            string action = "http://www.fang.com/ICaculator/Add";
            using (Message message = Message.CreateMessage(MessageVersion.Default, action, order))
            {
                WriteMessage(message, "message1.xml");
            }

            var bodyWriter = new XmlReaderBodyWriter("message1.xml");
            using (Message message = Message.CreateMessage(MessageVersion.Soap11, action, bodyWriter))
            {
                WriteMessage(message, "message2.xml");
            }
        }
        //失败消息
        public static void FaultCodeMain()
        {
            FaultCode code = FaultCode.CreateSenderFaultCode("CalcuError", "http://www.fang.com");
            FaultReasonText reasonText1 = new FaultReasonText("Divided by zero!", "en-US");
            FaultReasonText reasonText2 = new FaultReasonText("试图除以零！", "zh-CN");
            FaultReason reason = new FaultReason(new FaultReasonText[] { reasonText1, reasonText2 });
            MessageFault fault = MessageFault.CreateFault(code, reason);
            string action = "http://www.fang.com/ICaculator/Add";
            using (Message message = Message.CreateMessage(MessageVersion.Default, fault, action))
            {
                WriteMessage(message, "message3.xml");
            }
        }

        //消息的读取
        public static void ReadMessageStateMain()
        {
            Order order = new Order
            {
                ID = Guid.NewGuid(),
                Date = DateTime.Today,
                Customer = "屈文斌",
                ShipAddress = "北京市 沙河镇 巩华家园"
            };
            string action = "http://www.fang.com/ICaculator/Add";
            Message message = Message.CreateMessage(MessageVersion.Default, action, order);

            Console.WriteLine("消息当前状态:{0}", message.State);
            var order1 = message.GetBody<Order>();

            Console.WriteLine("从消息中读取到订单信息");
            Console.WriteLine("\t{0,-2}：{1}", "单号", order1.ID);
            Console.WriteLine("\t{0,-2}：{1}", "日期", order1.Date.ToString("yyyy-MM-dd"));
            Console.WriteLine("\t{0,-2}：{1}", "客户", order1.Customer);
            Console.WriteLine("\t{0,-2}：{1}", "地址", order1.ShipAddress);
            Console.WriteLine("消息当前状态:{0}", message.State);
        }

        //消息的拷贝
        public static void CopyMessage()
        {
            Order order = new Order
            {
                ID = Guid.NewGuid(),
                Date = DateTime.Today,
                Customer = "屈文斌",
                ShipAddress = "北京市 沙河镇 巩华家园"
            };
            string action = "http://www.fang.com/ICaculator/Add";
            
            using (Message message = Message.CreateMessage(MessageVersion.Default, action, order))
            {
                Console.WriteLine("消息当前状态:{0}", message.State);
                using (MessageBuffer messageBuffer = message.CreateBufferedCopy(int.MaxValue))
                {
                    Console.WriteLine("消息当前状态:{0}", message.State);
                    using (Message newMessage = messageBuffer.CreateMessage())
                    {
                        var order1 = newMessage.GetBody<Order>();
                        Console.WriteLine("从消息中读取到订单信息");
                        Console.WriteLine("\t{0,-2}：{1}", "单号", order1.ID);
                        Console.WriteLine("\t{0,-2}：{1}", "日期", order1.Date.ToString("yyyy-MM-dd"));
                        Console.WriteLine("\t{0,-2}：{1}", "客户", order1.Customer);
                        Console.WriteLine("\t{0,-2}：{1}", "地址", order1.ShipAddress);
                        Console.WriteLine("消息当前状态:{0}", message.State);
                    }

                    using (Message newMessage = messageBuffer.CreateMessage())
                    {
                        var orde2 = newMessage.GetBody<Order>();
                        Console.WriteLine("从消息中读取到订单信息");
                        Console.WriteLine("\t{0,-2}：{1}", "单号", orde2.ID);
                        Console.WriteLine("\t{0,-2}：{1}", "日期", orde2.Date.ToString("yyyy-MM-dd"));
                        Console.WriteLine("\t{0,-2}：{1}", "客户", orde2.Customer);
                        Console.WriteLine("\t{0,-2}：{1}", "地址", orde2.ShipAddress);
                    }
                    Console.WriteLine("消息当前状态:{0}", message.State);
                }
            }
        }

        //带报头集合的消息
        public static void MessageHeaderMain()
        {
            string action = "http://www.fang.com/ICaculator/Add";
            using (Message message = Message.CreateMessage(MessageVersion.Soap11WSAddressingAugust2004, action))
            {
                string ns = "http://www.fang.com/crm";
                EndpointAddress address = new EndpointAddress("http://www.fang.com/crm/client");
                message.Headers.To = new Uri("http://www.fang.com/crm/customerservice");
                message.Headers.From = address;
                message.Headers.ReplyTo = address;
                message.Headers.FaultTo = address;
                message.Headers.MessageId = new UniqueId(Guid.NewGuid());
                message.Headers.RelatesTo = new UniqueId(Guid.NewGuid());

                MessageHeader<string> foo = new MessageHeader<string>("ABC");
                MessageHeader<string> bar = new MessageHeader<string>("abc", true, "", false);
                MessageHeader<string> baz = new MessageHeader<string>("123", false, 
                    "http://schemas.xmlsoap.org/soap/actor/next", true);

                message.Headers.Add(foo.GetUntypedHeader("Foo", ns));
                message.Headers.Add(bar.GetUntypedHeader("Bar", ns));
                message.Headers.Add(baz.GetUntypedHeader("Baz", ns));

                WriteMessage(message, "message4.xml");
            }
        }

        //消息的写入
        static void WriteMessage(Message message, string fileName)
        {
            using (XmlWriter writer = new XmlTextWriter(fileName, Encoding.UTF8))
            {
                message.WriteMessage(writer);
            }
            Process.Start(fileName);
        }
    }

    public class XmlReaderBodyWriter : BodyWriter
    {
        public string FileName { get; private set; }

        public XmlReaderBodyWriter(string fileName) : base(false)
        {
            this.FileName = fileName;
        }

        protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
        {
            using (XmlReader reader = new XmlTextReader(this.FileName))
            {
                while (!reader.EOF)
                {
                    writer.WriteNode(reader, false);
                }
            }
        }
    }

    [DataContract(Namespace = "http://www.fang.com")]
    public class Order
    {
        [DataMember(Name = "OrderNo", Order = 1)]
        public Guid ID { get; set; }
        [DataMember(Name = "OrderDate", Order = 2)]
        public DateTime Date { get; set; }
        [DataMember(Order = 3)]
        public string Customer { get; set; }
        [DataMember(Order = 4)]
        public string ShipAddress { get; set; }
    }
}
