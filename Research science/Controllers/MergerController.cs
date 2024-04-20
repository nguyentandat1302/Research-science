using Research_science.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public ActionResult InforCty()
        {
            return PartialView();
        }


        public ActionResult DetailCty(int Id)
        {
            Users company = db.Users.FirstOrDefault(d => d.UserID == Id);
            return View(company);
        }


        [HttpGet]
        public ActionResult Applied()
        {
            // Truy vấn cơ sở dữ liệu để lấy danh sách các công việc đã được ứng tuyển
            var appliedJobs = db.Job.ToList(); // Đây chỉ là ví dụ, bạn cần thay đổi truy vấn này tùy theo logic của ứng dụng

            return View(appliedJobs);
        }




    }
}