using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Learn.Owin.Host
{
    public class SampleMiddleware : OwinMiddleware
    {
        public SampleMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            //中间件的实现代码
            PathString tickPath = new PathString("/tick");
            if (context.Request.Path.StartsWithSegments(tickPath)){
                string content = DateTime.Now.Ticks.ToString();
                //输出答案--当前的Tick数字
                context.Response.ContentType = "text/plain";
                context.Response.ContentLength = content.Length;
                context.Response.StatusCode = 200;
                context.Response.Expires = DateTimeOffset.Now;
                context.Response.Write(content);

                return Task.FromResult(0);
            }
            else {
                //如果不是/tick路径,那么交付后续Middleware处理
                return Next.Invoke(context);
            }
            

        }
    }
}
