using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Display
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient httpClient1 = new HttpClient();
            HttpClient httpClient2 = new HttpClient();
            HttpClient httpClient3 = new HttpClient();
            HttpClient httpClient4 = new HttpClient();

            httpClient2.DefaultRequestHeaders.Add("X-HTTP-Method-Override", "Post");
            httpClient3.DefaultRequestHeaders.Add("X-HTTP-Method-Override", "PUT");
            httpClient4.DefaultRequestHeaders.Add("X-HTTP-Method-Override", "DELETE");

            Console.WriteLine("{0,-7}{1,-24}{2,-6}", "Method", "X-HTTP-Method-Override", "Action");

            InvokeWebApi(httpClient1, HttpMethod.Get);
            InvokeWebApi(httpClient2, HttpMethod.Post);
            //InvokeWebApi(httpClient3, HttpMethod.Put);
            //InvokeWebApi(httpClient4, HttpMethod.Delete);

            Console.ReadKey();
        }

        async static void InvokeWebApi(HttpClient httpClient, HttpMethod method)
        {
            string requestUri = "http://www.kf.com/api/values";
            HttpRequestMessage request = new HttpRequestMessage(method, requestUri);
            HttpResponseMessage response = await httpClient.SendAsync(request);
            IEnumerable<string> methodsOverride;
            httpClient.DefaultRequestHeaders.TryGetValues("X-HTTP-Method-Override", out methodsOverride);
            string actionName = response.Content.ReadAsStringAsync().Result;
            string methodOverride = methodsOverride == null ? "N/A" : methodsOverride.First();
            Console.WriteLine("{0,-7}{1,-24}{2,-6}", method, methodOverride, actionName.Trim('"'));
        }
    }
}
