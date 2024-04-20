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
                var userName = Session["UserName"].ToString();

                var user = db.Users.FirstOrDefault(u => u.UserName == userName);

                if (user != null)
                {
                    return View(user);
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

                db.Users.AddOrUpdate(model);
                db.SaveChanges();

                Session["UserName"] = model.UserName;

                return RedirectToAction("CustomerProfile"); // Sau khi cập nhật thành công, chuyển hướng lại đến trang CustomerProfile
            }

            return View(model);
        }

        public ActionResult Error()
        {
            // Xử lý trang lỗi ở đây
            return View();
        }
    }
}
    

   