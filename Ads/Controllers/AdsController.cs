using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Ads.Models;
using PagedList;

namespace Ads.Controllers
{
    public class AdsController : Controller
    {
        private AdContext db = new AdContext();
        List<Category> ListALL;
        // GET: Ads
        public ActionResult Index(int? page, string sortOrder)
        {
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var allAds = from s in db.Ads select s;
            switch (sortOrder)
            {
                case "name_desc":
                    allAds = allAds.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    allAds = allAds.OrderBy(s => s.DateOfInsert);
                    break;
                case "date_desc":
                    allAds = allAds.OrderByDescending(s => s.DateOfInsert);
                    break;
                default:
                    allAds = allAds.OrderBy(s => s.Title);
                    break;
            }

            var pageNumber = page ?? 1;
            int amountOfPages = Convert.ToInt16(Session["MyPagination"]);
            if (amountOfPages <= 0)
            {
                amountOfPages = 5;
            }
            var onePageOfAds = allAds.ToPagedList(pageNumber, amountOfPages);
            ViewBag.OnePageOfAds = onePageOfAds;

            return View(allAds.ToList());

        }

        public ActionResult ChosenAds(int? idAd)
        {
            var ads = db.Ads.Where(x => x.CategoryID == idAd);
            foreach (Category c in db.Categories)
            {
                if (c.CategoryID == idAd)
                {
                    ViewBag.Title = c.Name;
                }
            }
            return View(ads.ToList());
        }


        [HttpPost]
        public ActionResult Filter(string sortOrder, string keyWord, string Option)
        {
            var allAds = from s in db.Ads select s;

            //Filter
            List<Ad> wszystkie = new List<Ad> ();
            wszystkie = allAds.ToList();
            List<Ad> searchedAds = new List<Ad>();
            string[] searchedWords;
            int flag = 0;
            searchedWords = keyWord.Split(',');

            if (!string.IsNullOrEmpty(keyWord))
            {
                if (Option.CompareTo("Not") == 0) // jesli operator NOT
                {
                    for (int i = 0; i < wszystkie.Count; i++)
                    {
                        for (int j = 0; j < searchedWords.Length; j++)
                        {
                            if (!wszystkie[i].Content.ToLower().Contains(searchedWords[j]))
                            {
                                flag++;
                                if (flag == searchedWords.Length)
                                {
                                    searchedAds.Add(wszystkie[i]);
                                }
                            }
                            else { break; }
                        }
                        flag = 0;
                    }
                }
                else if (Option.CompareTo("Or") == 0)     //jezeli jest operator or
                {
                    for (int i = 0; i < wszystkie.Count; i++)
                    {
                        for (int j = 0; j < searchedWords.Length; j++)
                        {
                            if (wszystkie[i].Content.ToLower().Contains(searchedWords[j]))
                            {
                                searchedAds.Add(wszystkie[i]);
                            }
                        }
                    }
                }
                else if (string.IsNullOrEmpty(Option))
                {
                    for (int i = 0; i < wszystkie.Count; i++)
                    {
                        for (int j = 0; j < searchedWords.Length; j++)
                        {
                            if (wszystkie[i].Content.ToLower().Contains(searchedWords[j]))
                            {
                                flag++;
                                if (flag == searchedWords.Length)
                                {
                                    searchedAds.Add(wszystkie[i]);
                                }
                            }
                            else { break; }
                        }
                        flag = 0;
                    }
                }
                else //defaultowo jest operator And
                {
                    for (int i = 0; i < wszystkie.Count; i++)
                    {
                        for (int j = 0; j < searchedWords.Length; j++)
                        {
                            if (wszystkie[i].Content.ToLower().Contains(searchedWords[j]))
                            {
                                flag++;
                                if (flag == searchedWords.Length)
                                {
                                    searchedAds.Add(wszystkie[i]);
                                }
                            }
                            else { break; }
                        }
                        flag = 0;
                    }
                }
            }

            return View(searchedAds.ToList());
        }

        // GET: Ads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ad ad = db.Ads.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }



        private List<Category> dropdownlist = new List<Category>();
        private int counter = 1;

        public void Recursive_two(List<Category> c, int guard)
        {
            if (c != null)
            {
                for (int i = 0; i < c.Count; i++)
                {
                    dropdownlist.Add(new Category() { CategoryID = c[i].CategoryID, Name = new string('-', guard) + ">" + c[i].Name });
                    var query = ListALL.Where(x => x.ParentcategoryID == c[i].CategoryID);
                    Recursive_two(query.ToList(), guard + 4);
                }
            }
            counter = 1;
        }

