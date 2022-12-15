using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Models
{
    [Table("TestDT")]
    public class MyLabTestList
    {
        [Key]
        public long Test_ID { get; set; }
        public string Test_Name { get; set; }
        public decimal Test_Rate { get; set; }
        public int Group_ID { get; set; }
        public string Group_Name { get; set; }
      
        public int Created_By { get; set; }
        public int Edited_By { get; set; }
        public DateTime Created_On { get; set; }
        public DateTime Edited_On { get; set; }
        public string T_ShortName { get; set; }
        public string Method_Name { get; set; }
        public string Instruemnt { get; set; }


        public string Interpretation { get; set; }
        public string Notes { get; set; }
        public string Comments { get; set; }


        [NotMapped]
        public SelectList GroupList { get; set; }
    }
}