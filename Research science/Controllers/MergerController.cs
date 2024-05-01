using PagedList;
using Research_science.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Research_science.Controllers
{
    public class MergerController : Controller
    {
        Model1 db = new Model1();

        // GET: Merger
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult InforCty(string searchString, int? page)
        {
            searchString = searchString ?? "";

            var filteredJobs = db.Job.Where(s => s.JobName.Contains(searchString)).OrderBy(s => s.JobID);

            int pageNumber = (page ?? 1);
            int pageSize = 3;
            var pagedJobs = filteredJobs.ToPagedList(pageNumber, pageSize);

            ViewBag.JobId = pagedJobs.FirstOrDefault()?.JobID;

            return PartialView("InforCty", pagedJobs);
        }

        public ActionResult JobCty(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var job = db.Job.FirstOrDefault(j => j.JobID == id);

            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);
        }





        public ActionResult DetailCty(int Id)
        {
            Users company = db.Users.FirstOrDefault(d => d.UserID == Id);
            return View(company);
        }




        [HttpGet]
        public ActionResult Applied()
        {
            var appliedJobs = db.Job.ToList(); 

            return View(appliedJobs); 
        }




    }
}