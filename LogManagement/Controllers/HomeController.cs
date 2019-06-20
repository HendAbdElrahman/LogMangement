using IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
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
            ViewBag.Message = "Your application description page.";

            //var jsonString = @"{""Name"":""Rick"",""Id"":""1""}";
            //this.logger.AddLog(xmlString);

            //var xmlString = @"<""Company""><""Name"">""Tove""</""Name""><""Id"">""Jani""</""Id""></""Company"">";
            //XmlDocument doc = new XmlDocument();
            //string xmlString = System.IO.File.ReadAllText("E:\\temp.xml");
            //XmlDocument doc = new XmlDocument();

            var xmlString = @"<?xml version='1.0' encoding='utf-8' ?>
            <Company Id='1' Name='Friska'/>";

            this.logger.AddWarningLog(xmlString);

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}