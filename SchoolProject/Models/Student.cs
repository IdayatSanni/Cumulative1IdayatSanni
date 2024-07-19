using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolProject.Models
{
    public class Student
    {
        // The following fields define a student
        public int StudentId { get; set; }
        public string StudentNumber { get; set; }
        public DateTime EnrolDate { get; set; }
        public string StudentFname { get; set; }
        public string StudentLname { get; set; }
    }
}