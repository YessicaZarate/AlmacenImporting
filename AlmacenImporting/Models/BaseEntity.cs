using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlmacenImporting.Models
{
    public class BaseEntity
    {
        public DateTimeOffset? DateCreated { get; set; } //Creation date for the field

        public DateTimeOffset? DateUpdated { get; set; }//Updating date for the field
    }
}