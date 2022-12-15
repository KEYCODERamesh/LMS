using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class PatientRegistrationController : Controller
    {
        SearchModels search = new SearchModels();
        // GET: PatientRegistration
        public ActionResult Pat_Registration_List()
        {
            if (Session["log"] != null)
            {
                HMSOnWebsEntities db = new HMSOnWebsEntities();
                return View(db.Assigned_test_DT);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }
        public ActionResult Pat_Registration()
        {
            if (Session["log"] != null)
            {
                HMSOnWebsEntities db = new HMSOnWebsEntities();
                Assigned_test_DT registrations = new Assigned_test_DT();
                List<SelectListItem> Titels = new List<SelectListItem>();
                Titels.Add(new SelectListItem
                {
                    Text = "Mr.",
                    Value = "1"
                });
                Titels.Add(new SelectListItem
                {
                    Text = "Mrs.",
                    Value = "2"
                });
                Titels.Add(new SelectListItem
                {
                    Text = "Smt.",
                    Value = "3"
                });
                Titels.Add(new SelectListItem
                {
                    Text = "Kumari.",
                    Value = "4"
                });
                Titels.Add(new SelectListItem
                {
                    Text = "Shri.",
                    Value = "5"
                });
                Titels.Add(new SelectListItem
                {
                    Text = "Miss.",
                    Value = "6"
                });
                Titels.Add(new SelectListItem
                {
                    Text = "Master.",
                    Value = "7"
                });
                Titels.Add(new SelectListItem
                {
                    Text = "Mohd.",
                    Value = "8"
                });
                Titels.Add(new SelectListItem
                {
                    Text = "Ms.",
                    Value = "9"
                });

                Titels.Add(new SelectListItem
                {
                    Text = "Dr.",
                    Value = "10"
                });
                Titels.Add(new SelectListItem
                {
                    Text = "Prof.",
                    Value = "11"
                });

                ViewData["ListItems"] = Titels;




                List<SelectListItem> Genders = new List<SelectListItem>();
                Genders.Add(new SelectListItem
                {
                    Text = "Male",
                    Value = "1"
                });

                Genders.Add(new SelectListItem
                {
                    Text = "Female",
                    Value = "2"
                });
                ViewData["Genders"] = Genders;


                List<SelectListItem> Centers = new List<SelectListItem>();
                Centers.Add(new SelectListItem
                {
                    Text = "Main",
                    Value = "1"
                });

                Centers.Add(new SelectListItem
                {
                    Text = "Branch",
                    Value = "2"
                });
                ViewData["Centers"] = Centers;



                List<SelectListItem> PayModes = new List<SelectListItem>();
                PayModes.Add(new SelectListItem
                {
                    Text = "Cash",
                    Value = "1"
                });

                PayModes.Add(new SelectListItem
                {
                    Text = "G Pay",
                    Value = "2"
                });
                PayModes.Add(new SelectListItem
                {
                    Text = "Pe Phone",
                    Value = "3"
                });
                PayModes.Add(new SelectListItem
                {
                    Text = "PayTm",
                    Value = "4"
                });
                PayModes.Add(new SelectListItem
                {
                    Text = "Amazon Pay",
                    Value = "5"
                });
                PayModes.Add(new SelectListItem
                {
                    Text = "Net Banking",
                    Value = "6"
                });
                PayModes.Add(new SelectListItem
                {
                    Text = "UPI",
                    Value = "7"
                });

                ViewData["PayModes"] = PayModes;


                string constr = ConfigurationManager.ConnectionStrings["LabCompanyContext"].ToString();
                SqlConnection _con = new SqlConnection(constr);
                SqlDataAdapter _da = new SqlDataAdapter("Select Referal_Name,RefId From ReferalDT WHERE RefType='Doctor'", constr);
                DataTable _dt = new DataTable();
                _da.Fill(_dt);
                ViewBag.ReferalList = ToSelectList(_dt, "RefId", "Referal_Name");

                string agents = "Select Referal_Name, RefId From ReferalDT WHERE RefType = 'Agent'";

                _con.Open();
                SqlCommand cmdagents = new SqlCommand(agents,_con);
                DataTable DTAGENTS = new DataTable();
                DTAGENTS.Load(cmdagents.ExecuteReader());
                ViewBag.AgentList = ToSelectList(DTAGENTS, "RefId", "Referal_Name");


                return View(registrations);




            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }
        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        [HttpPost]
        public JsonResult SearchPanelandTestNames(string term)
        {
            HMSOnWebsEntities entities = new HMSOnWebsEntities();


            var Packages = (from pkg in entities.TestsPackagesDTs
                             where pkg.PackageName.StartsWith(term)
                             select new
                             {
                                 label = "PKG-" + pkg.PackageName,
                                 val = pkg.Fees
                             }).ToList();

            var TestList = (from tests in entities.TestDTs
                             where tests.Test_Name.StartsWith(term)
                             select new
                             {
                                 label = "T-" + tests.Test_Name,
                                 val = tests.Test_Rate
                             }).ToList();

            var PanelNames = (from panels in entities.TestsPanels
                              where panels.PanelName.StartsWith(term)
                              select new
                              {
                                  label = "P-" + panels.PanelName,
                                  val = panels.Fees
                              }).ToList();

            //list = list1.Concat(list2).Concat(list3).ToList();
            var PaneltestList = Packages.Concat(PanelNames).Concat(TestList).ToList();

            return Json(PaneltestList, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SaveRegistrations(List<Assigned_test_DT> patients)
        {
            try
            {
                using (HMSOnWebsEntities entities = new HMSOnWebsEntities())
                {

                    //Check for NULL.
                    if (patients == null)
                    {
                        patients = new List<Assigned_test_DT>();
                    }

                    Assigned_test_DT test = new Assigned_test_DT();

                    //int PackagedID = entities.TestsPackagesDTs.Select(p => p.PackageID).DefaultIfEmpty(0).Max();
                    //PackagedID = PackagedID + 1;

                    int PatientID =Convert.ToInt32(entities.Assigned_test_DT.Select(p => p.PatieintID).DefaultIfEmpty(0).Max());
                    PatientID = PatientID + 1;


                    //Loop and insert records.
                   
                    long RegsID = 0;
                    foreach (Assigned_test_DT param in patients)
                    {
                        test = new Assigned_test_DT();

                        long MaxTestId = entities.Assigned_test_DT.Select(p => p.AssignID).DefaultIfEmpty(0).Max();
                        test.AssignID = MaxTestId + 1;
                        RegsID= MaxTestId + 1;
                        test.TDate = System.DateTime.Today;
                        test.SDate = System.DateTime.Today;
                        test.PanelID = 0;
                        test.TestID = 0;

                        test.DoctorID = param.DoctorID;
                        test.Title = param.Title;
                        test.Gender = param.Gender;
                        test.FirstName = param.FirstName;
                        test.LastName = param.LastName;
                        test.AadharNos = param.AadharNos;
                        test.AddressDetails = param.AddressDetails;
                        test.AgeYear = param.AgeYear;
                        test.Inmonth = param.Inmonth;
                        test.InDays = param.InDays;

                        test.MobileNo = param.MobileNo;
                        test.EmailID = param.EmailID;

                        test.CollectionCenter = param.CollectionCenter;
                        test.SampleColAgents = param.SampleColAgents;
                        test.TotRs = param.TotRs;
                        test.Discounts = param.Discounts;
                        test.Paid = param.Paid;
                        test.Balance = param.Balance;
                        test.PayMode = param.PayMode;
                        test.LoginID = Session["log"].ToString();
                        entities.Assigned_test_DT.Add(test);
                        entities.SaveChanges();

                        break;
                    }

                    int OrderNos = 1;
                    foreach (Assigned_test_DT param in patients)
                    {
                        Pat_InvestList testlist = new Pat_InvestList();

                        long MaxTestId = entities.Pat_InvestList.Select(p => p.LabSrNo).DefaultIfEmpty(0).Max();
                        testlist.LabSrNo = MaxTestId + 1;

                        testlist.PkgID = 0;
                        testlist.PnlID = 0;
                        testlist.TestID = 0;
                        testlist.RegsID = RegsID;
                        testlist.DoctorID = param.DoctorID;
                       
                        testlist.TOrderNos = OrderNos;

                        if (param.TsetName.Contains("P-"))
                        {
                            using (var groupcontext = new HMSOnWebsEntities())
                            {
                                string PanelNames = param.TsetName;
                                string T = PanelNames.Substring(2, PanelNames.Length - 2);

                                var data = groupcontext.TestsPanels.Where(x => x.PanelName == T).SingleOrDefault();
                                int  PnlID = data.PanelID;
                                testlist.PnlID = PnlID;
                                testlist.TestNames = T;
                                testlist.Amount = param.Rate;
                            }
                        }


                        if (param.TsetName.Contains("T-"))
                        {
                            using (var groupcontext = new HMSOnWebsEntities())
                            {
                                string TestNames = param.TsetName;
                                string T = TestNames.Substring(2, TestNames.Length - 2);
                                var data = groupcontext.TestDTs.Where(x => x.Test_Name == T).SingleOrDefault();
                                long TestID = data.Test_ID;
                                testlist.TestID =Convert.ToInt16(TestID);
                                testlist.TestNames = T;
                                testlist.Amount = param.Rate;
                            }
                        }

                        if (param.TsetName.Contains("PKG-"))
                        {
                            using (var groupcontext = new HMSOnWebsEntities())
                            {
                                string TestNames = param.TsetName;
                                string T = TestNames.Substring(4, TestNames.Length - 4);
                                // var data = groupcontext.TestsPackagesDTs.Where(x => x.PackageName == T).SingleOrDefault();
                                 var data = groupcontext.TestsPackagesDTs.Where(x => x.PackageName == T ).ToList().Take(1);

                               // List<TestPackageModels> pkgdatat = new List<TestPackageModels>();
                                List<TestsPackagesDT> pkgdatat2 = new List<TestsPackagesDT>();
                                var items = groupcontext.TestsPackagesDTs.OrderByDescending(x => x.PackageName == T).ToList().Take(1);
                                foreach(TestsPackagesDT t in items )
                                {
                                    long pkgid = t.PackageID;
                                    testlist.PkgID = Convert.ToInt16(pkgid);
                                }
                                 //long pkgid= items.pack

                                //var list = (from t in TestsPackagesDT
                                //            where t.DeliverySelection == true && t.Delivery.SentForDelivery == null
                                //            orderby t.Delivery.SubmissionDate
                                //            select t).Take(5);
                               
                                testlist.TestNames = T;
                                testlist.Amount = param.Rate;

                            }
                        }




                        entities.Pat_InvestList.Add(testlist);
                        entities.SaveChanges();

                        OrderNos = OrderNos + 1;


                    }


                    //return Json(0);
                }
                //return RedirectToAction("Pat_Registration_List");
                HMSOnWebsEntities db = new HMSOnWebsEntities();
                return View(db.Assigned_test_DT);
                //return View("Pat_Registration_List");
            }
            catch (Exception ex)
            {
                //  return RedirectToAction("Pat_Registration", "PatientRegistration", new { Redirected = "Redirected To PatientList" });
                return null;
            }
        }


        
        public ActionResult Pat_RegistrationReceipts(long id)
        {
            if (Session["log"] != null)
            {
                DataSet DS = search.GetDS("LabPatRegisReceipts", id.ToString(), "", "", "", "");
                /*----Labpratory Details---------------*/
                ViewBag.CompanyName = DS.Tables[0].Rows[0]["Company_Name"].ToString();
                ViewBag.Address = DS.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.Landmarks = DS.Tables[0].Rows[0]["Landmark"].ToString();
                ViewBag.Mobile = DS.Tables[0].Rows[0]["Mobile"].ToString();
                ViewBag.EmailID = DS.Tables[0].Rows[0]["Email_ID"].ToString();

                /*-----------------Patients Details---------------*/
                ViewBag.RegNos = DS.Tables[1].Rows[0]["AssignID"].ToString();
                ViewBag.Dates = DS.Tables[1].Rows[0]["TestDates"].ToString();
                ViewBag.Names = DS.Tables[1].Rows[0]["Names"].ToString();
                ViewBag.SexGenders = DS.Tables[1].Rows[0]["Genders"].ToString();
                ViewBag.PatMobiles = DS.Tables[1].Rows[0]["MobileNo"].ToString();
                ViewBag.PatAddress = DS.Tables[1].Rows[0]["AddressDetails"].ToString();
                ViewBag.Referredby = DS.Tables[1].Rows[0]["Referal_Name"].ToString();
                ViewBag.Receivedbys = DS.Tables[1].Rows[0]["LoginID"].ToString();


                ViewBag.Totals = DS.Tables[1].Rows[0]["TotRs"].ToString();
                ViewBag.Discounts = DS.Tables[1].Rows[0]["Discounts"].ToString();
                ViewBag.Payables = DS.Tables[1].Rows[0]["Payable"].ToString();
                ViewBag.Paid = DS.Tables[1].Rows[0]["Paid"].ToString();
                ViewBag.Dues = DS.Tables[1].Rows[0]["Balance"].ToString();
                ViewBag.Paymodes = DS.Tables[1].Rows[0]["PayMode"].ToString();

                return View(DS);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }

        public ActionResult Pat_RegistrationPreview(long id)
        {
            if (Session["log"] != null)
            {
                DataSet DS = search.GetDS("LabPatRegisReceipts", id.ToString(), "", "", "", "");
                /*----Labpratory Details---------------*/
                ViewBag.CompanyName = DS.Tables[0].Rows[0]["Company_Name"].ToString();
                ViewBag.Address = DS.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.Landmarks = DS.Tables[0].Rows[0]["Landmark"].ToString();
                ViewBag.Mobile = DS.Tables[0].Rows[0]["Mobile"].ToString();
                ViewBag.EmailID = DS.Tables[0].Rows[0]["Email_ID"].ToString();

                /*-----------------Patients Details---------------*/
                ViewBag.RegNos = DS.Tables[1].Rows[0]["AssignID"].ToString();
                ViewBag.Dates = DS.Tables[1].Rows[0]["TestDates"].ToString();
                ViewBag.Names = DS.Tables[1].Rows[0]["Names"].ToString();
                ViewBag.SexGenders = DS.Tables[1].Rows[0]["Genders"].ToString();
                ViewBag.PatMobiles = DS.Tables[1].Rows[0]["MobileNo"].ToString();
                ViewBag.PatAddress = DS.Tables[1].Rows[0]["AddressDetails"].ToString();
                ViewBag.Referredby = DS.Tables[1].Rows[0]["Referal_Name"].ToString();
                ViewBag.Receivedbys = DS.Tables[1].Rows[0]["LoginID"].ToString();


                ViewBag.Totals = DS.Tables[1].Rows[0]["TotRs"].ToString();
                ViewBag.Discounts = DS.Tables[1].Rows[0]["Discounts"].ToString();
                ViewBag.Payables = DS.Tables[1].Rows[0]["Payable"].ToString();
                ViewBag.Paid = DS.Tables[1].Rows[0]["Paid"].ToString();
                ViewBag.Dues = DS.Tables[1].Rows[0]["Balance"].ToString();
                ViewBag.Paymodes = DS.Tables[1].Rows[0]["PayMode"].ToString();

                return View(DS);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }



        public ActionResult DeleteRegistrations(long id)
        {
            if (Session["log"] != null)
            {
                HMSOnWebsEntities db = new HMSOnWebsEntities();

                DataSet DS = search.GetDS("DeleteRegistrations", id.ToString(), "", "", "", "");


              
                return RedirectToAction("Pat_Registration_List", "PatientRegistration", new { Redirected = "Redirected To Registration List" });
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }


        public ActionResult Pat_EditRegistration(long id)
        {
            if (Session["log"] != null)
            {
                HMSOnWebsEntities db = new HMSOnWebsEntities();
                //Assigned_test_DT registrations = new Assigned_test_DT();
                var registrations = db.Assigned_test_DT.Where(x => x.AssignID == id).SingleOrDefault();

                List<SelectListItem> Titels = new List<SelectListItem>();
                Titels.Add(new SelectListItem
                {
                    Text = "Mr.",
                    Value = "1"
                });
                Titels.Add(new SelectListItem
                {
                    Text = "Mrs.",
                    Value = "2"
                });
                Titels.Add(new SelectListItem
                {
                    Text = "Smt.",
                    Value = "3"
                });
                Titels.Add(new SelectListItem
                {
                    Text = "Kumari.",
                    Value = "4"
                });
                Titels.Add(new SelectListItem
                {
                    Text = "Shri.",
                    Value = "5"
                });
                Titels.Add(new SelectListItem
                {
                    Text = "Miss.",
                    Value = "6"
                });
                Titels.Add(new SelectListItem
                {
                    Text = "Master.",
                    Value = "7"
                });
                Titels.Add(new SelectListItem
                {
                    Text = "Mohd.",
                    Value = "8"
                });
                Titels.Add(new SelectListItem
                {
                    Text = "Ms.",
                    Value = "9"
                });

                Titels.Add(new SelectListItem
                {
                    Text = "Dr.",
                    Value = "10"
                });
                Titels.Add(new SelectListItem
                {
                    Text = "Prof.",
                    Value = "11"
                });
                Titels.Find(c => c.Text == registrations.Title).Selected = true;
                ViewData["ListItems"] = Titels;

                List<SelectListItem> Centers = new List<SelectListItem>();
                Centers.Add(new SelectListItem
                {
                    Text = "Main",
                    Value = "1"
                });

                Centers.Add(new SelectListItem
                {
                    Text = "Branch",
                    Value = "2"
                });
                Centers.Find(c => c.Text == registrations.CollectionCenter).Selected = true;
                ViewData["Centers"] = Centers;


                List<SelectListItem> Genders = new List<SelectListItem>();
                Genders.Add(new SelectListItem
                {
                    Text = "Male",
                    Value = "1"
                });

                Genders.Add(new SelectListItem
                {
                    Text = "Female",
                    Value = "2"
                });
                Genders.Find(c => c.Text == registrations.Gender).Selected = true;
                ViewData["Genders"] = Genders;


                List<SelectListItem> PayModes = new List<SelectListItem>();
                PayModes.Add(new SelectListItem
                {
                    Text = "Cash",
                    Value = "1"
                });

                PayModes.Add(new SelectListItem
                {
                    Text = "G Pay",
                    Value = "2"
                });
                PayModes.Add(new SelectListItem
                {
                    Text = "Pe Phone",
                    Value = "3"
                });
                PayModes.Add(new SelectListItem
                {
                    Text = "PayTm",
                    Value = "4"
                });
                PayModes.Add(new SelectListItem
                {
                    Text = "Amazon Pay",
                    Value = "5"
                });
                PayModes.Add(new SelectListItem
                {
                    Text = "Net Banking",
                    Value = "6"
                });
                PayModes.Add(new SelectListItem
                {
                    Text = "UPI",
                    Value = "7"
                });
                PayModes.Add(new SelectListItem
                {
                    Text = "--Select--",
                    Value = "8"
                });
                PayModes.Find(c => c.Text == registrations.PayMode).Selected = true;
                ViewData["PayModes"] = PayModes;

                string constr = ConfigurationManager.ConnectionStrings["LabCompanyContext"].ToString();
                SqlConnection _con = new SqlConnection(constr);
                SqlDataAdapter _da = new SqlDataAdapter("Select Referal_Name,RefId From ReferalDT WHERE RefType='Doctor'", constr);
                DataTable _dt = new DataTable();
                _da.Fill(_dt);

                List<SelectListItem> Doctors = new List<SelectListItem>();
                Doctors= ToSelectList(_dt, "RefId", "Referal_Name").ToList();
                Doctors.Find(c =>Convert.ToInt16(c.Value) == registrations.DoctorID).Selected = true;
                ViewBag.ReferalList = ToSelectList(_dt, "RefId", "Referal_Name");

                string agents = "Select Referal_Name, RefId From ReferalDT WHERE RefType = 'Agent'";
                _con.Open();
                SqlCommand cmdagents = new SqlCommand(agents, _con);
                DataTable DTAGENTS = new DataTable();
                DTAGENTS.Load(cmdagents.ExecuteReader());
                ViewBag.AgentList = ToSelectList(DTAGENTS, "RefId", "Referal_Name");

                return View(registrations);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }


        public ActionResult PrepareLabReports(long id)
        {
            if (Session["log"] != null)
            {
                HMSOnWebsEntities db = new HMSOnWebsEntities();
                TestReportModel TestReports = new TestReportModel();
                var patdetails = db.Assigned_test_DT.Where(x => x.AssignID == id).SingleOrDefault();
                ViewBag.RegisNos = patdetails.AssignID;
                ViewBag.RegDate = patdetails.TDate;

                ViewBag.PatientName = (patdetails.Title + " " + patdetails.FirstName + " " + patdetails.LastName);
                ViewBag.Gender = patdetails.Gender;
                ViewBag.Age = patdetails.AgeYear.ToString() + "Year " + patdetails.Inmonth.ToString() + "Mon " + patdetails.InDays.ToString() + "Days";
                ViewBag.Mobile = patdetails.MobileNo;
                ViewBag.Address = patdetails.AddressDetails;
                var Referals = db.ReferalDTs.Where(x => x.RefId == patdetails.DoctorID).SingleOrDefault();
                ViewBag.Referals = Referals.Referal_Name;

                DataSet DSTests = new DataSet();
                DSTests = search.GetDS("GetTestListForPatients", id.ToString(), "", "", "", "");

                //return View(TestReports);
                return View(DSTests);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }
    }
}