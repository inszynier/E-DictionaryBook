﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_DictionaryBook.Controllers
{
    public class AdminController : Controller
    {
        //[Authorize(Roles = "Admin")]
        // GET: Admin
        public ActionResult Home()
        {
            return View();
        }
    }
}