using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AlmacenImporting.ViewModels.Locations
{
    public class DeleteLocVM
    {
        public int Id { get; set; }

        [Display(Name = "Provider Name:")]
        public string LocationsName { get; set; }

        [Display(Name = "Notes:")]
        //[DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [Required]
        [Display(Name = "Date Created:")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTimeOffset? DateCreated { get; set; }

        [Required]
        [Display(Name = "Date Updated:")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTimeOffset? DateUpdated { get; set; }
    }
}