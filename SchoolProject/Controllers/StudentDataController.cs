using MySql.Data.MySqlClient;
using SchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolProject.Controllers
{
    
    public class StudentDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();
        /// <summary>
        /// It connects to the school database and return the list of students
        /// </summary>
        /// <example>Get api/StudentData/ListStudents -> 
        /// [{"EnrolDate":"2018-06-18T00:00:00","StudentFname":"Sarah","StudentId":1,"StudentLname":"Valdez","StudentNumber":"N1678"}, 
        /// {"EnrolDate":"2018-08-02T00:00:00","StudentFname":"Jennifer","StudentId":2,"StudentLname":"Faulkner","StudentNumber":"N1679"}, 
        /// {"EnrolDate":"2018-06-14T00:00:00","StudentFname":"Austin","StudentId":3,"StudentLname":"Simon","StudentNumber":"N1682"}]
        /// </example>
        /// <returns>A list of students</returns>
        [HttpGet]
        [Route("api/StudentData/ListStudents")]
        public IEnumerable<Student> ListStudents()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "SELECT * from students";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of students
            List<Student> Students = new List<Student>();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = (string)ResultSet["studentfname"];
                string StudentLname = (string)ResultSet["studentlname"];
                string StudentNumber = (string)ResultSet["studentnumber"];

                // convert date to string
                DateTime EnrolDate;
                DateTime.TryParse(ResultSet["enroldate"].ToString(), out EnrolDate);


                // creating a student object
                Student NewStudent = new Student();
                NewStudent.EnrolDate = EnrolDate;
                NewStudent.StudentId = StudentId;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentFname = StudentFname;
                //Add the student to the List
                Students.Add(NewStudent);

            }
            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of students
            return Students;
        }

        /// <summary>
        /// Finds a student using the id from the database
        /// </summary>
        /// <param name="id">The id to match against a primary key record in the MySQL Database</param>
        /// <returns>A student object</returns>
        public Student FindStudent(int id)
        {
            // create an instance of the student
            Student NewStudent = new Student();
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Students where studentid = " +id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();


            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {

                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = (string)ResultSet["studentfname"];
                string StudentLname = (string)ResultSet["studentlname"];
                string StudentNumber = (string)ResultSet["studentnumber"];

                // convert date to string
                DateTime EnrolDate;
                DateTime.TryParse(ResultSet["enroldate"].ToString(), out EnrolDate);


                // creating a student object
                NewStudent.EnrolDate = EnrolDate;
                NewStudent.StudentId = StudentId;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentFname = StudentFname;   

            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the student object
            return NewStudent;
        }
    }
}

