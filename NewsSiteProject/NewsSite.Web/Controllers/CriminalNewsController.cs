﻿namespace NewsSite.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class CriminalNewsController : Controller
    {
        // GET: CriminalNews
        public ActionResult Index()
        {
            return View();
        }
    }
}