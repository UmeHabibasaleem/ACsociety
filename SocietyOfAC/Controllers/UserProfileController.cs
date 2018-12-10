using Microsoft.AspNet.Identity;
using SocietyOfAC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocietyOfAC.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfile

        public ActionResult Index()
        {
            /*Session["Email"] = u1.Email;
            SocietyofACEntities db = new SocietyofACEntities();
            List<Registration> list = db.Registrations.ToList();
            foreach(Registration reg in list)
            {
                if(reg.Email == u1.Email)
                {
                    return View(reg);
                }
            }*/
            string displayimg = (string)Session["Email"];
            using (SocietyofACEntities1 db = new SocietyofACEntities1())
            {
                return View(db.Registrations.Where(x => x.Email.Trim() == displayimg.Trim()).FirstOrDefault());
            }


        }

        // GET: UserProfile/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserProfile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserProfile/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserProfile/Edit/5
        public ActionResult Edit(string Email)
        {
            using (SocietyofACEntities1 db = new SocietyofACEntities1())
            {
                return View(db.Registrations.Where(x => x.Email.Trim() == Email.Trim()).FirstOrDefault());
            }

        }

        // POST: UserProfile/Edit/5
        [HttpPost]
        public ActionResult Edit(string Email, Registration reg, HttpPostedFileBase postedFile)
        {
            try
            {
                // TODO: Add update logic here
                if (postedFile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(postedFile.FileName);
                    string Extenttion = Path.GetExtension(postedFile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + Extenttion;
                    reg.Imagepath = "~/User-Images/" + filename;
                    filename = Path.Combine(Server.MapPath("~/User-Images/"), filename);
                    postedFile.SaveAs(filename);
                }
                using (SocietyofACEntities1 db = new SocietyofACEntities1())
                {
                    db.Entry(reg).State = EntityState.Modified;
                    db.SaveChanges();

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserProfile/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserProfile/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
