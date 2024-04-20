using PagedList;
using Research_science.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Research_science.Areas.Admin.Controllers
{
    public class JobController : Controller
    {
        Model1 db = new Model1();
        // GET: Admin/Sach
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.Job.ToList().OrderBy(n => n.JobID).ToPagedList(iPageNum, iPageSize));
        }

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    ViewBag.SkillID = new SelectList(db.Skill.ToList().OrderBy(n => n.SkillName), "SkillID", "SKilName");
        //    ViewBag.IdLocation = new SelectList(db.Location.ToList().OrderBy(n => n.Country), "IdLocation", "Country");
        //    return View();
        //}

        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult Create(Job model, FormCollection f, HttpPostedFileBase fFileUpload)
        //{

        //    ViewBag.SkillID = new SelectList(db.Skill.ToList().OrderBy(n => n.SkillName), "SkillID", "SKilName");
        //    ViewBag.IdLocation = new SelectList(db.Location.ToList().OrderBy(n => n.Country), "IdLocation", "Country");
        //    if (ModelState.IsValid)
        //    {
        //        if (fFileUpload != null)
        //        {
        //            Image img = Image.FromStream(fFileUpload.InputStream, true, true);
        //            //model.im = Utility.ConvertImageToBase64(img);
        //        }

        //        db.Job.Add(model);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

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
        public ActionResult Details(int id)
        {
            var job = db.Job.SingleOrDefault(n => n.JobID == id);
            if (job == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(job);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var job = db.Job.SingleOrDefault(n => n.JobID == id);
            if (job == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(job);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var job = db.Job.SingleOrDefault(n => n.JobID == id);
            if (job == null)
            {
                Response.StatusCode = 404;
                return null;
            }
           
            db.Job.Remove(job);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var job = db.Job.SingleOrDefault(n => n.JobID == id);
            if (job == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.SkillID = new SelectList(db.Skill.ToList().OrderBy(n => n.SkillName), "SkillID", "SKilName");
            ViewBag.IdLocation = new SelectList(db.Location.ToList().OrderBy(n => n.Country), "IdLocation", "Country");
            return View(job);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            var job = db.Job.SingleOrDefault(n => n.JobID == int.Parse(f["iMaSach"]));
            ViewBag.SkillID = new SelectList(db.Skill.ToList().OrderBy(n => n.SkillName), "SkillID", "SKilName");
            ViewBag.IdLocation = new SelectList(db.Location.ToList().OrderBy(n => n.Country), "IdLocation", "Country");
            if (ModelState.IsValid)
            {
                if (fFileUpload != null)
                {

                    var sFileName = Path.GetFileName(fFileUpload.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    //sach.Anhbia = sFileName;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }   //    }

    }
}