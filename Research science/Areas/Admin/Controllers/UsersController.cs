using Research_science.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data.Entity.Migrations;
using System.IO;
using System.Drawing;

namespace Research_science.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        Model1 db = new Model1();

        // GET: Admin/Users
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.Users.ToList().OrderBy(n => n.UserID).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var KH = db.Users.SingleOrDefault(n => n.UserID == id);
            if (KH == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(KH);
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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Users KH)
        {
            if (ModelState.IsValid)
            {
                db.Users.AddOrUpdate(KH);
                db.SaveChanges();
                return RedirectToAction("Index", "Users", new { Area = "Admin" });
            }
            return View(KH);
        }
        public ActionResult Details(int id)
        {
            var kh = db.Users.SingleOrDefault(n => n.UserID == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var KH = db.Users.SingleOrDefault(n => n.UserID == id);
            if (KH == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(KH);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var KH = db.Users.SingleOrDefault(n => n.UserID == id);
            if (KH == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            db.Users.Remove(KH);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Users model, FormCollection f, HttpPostedFileBase fFileUpload)
        {

            if (ModelState.IsValid)
            {
                if (fFileUpload != null)
                {
                    Image img = Image.FromStream(fFileUpload.InputStream, true, true);
                    model.LogoCompany = Utility.ConvertImageToBase64(img);
                    model.Image = Utility.ConvertImageToBase64(img);
                }

                db.Users.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}