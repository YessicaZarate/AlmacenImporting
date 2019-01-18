using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AlmacenImporting.ViewModels.Locations
{
    public class CreateLocVM
    {
        public int Id { get; set; }

        [Display(Name = "Provider Name:")]
        public string LocationName { get; set; }

        [Display(Name = "Notes:")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
    }
}