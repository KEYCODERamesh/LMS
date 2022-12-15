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
    public class StockEntryController : Controller
    {
        private DateTime _SetDefaultDate = DateTime.Now;

        // GET: StockEntry
        public ActionResult ShowVendor()
        {
            if (Session["log"] != null)
            {
                LabCompanyContext vendorcontet = new LabCompanyContext();
                List<VendorModel> vendors = vendorcontet.vendordetails.ToList();
                return View(vendors);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login From Stock Entry " });
            }
        }
        public ActionResult AddVendor()
        {
            if (Session["log"] != null)
            {
                return View(new VendorDT());
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login From Vendor " });
            }
        }
        [HttpPost]
        public ActionResult AddVendor(VendorDT vendors)
        {
            if (Session["log"] != null)
            {

                using (var context = new HMSOnWebsEntities())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        int MaxRefID = context.VendorDTs.Select(p => p.VID).DefaultIfEmpty(0).Max();
                        vendors.VID = MaxRefID + 1;
                        vendors.SyncSts = true;
                        vendors.HID = 1;
                        context.VendorDTs.Add(vendors);
                        context.SaveChanges();
                        transaction.Commit();
                        ModelState.Clear();
                        string message = "Vendor Created Successfully";
                        ViewBag.Message = message;
                        return View(new VendorDT());
                    }

                }
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login From Vendor " });
            }
        }

        [HttpGet]
        public ActionResult EditVendor(int id)
        {
            if (Session["log"] != null)
            {
                using (var context = new HMSOnWebsEntities())
                {
                    var data = context.VendorDTs.Where(x => x.VID == id).SingleOrDefault();
                    return View(data);
                }
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login From Vendor " });
            }
           
        }
        [HttpPost]
        public ActionResult EditVendor(int id,VendorDT vendors)
        {
            if (Session["log"] != null)
            {
                using (var context = new HMSOnWebsEntities())
                {
                    var data = context.VendorDTs.Where(x => x.VID == id).SingleOrDefault();
                    if(data!=null)
                    {
                        data.VendorName = vendors.VendorName;
                        data.GSTIN = vendors.GSTIN;
                        data.StateName = vendors.StateName;
                        data.StateCode = vendors.StateCode;
                        data.Address = vendors.Address;
                        data.Mobile = vendors.Mobile;
                        data.EmailId = vendors.EmailId;
                        data.OpBal = vendors.OpBal;
                        context.SaveChanges();
                        ModelState.Clear();
                        string message = "Vendor Modified Successfully";
                        ViewBag.Message = message;

                        // return RedirectToAction("ShowVendor");
                        // return PartialView(new VendorDT()); it returns same view

                         return View(new VendorDT());
                        //return PartialView("PartialViewExamples");
                    }
                    else
                    return View(data);
                }
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login From Vendor " });
            }

        }

        public ActionResult DeleteVendor()
        {
            using (var context = new HMSOnWebsEntities())
            {
                return View(new VendorDT());
            }
            
        }

        [HttpPost]
        public ActionResult DeleteVendor(int id)
        {
            using (var context = new HMSOnWebsEntities())
            {
                var data = context.VendorDTs.FirstOrDefault(x => x.VID == id);
                if (data != null)
                {
                    context.VendorDTs.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("ShowVendor");
                }
                else
                    return View();
            }

        }


        /*----------------------------------------*/
        public ActionResult ShowProducts()
        {
            if (Session["log"] != null)
            {
                LabCompanyContext dbcontext = new LabCompanyContext();
                List<LabItems> products = dbcontext.Products.ToList();
                return View(products);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login From Stock Entry " });
            }
        }
        public ActionResult AddProducts()
        {
            if (Session["log"] != null)
            {
                return View(new LabItems());
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login From Stock Entry " });
            }
        }
        [HttpPost]
        public ActionResult AddProducts(LabItems models)
        {
            if (Session["log"] != null)
            {
                if (ModelState.IsValid)
                {
                    using (var context = new HMSOnWebsEntities())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {


                            LabITEMDT lab = new LabITEMDT();
                            lab.ItemName = models.ItemName;
                            lab.OPQTY = models.OPQTY;
                            long MaxRefID = context.LabITEMDTs.Select(p => p.ITEMID).DefaultIfEmpty(0).Max();
                            lab.ITEMID = MaxRefID + 1;

                            lab.OnDate = _SetDefaultDate;
                            lab.Createdby = Session["log"].ToString();
                            lab.BrID = 1;
                            lab.HID = 1;
                            context.LabITEMDTs.Add(lab);
                            context.SaveChanges();
                            transaction.Commit();
                            ModelState.Clear();
                            string message = "Product Added Successfully";
                            ViewBag.Message = message;
                            return View(new LabItems());
                        }

                    }
                }
                else
                {
                    return View(new LabItems());
                }
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login From Stock Entry " });
            }
        }


        public ActionResult EditProducts(long id)
        {
            if (Session["log"] != null)
            {
                using (var context = new HMSOnWebsEntities())
                {
                    var data = context.LabITEMDTs.Where(x => x.ITEMID == id).SingleOrDefault();
                    return View(data);
                }
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login From Stock Entry " });
            }
        }
        [HttpPost]
        public ActionResult EditProducts(long id,LabITEMDT labitems)
        {
            if (Session["log"] != null)
            {
                using (var context = new HMSOnWebsEntities())
                {
                    var data = context.LabITEMDTs.Where(x => x.ITEMID == id).SingleOrDefault();
                    if (data != null)
                    {
                        data.ItemName = labitems.ItemName;
                        data.OPQTY = labitems.OPQTY;
                        labitems.OnDate = _SetDefaultDate;
                        labitems.Createdby = Session["log"].ToString();
                        labitems.BrID = 1;
                        labitems.HID = 1;
                        context.SaveChanges();
                        ModelState.Clear();
                        string message = "Product Modified Successfully";
                        ViewBag.Message = message;
                        return RedirectToAction("ShowProducts");

                    }
                    else
                        return View(new LabITEMDT());
                }
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login From Stock Entry " });
            }
        }

       
        public ActionResult DeleteProducts()
        {
            using (var context = new HMSOnWebsEntities())
            {
                return View(new LabITEMDT());
            }

        }


        [HttpPost]
        public ActionResult DeleteProducts(int id)
        {
            using (var context = new HMSOnWebsEntities())
            {
                var data = context.LabITEMDTs.FirstOrDefault(x => x.ITEMID == id);
                if (data != null)
                {
                    context.LabITEMDTs.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("ShowProducts");
                }
                else
                    return View();
            }

        }

        [HttpPost]
        public ActionResult Details(int id)
        {
            using (var context = new HMSOnWebsEntities())
            {
                var product = context.LabITEMDTs.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return PartialView("Details", context.LabITEMDTs.Find(id));
            }
        }



        public JsonResult DeleteProduct(long ITEMID)
        {
            using (var context = new HMSOnWebsEntities())
            {
                bool result = false;
                LabITEMDT labitems = context.LabITEMDTs.Where(x => x.ITEMID == ITEMID).SingleOrDefault();

                if(labitems!=null)
                {
                    context.LabITEMDTs.Remove(labitems);
                    context.SaveChanges();

                    result = true;
                }
                else
                {
                    result = false;
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }


       
        public ActionResult AddProductsWithjquery()
        {
            if (Session["log"] != null)
            {

                return View(new LabItems());
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login From Stock Entry " });
            }
        }

        [HttpPost]
        public ActionResult AddProductsWithjquery(LabItems models)
        {
            if (Session["log"] != null)
            {
                if (ModelState.IsValid)
                {
                    using (var context = new HMSOnWebsEntities())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {


                            LabITEMDT lab = new LabITEMDT();
                            lab.ItemName = models.ItemName;
                            lab.OPQTY = models.OPQTY;
                            long MaxRefID = context.LabITEMDTs.Select(p => p.ITEMID).DefaultIfEmpty(0).Max();
                            lab.ITEMID = MaxRefID + 1;

                            lab.OnDate = _SetDefaultDate;
                            lab.Createdby = Session["log"].ToString();
                            lab.BrID = 1;
                            lab.HID = 1;
                            context.LabITEMDTs.Add(lab);
                            context.SaveChanges();
                            transaction.Commit();
                            ModelState.Clear();
                            string message = "Product Added Successfully";
                            ViewBag.Message = message;
                            return View(new LabItems());
                        }

                    }
                }
                else
                {
                    return View(new LabItems());
                }
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login From Stock Entry " });
            }
        }



        [HttpPost]
        public JsonResult InsertCustomer(LabItems labs)
        {
            string query = "INSERT INTO LabITEMDT(ITEMID,HID,ItemName,OPQTY,Createdby,BrID) VALUES(@ITEMID,@HID,@ItemName, @OPQTY,@createdby,@brid)";
            //query += "SELECT SCOPE_IDENTITY()";
            string constr = ConfigurationManager.ConnectionStrings["LabCompanyContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (var context = new HMSOnWebsEntities())
                    {
                        long MaxRefID = context.LabITEMDTs.Select(p => p.ITEMID).DefaultIfEmpty(0).Max();
                        labs.ITEMID = MaxRefID + 1;
                        cmd.Parameters.AddWithValue("@ITEMID", labs.ITEMID);
                        cmd.Parameters.AddWithValue("@HID", 1);
                        cmd.Parameters.AddWithValue("@ItemName", labs.ItemName);
                        cmd.Parameters.AddWithValue("@OPQTY", labs.OPQTY);
                        cmd.Parameters.AddWithValue("@createdby", Session["log"].ToString());
                        cmd.Parameters.AddWithValue("@brid", 1);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        //labs.ITEMID = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                    }

                   
                }
            }

            return Json(labs);
        }



        public JsonResult EditCustomer(int? id)
        {
            LabCompanyContext prodcontext = new LabCompanyContext();

            List<LabItems> LabItems = new List<LabItems>();
            string sqltxt = "SELECT * FROM LabITEMDT WHERE ITEMID='"+id+"'";
            SqlCommand cmd = new SqlCommand(sqltxt, MyLabstring.opencon());
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);
           
            foreach (DataRow dr in dt.Rows)
            {
                LabItems.Add(
                    new LabItems
                    {
                        ITEMID = Convert.ToInt32(dr["ITEMID"]),
                        ItemName = Convert.ToString(dr["ItemName"]),
                        OPQTY = Convert.ToDecimal(dr["OPQTY"])
                    });
            }

          //  var customer = prodcontext.GetCustomers().Find(x => x.ITEMID.Equals(id));
          
            return Json(LabItems, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateCustomer(LabItems labs)
        {
            string query = "UPDATE  LabITEMDT SET HID=@HID,ItemName=@ItemName,OPQTY=@OPQTY,Createdby=@createdby,BrID=@brid WHERE ITEMID=@ITEMID ";
            //query += "SELECT SCOPE_IDENTITY()";
            string constr = ConfigurationManager.ConnectionStrings["LabCompanyContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (var context = new HMSOnWebsEntities())
                    {
                        //long MaxRefID = context.LabITEMDTs.Select(p => p.ITEMID).DefaultIfEmpty(0).Max();
                        //labs.ITEMID = MaxRefID + 1;
                        cmd.Parameters.AddWithValue("@ITEMID", labs.ITEMID);
                        cmd.Parameters.AddWithValue("@HID", 1);
                        cmd.Parameters.AddWithValue("@ItemName", labs.ItemName);
                        cmd.Parameters.AddWithValue("@OPQTY", labs.OPQTY);
                        cmd.Parameters.AddWithValue("@createdby", Session["log"].ToString());
                        cmd.Parameters.AddWithValue("@brid", 1);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        //labs.ITEMID = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                    }

                }
            }

            return Json(labs);
        }
    }
}