using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    /*-----Declare Attribute routing at Controller Level -----
    [RoutePrefix("MyGroups")]
    [Route("{action=ListGroups}")] // Default Action mEthods */
    public class TestGroupController : Controller
    {
        // GET: TestGroup

        private DateTime _SetDefaultDate = DateTime.Now;

      ///  [Route("MyControllerName/MyACtionmethodeNAme")]  //It is also working with attribute outing .... here we can give any name and in url as we like security reason also
        public ActionResult AddgRoups()
        {
            if (Session["log"] != null)
            {
                return View(new GroupDT());
            }
            else
            {
                //return RedirectToAction("Login");
                return RedirectToAction("Login", "Home", new { FileUploadMsg = "" });
            }

            
        }
        [HttpPost]
        public ActionResult AddgRoups(GroupDT groups)
        {
            using (var context = new HMSOnWebsEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                  //  int intIdt = context.GroupDTs.Max(u => u.Group_ID);
                    int? intIdt = context.GroupDTs.Max(u => (int?)u.Group_ID);

                    // groups.Group_ID = (int)(intIdt + 1);
                    int MaxGrpID = context.GroupDTs.Select(p => p.Group_ID).DefaultIfEmpty(0).Max();
                    groups.Group_ID = MaxGrpID + 1;
                    groups.Created_On = _SetDefaultDate;
                    groups.Edited_On = _SetDefaultDate;
                    groups.Created_By = 1;
                    groups.Edited_By = 1;

                    context.GroupDTs.Add(groups);
                    context.SaveChanges();

                    transaction.Commit();
                }

            }
            string message = "Created the record successfully";
            ViewBag.Message = message;
            return RedirectToAction("ListGroups");
        }

       
        public ActionResult ListGroups()
        {
            LabCompanyContext Testgroups = new LabCompanyContext();
            List<TestGroupModel> testgroups = Testgroups.testgroups.ToList();

            return View(testgroups);
        }


        public ActionResult EditGroups(int id)
        {
            using (var context = new HMSOnWebsEntities())
            {
                var data = context.GroupDTs.Where(x => x.Group_ID == id).SingleOrDefault();
                return View(data);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGroups(int id, GroupDT groups)
        {
            using (var context = new HMSOnWebsEntities())
            {
                var data = context.GroupDTs.FirstOrDefault(x => x.Group_ID == id);

                if (data != null)
                {
                    data.Group_Name = groups.Group_Name;                    
                    data.Created_By = 1;
                    data.Edited_By = 1;
                    data.Created_On = _SetDefaultDate;
                    data.Edited_On = _SetDefaultDate;
                    context.SaveChanges();
                    return RedirectToAction("ListGroups");
                }
                else
                    return View();
            }
        }


        public ActionResult Delete()
        {
            using (var context = new HMSOnWebsEntities())
            {
                return View(new GroupDT());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            using (var context = new HMSOnWebsEntities())
            {
                var data = context.GroupDTs.FirstOrDefault(x => x.Group_ID == id);
                if (data != null)
                {
                    context.GroupDTs.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("ListGroups");
                }
                else
                    return View();
            }
        }


      
        public ActionResult DeleteMethod(bool isTrue)
        {
            if (isTrue)
            {
                //using (var context = new HMSOnWebsEntities())
                //{
                //    var data = context.GroupDTs.FirstOrDefault(x => x.Group_ID == id);
                //    if (data != null)
                //    {
                //        context.GroupDTs.Remove(data);
                //        context.SaveChanges();
                //        return RedirectToAction("ListGroups");
                //    }
                //    else
                //        return View();
                //}
            }
            else
            {
                //do something
            }
            return View();
        }

    }
}