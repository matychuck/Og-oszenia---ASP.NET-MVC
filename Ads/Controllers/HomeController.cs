using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Ads.Models;

namespace Ads.Controllers
{
    public class HomeController : Controller
    {
        private AdContext db = new AdContext();
        public ActionResult Index()
        {
            return View(db.AdminMessages.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Change(String LanguageAbbreviation)
        {
            if(LanguageAbbreviation != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbreviation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbreviation);
            }

            HttpCookie cookie = new HttpCookie("Language");
            cookie.Expires = DateTime.Now.AddDays(1);
            cookie.Value = LanguageAbbreviation;
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index");
        }

        public ActionResult SetTheme(String ThemeName)
        {
            HttpCookie cookie = new HttpCookie("Theme");
            cookie.Expires = DateTime.Now.AddDays(1);
            cookie.Value = ThemeName;
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index");
        }


    }
}