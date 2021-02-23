using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API.Models
{
    [Table("Tb_M_Employee")]
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        public string EmployeeName { get; set; }

        [Required]
        public string EmployeePhone { get; set; }

        [Required]
        public string EmployeeEmail { get; set; }

    }
}