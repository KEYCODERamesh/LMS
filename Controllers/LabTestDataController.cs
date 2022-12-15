using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class LabTestDataController : Controller
    {
        // GET: LabTestData
        public ActionResult AddTest()
        {
            if (Session["log"] != null)
            {
                return View();
            }
            else
            {
                //return RedirectToAction("Login");
                return RedirectToAction("Login","Home", new { FileUploadMsg = "File uploaded successfully" });
            }

           // return View();
        }
    }
}