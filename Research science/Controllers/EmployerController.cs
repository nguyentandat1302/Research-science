using Research_science.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Net.Mail;
using System.Net;


namespace Research_science.Controllers
{
    public class EmployerController : Controller
    {
        Model1 db = new Model1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JobIndex(int? page)
        {
            int iPageNum = page ?? 1;
            int iPageSize = 4;

            var jobs = db.Job.OrderBy(n => n.JobID).Skip((iPageNum - 1) * iPageSize).Take(iPageSize).ToList();

            ViewBag.HasPreviousPage = iPageNum > 1;
            ViewBag.HasNextPage = jobs.Count() == iPageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)db.Job.Count() / iPageSize);
            ViewBag.PageNumber = iPageNum;

            return View(jobs);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.IdLocation = new SelectList(db.Location.ToList().OrderBy(n => n.Country), "IdLocation", "Country");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Job model, HttpPostedFileBase fFileUpload)
        {
            if (ModelState.IsValid)
            {
                if (fFileUpload != null && fFileUpload.ContentLength > 0)
                {
                    // Lưu tập tin ảnh vào thư mục hoặc lưu trữ tùy chọn
                    string fileName = Path.GetFileName(fFileUpload.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    fFileUpload.SaveAs(filePath);

                    model.Anh = "/Uploads/" + fileName;
                }

                db.Job.Add(model);
                db.SaveChanges();
                return RedirectToAction("JobIndex");
            }
            ViewBag.IdLocation = new SelectList(db.Location.ToList().OrderBy(n => n.Country), "IdLocation", "Country", model.IdLocation);
            return View(model);
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

            return RedirectToAction("JobIndex");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var job = db.Job.SingleOrDefault(n => n.JobID == id);
            if (job == null)
            {
                return HttpNotFound(); 
            }
            ViewBag.IdLocation = new SelectList(db.Location.ToList().OrderBy(n => n.Country), "IdLocation", "Country", job.IdLocation);
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Job model, HttpPostedFileBase fFileUpload)
        {
            if (ModelState.IsValid)
            {
                var jobInDb = db.Job.SingleOrDefault(n => n.JobID == model.JobID);
                if (jobInDb == null)
                {
                    return HttpNotFound();
                }

                if (fFileUpload != null && fFileUpload.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(fFileUpload.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                    fFileUpload.SaveAs(filePath);

                    jobInDb.Anh = fileName;
                }

                jobInDb.JobName = model.JobName;
                jobInDb.IdLocation = model.IdLocation;
                jobInDb.Title = model.Title;
                jobInDb.Description = model.Description;
                jobInDb.Budget = model.Budget;

                db.SaveChanges();

                return RedirectToAction("JobIndex"); 
            }

            ViewBag.IdLocation = new SelectList(db.Location.ToList().OrderBy(n => n.Country), "IdLocation", "Country", model.IdLocation);
            return View(model);
        }



        [HttpGet]
        public ActionResult Menu()
        {

            if (Session["UserName"] != null) 
            {
                var employer = (Users)Session["UserName"];
                return View(employer);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Menu(Users model, HttpPostedFileBase myFile)
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

                Session["Users"] = model;
            }
            return View(model);
        }


        public ActionResult ProfileEmployer()
        {

            if (Session["UserName"] != null)
            {
                var employer = (Users)Session["UserName"];
                employer.ConfirmPass = employer.Password;

                if (employer != null)
                {
                    return View(employer);
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
        public ActionResult ProfileEmployer(Users model, HttpPostedFileBase myFile)
        {


            if (ModelState.IsValid)
            {


                if (myFile != null)
                {
                    Image img = Image.FromStream(myFile.InputStream, true, true);
                    model.LogoCompany = Utility.ConvertImageToBase64(img);
                }

                db.Users.AddOrUpdate(model);
                db.SaveChanges();
                Session["UserName"] = model;
            }


            return View(model);
        }




       
        public ActionResult PaymentEmployer()
        {
            return PartialView();
        }














        public ActionResult Accept(int id)
        {
            var apply = db.Apply.FirstOrDefault(n => n.JobID == id);

            if (apply != null)
            {
                apply.Accept = true;
                db.SaveChanges();

                SendApplicationStatusEmail(apply.Users.Email, true); // Gửi email với tình trạng đã chấp nhận
            }
            else
            {
                ViewBag.Error = "Error";
            }

            return RedirectToAction("AppliedView");
        }

        public ActionResult Refuse(int id)
        {
            var apply = db.Apply.FirstOrDefault(n => n.JobID == id);

            if (apply != null)
            {
                apply.Accept = false;
                db.SaveChanges();

                SendApplicationStatusEmail(apply.Users.Email, false); // Gửi email với tình trạng bị từ chối
            }
            else
            {
                ViewBag.Error = "Error";
            }

            return RedirectToAction("AppliedView");
        }

        private void SendApplicationStatusEmail(string applicantEmail, bool accepted)
        {
            var subject = accepted ? "TD-Freelancer Thông báo kết quả ứng tuyển" : "TD-Freelancer Thông báo kết quả ứng tuyển";
            var body = accepted ? "Chúc mừng bạn, ứng tuyển của bạn đã được chấp nhận. \n Xin chào Nguyễn Tấn Đạt. \n TD-FreeLancer and Development xác nhận bạn đã ứng tuyển thành công vị trí mong đợi. Hồ sơ của bạn đã được gửi đến công ty. Nhà tuyển dụng sẽ liên hệ với bạn trong thời gian sớm nhất nếu hồ sơ của bạn phù hợp và đáp ứng yêu cầu tuyển dụng." : "Rất tiếc, ứng tuyển của bạn đã bị từ chối và không đủ điều kiện mà công ty mong đợi. Vui lòng xem xét lại một lần nữa trước khi Apply.";
            var imageUrl = "https://cdn.dribbble.com/users/1304441/screenshots/3659805/media/5db56659b6ad74a29dc71d92e9c72f62.png?resize=400x0";

            body += $"<br><img src=\"{imageUrl}\" alt=\"Your Image\" style=\"max-width: 50%;\"><br>";

            var mail = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("21280103e0017@student.tdmu.edu.vn", "sihi ngnu oipz plao"),
                EnableSsl = true,
            };

            var message = new MailMessage();
            message.From = new MailAddress("21280103e0017@student.tdmu.edu.vn");
            message.ReplyToList.Add("21280103e0017@student.tdmu.edu.vn");
            message.To.Add(new MailAddress(applicantEmail));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true; // Đảm bảo email được gửi với định dạng HTML

            mail.Send(message);
        }















        //[HttpGet]
        //public ActionResult DetailCV()
        //{

        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Detail(Apply model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Apply.AddOrUpdate(model);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}







        ////////////////////////////////////////////////////
        public ActionResult FooterEmployer()
        {
            return PartialView();
        }
        public ActionResult Error()
        {
            return View();
        }


        public ActionResult AppliedView(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.Apply.ToList().OrderBy(n => n.ApplyID).ToPagedList(iPageNum, iPageSize));
        }




        public ActionResult Detail(int id)
        {
            var userDetail = db.Users.SingleOrDefault(n => n.UserID == id);

            if (userDetail == null)
            {
                return HttpNotFound(); 
            }

            userDetail.MaLoaiUser = 2;

            return View(userDetail);
        }


    }
}