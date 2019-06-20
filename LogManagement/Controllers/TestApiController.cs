using IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LogManagement.Controllers
{
    public class TestApiController : ApiController
    {
        private readonly ILogger<int> logger;
        public TestApiController(ILogger<int> logger)
        {
            this.logger = logger;
        }
        public object About1()
        {

            var jsonString = @"{""Name"":""Rick"",""Id"":""1""}";

            this.logger.AddWarningLog(jsonString);

            return null;
        }
    }
}
