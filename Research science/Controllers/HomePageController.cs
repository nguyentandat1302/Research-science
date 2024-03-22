using Research_science.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Research_science.Controllers
{
    public class HomePageController : Controller
    {
        // GET: HomePage
        Model1 db = new Model1();

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


        public static string ConverImageToBase64(string path)
        {
            using (Image image = Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    string base64String = "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

        [HttpGet]
        public ActionResult CustomerProfile()
        {

            if (Session["UserName"] != null)
            {
                var customer = (Customer)Session["Customer"];
                // Lấy thông tin hồ sơ từ cơ sở dữ liệu dựa trên ID
                customer.MatKhauNL = customer.Password;

                if (customer != null)
                {
                    return View(customer);
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CustomerProfile(Customer model, HttpPostedFileBase myFile)
        {
            if (ModelState.IsValid)
            {


                if (myFile != null)
                {
                    Image img = Image.FromStream(myFile.InputStream, true, true);
                    //model.Avatar = Utility.ConvertImageToBase64(img);
                }

                db.Customer.AddOrUpdate(model);
                db.SaveChanges();
                Session["UserName"] = model;
            }


            return View(model);
        }
        public ActionResult ChatBox() 
        {
            return View();
        }
    

    }
}