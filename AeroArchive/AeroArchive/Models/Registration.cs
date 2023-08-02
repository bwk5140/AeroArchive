using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AeroArchive.Models
{
    public class Registration
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        //public string Filename { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Date { get; set; }
    }
}
