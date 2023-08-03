using System;
using SQLite;

namespace AeroArchive.Models
{
    public class Employee
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        //public string Filename { get; set; }
        public string FullName { get; set; }
        public string EmployeeID { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime Date { get; set; }
    }
}
