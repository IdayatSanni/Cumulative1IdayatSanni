using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.ModelBinding;

namespace SchoolProject.Models
{
    public class Teacher
    {
        // The following fields define a teacher
        
        public int TeacherId { get; set; } 
        [Required(ErrorMessage = "Teacher's First name is required")]
        public decimal? Salary { get; set; }
        [Required]
        public string EmployeeNumber { get; set; }
        public DateTime HireDate { get; set; }
        [Required]
        public string TeacherFname { get; set; }
        [Required]
        public string TeacherLname { get; set; }
        
        // server side validation
        public bool IsValid()
        {
            bool valid = true;

            if (TeacherFname == null || TeacherLname == null || Salary == null || EmployeeNumber == null)
            {
                //Base validation to check if the fields are entered.
                valid = false;
            }
            else
            {
                //Validation for fields to make sure they meet server constraints
                if (TeacherFname.Length < 2 || TeacherFname.Length > 255) valid = false;
                if (TeacherLname.Length < 2 || TeacherFname.Length > 255) valid = false;

            }
            Debug.WriteLine("The model validity is : " + valid);

            return valid;
        }


        // parameter less constructor function

        public Teacher() { }
    }
}