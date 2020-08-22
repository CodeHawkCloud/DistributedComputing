using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessTier.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }

        [Route("Home/Users")]
        [HttpGet]
        public ActionResult Users()
        {
            return PartialView();
        }

        [Route("Home/Accounts")]
        [HttpGet]
        public ActionResult Accounts()
        {
            return PartialView();
        }

        [Route("Home/Transactions")]
        [HttpGet]
        public ActionResult Transactions()
        {
            return PartialView();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}