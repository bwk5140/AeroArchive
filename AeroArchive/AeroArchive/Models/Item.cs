using SQLite;
using System;

namespace AeroArchive.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string WarrantyStatus { get; set; }
    }
}