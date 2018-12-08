using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocietyOfAC.Controllers
{
    public class AboutUsController : Controller
    {
        // GET: AboutUs
        public ActionResult Motivation()
        {
            return View();
        }
        
    }
}