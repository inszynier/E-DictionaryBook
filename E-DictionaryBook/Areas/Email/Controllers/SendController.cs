using E_DictionaryBook.Areas.Email.Models;
using E_DictionaryBook.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace E_DictionaryBook.Areas.Email.Controllers
{
    /////<summary>
    ///This class is used to send emails to server.
    ///</summary>
    public class SendController : Controller
    {
        ///<summary>
        ///This method is used to send email to server for authorized users.
        ///</summary>
        // GET: Email/Send
        ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null && HttpContext == null)
                {
                    return new ApplicationUserManager(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(db));
                }
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        [Authorize]
        [HttpPost]
        public void SendEmail(EmailVM emailParameter)
        {
            //We find user which is logged now
            var user = UserManager.FindById(User.Identity.GetUserId());
            var userEmail = user.Email;
            var userName = User.Identity.GetUserName();
            //We create a email model which is send to email server
            MailAddress from = new MailAddress(userEmail, userName);
            MailAddress to = new MailAddress(emailParameter.To);
            
            MailMessage message = new MailMessage(from, to);
            message.Subject = emailParameter.Subject;
            message.Body = emailParameter.Body;
            //We create mailaddresses for ccusers like for "from" or "to" users
            for(int FirstCCUserNumber = 0 ; FirstCCUserNumber < emailParameter.CClist.Length; FirstCCUserNumber++)
            {
                MailAddress copy = new MailAddress(emailParameter.CClist[FirstCCUserNumber].Email);
                message.CC.Add(copy);
            }
            //We connect with email server
            SmtpClient mailer = new SmtpClient("127.0.0.1", 25);

            //We read password of our user from cookie - this cookie is created when the user is logged to application
            string PasswordCookie = Request.Cookies["PasswordCookie"].Value;
            mailer.Credentials = new NetworkCredential(userEmail, PasswordCookie); 
            //mailer.EnableSsl = true;
            mailer.EnableSsl = false;
            mailer.Send(message);
            // we redirect to receive email when we was sended email
            Response.Redirect("~/Email/Receive/ReceiveEmail");
            //return RedirectToAction("ReceiveEmail", "Receive");;
        }
        
        [Authorize]
        [HttpGet]
        public ActionResult SendEmail()
        {
            //We find user which is logged now
            //var user = UserManager.FindById(User.Identity.GetUserId());
            //var userEmail = user.Email;
            //var userName = User.Identity.GetUserName();

            //MailAddress from = new MailAddress(userEmail, userName);
            //MailAddress to = new MailAddress("studenttestdictionarybook@gmail.com");
            //MailMessage message = new MailMessage(from, to);

            //message.Subject = "What Up, Dog?";
            //message.Body = "Why you gotta be all up in my grill?";
            //SmtpClient mailer = new SmtpClient("smtp.gmail.com", 587);
            //mailer.Credentials = new NetworkCredential("studenttestdictionarybook@gmail.com", "pindol1993");
            //mailer.EnableSsl = true;
            //mailer.Send(message);
            
            //We return default send form which user fill with data ( This is form of send email)
            return View();
        }

    }
}