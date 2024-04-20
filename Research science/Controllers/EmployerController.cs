using Research_science.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;

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
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.Job.ToList().OrderBy(n => n.JobID).ToPagedList(iPageNum, iPageSize));
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

        //Acction de dang ky tai khoan do e

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
                //employer.ConfirmPass = employer.Password;

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




        //Gio sua search hay sao em

        // cho em hỏi là cái view đỡ chấp nhận cv của  emloyee á thầy là tự tạo hay tạo theo cách lúc tạo admin ạ
        public ActionResult PaymentEmployer()
        {
            return PartialView();
        }

        //////////////////////////////////////////////
        public ActionResult Accept(int id)
        {
            var job = db.Job.FirstOrDefault(n => n.JobID == id);

            if (job != null)
            {
                job.Accept = true;
                db.SaveChanges();

                return RedirectToAction("Menu", "Employer"); // Chuyển hướng người dùng đến action "Menu" trong controller "Employer"
            }
            else
            {
                ViewBag.Error = "Error";
                return RedirectToAction("Index", "Home"); // Trong trường hợp không tìm thấy công việc, chuyển hướng người dùng đến trang chủ
            }
        }



        public ActionResult Refuse(int id)
        {
            var job = db.Job.FirstOrDefault(n => n.JobID == id);

            if (job != null)
            {
                job.Accept = false;
                db.SaveChanges();

                SendBookingConfirmationEmail(job.Users.Email, false);

            }
            else
            {
                ViewBag.Error = "Error";
            }

            return RedirectToAction("Menu");
        }

        private void SendBookingConfirmationEmail(string patientEmail, bool accepted)
        {
            var subject = accepted ? "Booking Confirmation" : "Booking Refusal";
            var body = accepted ? "Your appointment has been successfully booked." : "Unfortunately, your appointment has been refused due to an emergency.";

            var mail = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("21280103e0017@student.tdmu.edu.vn", "sihi ngnu oipz plao"),
                EnableSsl = true,
            };

            var message = new MailMessage();
            message.From = new MailAddress("21280103e0017@student.tdmu.edu.vn");
            message.ReplyToList.Add("21280103e0017@student.tdmu.edu.vn");
            message.To.Add(new MailAddress(patientEmail));
            message.Subject = subject;
            message.Body = body;

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
     

        //public ActionResult Accept(int id)
        //{
        //    var booking = db.Booking.FirstOrDefault(n => n.IDBooking == id);

        //    if (booking != null)
        //    {
        //        booking.Accept = true;
        //        db.SaveChanges();

        //        SendBookingConfirmationEmail(booking.Patient.Email, true);

        //    }
        //    else
        //    {
        //        ViewBag.Error = "Error";
        //    }

        //    return RedirectToAction("Index");
        //}

        //public ActionResult Refuse(int id)
        //{
        //    var booking = db.Booking.FirstOrDefault(n => n.IDBooking == id);

        //    if (booking != null)
        //    {
        //        booking.Accept = false;
        //        db.SaveChanges();

        //        // Send email to patient
        //        SendBookingConfirmationEmail(booking.Patient.Email, false);

        //        // Additional logic if needed
        //    }
        //    else
        //    {
        //        ViewBag.Error = "Error";
        //    }

        //    return RedirectToAction("Index");
        //}

        //private void SendBookingConfirmationEmail(string patientEmail, bool accepted)
        //{
        //    var subject = accepted ? "Booking Confirmation" : "Booking Refusal";
        //    var body = accepted ? "Your appointment has been successfully booked." : "Unfortunately, your appointment has been refused due to an emergency.";

        //    var mail = new SmtpClient("smtp.gmail.com", 587)
        //    {
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential("21280103e0017@student.tdmu.edu.vn", "sihi ngnu oipz plao"),
        //        EnableSsl = true,
        //    };

        //    var message = new MailMessage();
        //    message.From = new MailAddress("21280103e0017@student.tdmu.edu.vn");
        //    message.ReplyToList.Add("21280103e0017@student.tdmu.edu.vn");
        //    message.To.Add(new MailAddress(patientEmail));
        //    message.Subject = subject;
        //    message.Body = body;

        //    mail.Send(message);
        //}

        [HttpGet]
        public ActionResult Detail(int id)
        {
            Apply model = db.Apply.FirstOrDefault(n => n.ApplyID == id);
      
            return View(model);
        }
        [HttpPost]
        public ActionResult Detail(Apply model)
        {
            if (ModelState.IsValid)
            {
                db.Apply.AddOrUpdate(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}