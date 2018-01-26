using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_DictionaryBook.Controllers
{
    
    public class StudentController : Controller
    {
        [Authorize(Roles = "Admin, Student, Teacher")]
        // GET: Student
        public ActionResult Home()
        {
            return View();
        }
    }
}