using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_DictionaryBook.Areas.Admin.Controllers
{
    public class ProfileController : Controller
    {
        [Authorize(Roles = "Admin")]
        // GET: Admin/Profile
        public ActionResult Account()
        {
            return View();
        }
    }
}