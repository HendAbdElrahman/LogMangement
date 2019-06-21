using IBusiness;
using System.Web.Mvc;
using ViewModels;

namespace LogManagement.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<Company> logger;
        public HomeController(ILogger<Company> logger)
        {
            this.logger = logger;
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}