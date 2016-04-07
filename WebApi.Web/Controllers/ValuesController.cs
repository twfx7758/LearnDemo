using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Web.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        public string Get()
        {
            return "Get";
        }
        
        public string Post()
        {
            return "Post";
        }
        
        public string Put()
        {
            return "Put";
        }
        
        public string Delete()
        {
            return "Delete";
        }
    }
}
