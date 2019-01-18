using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AlmacenImporting.ViewModels.Products
{
    public class IndexProdVM
    {
        [Key]
        public int ProducId { get; set; }
  
        [Display(Name = "Item:")]
        public string Item { get; set; }

        //public string Brand { get; set; }

        [Display(Name = "Description:")]
        public string Description { get; set; }

        [Display(Name = "Quantity:")]
        public int Qty { get; set; } //In stock

        [Display(Name = "Cost:")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double Cost { get; set; }

        [Display(Name = "Sale Price:")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double Price { get; set; }

        //public int LocId { get; set; }
        [Display(Name = "Provider:")]
        public string ProvName { get; set; }

        //public int BrandId { get; set; }

        //public int LocationId { get; set; }
        [Display(Name = "Warranty (in weeks):")]
        public int Warranty { get; set; } //In weeks

        [Display(Name = "Acquisition Date (dd/mm/yyyy):")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTimeOffset? DateAd { get; set; } //Date when the items arrived
    }
}