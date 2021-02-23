using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class VM_RegistrationModel
    {
        [Required]
        public string EmployeeName { get; set; }

        [Required]
        public string EmployeePhone { get; set; }

        [Required]
        public string EmployeeEmail { get; set; }

        [Required][DataType(DataType.Password)]
        public string AccountPassword { get; set; }
        [DataType(DataType.Password)]
        public string AccountNewPassword { get; set; }
        public int AccountID { get; set; }
        public int EmployeeID { get; set; }

    }
}