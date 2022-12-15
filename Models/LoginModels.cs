using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Models
{
    [Table("User_DT")]
    public class LoginModels
    {
        [Key]
        public int User_Id { get; set; }
        public string User_Name { get; set; }
        public string Old_Password { get; set; }
        public string New_Password { get; set; }
      
        //[Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public string LoginID { get; set; }
        

       // [Required(ErrorMessage = "Please provide password", AllowEmptyStrings = false)]
       // [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Re_Password { get; set; }

        public string Modules { get; set; }
        public string DoctorsName { get; set; }
        public string MobileNo { get; set; }
        public SelectList LoginList { get; internal set; }
    }
}