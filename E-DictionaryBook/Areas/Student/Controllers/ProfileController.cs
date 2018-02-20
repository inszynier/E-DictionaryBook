using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_DictionaryBook.Areas.Student.Controllers
{
    public class ProfileController : Controller
    {
        [Authorize(Roles = "Admin, Student, Teacher")]
        // GET: Student/Profile
        public ActionResult Account()
        {
            return View();
        }
    }
}