        public void Recursive(List<Category> c)
        {
            foreach (Category cat in c)
            {
                if (cat.ParentcategoryID == null)
                {
                    dropdownlist.Add(new Category() { CategoryID = cat.CategoryID, Name = cat.Name });
                    var query = ListALL.Where(x => x.ParentcategoryID == cat.CategoryID);
                    Recursive_two(query.ToList(), counter);

                }
                else
                {

                    //abcd.Add(new Category() { CategoryID = cat.CategoryID, Name = "-> " + cat.Name });
                    /* for (int i = 0; i < abcd.Count; i++)
                     {

                         if (abcd[i].CategoryID == cat.ParentcategoryID)
                         {
                             for (int k = 0; k < abcd.Count; k++)
                             {

                                 if (abcd[k].CategoryID == cat.CategoryID)
                                 {
                                     counter++;
                                 }

                             }

                         }
                     }
                      if(counter==0)
                      {
                          abcd.Add(new Category() { CategoryID = cat.CategoryID, Name = "-> " + cat.Name });

                      }
                     counter = 0;*/
                }

            }
        }

        public ActionResult ChooseCategory()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var listAll = db.Categories;
            ListALL = listAll.ToList();
            Recursive(ListALL);
            ViewBag.CategoryID = new SelectList(dropdownlist, "CategoryID", "Name");
            return View();
        }

        [HttpPost]

        public ActionResult ChooseCategory([Bind(Include = "CategoryID,Name")]Category c)
        {
            //var abc = db.Attributes.Where(x => x.AttributeID == c.AttributeID);
            //if(abc!=null)
            int id = c.CategoryID;

            //List<Ads.Models.Attribute> attributesToSend = abc.ToList();
            //List<string> attributesToSend = new List<string>();
            //int counter = 0;
            //foreach(var v in attributes)
            //{
            //    counter++;
            //}
            //if (attributes != null)
            //{
            //    for (int i = 0; i < counter; i++)
            //    {
            //        attributesToSend.Add(attributes.ToList()[i].Name);
            //    }
            if (c == null)
            {
                return RedirectToAction("Index", "Home");
            }


            return RedirectToAction("Create", "Ads", new { category = id });
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Home");
            //}
        }

        // GET: Ads/Create
        public ActionResult Create(int category)
        {
            if (category == null)
            {
                return RedirectToAction("Login", "Account");
            }
            //Category catus = db.Categories.Find(category);
            var list = db.Attributes.Where(x => x.CategoryID == category).ToList();
            ViewBag.Attributes = list;
            ViewBag.CategoryID = category;
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Login");
            return View();
        }

