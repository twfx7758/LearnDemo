using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.SDK
{
    public sealed class PipeClient
    {
        private readonly NamedPipeClientStream m_pipe;

        public PipeClient(string serverName, string message)
        {
            m_pipe = new NamedPipeClientStream(serverName, "Echo",
                PipeDirection.InOut,
                PipeOptions.Asynchronous | PipeOptions.WriteThrough);
            m_pipe.Connect();//必须先连接才能设置ReadMode
            m_pipe.ReadMode = PipeTransmissionMode.Message;

            //异步地将数据发送给服务器
            Byte[] output = Encoding.UTF8.GetBytes(message);

            m_pipe.BeginWrite(output, 0, output.Length, WriteDone, null);
        }

        private void WriteDone(IAsyncResult result)
        { 
            //数据已发送给服务器
            m_pipe.EndWrite(result);

            //异步地读取服务器的响应
            Byte[] data = new Byte[1000];
            m_pipe.BeginWrite(data, 0, data.Length, GotResponse, data);
        }

        private void GotResponse(IAsyncResult result)
        {
            //服务器已响应，显示响应，并关闭出站连接
            int bytesRead = m_pipe.EndRead(result);

            Byte[] data = (Byte[])result.AsyncState;
            Console.WriteLine("Server Response: " + Encoding.UTF8.GetString(data, 0, bytesRead));
            m_pipe.Close();
        }
    }
}
