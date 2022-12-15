using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class TestPanelsController : Controller
    {
        // GET: TestPanels
        public ActionResult TestPanelList()
        {
            if (Session["log"] != null)
            {
                HMSOnWebsEntities db = new HMSOnWebsEntities();
                // List<MyTestPanels> reflist = testpanels.mytestpanels.ToList();

                return View(db.TestsPanels);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }
        public ActionResult AddNewTestPanel()
        {
            if (Session["log"] != null)
            {
                LabCompanyContext MCon = new LabCompanyContext();
                MyTestPanels MD = new MyTestPanels();
               // MD.GroupList = new SelectList(MCon.GetGroupList(), "Group_ID", "Group_Name");
                return View(MD);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }

        //[HttpPost]
        //public JsonResult SearchCustomers(string customerName)
        //{
        //    HMSOnWebsEntities entities = new HMSOnWebsEntities();
        //    var customers = from c in entities.TestDTs
        //                    where c.Test_Name.Contains(customerName)
        //                    select c;
        //    return Json(customers.ToList().Take(10));


        //    //HMSOnWebsEntities entities = new HMSOnWebsEntities();
        //    //var customers = (from customer in entities.TestDTs
        //    //                 where customer.Test_Name.StartsWith(customerName)
        //    //                 select new
        //    //                 {
        //    //                     label = customer.Test_Name,
        //    //                     val = customer.Test_ID
        //    //                 }).ToList();

        //    //return Json(customers, JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        public JsonResult AutoComplete(string term)
        {
            HMSOnWebsEntities entities = new HMSOnWebsEntities();
            var customers = (from customer in entities.TestDTs
                             where customer.Test_Name.StartsWith(term)
                             select new
                             {
                                 label = customer.Test_Name,
                                 val = customer.Test_Rate
                             }).ToList();

            return Json(customers, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveTestInPanels(List<MyTestPanels> customers)
        {
            using (HMSOnWebsEntities entities = new HMSOnWebsEntities())
            {
               
                //Check for NULL.
                if (customers == null)
                {
                    customers = new List<MyTestPanels>();
                }

                TestsPanel test = new TestsPanel();
                int MaxTestId = entities.TestsPanels.Select(p => p.PanelID).DefaultIfEmpty(0).Max();
                test.PanelID = MaxTestId + 1;

                //Loop and insert records.
                string TestName = " ";
                decimal Fees = 0;
                test.Fees = 0;
                foreach (MyTestPanels param in customers)
                {
                    test.PanelName = param.PanelName;
                    test.CategoryID = 0;
                    TestName = param.TestNames;
                    if (TestName == " ")
                    {
                        test.TestNames =TestName;
                    }
                    else
                    {
                        test.TestNames = test.TestNames + ',' + TestName;
                    }
                     Fees =Convert.ToDecimal( param.Fees);
                     test.Fees = test.Fees + Fees;
                }
                entities.TestsPanels.Add(test);
                entities.SaveChanges();
                //return Json(0);

                return null;
            }
        }


    }
}