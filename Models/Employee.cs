using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bulk_Email_Sending_Groupwise.Models
{
    public class Employee
    {
        [Key]
        public int Emp_ID { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        [StringLength(15, ErrorMessage = "FirstName cannot exceed 15 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(15, ErrorMessage = "LastName cannot exceed 15 characters.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "BirthDate is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "BirthDate")]
        public DateOnly BirthDate { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{3,}$", ErrorMessage = "Invalid Email Address")]
        public string? Email_ID { get; set; }
        [NotMapped]
        [Display(Name = "Selected Menu")]
        public int SelectedMenuId { get; set; }
        //public ICollection<DetailsEmp> DetailsEmp { get; set; } = new List<DetailsEmp>();
        public ICollection<EmpMenuMapping> EmpMenuMapping { get; set; } = new List<EmpMenuMapping>();
        public ICollection<EmpDeptMapping> EmpDeptMapping { get; set; } = new List<EmpDeptMapping>();
    }
}
