using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;

namespace HospitalManagementSystem.Models
{
    [Table("TestParameterDT")]
    public class TestParametsrsModels
    {
        [Key]
        public long Test_P_ID { get; set; }
        public int GrpID { get; set; }
        public long TestID { get; set; }

        public string GroupName { get; set; }
        public string TestName { get; set; }
        public string TestParam { get; set; }
        public string TSubParam { get; set; }
        public string InputType { get; set; }
        public string PGroupBy { get; set; }
        public string DefaultResult { get; set; }
        public string PreResult { get; set; }
        public string Units { get; set; }
        public int Inorder { get; set; }
        public int SbInorder { get; set; }

        public string ShSTS { get; set; }


        [NotMapped]
        public SelectList TestList { get; set; }


        [NotMapped]
        public SelectList GroupList { get; set; }

        //public List<SelectListItem> UnitList { get; set; }

       
    }
}