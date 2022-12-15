using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Controllers
{
    public class LabPurchaseDTsController : Controller
    {
        private HMSOnWebsEntities db = new HMSOnWebsEntities();
        private static DataTable DTITEMS = new DataTable();
        // GET: LabPurchaseDTs
        public ActionResult Index()
        {
            if (Session["log"] != null)
            {
                var labPurchaseDTs = db.LabPurchaseDTs.Include(l => l.LabITEMDT).Include(l => l.VendorDT);
                return View(labPurchaseDTs.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login From Purchase Stock Entry " });
            }
        }

        // GET: LabPurchaseDTs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabPurchaseDT labPurchaseDT = db.LabPurchaseDTs.Find(id);
            if (labPurchaseDT == null)
            {
                return HttpNotFound();
            }
            return View(labPurchaseDT);
        }

        // GET: LabPurchaseDTs/Create
        public ActionResult Create()
        {
            if (Session["log"] != null)
            {
                ViewBag.ITEMID = new SelectList(db.LabITEMDTs, "ITEMID", "ItemName");
                ViewBag.VendorID = new SelectList(db.VendorDTs, "VID", "VendorName");
                return View(new LabPurchaseDT());
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Redirected = "Redirected To Login From Purchase Stock Entry " });
            }
        }

        // POST: LabPurchaseDTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PurchId,HID,VendorID,BillNo,BillDate,ItemName,QTY,Units,Rate,GSTP,GSTMAT,DisPer,Grands,EntryBy,EDate,ITEMID")] LabPurchaseDT labPurchaseDT)
        {
            if (ModelState.IsValid)
            {
                db.LabPurchaseDTs.Add(labPurchaseDT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ITEMID = new SelectList(db.LabITEMDTs, "ITEMID", "ItemName", labPurchaseDT.ITEMID);
            ViewBag.VendorID = new SelectList(db.VendorDTs, "VID", "VendorName", labPurchaseDT.VendorID);
            return View(labPurchaseDT);
        }

        // GET: LabPurchaseDTs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabPurchaseDT labPurchaseDT = db.LabPurchaseDTs.Find(id);
            if (labPurchaseDT == null)
            {
                return HttpNotFound();
            }
            ViewBag.ITEMID = new SelectList(db.LabITEMDTs, "ITEMID", "ItemName", labPurchaseDT.ITEMID);
            ViewBag.VendorID = new SelectList(db.VendorDTs, "VID", "VendorName", labPurchaseDT.VendorID);
            return View(labPurchaseDT);
        }

        // POST: LabPurchaseDTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PurchId,HID,VendorID,BillNo,BillDate,ItemName,QTY,Units,Rate,GSTP,GSTMAT,DisPer,Grands,EntryBy,EDate,ITEMID")] LabPurchaseDT labPurchaseDT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(labPurchaseDT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ITEMID = new SelectList(db.LabITEMDTs, "ITEMID", "ItemName", labPurchaseDT.ITEMID);
            ViewBag.VendorID = new SelectList(db.VendorDTs, "VID", "VendorName", labPurchaseDT.VendorID);
            return View(labPurchaseDT);
        }

        // GET: LabPurchaseDTs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabPurchaseDT labPurchaseDT = db.LabPurchaseDTs.Find(id);
            if (labPurchaseDT == null)
            {
                return HttpNotFound();
            }
            return View(labPurchaseDT);
        }

        // POST: LabPurchaseDTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            LabPurchaseDT labPurchaseDT = db.LabPurchaseDTs.Find(id);
            db.LabPurchaseDTs.Remove(labPurchaseDT);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }




        /*-----------------------------Add Item & Bind to grid--------------------*/

        public void Additems()
        {
            DTITEMS = new DataTable();
            if(DTITEMS.Rows.Count>0)
            {
                DTITEMS.Columns.Add("Name");
                DTITEMS.Columns.Add("Rate");
                DTITEMS.Columns.Add("QTY");
                DataRow dtr = DTITEMS.NewRow();
                dtr["Name"] = "Shirt";
                dtr["Rate"] = "250";
                dtr["QTY"] = "100";
                DTITEMS.Rows.Add(dtr);
            }
            else
            {
                DataRow dtr = DTITEMS.NewRow();
                dtr["Name"] = "Shirt";
                dtr["Rate"] = "250";
                dtr["QTY"] = "100";
                DTITEMS.Rows.Add(dtr);
            }

        }
    }
}
