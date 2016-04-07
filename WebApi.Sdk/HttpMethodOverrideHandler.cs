using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Sdk
{
    public class HttpMethodOverrideHandler : DelegatingHandler
    {
        public HttpMethodOverrideHandler() { }

        protected HttpMethodOverrideHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {

        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            IEnumerable<string> methodOverrideHeader;
            if (request.Headers.TryGetValues("X-HTTP-Method-Override", out methodOverrideHeader))
            {
                request.Method = new HttpMethod(methodOverrideHeader.First());
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
