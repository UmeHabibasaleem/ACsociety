using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocietyOfAC.Models;
using System.IO;

namespace SocietyOfAC.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }
        /*  [HttpPost]
          public ActionResult Index(UserClass imageModel )
          {
              string filename = Path.GetFileNameWithoutExtension(imageModel.uploadfile.FileName);
              string Extenttion = Path.GetExtension(imageModel.uploadfile.FileName);
              filename = filename + DateTime.Now.ToString("yymmssfff" + Extenttion);
              imageModel.ImagePath = "~/User-Images" + filename;
              filename = Path.Combine(Server.MapPath("~/User-Images") , filename);
              imageModel.uploadfile.SaveAs(filename);
            /*  using (SocietyofACEntities db = new SocietyofACEntities())
              {
                  db.Registrations.Add(imageModel);
                  db.SaveChanges();
              }
              ModelState.Clear();
                  return View(); 
          }*/
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
                ViewBag.Message = "File uploaded successfully.";
            }

            return View();
        }
    }
}