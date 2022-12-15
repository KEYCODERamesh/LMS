using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class TestPackageController : Controller
    {
        // GET: TestPackage
        public ActionResult ViewPackageList()
        {
            if (Session["log"] != null)
            {
                HMSOnWebsEntities db = new HMSOnWebsEntities();
                return View(db.TestsPackagesDTs);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }
        public ActionResult AddNewTestPackages()
        {
            if (Session["log"] != null)
            {
                LabCompanyContext MCon = new LabCompanyContext();
                TestsPackagesDT packages = new TestsPackagesDT();
               
                return View(packages);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }

        [HttpPost]
        public JsonResult SearchPanelandTestNames(string term)
        {
            HMSOnWebsEntities entities = new HMSOnWebsEntities();

            var customers = (from customer in entities.TestDTs
                             where customer.Test_Name.StartsWith(term)
                             select new
                             {
                                 label ="T-"+ customer.Test_Name,
                                 val = customer.Test_Rate
                             }).ToList();

            var PanelNames = (from customer in entities.TestsPanels
                             where customer.PanelName.StartsWith(term)
                             select new
                             {
                                 label ="P-"+ customer.PanelName,
                                 val = customer.Fees
                             }).ToList();

            //list = list1.Concat(list2).Concat(list3).ToList();
             var PaneltestList= customers.Concat(PanelNames).ToList();

            return Json(PaneltestList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveTestInPackages(List<MyTestPackage> packages)
        {
            try
            {
                using (HMSOnWebsEntities entities = new HMSOnWebsEntities())
                {

                    //Check for NULL.
                    if (packages == null)
                    {
                        packages = new List<MyTestPackage>();
                    }

                    TestsPackagesDT test = new TestsPackagesDT();


                    int PackagedID = entities.TestsPackagesDTs.Select(p => p.PackageID).DefaultIfEmpty(0).Max();
                    PackagedID = PackagedID + 1;



                    //Loop and insert records.
                    string TestName = " ";
                    decimal Fees = 0;
                    test.Fees = 0;
                    foreach (MyTestPackage param in packages)
                    {
                        test = new TestsPackagesDT();
                        test.PackageID = PackagedID;
                        int MaxTestId = entities.TestsPackagesDTs.Select(p => p.PSRNO).DefaultIfEmpty(0).Max();
                        test.PSRNO = MaxTestId + 1;

                        test.PackageName = param.PackageName;
                        test.PanelID = 0;
                        test.TestID = 0;
                        // TestName = param.Panel_Test_Namelist;
                        //if (TestName == " ")
                        //{
                        //    TestName = param.Panel_Test_Namelist;
                        //}
                        //else
                        //{
                        //    TestName = TestName + test + ',' + param.Panel_Test_Namelist;
                        //}

                        int PanelID = 0;
                        long TestID = 0;

                        if (param.Panel_Test_Namelist.Contains("P-"))
                        {
                            using (var groupcontext = new HMSOnWebsEntities())
                            {
                                string PanelNames = param.Panel_Test_Namelist;
                                string T = PanelNames.Substring(2, PanelNames.Length - 2);

                                var data = groupcontext.TestsPanels.Where(x => x.PanelName == T).SingleOrDefault();
                                PanelID = data.PanelID;
                                test.PanelID = PanelID;
                                test.Panel_Test_Namelist = T;
                            }
                        }


                        if (param.Panel_Test_Namelist.Contains("T-"))
                        {
                            using (var groupcontext = new HMSOnWebsEntities())
                            {
                                string TestNames = param.Panel_Test_Namelist;
                                string T = TestNames.Substring(2, TestNames.Length - 2);
                                var data = groupcontext.TestDTs.Where(x => x.Test_Name == T).SingleOrDefault();
                                TestID = data.Test_ID;
                                test.TestID = Convert.ToInt32(TestID);
                                test.Panel_Test_Namelist = T;
                            }
                        }


                       
                        //  Fees = Convert.ToDecimal(param.Fees);
                        // test.Fees = test.Fees + Fees;
                        test.Fees =Convert.ToDecimal(param.Fees);
                        entities.TestsPackagesDTs.Add(test);
                        entities.SaveChanges();
                    }
                    


                    //return Json(0);

                   
                }
                return null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}