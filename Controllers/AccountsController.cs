using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts


        private void BindAccountslists()
        {
            string constr = ConfigurationManager.ConnectionStrings["LabCompanyContext"].ToString();
            SqlConnection _con = new SqlConnection(constr);
            SqlDataAdapter _da = new SqlDataAdapter("Select Accounts,HeadID From AccSubHeadsDT ", constr);
            DataTable _dt = new DataTable();
            _da.Fill(_dt);
            ViewBag.AccountList = ToSelectList(_dt, "HeadID", "Accounts");
        }

        public ActionResult AddNewHeads()
        {
            if (Session["log"] != null)
            {
                return View(new AccHeadDT());
            }
            else
            {
                //return RedirectToAction("Login");
                return RedirectToAction("Login", "Home", new { FileUploadMsg = "" });
            }

        }

        [HttpPost]
        public ActionResult AddNewHeads(AccHeadDT accheads)
        {
            if (Session["log"] != null)
            {
                using (var context = new HMSOnWebsEntities())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        // groups.Group_ID = (int)(intIdt + 1);
                        int MaxGrpID = context.AccHeadDTs.Select(p => p.AHID).DefaultIfEmpty(0).Max();
                        accheads.AHID = MaxGrpID + 1;
                        context.AccHeadDTs.Add(accheads);
                        context.SaveChanges();

                        transaction.Commit();
                    }

                }
                string message = "Account Head Created successfully";
                ViewBag.Message = message;
                return RedirectToAction("AddNewHeads");
            }
            else
            {
                //return RedirectToAction("Login");
                return RedirectToAction("Login", "Home", new { FileUploadMsg = "" });
            }

        }



        public ActionResult AddAccountHeadsList()
        {
            if (Session["log"] != null)
            {
                AccountsModels accounts = new AccountsModels();

                string constr = ConfigurationManager.ConnectionStrings["LabCompanyContext"].ToString();
                SqlConnection _con = new SqlConnection(constr);
                SqlDataAdapter _da = new SqlDataAdapter("Select Acc_HeadName,AHID From AccHeadDT ", constr);
                DataTable _dt = new DataTable();
                _da.Fill(_dt);
                ViewBag.HeadList = ToSelectList(_dt, "AHID", "Acc_HeadName");
                return View(accounts);
                //HMSOnWebsEntities db = new HMSOnWebsEntities();
                //return View(db.AccSubHeadsDTs);


                // return View(accounts);
            }
            else
            {
                //return RedirectToAction("Login");
                return RedirectToAction("Login", "Home", new { FileUploadMsg = "" });
            }

        }

        [HttpPost]
        public ActionResult AddAccountHeadsList(AccountsModels accheadsfromview)
        {
            if (Session["log"] != null)
            {
                using (var context = new HMSOnWebsEntities())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        AccSubHeadsDT accheads = new AccSubHeadsDT();
                        int MaxGrpID = context.AccSubHeadsDTs.Select(p => p.HeadID).DefaultIfEmpty(0).Max();
                        accheads.HeadID = MaxGrpID + 1;
                        string strDDLValue = Request.Form["ddlheads"].ToString();
                        var data = context.AccHeadDTs.Where(x => x.Acc_HeadName == strDDLValue).SingleOrDefault();

                        accheads.AccID =Convert.ToInt16(strDDLValue);

                        accheads.Accounts = accheadsfromview.Accounts;
                        accheads.AccTypes = "C";
                        context.AccSubHeadsDTs.Add(accheads);
                        context.SaveChanges();

                        transaction.Commit();
                    }

                }
                string message = "Account Head Created successfully";
                ViewBag.Message = message;
                return RedirectToAction("AddAccountHeadsList");
            }
            else
            {
                //return RedirectToAction("Login");
                return RedirectToAction("Login", "Home", new { FileUploadMsg = "" });
            }

        }

        public ActionResult ShowAccountsList()
        {
            if (Session["log"] != null)
            {
                HMSOnWebsEntities db = new HMSOnWebsEntities();
                return View(db.AccSubHeadsDTs);
            }
            else
            {
                //return RedirectToAction("Login");
                return RedirectToAction("Login", "Home", new { FileUploadMsg = "" });
            }

        }



        /*---------------Expense Entry----------------*/
        public ActionResult ExpenseEntry()
        {
            if (Session["log"] != null)
            {

                BindAccountslists();
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

                return View(new ExPDT());
            }
            else
            {
                //return RedirectToAction("Login");
                return RedirectToAction("Login", "Home", new { FileUploadMsg = "" });
            }

        }

        [HttpPost]
        public ActionResult ExpenseEntry(ExPDT getexpenses)
        {
            if (Session["log"] != null)
            {


                using (var context = new HMSOnWebsEntities())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        ExPDT accheads = new ExPDT();
                        long MaxGrpID = context.ExPDTs.Select(p => p.ExpID).DefaultIfEmpty(0).Max();
                        accheads.ExpID = MaxGrpID + 1;
                        string strDDLValue = Request.Form["ddlaccountsheads"].ToString();
                     

                        accheads.AccountID = Convert.ToInt16(strDDLValue);

                        accheads.Amount = getexpenses.Amount;
                       // string paymodes = Request.Form["ddlpaymodes"].ToString();
                        string paymodes2 = Request.Form["ddlpaymodes2"].ToString();
                        accheads.PayMode = paymodes2;
                        accheads.ExDate = getexpenses.ExDate;
                        accheads.Remarks = getexpenses.Remarks;
                        //accheads.Edate = getexpenses.ExDate;

                        accheads.EType = "C";
                        context.ExPDTs.Add(accheads);
                        context.SaveChanges();

                        transaction.Commit();
                    }

                }
                string message = "Account Head Created successfully";
                ViewBag.Message = message;
                //return RedirectToAction("AddAccountHeadsList");
                BindAccountslists();

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

                return View(new ExPDT());
            }
            else
            {
                //return RedirectToAction("Login");
                return RedirectToAction("Login", "Home", new { FileUploadMsg = "" });
            }

        }

        public ActionResult ShowExpenseDetails()
        {
            if (Session["log"] != null)
            {
                HMSOnWebsEntities db = new HMSOnWebsEntities();
                return View(db.ExPDTs);
            }
            else
            {
                //return RedirectToAction("Login");
                return RedirectToAction("Login", "Home", new { FileUploadMsg = "" });
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
    }
}