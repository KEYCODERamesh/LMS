using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class ReferalController : Controller
    {
        // GET: Referal
        public ActionResult Index()
        {
            if(Session["log"]!=null)
            {
                return View(new ReferalDT());
            }
            else
            {
                return RedirectToAction("Login", "Home",new { Redirected= "Redirected To Login"});
            }
          
        }
        [HttpPost]
        public ActionResult Index(ReferalDT referals)
        {
            try
            {
                if (Session["log"] != null)
                {
                    using (var context = new HMSOnWebsEntities())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {

                            //int? intIdt = context.ReferalDTs.Max(u => (int?)u.RefId);
                            //referals.RefId = 1;
                            // int MaxRefID = context.ReferalDTs.Max(p => p.RefId);
                            int MaxRefID = context.ReferalDTs.Select(p => p.RefId).DefaultIfEmpty(0).Max();
                            referals.RefId = MaxRefID + 1;
                            if (context.ReferalDTs.Any(o => o.MobileNos == referals.MobileNos))
                            {
                                string message = "Mobile No. Exists !";
                                ViewBag.Message = message;
                                return View(new ReferalDT());
                            }
                            else
                            {
                                context.ReferalDTs.Add(referals);
                                context.SaveChanges();
                                transaction.Commit();
                                ModelState.Clear();
                                string message = "Referal Created Successfully";
                                ViewBag.Message = message;
                                return View(new ReferalDT());
                            }


                        }

                    }

                }
                else
                {
                    return RedirectToAction("Login", "Home", new { Redirected = "You are loged out from session !" });
                }
            }
            catch(DbEntityValidationException e)
            {
                //foreach (var eve in e.EntityValidationErrors)
                //{
                //    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                //            ve.PropertyName,
                //            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                //            ve.ErrorMessage);
                //    }
                //}


                //  Console.WriteLine(e);


                //string message = string.Empty;
                //foreach (var validationErrors in e.EntityValidationErrors)
                //{
                //    foreach (var validationError in validationErrors.ValidationErrors)
                //    {
                //        message += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                //    }
                //}


                throw e;
            }

        }
        public ActionResult ShowReferals()
        {
            if (Session["log"] != null)
            {
                LabCompanyContext refcontext = new LabCompanyContext();
                List<ReferalDetails> reflist = refcontext.referals.ToList();

                return View(reflist);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }

        }

        [HttpGet]
        public ActionResult EditReferals(int id)
        {
            using (var context = new HMSOnWebsEntities())
            {
                var data = context.ReferalDTs.Where(x => x.RefId == id).SingleOrDefault();
                return View(data);
            }
        }
        [HttpPost]
        public ActionResult EditReferals(int id,ReferalDT referals)
        {
            using (var context = new HMSOnWebsEntities())
            {
                var data = context.ReferalDTs.FirstOrDefault(x => x.RefId == id);

                if (data != null)
                {
                    data.RefType = referals.RefType;
                    data.Referal_Name = referals.Referal_Name;
                    data.MobileNos = referals.MobileNos;
                    data.Address = referals.Address;
                    data.ComP = referals.ComP;
                    data.Ref_Status = referals.Ref_Status;

                    context.SaveChanges();
                    return RedirectToAction("ShowReferals");
                }
                else
                    return View();
            }
        }



        public ActionResult Delete()
        {
            using (var context = new HMSOnWebsEntities())
            {
                return View(new ReferalDT());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            using (var context = new HMSOnWebsEntities())
            {
                var data = context.ReferalDTs.FirstOrDefault(x => x.RefId == id);
                if (data != null)
                {
                    context.ReferalDTs.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("ShowReferals");
                }
                else
                    return View();
            }
        }
    }
}