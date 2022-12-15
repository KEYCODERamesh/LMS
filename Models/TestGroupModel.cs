using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace HospitalManagementSystem.Models
{
    [Table("GroupDT")]
    public class TestGroupModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Group_ID { get; set; }
        public string Group_Name { get; set; }
        public int Created_By { get; set; }
        public int Edited_By { get; set; }


        [Display(Name = "Created On")]
        [DataType(DataType.Date)]
        public DateTime Created_On { get; set; }

        [Display(Name = "Edited On")]
        [DataType(DataType.Date)]
        public DateTime Edited_On { get; set; }


    }
}