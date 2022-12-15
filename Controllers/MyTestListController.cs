using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
namespace HospitalManagementSystem.Controllers
{
    public class MyTestListController : Controller
    {
        private static int Test_ID = 0;
        private static int GroupId = 0;
        private static int TestParemter_ID = 0;
        private static string Test_Name = "";
        private static string Group_Name = "";

        public ActionResult Index()
        {
            if (Session["log"] != null)
            {
                LabCompanyContext refcontext = new LabCompanyContext();
                List<MyLabTestList> reflist = refcontext.labtestlits.ToList();

                return View(reflist);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }

        // GET: MyTestList/Create
        public ActionResult Create()
        {
            if (Session["log"] != null)
            {
                LabCompanyContext MCon = new LabCompanyContext();
                MyLabTestList MD = new MyLabTestList();
                MD.GroupList = new SelectList(MCon.GetGroupList(), "Group_ID", "Group_Name");

                // GET: MyTestList
                return View(MD);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }

        // POST: MyTestList/Create
        [HttpPost]
        public ActionResult Create(TestDT tests)
        {
            LabCompanyContext MCon = new LabCompanyContext();
            MyLabTestList mtnewtests = new MyLabTestList();
            mtnewtests.GroupList = new SelectList(MCon.GetGroupList(), "Group_ID", "Group_Name");
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
                            long MaxRefID = context.TestDTs.Select(p => p.Test_ID).DefaultIfEmpty(0).Max();
                            tests.Test_ID = MaxRefID + 1;

                            tests.Created_By = 1;
                            tests.Edited_By = 1;
                            tests.Created_On = System.DateTime.Today;
                            tests.Edited_On = System.DateTime.Today;

                            string GroupNAme = "";
                            using (var groupcontext = new HMSOnWebsEntities())
                            {
                                var data = groupcontext.GroupDTs.Where(x => x.Group_ID == tests.Group_ID).SingleOrDefault();
                                GroupNAme = data.Group_Name;
                            }


                            tests.Group_Name = GroupNAme;
                            //tests.Group_ID = mytest.Group_ID;
                            context.TestDTs.Add(tests);
                            context.SaveChanges();
                            transaction.Commit();
                            ModelState.Clear();
                            string message = "Test Created Successfully";
                            ViewBag.Message = message;
                            return View(mtnewtests);
                        }

                    }

                }
                else
                {
                    return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
                }
                // TODO: Add insert logic here


            }
            catch (Exception ex)
            {
                return View(mtnewtests);
            }
        }

        // GET: MyTestList/Edit/5
        [HttpGet]
        public ActionResult Edit(long id)
        {
            if (Session["log"] != null)
            {
                LabCompanyContext MCon = new LabCompanyContext();
                MyLabTestList MD = new MyLabTestList();

                // ViewBag.DepartmentId = new SelectList(MCon.GetGroupList, "Group_ID", "DepartmentName", employee.DepartmentId);
                using (var context = new HMSOnWebsEntities())
                {
                    var data = context.TestDTs.Where(x => x.Test_ID == id).SingleOrDefault();
                    MD.Test_Name = data.Test_Name;
                    MD.T_ShortName = data.T_ShortName;
                    MD.Test_Rate = Convert.ToDecimal(data.Test_Rate);
                    MD.Group_Name = data.Group_Name;
                    MD.Method_Name = data.Method_Name;
                    MD.Instruemnt = data.Instruemnt;
                    MD.Comments = data.Comments;
                    MD.Group_ID = Convert.ToInt16(data.Group_ID);
                    MD.GroupList = new SelectList(MCon.GetGroupList(), "Group_ID", "Group_Name", data.Group_ID);
                    ViewBag.MyLabTestList = new SelectList(MCon.GetGroupList(), "Group_ID", "Group_Name", data.Group_ID);
                    // return View(data);
                }

                // GET: MyTestList
                return View(MD);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }

        // POST: MyTestList/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TestDT tests)
        {
            try
            {
                // TODO: Add update logic here
                if (Session["log"] != null)
                {
                    using (var context = new HMSOnWebsEntities())
                    {
                        var data = context.TestDTs.FirstOrDefault(x => x.Test_ID == id);

                        if (data != null)
                        {
                            data.Test_Name = tests.Test_Name;
                            data.T_ShortName = tests.T_ShortName;
                            data.Test_Rate = tests.Test_Rate;

                            string GroupNAme = "";
                            using (var groupcontext = new HMSOnWebsEntities())
                            {
                                var data2 = groupcontext.GroupDTs.Where(x => x.Group_ID == tests.Group_ID).SingleOrDefault();
                                GroupNAme = data2.Group_Name;
                            }

                            data.Group_Name = GroupNAme;
                            data.Comments = tests.Comments;
                            data.Instruemnt = tests.Instruemnt;
                            data.Method_Name = tests.Method_Name;

                            data.Created_By = 1;
                            data.Edited_By = 1;
                            data.Created_On = System.DateTime.Today;
                            data.Edited_On = System.DateTime.Today;
                            context.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
                }

            }
            catch
            {
                return View("Edit");
            }
        }

        // GET: MyTestList/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MyTestList/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        // Unit Methods
        [HttpPost]
        public JsonResult InsertUnits(UnitDT units)
        {
            string query = "INSERT INTO UnitDT(Unit_Name)VALUES(@UnitName)";
            //query += "SELECT SCOPE_IDENTITY()";
            string constr = ConfigurationManager.ConnectionStrings["LabCompanyContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (var context = new HMSOnWebsEntities())
                    {
                        int MaxRefID = context.UnitDTs.Select(p => p.Unit_ID).DefaultIfEmpty(0).Max();
                        units.Unit_ID = MaxRefID + 1;
                        cmd.Parameters.AddWithValue("@ITEMID", units.Unit_ID);
                        cmd.Parameters.AddWithValue("@UnitName", units.Unit_Name);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        //labs.ITEMID = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                    }


                }
            }

            return Json(units);
        }


        public ActionResult ShowUnits()
        {
            if (Session["log"] != null)
            {
                LabCompanyContext refcontext = new LabCompanyContext();
                List<MyUnits> reflist = refcontext.unitlist.ToList();

                return View(reflist);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }

        // Parametres Methods
        public ActionResult AddParameter()
        {
            if (Session["log"] != null)
            {
                LabCompanyContext MCon = new LabCompanyContext();
                TestParametsrsModels Parameters = new TestParametsrsModels();
                Parameters.GroupList = new SelectList(MCon.TestGroupsGroupList(), "Group_ID", "Group_Name");
                Parameters.TestList = new SelectList(MCon.GetTestList(), "Test_ID", "Test_Name");

                // GET: MyTestList
                return View(Parameters);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }

        [HttpPost]
        public ActionResult AddParameter(string[] DynamicTextBox, string[] DynamicTextBox2)
        {
            LabCompanyContext MCon = new LabCompanyContext();
            TestParametsrsModels Parameters = new TestParametsrsModels();
            Parameters.GroupList = new SelectList(MCon.TestGroupsGroupList(), "Group_ID", "Group_Name");
            Parameters.TestList = new SelectList(MCon.GetTestList(), "Test_ID", "Test_Name");

            // GET: MyTestList

            DataTable DT = new DataTable();
            DT.Columns.Add("Parameter");
            DT.Columns.Add("Unit");

            //Serialize the Array and assign to ViewBag.
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ViewBag.Values = serializer.Serialize(DynamicTextBox);
            int C = DynamicTextBox.Count();


            //Loop through the dynamic TextBox values.
            int RC = 0;
            foreach (string textboxValue in DynamicTextBox)
            {
                //Insert the dynamic TextBox values to Database Table.
                //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                //using (SqlConnection con = new SqlConnection(constr))
                //{
                //    using (SqlCommand cmd = new SqlCommand("INSERT INTO Names(Name) VALUES(@Name)"))
                //    {
                //        cmd.Connection = con;
                //        cmd.Parameters.AddWithValue("@Name", textboxValue);
                //        con.Open();
                //        cmd.ExecuteNonQuery();
                //        con.Close();
                //    }
                //}

                DataRow dtr = DT.NewRow();
                dtr["Parameter"] = textboxValue;
                dtr["Unit"] = "";
                DT.Rows.Add(dtr);
            }

            ViewBag.Values = serializer.Serialize(DynamicTextBox2);
            RC = 0;
            foreach (DataRow Rows in DT.Rows)
            {
                Rows["Unit"] = DynamicTextBox2[RC].ToString();
                RC = RC + 1;
            }

            //Set the Message to be displayed later in View.

            ViewBag.Message = DynamicTextBox.Count() + " records saved.";

            //return View();

            return View(Parameters);
        }

        public ActionResult AddNewParameter()
        {
            if (Session["log"] != null)
            {
                //LabCompanyContext MCon = new LabCompanyContext();
                //TestParametsrsModels Parameters = new TestParametsrsModels();
                //Parameters.GroupList = new SelectList(MCon.TestGroupsGroupList(), "Group_ID", "Group_Name");
                //Parameters.TestList = new SelectList(MCon.GetTestList(), "Test_ID", "Test_Name");

                // GET: MyTestList
                // return View(Parameters);
                HMSOnWebsEntities db = new HMSOnWebsEntities();

                return View(db.Customers);

            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }


        //public JsonResult InsertCustomers(List<Customer> customers)
        //{
        //    using (HMSOnWebsEntities entities = new HMSOnWebsEntities())
        //    {
        //        //Truncate Table to delete all old records.
        //        //entities.Database.ExecuteSqlCommand("TRUNCATE TABLE [Customer]");

        //        //Check for NULL.
        //        if (customers == null)
        //        {
        //            customers = new List<Customer>();
        //        }

        //        //Loop and insert records.
        //        foreach (Customer customer in customers)
        //        {
        //            entities.Customers.Add(customer);
        //        }
        //        int insertedRecords = entities.SaveChanges();
        //        return Json(insertedRecords);
        //    }
        //}

        //[Route("ArtistImages/Values/Add2")]
        //[HttpGet("{id}/{first}/{second}")]
        public ActionResult AddTestParameterParameter(string TestName, string CategoryName,string  TestID, string CategoryID)
        {
            if (Session["log"] != null)
            {
                //LabCompanyContext MCon = new LabCompanyContext();
                TestParametsrsModels Parameters = new TestParametsrsModels();
                //Parameters.GroupList = new SelectList(MCon.TestGroupsGroupList(), "Group_ID", "Group_Name");
                //Parameters.TestList = new SelectList(MCon.GetTestList(), "Test_ID", "Test_Name");
                //TempData["TestName"] = TestName;
                //TempData["CategoryName"] = CategoryName;
                Parameters.TestName = TestName;
                Parameters.GroupName = CategoryName;
                Parameters.TestID = Convert.ToInt16(TestID);
                Parameters.GrpID = Convert.ToInt16(CategoryID);

                Test_ID = Convert.ToInt16(TestID);
                GroupId = Convert.ToInt16(CategoryID);
                Test_Name = TestName;
                Group_Name = CategoryName;
                //// GET: MyTestList
                //return View(Parameters);
                ViewBag.TestName = TestName;
                ViewBag.CategoryName = CategoryName;

               
                //Parameters.UnitList = PopulateUnitList();


                HMSOnWebsEntities db = new HMSOnWebsEntities();
                var UnitList = db.UnitDTs.ToList();
                if (UnitList != null)
                {
                    ViewBag.UnitList = UnitList;
                }


                

                return View(db.TestParameterDTs);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }


        //[HttpPost]
        [HttpPost]
        public JsonResult AddTestParameterParameter(List<TestParameterDT> TestParameterDTsArrayFromJson)
        {
            using (HMSOnWebsEntities entities = new HMSOnWebsEntities())
            {
                TestParametsrsModels Parameters = new TestParametsrsModels();
                //Truncate Table to delete all old records.
                //entities.Database.ExecuteSqlCommand("TRUNCATE TABLE [Customer]");

                //Check for NULL.
                if (TestParameterDTsArrayFromJson == null)
                {
                    TestParameterDTsArrayFromJson = new List<TestParameterDT>();
                }

                //Loop and insert records.
                foreach (TestParameterDT param in TestParameterDTsArrayFromJson)
                {
                    long MaxTestId = entities.TestParameterDTs.Select(p => p.Test_P_ID).DefaultIfEmpty(0).Max();
                    param.Test_P_ID = MaxTestId + 1;
                    param.TestID = Test_ID;
                    param.GrpID = GroupId;
                    param.TestName = Test_Name;
                    param.GroupName = Group_Name;
                    param.PreResult = param.DefaultResult;
                    param.Inorder = param.Inorder;
                    param.Units = param.Units;
                    param.SbInorder = 0;
                    entities.TestParameterDTs.Add(param);
                    entities.SaveChanges();
                }
                

                //int insertedRecords = entities.SaveChanges();

                //return Json(insertedRecords);

                return Json(0);
            }
        }


        public ActionResult ShowTestParemeters(string TestName, string CategoryName, string TestID, string CategoryID)
        {
            if (Session["log"] != null)
            {
                //LabCompanyContext MCon = new LabCompanyContext();
                //TestParametsrsModels Parameters = new TestParametsrsModels();
                //Parameters.GroupList = new SelectList(MCon.TestGroupsGroupList(), "Group_ID", "Group_Name");
                //Parameters.TestList = new SelectList(MCon.GetTestList(), "Test_ID", "Test_Name");

                // GET: MyTestList
                // return View(Parameters);
                TestParametsrsModels Parameters = new TestParametsrsModels();
                Parameters.TestName = TestName;
                Parameters.GroupName = CategoryName;
                Parameters.TestID = Convert.ToInt16(TestID);
                Parameters.GrpID = Convert.ToInt16(CategoryID);

                Test_ID = Convert.ToInt16(TestID);
                GroupId = Convert.ToInt16(CategoryID);
                Test_Name = TestName;
                Group_Name = CategoryName;
                //// GET: MyTestList
                //return View(Parameters);
                ViewBag.TestName = TestName;
                ViewBag.CategoryName = CategoryName;


                HMSOnWebsEntities db = new HMSOnWebsEntities();
                return View(db.TestParameterDTs.Where(x => x.TestID==Test_ID && x.GrpID==GroupId));

            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }


        public ActionResult Edit_Test_Paremeters(string TestName, string CategoryName, string TestID, string CategoryID)
        {
            if (Session["log"] != null)
            {
              
                TestParametsrsModels Parameters = new TestParametsrsModels();
                Parameters.TestName = TestName;
                Parameters.GroupName = CategoryName;
                Parameters.TestID = Convert.ToInt16(TestID);
                Parameters.GrpID = Convert.ToInt16(CategoryID);

                Test_ID = Convert.ToInt16(TestID);
                GroupId = Convert.ToInt16(CategoryID);
                Test_Name = TestName;
                Group_Name = CategoryName;
                //// GET: MyTestList
                //return View(Parameters);
                ViewBag.TestName = TestName;
                ViewBag.CategoryName = CategoryName;


                HMSOnWebsEntities db = new HMSOnWebsEntities();
                var UnitList = db.UnitDTs.ToList();
                if (UnitList != null)
                {
                    ViewBag.UnitList = UnitList;
                    //UnitList.Find(c => c.Unit_Name == "2").Selected = true;
                }

                //HMSOnWebsEntities db = new HMSOnWebsEntities();
                //return View(db.TestParameterDTs);

                return View(db.TestParameterDTs.Where(x => x.TestID == Test_ID && x.GrpID == GroupId));
               // return View(db.TestParameterDTs);

            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }

        [HttpPost]
        [HandleError]
        public ActionResult Edit_Modify_TestParameterParameter(List<TestParameterDT> TestParameterDTsArrayFromJson)
        {
            try
            {
                using (HMSOnWebsEntities entities = new HMSOnWebsEntities())
                {
                    TestParametsrsModels Parameters = new TestParametsrsModels();
                    //Truncate Table to delete all old records.
                    //entities.Database.ExecuteSqlCommand("TRUNCATE TABLE [Customer]");
                    //TestParameterDT p;
                    //p = entities.TestParameterDTs.Where(d => d.TestID == Test_ID && d.GrpID==GroupId).First();
                    //entities.TestParameterDTs.Remove(p);
                    //entities.SaveChanges();

                    List<TestParameterDT> removelist = new List<TestParameterDT>();
                    removelist = entities.TestParameterDTs.Where(d => d.TestID == Test_ID && d.GrpID == GroupId).ToList();
                    entities.TestParameterDTs.RemoveRange(removelist);
                    entities.SaveChanges();

                    //Check for NULL.
                    if (TestParameterDTsArrayFromJson == null)
                    {
                        TestParameterDTsArrayFromJson = new List<TestParameterDT>();
                        return Json(0);
                    }
                    else
                    {

                        //Loop and insert records.
                        foreach (TestParameterDT param in TestParameterDTsArrayFromJson)
                        {
                            long MaxTestId = entities.TestParameterDTs.Select(p => p.Test_P_ID).DefaultIfEmpty(0).Max();
                            param.Test_P_ID = MaxTestId + 1;

                            param.TestID = Test_ID;
                            param.GrpID = GroupId;
                            param.TestName = Test_Name;
                            param.GroupName = Group_Name;


                            param.Inorder = param.Inorder;
                            string TestParam = param.TestParam;
                            string S= TestParam.Replace("\n", String.Empty).Trim();
                            param.TestParam = S;
                            param.Units= param.Units.Replace("\n", String.Empty).Trim();
                            param.InputType = param.InputType.Replace("\n", String.Empty).Trim();
                            param.DefaultResult = param.DefaultResult.Replace("\n", String.Empty).Trim();
                            param.PreResult = param.DefaultResult;

                            param.SbInorder = 0;


                            entities.TestParameterDTs.Add(param);
                            entities.SaveChanges();
                        }


                        //int insertedRecords = entities.SaveChanges();

                        //return Json(insertedRecords);

                        //return Json(0);
                         //return RedirectToAction("Index");
                        return RedirectToAction("ShowTestParemeters", "MyTestList", new { Redirected = "Redirected To Login" });
                    }
                }
            }
            catch(Exception  ex)
            {
                return Json(ex);
            }
        }




        public ActionResult ParameterNormalValues(string TestName, string CategoryName, string TestID, string CategoryID)
        {
            if (Session["log"] != null)
            {
                HMSOnWebsEntities db = new HMSOnWebsEntities();
                return View(db.ReferenceValuesDTs);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }

       
        public ActionResult AddParameterNormalValues(string TestName, string CategoryName, string TestID, string CategoryID,string TestParameterID,string ParameterName)
        {
            if (Session["log"] != null)
            {
                HMSOnWebsEntities db = new HMSOnWebsEntities();
                Test_ID = Convert.ToInt16(TestID);
                GroupId = Convert.ToInt16(CategoryID);
                Test_Name = TestName;
                Group_Name = CategoryName;
                TestParemter_ID= Convert.ToInt16(TestParameterID);
                ViewBag.TestName = TestName;
                ViewBag.CategoryName = CategoryName;
                ViewBag.ParameterName = ParameterName;
                return View(db.ReferenceValuesDTs);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }

        [HttpPost]
        public ActionResult AddParameterNormalValues(List<ReferenceValuesDT> TestParameterDTsArrayFromJson)
        {
            using (HMSOnWebsEntities entities = new HMSOnWebsEntities())
            {
               
                if (TestParameterDTsArrayFromJson == null)
                {
                    TestParameterDTsArrayFromJson = new List<ReferenceValuesDT>();
                }

                //Loop and insert records.
                foreach (ReferenceValuesDT param in TestParameterDTsArrayFromJson)
                {
                    long MaxTestId = entities.ReferenceValuesDTs.Select(p => p.RefSRNO).DefaultIfEmpty(0).Max();
                    param.RefSRNO = MaxTestId + 1;
                    param.TestID = Test_ID;
                    param.CateID = GroupId;
                    param.TestParamID = TestParemter_ID;
                    param.MinInYears =(param.MinimumAgeInDays / 365);
                    param.MaxInYear= (param.MaxAgeIndays / 365);

                    entities.ReferenceValuesDTs.Add(param);
                    entities.SaveChanges();
                }

                return RedirectToAction("Index", "MyTestList", new { Redirected = "Redirected To Test List" });

            }
        }

        private static List<SelectListItem> PopulateUnitList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["LabCompanyContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = " SELECT Unit_Name, Unit_ID FROM UnitDT";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr["Unit_Name"].ToString(),
                                Value = sdr["Unit_ID"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items;
        }
    }
}

