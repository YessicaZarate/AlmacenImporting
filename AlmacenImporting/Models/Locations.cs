using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AlmacenImporting.Models
{
    public class Locations
    {
        [Key]
        public int Id { get; set; }

        public string LocName { get; set; }

        public string Notes { get; set; }

        public DateTimeOffset? DateCreated { get; set; }
        //Creation date for the field

        public DateTimeOffset? DateUpdated { get; set; }
        //Updating date for the field
    }
}