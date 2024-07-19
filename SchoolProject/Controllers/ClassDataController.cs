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
    public class ClassDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();
        /// <summary>
        /// It connects to the school database and return the list of classes
        /// </summary>
        /// <example>Get api/ClassData/ListClasses -> 
        /// [{"ClassCode":"http5101","ClassId":1,"ClassName":"Web Application Development", "FinishDate":"2018-12-14T00:00:00","StartDate":"2018-09-04T00:00:00"}, 
        /// {"ClassCode":"http5102","ClassId":2,"ClassName":"Project Management", "FinishDate":"2018-12-14T00:00:00","StartDate":"2018-09-04T00:00:00"}, 
        /// {"ClassCode":"http5103","ClassId":3,"ClassName":"Web Programming", "FinishDate":"2018-12-14T00:00:00","StartDate":"2018-09-04T00:00:00"}]
        /// </example>
        /// <returns>A list of classes</returns>
        [HttpGet]
        [Route("api/ClassData/ListClasses")]
        public IEnumerable<Class> ListClasses()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "SELECT * from classes";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of classes
            List<Class> Classes = new List<Class>();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string ClassName = (string)ResultSet["classname"];
                string ClassCode = (string)ResultSet["classcode"];

                // convert date to string
                DateTime StartDate;
                DateTime.TryParse(ResultSet["startdate"].ToString(), out StartDate);
                

                DateTime FinishDate;
                DateTime.TryParse(ResultSet["finishdate"].ToString(), out FinishDate);
                
                // creating a class object
                Class NewClass = new Class();
                NewClass.StartDate = StartDate;
                NewClass.FinishDate = FinishDate;
                NewClass.ClassId = ClassId;
                NewClass.ClassName = ClassName;
                NewClass.ClassCode = ClassCode;
                //Add the class to the List
                Classes.Add(NewClass);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of classes
            return Classes;
        }
        /// <summary>
        /// Finds a class using the id from the database
        /// </summary>
        /// <param name="id">The id to match against a primary key record in the MySQL Database</param>
        /// <returns>A class object</returns>
        public Class FindClass(int id)
        {
            // create an instance of the class
            Class NewClass = new Class();
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classes where classid = " +id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();


            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {

                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string ClassName = (string)ResultSet["classname"];
                string ClassCode = (string)ResultSet["classcode"];

                // convert date to string
                DateTime StartDate;
                DateTime.TryParse(ResultSet["startdate"].ToString(), out StartDate);

                DateTime FinishDate;
                DateTime.TryParse(ResultSet["finishdate"].ToString(), out FinishDate);

                // creating a class object
                NewClass.StartDate = StartDate;
                NewClass.FinishDate = FinishDate;
                NewClass.ClassId = ClassId;
                NewClass.ClassName = ClassName;
                NewClass.ClassCode = ClassCode;

            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the class object
            return NewClass;
        }
    }
}
