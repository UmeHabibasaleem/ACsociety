using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocietyOfAC.Models;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc.Html;
using System.Net;


namespace SocietyOfAC.Controllers
{
    public class MemberShipController : Controller

    {
        SocietyofACEntities1 db = new SocietyofACEntities1();
        // GET: MemberShip
        static string messageRecord= "";
     
        [HttpGet]
        public ActionResult ApplymembershipForm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplymembershipForm(MemberShip mem)
        {
            try
            {
                if(mem != null)
                {
                    string Registrationno = " start";
                    List<MemberShip> list1 = db.MemberShips.ToList();
                    List<Registration> list = db.Registrations.ToList();
                    var usermail = (string)Session["Email"];

                    foreach (Registration reg in list)
                    {
                        if (reg.Email == usermail)
                        {
                            Registrationno = reg.RegistrationNumber;
                            if (list1.Count == 0)
                            {
                                mem.RegistrationNumber = reg.RegistrationNumber;
                                mem.status = false;
                                db.MemberShips.Add(mem);
                                db.SaveChanges();
                                ModelState.Clear();
                            }
                            else
                            {
                              var Exist =  db.MemberShips.Where(x => x.RegistrationNumber == Registrationno).SingleOrDefault();
                               
                                    if (Exist == null)
                                    {
                                        mem.RegistrationNumber = reg.RegistrationNumber;mem.status = false;
                                        db.MemberShips.Add(mem);
                                        db.SaveChanges();
                                        ModelState.Clear();
                                    }
                                    else
                                    {
                                    ViewBag.message = "You are already applied for membership";
                                   }
                                
                            }
                        }

                    }

                }

                else
                {
                    ViewBag.message = "Please Fill this form";
                    return View();
                }


            }
            catch (Exception ex)
            {
                return View();
            }
            ViewBag.Uppermessage = "Your request has been sent successfully";
            return View();
        }

        [HttpGet]
        public ActionResult Applymembership()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Applymembership(string submit)
        {
            if (Logout.logout == 1)
            {
               return  RedirectToAction("ApplymembershipForm");
            }
            ViewBag.Message = "In order to apply firstly registered your self.";
            return View();

        }
        [HttpGet]
        public ActionResult Approvedmembership()
        {
            List<MemberShip> list1 = db.MemberShips.ToList();
            List<Registration> list2 = db.Registrations.ToList();
            List<MemberShip> listofrequestedpeople = new List<MemberShip>();
            HoldingClass HC = new HoldingClass();
            foreach(MemberShip m in list1)
            {
                if(m.status == false)
                {
                    listofrequestedpeople.Add(m);
                }
            }

            if (list1 != null)
            {
                foreach (MemberShip mem in listofrequestedpeople)
                {
                    RequestClass RC = new RequestClass();
                    foreach (Registration reg in list2)
                    {

                        if (mem.RegistrationNumber == reg.RegistrationNumber)
                        {
                            RC.RegistrationNumber = mem.RegistrationNumber;
                            RC.Name = reg.Name;
                            RC.Email = reg.Email;
                            RC.Team = mem.Team;
                            HC.RQLIST1.Add(RC);

                        }
                    }
                }
            }
            if(messageRecord != null)
            {
                ViewBag.EmailSend = messageRecord;
            }
            return View(HC.RQLIST);
        }
        [HttpPost]
        public ActionResult Approvedmembership(string []tags)
        {
            try
            {
                int length = tags.Count();

                List<MemberShip> list = db.MemberShips.ToList();
                foreach (MemberShip ms in list)
                {
                    for (int i = 0; i < length; i++)
                    {
                        if (tags[i] == ms.RegistrationNumber)
                        {
                            ms.status = true;
                        }
                    }
                    db.SaveChanges();
                }
               return RedirectToAction("Contact");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Approvedmembership");
            }
            
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
 
        [HttpPost] 
        public async Task<ActionResult> Contact(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                List<string> emailList = new List<string>();
                string mail = " ";
                List<MemberShip> list1 = db.MemberShips.ToList();
                List<MemberShip> list2 = new List<MemberShip>();
                List<Registration> list3 = db.Registrations.ToList();
                foreach (MemberShip ms in list1)
                {
                   MemberShip mem = new MemberShip();
                   if(ms.status == true && (ms.emailsend == null || ms.emailsend == false))
                    {
                        RequestClass RC = new RequestClass();
                        foreach (Registration reg in list3)
                        {
                           if (ms.RegistrationNumber == reg.RegistrationNumber)
                            {
                                mail = reg.Email.Trim();
                                emailList.Add(mail);
                               // ms.emailsend = true;
                            }
                        }
                    } 
                }
                var message = new MailMessage();

                var emails = String.Join(",", emailList);
                message.To.Add(emails);
                message.From = new MailAddress("umehabib.cs@gmail.com"); 
                message.Subject = "MemberShip Appproval";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    var credential = new System.Net.NetworkCredential
                    {
                       UserName = "umehabib.cs@gmail.com",  
                       Password = "Ume-111575"  
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                 }
                messageRecord = "All the mails has been successfully sent";
                 return RedirectToAction("Approvedmembership");
            }
            return View(model);
        }

    }
}