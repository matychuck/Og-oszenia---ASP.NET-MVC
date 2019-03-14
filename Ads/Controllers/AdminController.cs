using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ads.Models;

namespace Ads.Controllers
{
    public class AdminController : Controller
    {
        private AdContext db = new AdContext();

        // GET: Admin
        public ActionResult Index()
        {   
            return View();
        }

      
    }
}
