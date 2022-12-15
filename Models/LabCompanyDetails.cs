using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
	[Table("LabCompany")]
	public class LabCompanyDetails
    {
		//private DateTime _SetDefaultDate = DateTime.Now;

		//public LabCompanyDetails()
  //      {
		//	Website = "www.keycodeteh.com";
		//	Created_On = _SetDefaultDate;
		//	Edited_On = _SetDefaultDate;
		//	Company_ID = 1;
		//}

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Company_ID { get; set; }


		[Required(ErrorMessage = "Must Enter Company Name !")]
		public string Company_Name { get; set; }
		public string Address { get; set; }
		public string Landmark { get; set; }
		public string State { get; set; }
		public string District { get; set; }
		public string Pincode { get; set; }
		public string Contact1 { get; set; }
		public string Contact2 { get; set; }
		public string Email_ID { get; set; }
		public string Website { get; set; }
		public string Reg_No { get; set; }
		public string Slogan1 { get; set; }
		public string Slogan2 { get; set; }
		public string Logo { get; set; }
		public int Created_By { get; set; }
		public int Edited_By { get; set; }

		[Display(Name = "Created On")]
		[DataType(DataType.Date)]
		public DateTime Created_On { get; set; }

		[Display(Name = "Edited On")]
		[DataType(DataType.Date)]
		public DateTime Edited_On { get; set; }
		public string StateCode { get; set; }
		public string MsgSend { get; set; }
	}
}