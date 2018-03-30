using AE.Net.Mail;
using E_DictionaryBook.Areas.Email.Models;
using E_DictionaryBook.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace E_DictionaryBook.Areas.Email.Controllers
{
    ///<summary>
    ///This class is used to receive emails from server.
    ///</summary>
    public class ReceiveController : Controller
    {
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

        ///<summary>
        ///This method is used to receive email from server for authorized users.
        ///</summary>
        // GET: Email/Receive
        [HttpGet]
        [Authorize]
        public ActionResult ReceiveEmail()
        {
            //We find user which is logged now
            var user = UserManager.FindById(User.Identity.GetUserId());
            var userEmail = user.Email;
            var userName = User.Identity.GetUserName();

            //We read password of our user from cookie - this cookie is created when the user is logged to application
            string PasswordCookie = Request.Cookies["PasswordCookie"].Value; 
            // Connect to the IMAP server. The 'true' parameter specifies to use SSL
            // which is important (for Gmail at least)
            ImapClient ic = new ImapClient("127.0.0.1", userEmail, PasswordCookie, AuthMethods.Login ,143
                           , false);
            // Select a mailbox. Case-insensitive
            ic.SelectMailbox("INBOX");
            // Get the first *11* messages. 0 is the first message;
            // and it also includes the 10th message, which is really the eleventh ;)
            // MailMessage represents, well, a message in your mailbox
            //This is list which has all messages from inbox from email server for logged user
            List<EmailVM> ListOfMessages = new List<EmailVM>();
            MailMessage[] mm = ic.GetMessages(0,50,false);
            //We rewrite messages from IMAPX lib model class to our model class which is used in our view.
            foreach (MailMessage m in mm)
            {
                var ccUsers = m.Cc.ToArray(); //CCusers is array in our view so we have to convert ccusers from impax to this array
                EDictionaryUser[] EmailCCUsers = new EDictionaryUser[m.Cc.Count];
                for(int EmailCCUserNumber = 0; EmailCCUserNumber < m.Cc.Count; EmailCCUserNumber++)
                {
                    EmailCCUsers[EmailCCUserNumber] = new EDictionaryUser() { Email = ccUsers[EmailCCUserNumber].Address};
                };
                ListOfMessages.Add(new EmailVM() { CClist = EmailCCUsers, To = m.To.ToString(), From = m.From.Address, Body = m.Body.ToString(), Subject = m.Subject.ToString(), Date = m.Date });
            }
            //We diconnected with email server
            ic.Dispose();
            //Redirect to receive email view
            return View("ReceiveEmail", ListOfMessages);
        }

        ///<summary>
        ///This method is used to receive email from server for authorized users.
        ///</summary>
        // GET: Email/Receive
        [HttpPost]
        [Authorize]
        public ActionResult ReceiveEmail(int id)
        {
            //We find user which is logged now
            var user = UserManager.FindById(User.Identity.GetUserId());
            var userEmail = user.Email;
            var userName = User.Identity.GetUserName();

            //We read password of our user from cookie - this cookie is created when the user is logged to application
            string PasswordCookie = Request.Cookies["PasswordCookie"].Value;
            // Connect to the IMAP server. The 'true' parameter specifies to use SSL
            // which is important (for Gmail at least)
            ImapClient ic = new ImapClient("127.0.0.1", userEmail, PasswordCookie, AuthMethods.Login, 143
                           , false);
            // Select a mailbox. Case-insensitive
            ic.SelectMailbox("INBOX");
            // Get the first *11* messages. 0 is the first message;
            // and it also includes the 10th message, which is really the eleventh ;)
            // MailMessage represents, well, a message in your mailbox
            //This is list which has all messages from inbox from email server for logged user
            List<EmailVM> ListOfMessages = new List<EmailVM>();
            MailMessage[] mm = ic.GetMessages(0, 50, false);
            //We rewrite messages from IMAPX lib model class to our model class which is used in our view.
            foreach (MailMessage m in mm)
            {
                var ccUsers = m.Cc.ToArray(); //CCusers is array in our view so we have to convert ccusers from impax to this array
                EDictionaryUser[] EmailCCUsers = new EDictionaryUser[m.Cc.Count];
                for (int EmailCCUserNumber = 0; EmailCCUserNumber < m.Cc.Count; EmailCCUserNumber++)
                {
                    EmailCCUsers[EmailCCUserNumber] = new EDictionaryUser() { Email = ccUsers[EmailCCUserNumber].Address };
                };
                ListOfMessages.Add(new EmailVM() { CClist = EmailCCUsers, To = m.To.ToString(), From = m.From.Address, Body = m.Body.ToString(), Subject = m.Subject.ToString(), Date = m.Date });
            }
            //We diconnected with email server
            ic.Dispose();
            //Redirect to receive email view
            return View("ReceiveEmail", ListOfMessages);
        }
    }
}