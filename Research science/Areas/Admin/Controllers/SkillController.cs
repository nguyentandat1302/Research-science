using PagedList;
using Research_science.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Research_science.Areas.Admin.Controllers
{
    public class SkillController : Controller
    {
        Model1 db = new Model1();
        // GET: Admin/Sach
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.Skill.ToList().OrderBy(n => n.SkillID).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.SkillID = new SelectList(db.Skill.ToList().OrderBy(n => n.SkillID), "SkillID", "SkillName");

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Skill model, FormCollection f, HttpPostedFileBase fFileUpload)
        {

            ViewBag.SkillID = new SelectList(db.Skill.ToList().OrderBy(c => c.SkillID), "SkillID", "SkillName");


            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var skill = db.Skill.SingleOrDefault(n => n.SkillID == id);
            if (skill == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(skill);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var skill = db.Skill.SingleOrDefault(n => n.SkillID == id);
            if (skill == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            db.Skill.Remove(skill);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var skill = db.Skill.SingleOrDefault(n => n.SkillID == id);
            if (skill == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(skill);
        }
    }
}