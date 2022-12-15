using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    [Table("UnitDT")]
    public class MyUnits
    {
        [Key]
        public int Unit_ID { get; set; }
        public string Unit_Name { get; set; }
    }
}