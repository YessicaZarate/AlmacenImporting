using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AlmacenImporting.Models
{
    public class Providers
    {
        [Key]
        public int Id { get; set; }

        public string ProvName { get; set; }

        public string Notes { get; set; }
    }
}