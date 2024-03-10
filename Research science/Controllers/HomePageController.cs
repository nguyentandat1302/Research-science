using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Research_science.Controllers
{
    public class HomePageController : Controller
    {
        // GET: HomePage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NavBar()
        {
            return PartialView();
        }
        public ActionResult FooterPartial()
        {
            return PartialView();
        }
        public ActionResult Card()
        {
            return PartialView();
        }
         public ActionResult Video()
        {
            return PartialView();
        }
        public ActionResult One()
        {
            return PartialView();
        }
        public ActionResult Two()
        {
            return PartialView();
        }
        public ActionResult Three()
        {
            return PartialView();
        }
        public ActionResult Four()
        {
            return PartialView();
        }
        public ActionResult Infor1Partial()
        {
            return PartialView();
        }
        public ActionResult LayoutNavbar()
        {
            return View();
        }
        public ActionResult NavBarDetail()
        {
            return PartialView();
        }
        public ActionResult Contract()
        {
            return PartialView();
        }

    }
}