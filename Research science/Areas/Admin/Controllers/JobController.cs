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


   //    }

    }
}