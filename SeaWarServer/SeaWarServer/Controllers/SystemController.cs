using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SeaWarServer.Controllers
{
    public class SystemController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetStringMessages()
        {
            return this.Ok(Messages.GetStrings().Select(a=>new { Key=a.Name, Value=a.GetValue(a)}));
        }
    }
}
