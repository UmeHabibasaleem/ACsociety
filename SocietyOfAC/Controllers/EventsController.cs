using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
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
            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            imageModel.img_path = "~/User-Images/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/User-Images/"), fileName);
            imageModel.ImageFile.SaveAs(fileName);
            using (SocietyofACEntities1 db = new SocietyofACEntities1())
            {
                db.upload_image.Add(imageModel);
                db.SaveChanges();
            }
            ModelState.Clear();
            return View();
        }
    }
}