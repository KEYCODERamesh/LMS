using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using System.Data.SqlClient;
using System.Data;
using System.Dynamic;
using System.Data.Entity.Core.EntityClient;

namespace HospitalManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private DateTime _SetDefaultDate = DateTime.Now;
        private HMSOnWebsEntities db = new HMSOnWebsEntities();
        SearchModels search = new SearchModels();
        // GET: Home
        public ActionResult Login()
        {
           // var v = db.User_DT.Where(l => l.LoginID.Equals(u.LoginID) && l.Re_Password.Equals(u.Re_Password)).FirstOrDefault();
            return View(new User_DT());
        }

        public ActionResult LogOuts()
        {
            // var v = db.User_DT.Where(l => l.LoginID.Equals(u.LoginID) && l.Re_Password.Equals(u.Re_Password)).FirstOrDefault();
            Session.Abandon();
            Session.Clear();
            Session.Remove("log");
            return RedirectToAction("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User_DT u)
        {
            if (ModelState.IsValid)
            {
                using (HMSOnWebsEntities db = new HMSOnWebsEntities())
                {
                    string createdby = u.Created_By.ToString();

                    var v = db.User_DT.Where(l => l.LoginID.Equals(u.LoginID) && l.Re_Password.Equals(u.Re_Password)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["log"] = v.LoginID.ToString();

                        return RedirectToAction("Index");
                    }
                }
            }

            return View(u);
        }

        public ActionResult Index()
        {
            if (Session["log"] != null)
            {

                DataSet DS= search.GetDS("FillDashBoards", "", "", "", "", "");
                ViewBag.RCOUNTS = DS.Tables[0].Rows[0]["RCOUNTS"].ToString();
                ViewBag.Incomes = DS.Tables[0].Rows[0]["Income"].ToString();
                ViewBag.Paid = DS.Tables[0].Rows[0]["Paid"].ToString();
                ViewBag.Dues = DS.Tables[0].Rows[0]["Dues"].ToString();

                ViewBag.TodayCount = DS.Tables[0].Rows[0]["TodayCount"].ToString();
                ViewBag.TodayIncome = DS.Tables[0].Rows[0]["TodayIncome"].ToString();
                ViewBag.TodayPaid = DS.Tables[0].Rows[0]["TodayPaid"].ToString();
                ViewBag.TodayDues = DS.Tables[0].Rows[0]["TodayDues"].ToString();

                ViewBag.MonthExpense = DS.Tables[0].Rows[0]["MonthExpense"].ToString();
                ViewBag.TodayExpense = DS.Tables[0].Rows[0]["TodayExpense"].ToString();

                DataTable DT = new DataTable();
                DT = DS.Tables[0];

                return View(DS);
            }
            else
            {
                Session.Abandon();
                Session.Clear();
                Session.Remove("log");
                return RedirectToAction("Login");
            }
            
        }
        public ActionResult AddCenterDetails()
        {
            return View(new LabCompanyDetails());

            //return View();
        }
        [HttpPost]
        public ActionResult AddCenterDetails(LabCompany company)
        {

            using (var context = new HMSOnWebsEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    // context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[LabCompany] ON");

                    //string sqltxt = "SELECT ISNULL(MAX(Company_ID),0)+1 FROM LabCompany";
                    int MaxRefID = context.LabCompanies.Select(p => p.Company_ID).DefaultIfEmpty(0).Max();
                    company.Company_ID = MaxRefID+1;
                    company.Created_On = _SetDefaultDate;
                    company.Edited_On = _SetDefaultDate;
                    company.Created_By = 1;
                    company.Edited_By = 1;

                    context.LabCompanies.Add(company);
                    context.SaveChanges();

                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[LabCompany] OFF");

                    transaction.Commit();
                }

            }
            string message = "Created the record successfully";
            ViewBag.Message = message;
            return RedirectToAction("ListCenterDetails");
            //return View(new LabCompanyDetails());
          
        }


        public ActionResult EditCenterDetails(int id)
        {
            //LabCompanyContext labCompanyContext = new LabCompanyContext();
            //LabCompanyDetails labCompanies = labCompanyContext.labCompanyDetails.Single(x=> x.Company_ID == CompanyID).Single;
            //return View(labCompanies);

            using (var context = new HMSOnWebsEntities())
            {
                var data = context.LabCompanies.Where(x => x.Company_ID == id).SingleOrDefault();
                return View(data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCenterDetails(int id,LabCompanyDetails company)
        {
            using (var context = new HMSOnWebsEntities())
            {
                var data = context.LabCompanies.FirstOrDefault(x => x.Company_ID == id);

                if (data != null)
                {
                    data.Company_Name = company.Company_Name;
                    data.Address = company.Address;
                    data.Contact1 = company.Contact1;
                    data.Created_By = 1;
                    data.Edited_By = 1;

                    context.SaveChanges();

                    // It will redirect to
                    // the Read method
                    return RedirectToAction("ListCenterDetails");
                }
                else
                    return View();

                //return View(data);
            }
        }

        [HttpGet]
        public ActionResult ListCenterDetails()
        {
            //HMSOnWebsEntities hms = new HMSOnWebsEntities();
            //return View(from company in hms.LabCompanies.Take(10) select company);

            LabCompanyContext labCompanyContext = new LabCompanyContext();
            List<LabCompanyDetails> labCompanies = labCompanyContext.labCompanyDetails.ToList();
            return View(labCompanies);
        }

        public ActionResult Delete()
        {
            using (var context = new HMSOnWebsEntities())
            {
                
                return View(new LabCompany());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            using (var context = new HMSOnWebsEntities())
            {
                var data = context.LabCompanies.FirstOrDefault(x => x.Company_ID == id);
                if (data != null)
                {
                    context.LabCompanies.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("ListCenterDetails");
                }
                else
                    return View();
            }
        }


        public ActionResult Order()
        {
            return View();
        }


        [ActionName("Dashboard")]
        public ActionResult Dashboard()
        {
            return View();
        }


        public ActionResult AddUsers()
        {
            if (Session["log"] != null)
            {
                return View(new User_DT());
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login" });
            }
        }

        [HttpPost]
        public ActionResult AddUsers(User_DT newusers)
        {

            using (var context = new HMSOnWebsEntities())
            {
                int MaxRefID = context.User_DT.Select(p => p.User_Id).DefaultIfEmpty(0).Max();
                newusers.User_Id = MaxRefID + 1;
                newusers.Created_On = _SetDefaultDate;
                newusers.Edited_On = _SetDefaultDate;
                newusers.Created_By = 1;
                newusers.Edited_By = 1;

                if (context.User_DT.Any(o => o.LoginID == newusers.LoginID))
                {
                    string Message = "Login Id Allready Exists !";
                    ViewBag.Message = Message;
                    return View(new User_DT());
                }
                else
                {
                    context.User_DT.Add(newusers);
                    context.SaveChanges();
                    string Meessage = "User Created Successfully";
                    ModelState.Clear();
                    ViewBag.Message = Meessage;
                    return View(new User_DT());
                }

            }
        }
       
        public ActionResult Userslists()
        {
            LabCompanyContext usercontext = new LabCompanyContext();
            List<LoginModels> logins = usercontext.userslist.ToList();
            return View(logins);
        }


        public ActionResult EditUsersDetails(int id)
        {
            using (var context = new HMSOnWebsEntities())
            {
                var data = context.User_DT.Where(x => x.User_Id == id).SingleOrDefault();
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult EditUsersDetails(int id, User_DT oldusers)
        {
            using (var context = new HMSOnWebsEntities())
            {
                var data = context.User_DT.FirstOrDefault(x => x.User_Id == id);

                if (data != null)
                {
                    data.User_Name = oldusers.User_Name;
                    data.MobileNo = oldusers.MobileNo;
                    data.LoginID = oldusers.LoginID;
                    data.Old_Password = oldusers.Old_Password;
                    data.New_Password = oldusers.New_Password;
                    data.Re_Password = oldusers.Re_Password;

                    data.Created_By = 1;
                    data.Edited_By = 1;
                    context.SaveChanges();
                    return RedirectToAction("Userslists");
                }
                else
                    return View();

                //return View(data);
            }
        }



        public ActionResult DeleteUsers()
        {
            using (var context = new HMSOnWebsEntities())
            {

                return View(new User_DT());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUsers(int id)
        {
            using (var context = new HMSOnWebsEntities())
            {
                var data = context.User_DT.FirstOrDefault(x => x.User_Id == id);
                if (data != null)
                {
                    context.User_DT.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("Userslists");
                }
                else
                    return View();
            }
        }


        /*---------Add USers Role------------------*/
        public ActionResult AddUSersRoles()
        {
            dynamic dynamicmodels= new ExpandoObject();

            LoginModels MD = new LoginModels();
            LabCompanyContext MCon = new LabCompanyContext();
            MD.LoginList = new SelectList(MCon.GetLoginList(), "SRNO", "LoginID"); // model binding

            //  dynamicmodels.UsesRole = MCon.usersroles;
            using (var context = new HMSOnWebsEntities())
            {
                var items = context.User_DT.ToList();
                if (items != null)
                {
                    ViewBag.data = items;
                }

            }


            return View(new UserRole());
        }

        [HttpPost]
        public ActionResult AddUSersRoles(UserRole roles)
        {
            try
            {
                string SelectedLogins = "";
              

                using (var context = new HMSOnWebsEntities())
                {
                    //  var studentName = context.User_DT.SqlQuery("Select LoginID from User_DT where User_Id='"+ roles.LoginID + "'").FirstOrDefault<User_DT>();

                    //string LogNames = studentName.ToString();

                    var items = context.User_DT.Where(i => i.User_Id.ToString() == roles.LoginID).FirstOrDefault<User_DT>(); ;
                    if (items != null)
                    {
                        ViewBag.data = items;
                        SelectedLogins = items.LoginID;
                    }

                 

                }


                string deletefirst = "DELETE FROM  UserRoles where loginid='" + SelectedLogins + "'";
                SqlCommand cmdelete = new SqlCommand(deletefirst, MyLabstring.opencon());
                cmdelete.CommandType = CommandType.Text;
                cmdelete.CommandText = deletefirst;
                cmdelete.ExecuteNonQuery();

                SqlCommand cmd = new SqlCommand("SP_SaveUsersRoles", MyLabstring.opencon());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_SaveUsersRoles";
                cmd.Parameters.AddWithValue("@OPERATION", "SaveROles");
                cmd.Parameters.AddWithValue("@MODULES", roles.Modules);
                cmd.Parameters.AddWithValue("@A", roles.AddRecord);
                cmd.Parameters.AddWithValue("@E", roles.EditRecord);
                cmd.Parameters.AddWithValue("@D", roles.DeleteRecord);
                cmd.Parameters.AddWithValue("@Delpass", roles.DelPass);
                cmd.Parameters.AddWithValue("@EditPass", roles.EditPass);
                cmd.Parameters.AddWithValue("@loginid", SelectedLogins);


                //using (var con = new EntityConnection("name=KeyCodeEntity"))
                //{
                //    var context = new HMSOnWebsEntities();
                //    con.Open();
                //    EntityCommand cmdentity = con.CreateCommand();
                //    cmd.CommandText = "SELECT VALUE st FROM KeyCodeEntity.User_DT as st where st.User_Id='" + roles.LoginID + "'";
                //    Dictionary<int, string> dict = new Dictionary<int, string>();
                //    using (EntityDataReader rdr = cmd.ExecuteReader(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection))
                //    {
                //        while (rdr.Read())
                //        {
                //            int a = rdr.GetInt32(0);
                //            var b = rdr.GetString(1);
                //            dict.Add(a, b);
                //        }
                //    }
                //}



                // var prov_ids = roles.LoginID.Where(r => r.ProjectId == roles.LoginID).Select(r => r.ProvinceId);



                //UsersRole umodels = new UsersRole();
                //var selectedItem = umodels.getLoginID.Find(p => p.Value == roles.LoginID.ToString());
                //if (selectedItem != null)
                //{
                //    selectedItem.Selected = true;
                //    //ViewBag.Message = "Fruit: " + selectedItem.Text;
                //    //ViewBag.Message += "\\nQuantity: " + fruit.Quantity;

                //}


                cmd.ExecuteNonQuery();

                return RedirectToAction("ShowUSersRoles");
            }
            catch(Exception ex)
            {
                return RedirectToAction("ShowUSersRoles");
            }

            
        }
        public ActionResult ShowUSersRoles()
        {
            // LabCompanyContext usercontext = new LabCompanyContext();
            //List<UsersRole> loginsroles = usercontext.usersroles.ToList();
            //return View(loginsroles);

            // Calling Stored Procedures its working

            //HMSOnWebsEntities enitiess = new HMSOnWebsEntities();
            //return View(enitiess.SearchUserRoles(""));

            // not working with parameter
            //HMSOnWebsEntities enitiess = new HMSOnWebsEntities();
            //return View(enitiess.SPSearch("GetUsersRoles", "Admin", "", "", "", ""));


            LabCompanyContext context = new LabCompanyContext();
            DataTable dt =context.SearchDataTables("GetUsersRolesLabs", "Admin","","","","");
            //return ConvertToDictionary(ds.Tables[0]);

            return View(dt);

        }
    }
}