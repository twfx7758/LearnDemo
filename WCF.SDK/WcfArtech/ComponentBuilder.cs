using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WCF.SDK
{
    public static class ComponentBuilder
    {
        /// <summary>
        /// 创建基于指定操作的消息格式化器
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="isProxy">表示创建的是客户端消息格式化器(true)，还是分发消息格式化器(false)</param>
        /// <returns></returns>
        public static object GetFormatter(OperationDescription operation, bool isProxy)
        {
            bool formatRequest = false;
            bool formatReply = false;

            DataContractSerializerOperationBehavior behavior =
                new DataContractSerializerOperationBehavior(operation);

            MethodInfo method = typeof(DataContractSerializerOperationBehavior)
                .GetMethod("GetFormatter", BindingFlags.Instance | BindingFlags.NonPublic);

            return method.Invoke(behavior, new object[] { operation, formatRequest, formatReply, isProxy });
        }

        public static MessageEncoderFactory GetMessageEncoderFactory(
            MessageVersion messageVersion, Encoding writeEncoding)
        {
            TextMessageEncodingBindingElement bindingElement =
                new TextMessageEncodingBindingElement(
                    messageVersion, writeEncoding);

            return bindingElement.CreateMessageEncoderFactory();
        }

        public static IOperationInvoker GetOperationInvoker(MethodInfo method)
        {
            //因为SyncMethodInvoker是一个内部类型，需要用反射来创建它
            string syncMethodInvokerType =
                @"System.ServiceModel.Dispatcher.SyncMethodInvoker,
                  System.ServiceModel, Version=4.0.0.0, Culture=neutral,
                  PublicKeyToken=b77a5c561934e089";

            Type type = Type.GetType(syncMethodInvokerType);
            return (IOperationInvoker)Activator.CreateInstance(type, new object[] { method });
        }
    }
}
