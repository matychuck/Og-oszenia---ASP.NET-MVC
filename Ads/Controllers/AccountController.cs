using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using Ads.Models;
using System.Data;
using System.Data.Entity;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Data.Entity.Validation;

namespace Ads.Controllers
{
    
    public class AccountController : Controller
    {
        
        // GET: Account
        private AdContext db = new AdContext();

        public ActionResult Index()
        {
            return View(db.Users.ToList()); //lista zarejestrowanych
            //return View();
        }

        public ActionResult ViewMyProfile()
        {
            if(Session["UserID"]==null)
            {
                return RedirectToAction("Login","Account");
            }
            int userID = Convert.ToInt16(Session["UserID"]);
            var ads = db.Ads.Where(x => x.UserID == userID);
            User user = db.Users.Find(userID);
            ViewBag.currentUser = user;
            ViewBag.myAds = ads.ToList();
            return View(user);
        }

        public ActionResult DeleteMyAd(int? id)
        {
            Ad toDelete = db.Ads.Find(id);
            db.Ads.Remove(toDelete);
            db.SaveChanges();
            return RedirectToAction("ViewMyProfile");
        }

        public ActionResult Logout()
        {
            Session["UserID"] = null;
            Session["Admin"] = null;
            return RedirectToAction("Index","Home");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Login,Name,Surname,Phone,Password,ConfirmPassword,IsActive,IsAdmin,MyPagination")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewMyProfile");
            }
            return View(user);
        }

        //Rejestracja

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.Message = user.Name + " " + user.Surname + "  ZAREJESTROWANY";
            return RedirectToAction("Index", "Home");
        }


        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string oldPassword,string newPassword)
        {
            User user = db.Users.Find(Convert.ToInt16(Session["UserID"]));
            if(user.Password != oldPassword)
            {
                return RedirectToAction("ViewMyProfile"); 
            }
            //user.ActivationCode = Guid.NewGuid().ToString();
            //db.Entry(user).State = EntityState.Modified;
            //db.SaveChanges();
            SendVerificationLinkEmail(user.Login, user.UserID, user.Name, newPassword);
            return View();
        }

        [NonAction]
        public void SendVerificationLinkEmail(string email, int id, string firstName, string newPassword)
        {
            ///api / person ? firstName = john & lastName = doe
            var verifyUrl = "/Account/VerifyPassword/?id=" + id + "&pass="+newPassword;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            string subject = "Twoje hasło zostało pomyślnie zmienione!";

            string body = "Twoje hasło zostało pomyślnie zmienione.<br/>" +
                "Aby zatwierdzić zmiane hasła kliknij w poniższy link aktywacyjny:" +
                " <br/><a href='" + link + "'>" + link + "</a> ";

            Email.SendEmail(subject, body, email);
            

        }

        [HttpGet]
        public ActionResult VerifyPassword(string id,string pass)
        {
            bool Status = false;

            User user = db.Users.Find(Convert.ToInt16(id));
            if (user!=null)
            {
                user.ActivationCode = id;
                
                user.Password = pass;
                user.ConfirmPassword = pass;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                Session["UserID"] = null;
                Session["Admin"] = null;
                Status = true;
            }
            else
            {
                ViewBag.Message = "Nieprawidłowe żądanie";
            }

            ViewBag.Status = Status;
            return View();
        }
        public ActionResult RemindPassword()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemindPassword(string emailToSend)
        {
            string subject = "Przypomnienie hasła";
            
            User tmp = null;
            foreach (User user in db.Users)
            {
                if (user.Login == emailToSend)
                {
                    tmp = user;
                }
            }
            string body = "<br> Oto twoje hasło w serwisie ogłoszeń: " + tmp.Password + "<br> Jeśli to nie Ty wysyłałeś prośbę o przypomnienie hasła, zignoruj tego e-maila lub skontaktuj się z administratorem strony.<br>Pozdrawiamy, serwis Ogłoszenia";
            Email.SendEmail(subject,body, emailToSend);
            /*
            if(!emailToSend.Contains("@"))
            {
                ModelState.AddModelError("", "Nie podałeś e-maila");
            }
            //var Password = db.Users.Where(x => x.Login == emailToSend);
            User tmp = null;
            foreach(User user in db.Users)
            {
                if(user.Login==emailToSend)
                {
                    tmp = user;
                }
            }
             
            if (tmp!=null)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
               System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add(new MailAddress(emailToSend));  // replace with valid value 
                message.From = new MailAddress("ogloszenia.localhost@wp.pl");  // replace with valid value
                message.Subject = "Przypomnienie hasła do serwisu Ogłoszenia";
                message.Body = "Oto twoje hasło: " + tmp.Password;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                //If you need to authenticate
                client.Credentials = new NetworkCredential("ogloszenia.localhost@wp.pl", "12qwaszx");
                
                    client.Host = "smtp.wp.pl";
                    client.Port = 465;
                    client.EnableSsl = true;
                await client.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }

            

            */
            return RedirectToAction("Sent"); 
        }

        public ActionResult Sent()
        {
            return View();
        }

        //Logowanie

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            bool ifExists = false;
            bool wrongPassword = false;
            bool wrongLogin = false;
            User usr=new User();// = db.Users.Single(u => u.Login == user.Login && u.Password == user.Password);
            if (user != null)
            {
                foreach (User tmp in db.Users)
                {
                    if(user.Login == tmp.Login)
                    {
                        ifExists = true;
                        usr.Login = user.Login;
                    }
                    else
                    {
                        wrongLogin = true;
                    }
                    if(ifExists)
                    {
                        if (user.Password == tmp.Password)
                        {
                            wrongLogin = false;
                            wrongPassword = false;
                            usr.Password = user.Password;
                            Session["UserID"] = tmp.UserID.ToString();
                            Session["Email"] = tmp.Login.ToString();
                            Session["Name"] = tmp.Name.ToString();
                            Session["Surname"] = tmp.Surname.ToString();
                            Session["Admin"] = tmp.IsAdmin;
                            Session["MyPagination"] = tmp.MyPagination.ToString();

                            if(Convert.ToBoolean(Session["Admin"])==true)
                            {
                                return this.RedirectToAction("Index", "Admin");
                            }
                                return this.RedirectToAction("Index","Home");
                        }
                        else
                        {
                            wrongLogin = false;
                            wrongPassword = true;
                        }
                        break;
                    }
                    
                }
                if(wrongLogin)
                {
                    ModelState.AddModelError("", "Zly Login");
                }
                if (wrongPassword)
                {
                    ModelState.AddModelError("", "Złe Hasło");
                }
            }
            
            return View();
        }

        public ActionResult LoggedIn()
        {
           if( Session["UserID"] != null)
           {
                return View();
           }
           else
           {
                return RedirectToAction("Login");
           }
          
        }




    }
}