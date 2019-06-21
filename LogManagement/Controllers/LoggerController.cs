using IBusiness;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModels;

namespace LogManagement.Controllers
{
    [RoutePrefix("api/logger")]
    public class LoggerController : ApiController
    {
        private readonly ILogger<Modules> logger;
        public LoggerController(ILogger<Modules> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        [Route("LogWarning")]
        public HttpResponseMessage LogWarning([FromBody] string message)
        {
            try
            {
                logger.AddWarningLog(message);
                return Request.CreateResponse(HttpStatusCode.OK, "Added successfully") ;
            }
            catch (FormatException formatException)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, formatException);
            }
            catch (ArgumentNullException argumentNullException)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, argumentNullException);
            }
            catch (InvalidOperationException invalidOperationException)
            {
                //return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetType().FullName + ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, invalidOperationException);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,ex);
            }
        }

        [HttpPost]
        [Route("LogInfo")]
        public HttpResponseMessage LogInfo([FromBody] string message)
        {
            try
            {
                this.logger.AddInfoLog(message);
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (FormatException fEx)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, fEx);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("LogFatel")]
        public HttpResponseMessage LogFatel([FromBody] string message)
        {
            try
            {
                logger.AddFatelLog(message);
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (FormatException fEx)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, fEx);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