        // POST: Ads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdID,Title,Content,DateOfInsert,CategoryID,UserID")] Ad ad, HttpPostedFileBase file, List<string> aaa)
        {
            // var query = db.Ads.Where(c => c.Name.ToLower() == ad.Name.ToLower());
            //var query = db.Companies.Where(c => c.Name.Contains("A")).OrderBy(c => c.Name);
            //var queryFW = db.ForbiddenWords;

            int count = 0;
            var queryFW = from c in db.ForbiddenWords
                          select c.Name;

            List<string> fWords = queryFW.ToList();

            for (int i = 0; i < fWords.Count; i++)
            {
                if (ad.Content.Contains(fWords[i]))
                {
                    count++;
                }
            }

            if (count != 0)
            {
                ModelState.AddModelError("", "Nie można dodać ogłoszenia, które zawiera słowa zakazane");
            }

            //--------------------------------Dodawanie multimediow ---------------------------

            var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));

            var pathString1 = Path.Combine(originalDirectory.ToString(), "Ads");
            var pathString2 = Path.Combine(originalDirectory.ToString(), "Ads" + ad.AdID.ToString());
            var pathString3 = Path.Combine(originalDirectory.ToString(), "Ads" + ad.AdID.ToString() + "\\Thumbs");
            var pathString4 = Path.Combine(originalDirectory.ToString(), "Ads" + ad.AdID.ToString() + "\\Galery");
            var pathString5 = Path.Combine(originalDirectory.ToString(), "Ads" + ad.AdID.ToString() + "\\Galery\\Thumbs");

            if (!Directory.Exists(pathString1)) Directory.CreateDirectory(pathString1);
            if (!Directory.Exists(pathString2)) Directory.CreateDirectory(pathString2);
            if (!Directory.Exists(pathString3)) Directory.CreateDirectory(pathString3);
            if (!Directory.Exists(pathString4)) Directory.CreateDirectory(pathString4);
            if (!Directory.Exists(pathString5)) Directory.CreateDirectory(pathString5);

            if (file != null && file.ContentLength > 0)
            {
                string ext = file.ContentType.ToLower();

                if (ext != "image/jpg" && ext != "image/jpeg" && ext != "image/pjpeg" && ext != "image/gif" && ext != "image/png")
                {
                    ModelState.AddModelError("", "Plik nie zostal załadowany - złe rozszerzenie");
                    return View(ad);
                }

                string imageName = file.FileName;
                ad.MediaPath = imageName;
                db.SaveChanges();

                var path = string.Format("{0}\\{1}", pathString2, imageName);
                var path2 = string.Format("{0}\\{1}", pathString3, imageName);

                file.SaveAs(path);

                WebImage img = new WebImage(file.InputStream);
                img.Resize(200, 200);
                img.Save(path2);
            }

            //-------------------Koniec : dodawanie multimediow------------

            var list = db.Attributes.Where(x => x.CategoryID == ad.CategoryID).ToList();

            if (aaa != null)
            {
                ad.Content += "\n";
                for (int i = 0; i < aaa.Count; i++)
                {
                    ad.Content += "\n" + list[i].Name + ": " + aaa[i];
                }

            }

            if (ModelState.IsValid)
            {
                ad.DateOfInsert = DateTime.Now;
                ad.UserID = Convert.ToInt16(Session["UserID"]);
                db.Ads.Add(ad);
                db.SaveChanges();
                // return RedirectToAction("Create");
                return RedirectToAction("Showsubcategories", "Categories", new { id = ad.CategoryID });
            }
            else
            {
                return RedirectToAction("ForbiddenError");
            }
            
                
            
           /* ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", ad.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Login", ad.UserID);

            return View(ad);*/
        }

        public ActionResult ForbiddenError()
        {
            return View();
        }

        // GET: Ads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ad ad = db.Ads.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "Login", ad.UserID);
            return View(ad);
        }

        // POST: Ads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdID,Title,Content,DateOfInsert,CategoryID,UserID")] Ad ad, HttpPostedFileBase file)
        {
            //--------------------------------Dodawanie multimediow ---------------------------

            var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));

            var pathString1 = Path.Combine(originalDirectory.ToString(), "Ads");
            var pathString2 = Path.Combine(originalDirectory.ToString(), "Ads" + ad.AdID.ToString());
            var pathString3 = Path.Combine(originalDirectory.ToString(), "Ads" + ad.AdID.ToString() + "\\Thumbs");
            var pathString4 = Path.Combine(originalDirectory.ToString(), "Ads" + ad.AdID.ToString() + "\\Galery");
            var pathString5 = Path.Combine(originalDirectory.ToString(), "Ads" + ad.AdID.ToString() + "\\Galery\\Thumbs");

            if (!Directory.Exists(pathString1)) Directory.CreateDirectory(pathString1);
            if (!Directory.Exists(pathString2)) Directory.CreateDirectory(pathString2);
            if (!Directory.Exists(pathString3)) Directory.CreateDirectory(pathString3);
            if (!Directory.Exists(pathString4)) Directory.CreateDirectory(pathString4);
            if (!Directory.Exists(pathString5)) Directory.CreateDirectory(pathString5);

            if (file != null && file.ContentLength > 0)
            {
                string ext = file.ContentType.ToLower();

                if (ext != "image/jpg" && ext != "image/jpeg" && ext != "image/pjpeg" && ext != "image/gif" && ext != "image/png")
                {
                    ModelState.AddModelError("", "Plik nie zostal załadowany - złe rozszerzenie");
                    return View(ad);
                }

                string imageName = file.FileName;
                ad.MediaPath = imageName;
                db.SaveChanges();

                var path = string.Format("{0}\\{1}", pathString2, imageName);
                var path2 = string.Format("{0}\\{1}", pathString3, imageName);

                file.SaveAs(path);

                WebImage img = new WebImage(file.InputStream);
                img.Resize(200, 200);
                img.Save(path2);
            }
            /////////////////////////
            if (ModelState.IsValid)
            {
                ad.DateOfInsert = DateTime.Now;
                ad.UserID = Convert.ToInt16(Session["UserID"]);
                db.Entry(ad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "Login", ad.UserID);

            return View(ad);
        }

        // GET: Ads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ad ad = db.Ads.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ad ad = db.Ads.Find(id);
            db.Ads.Remove(ad);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SendToModeration(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ad ad = db.Ads.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("SendToModeration")]
        [ValidateAntiForgeryToken]
        public ActionResult SendToModerationConfirmed(int id)
        {
            Ad ad = db.Ads.Find(id);
            ad.IsModerated = true;
            //  db.Entry(ad).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ModeratedAds()
        {
            var ads = db.Ads.Include(a => a.Category).Include(a => a.User);
            var moderatedAds = db.Ads.Where(x => x.IsModerated == true);
            return View(moderatedAds.ToList());

            // return View(db.Ads.ToList());
        }



        // GET: Ads/Edit/5
        public ActionResult Moderate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ad ad = db.Ads.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }

            //ad.MediaPath =(Server.MapPath("~/Images/Uploads/Ads/0/Gallery/Thumbs"))
            //                .Select(fn=> Path.GetFileName(fn));
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Login", ad.UserID);
            return View(ad);
        }

        // POST: Ads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Moderate([Bind(Include = "AdID,Title,Content,DateOfInsert,CategoryID,UserID")] Ad ad)// HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                ad.IsModerated = false;
                ad.UserID = ad.UserID;
                db.Entry(ad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Login", ad.UserID);

            return View(ad);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ShowAd(int? id, string media)
        {
            var viewsCounter = HttpContext.Application;
            Ad tmp=null;
            foreach (Ad ad in db.Ads)
            {
                if (ad.AdID == id)
                {
                    ad.VisitCounter++;
                    tmp = ad;
                    ViewBag.Title = ad.Title;
                    ViewBag.MediaPath = media;
                    viewsCounter["counter"] = ad.VisitCounter;
                }

            }
            ViewBag.Counter = viewsCounter["counter"];
            db.SaveChanges();
            return View(tmp);

        }



    }
}
