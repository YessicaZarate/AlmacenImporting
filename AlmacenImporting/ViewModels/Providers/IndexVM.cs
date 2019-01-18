using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AlmacenImporting.ViewModels.Providers
{
    public class IndexVM
    {
        public int Id { get; set; }

        [Display(Name = "Provider Name:")]
        public string ProvName { get; set; }

        [Display(Name = "Notes:")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
    }
}