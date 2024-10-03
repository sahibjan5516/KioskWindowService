using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskUpdater
{
    public class Header
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string centerImage { get; set; }
        public string centerImagePath { get; set; }
        public bool isActive { get; set; }
        public string insertedBy { get; set; }
        public DateTime insertedDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public bool isDeleted { get; set; }
        public string deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }




    }
}
