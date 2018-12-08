using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocietyOfAC.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace SocietyOfAC.Controllers
{
    public class RegistrationController : Controller
    {
        SocietyofACEntities1 db = new SocietyofACEntities1();
        // GET: Registration
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
                 [HttpPost]
                [ValidateAntiForgeryToken]
                [AllowAnonymous]
                      public ActionResult Index(UserClass US,HttpPostedFileBase postedFile)
                      {
                          try
                          {
                              bool check = true;
                              List<Registration> list = db.Registrations.ToList();
                                  if (list != null)
                                  {
                                      foreach (Registration re in list)
                                      {
                                          string R_N = re.RegistrationNumber.Trim();
                                          if (R_N == US.RegistrationNumber)
                                          {
                                              check = false;
                                          }
                                         
                                      }
                                  }
                                  if (check == true)
                                  {
                                      Registration reg = new Registration();
                                      reg.RegistrationNumber = US.RegistrationNumber.Trim();
                                      reg.Name = US.Name.Trim();
                                      reg.Email = US.Email.Trim();
                                      reg.Password = US.Password.Trim();
                                      reg.Conform_Password = US.Conform_password.Trim();
                                      if (postedFile != null && postedFile.ContentLength > 0)
                                      {
                                          string filename = Path.GetFileNameWithoutExtension(postedFile.FileName);
                                          string Extenttion = Path.GetExtension(postedFile.FileName);
                                          filename = filename + DateTime.Now.ToString("yymmssfff") + Extenttion;
                                          US.ImagePath = "~/User-Images/" + filename;
                                          filename = Path.Combine(Server.MapPath("~/User-Images/"), filename);
                                          postedFile.SaveAs(filename);
                                          reg.Imagepath = US.ImagePath;
                                      }
                                      using (SocietyofACEntities1 db = new SocietyofACEntities1())
                                      {
                                          db.Registrations.Add(reg);
                                          db.SaveChanges();
                                      }
                                  }
                                  if (check == false)
                                  {
                                      ViewBag.message = "Registration number already exist.please enter another one.";
                                  }
                                  if (list == null)
                                  {
                                      Registration reg = new Registration();
                                      reg.RegistrationNumber = US.RegistrationNumber;
                                      reg.Name = US.Name;
                                      reg.Email = US.Email;
                                      reg.Password = US.Password;
                                      reg.Conform_Password = US.Conform_password;
                                      if (postedFile != null && postedFile.ContentLength > 0)
                                      {
                                          string filename = Path.GetFileNameWithoutExtension(postedFile.FileName);
                                          string Extenttion = Path.GetExtension(postedFile.FileName);
                                          filename = filename + DateTime.Now.ToString("yymmssfff") + Extenttion;
                                          US.ImagePath = "~/User-Images/" + filename;
                                          filename = Path.Combine(Server.MapPath("~/User-Images/"), filename);
                                          postedFile.SaveAs(filename);
                                          reg.Imagepath = US.ImagePath;
                                      }
                                      using (SocietyofACEntities1 db = new SocietyofACEntities1())
                                      {
                                          db.Registrations.Add(reg);
                                          db.SaveChanges();
                                      }
                                  }
                                  ModelState.Clear();
                              



                          }
                          catch(Exception ex)
                          {
                              return View();
                          }
                          return View();
                      } 
 
    

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

            public ActionResult Login(UserClass u1)
            {
                try
                {
                   
                Session["Email"] = u1.Email;
                List<Registration> list = db.Registrations.ToList();
                Registration rg = new Registration();
                rg = list.ElementAt(list.Count() - 1);
                if (rg.Email == u1.Email && rg.Password == u1.Password)
                {
                    Logout.logout = 1;
                    return RedirectToAction("Home", "AdminHome");

                }
                if (u1.Email != null && u1.Password != null)
                    {
                    foreach (Registration R in list)
                    {
                        
                        if (R.Email == u1.Email && R.Password == u1.Password)
                        {
                            Logout.logout = 1;
                            return RedirectToAction("Applymembership", "Membership");
                        }
                        if (R.Email != u1.Email || R.Password != u1.Password)
                        {
                            ViewBag.msg = "Incorrect email or password";
                        }
                    }
                }
              }
                catch(Exception ex)
                {
                    return View();
                }
            return View();
            }
        public ActionResult LogOut()
        {
            Logout.logout = 0;
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("index", "Home", null);
        }
   

    }
}
