using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoList.Repositories;

namespace ToDoList.Controllers
{
    [System.Web.Http.Route("api/user")]
    public class BaseApiController : ApiController
    {
        [HttpGet]
        [Route("api/base/getConfiguration")]
        public IHttpActionResult GetConfiguration()
        {
            return Ok(String.Format("{0}://{1}", Request.RequestUri.Scheme, Request.RequestUri.Authority));
            
        }
    }
}
