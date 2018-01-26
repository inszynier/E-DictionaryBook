using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_DictionaryBook.Controllers
{
    public class TeacherController : Controller
    {
        [Authorize(Roles = "Admin, Teacher")]
        // GET: Teacher
        public ActionResult Home()
        {
            return View();
        }
    }
}