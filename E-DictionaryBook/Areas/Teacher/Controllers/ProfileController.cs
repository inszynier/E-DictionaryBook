using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_DictionaryBook.Areas.Teacher.Controllers
{
    public class ProfileController : Controller
    {
        [Authorize(Roles = "Admin, Teacher")]
        // GET: Teacher/Profile
        public ActionResult Account()
        {
            return View();
        }
    }
}