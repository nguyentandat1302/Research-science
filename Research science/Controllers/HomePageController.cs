using PagedList;
using Research_science.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI;
using SelectPdf;
using IronPdf.Rendering;

namespace Research_science.Controllers
{
    public class HomePageController : BaseController
    {
        Model1 db = new Model1();

        private object _accountService;
       

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
        //
        public ActionResult ListCompany(string skillName, string country, string searchString, int? page)
        {
            //
            searchString = searchString ?? "";
            var lstjob = db.Job.Where(s => s.JobName.Contains(searchString)).OrderBy(s => s.JobID).ToList();
            
         
            IQueryable<Users> query = db.Users;
            if (!string.IsNullOrEmpty(skillName))
            {
                query = query.Where(company => company.Business == skillName);
            }
            if (!string.IsNullOrEmpty(country))
            {
                query = query.Where(company => company.Address == country);
            }
            var listCom = query.ToList();
            return View(listCom);
        }







        public ActionResult SkillPartial()
        {
            var listSkill = from cd in db.Skill select cd;
            return PartialView(listSkill);
        }

        public ActionResult CityPartial()
        {
            var listCity = from cd in db.Location select cd;
            return PartialView(listCity);
        }


        public ActionResult LanguagePartial()
        {
            var listLanguage = from cd in db.Language select cd;
            return PartialView(listLanguage);
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
                var customer = (Users)Session["UserName"];
                customer.ConfirmPass = customer.Password;

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
        public ActionResult CustomerProfile(Users model, HttpPostedFileBase myFile)
        {
            if (ModelState.IsValid)
            {
                if (myFile != null)
                {
                    Image img = Image.FromStream(myFile.InputStream, true, true);
                    model.Image = Utility.ConvertImageToBase64(img);
                }
                model.MaLoaiUser = 2;

                db.Users.AddOrUpdate(model);
                db.SaveChanges();

                Session["UserName"] = model;

                return RedirectToAction("CustomerProfile");
            }

            return View(model);
        }

        public ActionResult ExportPdf()
        {
            var model = GetModelData();

            HtmlToPdf converter = new HtmlToPdf();

            // Kiểm tra xem model có dữ liệu không trước khi tạo file PDF
            if (model != null)
            {
                var htmlPdf = RenderPartialToString("~/Views/HomePage/DownloadPdf.cshtml", model, ControllerContext);

                PdfDocument doc = converter.ConvertHtmlString(htmlPdf);

                string fileName = SavePdfToFile(doc);

                return Json(fileName, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Content("Không có dữ liệu để tạo file PDF.");
            }
        }

        public Users GetModelData()
        {
            if (Session["UserName"] != null)
            {
                return (Users)Session["UserName"];
            }
            return null;
        }

        public string RenderPartialToString(string viewName, object model, ControllerContext controllerContext)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = controllerContext.RouteData.GetRequiredString("action");

            // Kiểm tra xem model có dữ liệu không trước khi gán vào ViewData
            if (model != null)
            {
                ViewData.Model = model;
            }

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var viewContext = new ViewContext(controllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }


        // Phương thức này để lưu file PDF và trả về tên file
        public string SavePdfToFile(PdfDocument doc)
        {
            string fileName = string.Format("{0}.pdf", DateTime.Now.Ticks);
            string pathFile = Path.Combine(Server.MapPath("~/Resource/Pdf"), fileName);

            doc.Save(pathFile);
            doc.Close();

            return fileName;
        }


        public ActionResult DownloadPdf(string fileName)
        {
            if (Session["UserName"] != null)
            {
                var customer = (Users)Session["UserName"];
                customer.ConfirmPass = customer.Password;

                if (customer != null)
                {
                    return PartialView("DownloadPdf", customer);
                }
            }
            string filePath = Path.Combine(Server.MapPath("~/Resource/Pdf"), fileName);
            return File(filePath, "application/pdf", fileName);
        }



        public ActionResult Error()
        {
            return View();
        }
    }
}
    

   