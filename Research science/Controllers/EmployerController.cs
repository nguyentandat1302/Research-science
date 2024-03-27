using Research_science.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Research_science.Controllers
{
    public class EmployerController : Controller
    {
        Model1 db = new Model1();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Menu()
        {
            if (Session["Employer"] != null)
            {
                var employer = (Employer)Session["Employer"];
                return View(employer);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Menu(Employer model, HttpPostedFileBase myFile)
        {
            if (ModelState.IsValid)
            {
                if (myFile != null)
                {
                    Image img = Image.FromStream(myFile.InputStream, true, true);
                    //model.Avatar = Utility.ConvertImageToBase64(img);
                }

                db.Employer.AddOrUpdate(model);
                db.SaveChanges();

                Session["Employer"] = model;
            }
            return View(model);
        }

       



        [HttpGet]
        public ActionResult ProfileEmployer()
        {
            
            //Cai nay bi null
            if (Session["Employer"] != null)
            {
                var employer = (Employer)Session["Employer"];
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
                //Nen xuong day
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ProfileEmployer(Employer model)
        {
            if (ModelState.IsValid)
            {
                var existingEmployer = db.Employer.FirstOrDefault(e => e.IDEmployer == model.IDEmployer);
                if (existingEmployer != null)
                {
                   

                    db.SaveChanges();
                    Session["UserName"] = existingEmployer;
                }
                else
                {
                    db.Employer.Add(model);
                    db.SaveChanges();
                    Session["UserName"] = model;
                }

                return RedirectToAction("ProfileEmployer");
            }

            return View(model);
        }

    



    public ActionResult PaymentEmployer() 
        {
            return PartialView();
        }

        public ActionResult FooterEmployer()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult MessEmployer(int customerId,int employerId)
        {
            // Lấy tin nhắn giữa employer và customer từ cơ sở dữ liệu
            var messages = db.Message.Where(m => m.IDEmployer == employerId && m.IDCustomer == customerId).ToList();

            // Truyền dữ liệu tin nhắn vào view
            return PartialView(messages);
        }

        // Phương thức để gửi tin nhắn từ employer
        [HttpPost]
        public ActionResult SendMessageFromEmployer(int customerId, string messageContent)
        {
            // Tạo một tin nhắn mới
            var newMessage = new Message
            {
                IDCustomer = customerId,
                Content = messageContent,
                SendDate = DateTime.Now
                // Thêm các trường dữ liệu khác nếu cần
            };

            // Lưu tin nhắn vào cơ sở dữ liệu
            db.Message.Add(newMessage);
            db.SaveChanges();

            // Chuyển hướng lại đến trang MessEmployer với dữ liệu đã được cập nhật
            return RedirectToAction("MessEmployer", new { customerId });
        
    }


}
}