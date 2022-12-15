using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class MultiModelView
    {
        public IEnumerable<LoginModels> logins { get; set; }
        public IEnumerable<UserRole> roles { get; set; }
    }
}