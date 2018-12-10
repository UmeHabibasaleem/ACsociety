using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity;

namespace SocietyOfAC.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Events()
        {
            SocietyofACEntities1 db = new SocietyofACEntities1();
            return View(db.upload_image.ToList());
        }


        [HttpGet]
        public ActionResult AddEvents()
        {
            return View();

        }
        [HttpPost]
        public ActionResult AddEvents(upload_image imageModel)
        {
            try
            {
                if (imageModel.ImageFile != null)
                {

                    string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
                    string extension = Path.GetExtension(imageModel.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    imageModel.img_path = "~/User-Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/User-Images/"), fileName);
                    imageModel.ImageFile.SaveAs(fileName);

                }
                using (SocietyofACEntities1 db = new SocietyofACEntities1())
                {
                    db.upload_image.Add(imageModel);
                    db.SaveChanges();
                }
                ModelState.Clear();
                return View();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Details(int id)
        {
            using (SocietyofACEntities1 db = new SocietyofACEntities1())
            {
                return View(db.upload_image.Where(x => x.id == id).FirstOrDefault());
            }
        }

        public ActionResult Edit(int id)
        {
            using (SocietyofACEntities1 db = new SocietyofACEntities1())
            {
                return View(db.upload_image.Where(x => x.id == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult Edit(int id, upload_image img)
        {
            try
            {
                if (img.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(img.ImageFile.FileName);
                    string extension = Path.GetExtension(img.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    img.img_path = "~/User-Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/User-Images/"), fileName);
                    img.ImageFile.SaveAs(fileName);
                }
                using (SocietyofACEntities1 db = new SocietyofACEntities1())
                {
                    db.Entry(img).State = EntityState.Modified;
                    db.SaveChanges();

                }
                return RedirectToAction("Events");

            }
            catch
            {
                return View();
            }

        }

        public ActionResult Delete(int id)
        {
            try

            {
                using (SocietyofACEntities1 db = new SocietyofACEntities1())
                {

                    return View(db.upload_image.Where(x => x.id == id).FirstOrDefault());
                }

            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try

            {
                using (SocietyofACEntities1 db = new SocietyofACEntities1())
                {
                    upload_image img = db.upload_image.Where(x => x.id == id).FirstOrDefault();
                    db.upload_image.Remove(img);
                    db.SaveChanges();
                }
                return RedirectToAction("Events");
            }


            catch
            {
                return View();
            }
        }
    }
}