using IBusiness;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
        #region Async Log Api
        [HttpPost]
        [Route("LogWarning")]
        public async Task<HttpResponseMessage> LogWarningAsync([FromBody] string message)
        {
            try
            {
                await logger.AddWarningLogAsync(message);
                return Request.CreateResponse(HttpStatusCode.OK, "Added successfully");
            }
            catch (FormatException formatException)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $" {formatException.GetType().FullName} - {formatException.Message}");
            }
            catch (ArgumentNullException argumentNullException)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $" {argumentNullException.GetType().FullName} - {argumentNullException.Message}");
            }
            catch (InvalidOperationException invalidOperationException)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $" {invalidOperationException.GetType().FullName} - {invalidOperationException.Message}");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $" {ex.GetType().FullName} - {ex.Message}");
            }
        }

        [HttpPost]
        [Route("LogInfo")]
        public async Task<HttpResponseMessage> LogInfoAsync([FromBody] string message)
        {
            try
            {
                await logger.AddInfoLogAsync(message);
                return Request.CreateResponse(HttpStatusCode.OK, "Added successfully");
            }
            catch (FormatException formatException)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $" {formatException.GetType().FullName} - {formatException.Message}");
            }
            catch (ArgumentNullException argumentNullException)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $" {argumentNullException.GetType().FullName} - {argumentNullException.Message}");
            }
            catch (InvalidOperationException invalidOperationException)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $" {invalidOperationException.GetType().FullName} - {invalidOperationException.Message}");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $" {ex.GetType().FullName} - {ex.Message}");
            }
        }

        [HttpPost]
        [Route("LogFatel")]
        public async Task<HttpResponseMessage> LogFatelAsync([FromBody] string message)
        {
            try
            {
                await logger.AddFatelLogAsync(message);
                return Request.CreateResponse(HttpStatusCode.OK, "Added successfully");
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
                return Request.CreateResponse(HttpStatusCode.InternalServerError, invalidOperationException);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        #endregion
    }
}
