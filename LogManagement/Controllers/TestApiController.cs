using IBusiness;
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

            logger.AddWarningLog(jsonString);

            return null;
        }
    }
}
