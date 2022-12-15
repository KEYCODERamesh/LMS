using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    [Table("TestsPanels")]
    public class MyTestPanels
    {
        [Key]
        public int PanelID { get; set; }
        public string PanelName { get; set; }
        public int CategoryID { get; set; }
        public string TestNames { get; set; }
        public double Fees { get; set; }
    }
}