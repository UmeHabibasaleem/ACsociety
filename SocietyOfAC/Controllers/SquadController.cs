using SocietyOfAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocietyOfAC.Controllers
{
    public class SquadController : Controller
    {
        // GET: Squad
        public ActionResult Index()
        {
            SocietyofACEntities1 db = new SocietyofACEntities1();
            List<Registration> list = db.Registrations.ToList();
            List<UserClass> viewList = new List<UserClass>();
            foreach (Registration reg in list)
            {
                if (reg.RegistrationNumber[3] == 54)
                {
                    UserClass user = new UserClass();
                    user.RegistrationNumber = reg.RegistrationNumber;
                    user.Name = reg.Name;
                    user.Email = reg.Email;
                    user.ImagePath = reg.Imagepath;
                    viewList.Add(user);
                }
            }

            return View(viewList);
        }
        public ActionResult MemberRecord()
        {
            return View();
        }

        public ActionResult Details(string RegistrationNumber)
        {
            using (SocietyofACEntities1 db = new SocietyofACEntities1())
            {
                return View(db.Registrations.Where(x => x.RegistrationNumber.Trim() == RegistrationNumber.Trim()).FirstOrDefault());
            }
        }


        public ActionResult Delete(string RegistrationNumber)
        {
            try

            {
                using (SocietyofACEntities1 db = new SocietyofACEntities1())
                {

                    return View(db.Registrations.Where(x => x.RegistrationNumber == RegistrationNumber).FirstOrDefault());
                }

            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult Delete(string RegistrationNumber, FormCollection collection)
        {
            try

            {
                using (SocietyofACEntities1 db = new SocietyofACEntities1())
                {
                    Registration reg = db.Registrations.Where(x => x.RegistrationNumber == RegistrationNumber).FirstOrDefault();
                    db.Registrations.Remove(reg);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }


            catch
            {
                return View();
            }
        }
    }


}
