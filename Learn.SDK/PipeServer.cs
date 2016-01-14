using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.SDK
{
    public sealed class PipeServer
    {
        private readonly NamedPipeServerStream m_pipe = new NamedPipeServerStream(
            "Echo", PipeDirection.InOut, -1, PipeTransmissionMode.Message,
            PipeOptions.Asynchronous | PipeOptions.WriteThrough);

        public PipeServer()
        {
            m_pipe.BeginWaitForConnection(ClientConnected, null);
        }

        private void ClientConnected(IAsyncResult result)
        {
            //一个客户端建立了连接，让我们接受另一个客户端
            new PipeServer();

            //接受客户端链接
            m_pipe.EndWaitForConnection(result);

            //异常地从客户端读取一个请求
            Byte[] data = new Byte[1000];
            m_pipe.BeginRead(data, 0, data.Length, GotRequest, data);
        }

        private void GotRequest(IAsyncResult result)
        {
            //客户端向我们发送一个请求，处理它
            int bytesRead = m_pipe.EndRead(result);
            Byte[] data = (Byte[])result.AsyncState;

            data = Encoding.UTF8.GetBytes(
                Encoding.UTF8.GetString(data, 0, bytesRead).ToUpper().ToCharArray());

            m_pipe.BeginWrite(data, 0, data.Length, WriteDone, null);
        }

        private void WriteDone(IAsyncResult result)
        {
            //响应已发送给了客户端，关闭我们这一端的连接
            m_pipe.EndWrite(result);
            m_pipe.Close();
        }
    }
}
