using PagedList;
using Research_science.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Research_science.Areas.Admin.Controllers
{
    public class LocationController : Controller
    {
        Model1 db = new Model1();
        // GET: Admin/Sach
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.Location.ToList().OrderBy(n => n.IdLocation).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.IdLocation = new SelectList(db.Location.ToList().OrderBy(n => n.IdLocation), "IdLocation", "Country");

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Location model, FormCollection f, HttpPostedFileBase fFileUpload)
        {

            ViewBag.IdLocation = new SelectList(db.Location.ToList().OrderBy(c => c.IdLocation), "IdLocation", "Country");


            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var location = db.Location.SingleOrDefault(n => n.IdLocation == id);
            if (location == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(location);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var location = db.Location.SingleOrDefault(n => n.IdLocation == id);
            if (location == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            db.Location.Remove(location);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}