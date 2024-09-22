using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Managament_System.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name cannot be longer than 100 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hire Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid salary.")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "CNIC is required.")]
        public string CNIC { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        public string PhoneNumber { get; set; }

        public string Title { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Department { get; set; }


        [Required(ErrorMessage = "Department is required.")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        public int PositionID { get; set; }
        [Required(ErrorMessage = "Employee Type is required.")]
        public int EmpTypeID { get; set; }



    }
}