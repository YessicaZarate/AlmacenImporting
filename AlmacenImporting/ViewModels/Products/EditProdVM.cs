using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AlmacenImporting.ViewModels.Products
{
    public class EditProdVM
    {
        [Key]
        public int ProducId { get; set; }
        [Required]
        [Display(Name = "Item:")]
        public string Item { get; set; }

        [Required]
        [Display(Name = "Brand:")]
        public string Brand { get; set; }

        [Display(Name = "Description:")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Quantity in stock:")]
        public int Qty { get; set; } //In stock

        [Required]
        [Display(Name = "Cost:")]
        public double Cost { get; set; }

        [Required]
        [Display(Name = "Sale Price:")]
        public double Price { get; set; }

        //public int LocId { get; set; }
        [Display(Name = "Provider:")]
        [Required]
        public int ProvidId { get; set; }
        public IEnumerable<SelectListItem> Providers { get; set; }

        [Display(Name = "Source Country:")]
        [Required]
        public int LocId { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }

        //public int BrandId { get; set; }

        //public int LocationId { get; set; }
        [Required]
        [Display(Name = "Warranty (in weeks):")]
        public int Warranty { get; set; } //In weeks

        [Required]
        [Display(Name = "Acquisition Date:")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? DateAd { get; set; } //Date when the items arrived

        //[Required]
        //[Display(Name = "Date Updated:")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        //public DateTimeOffset? DateUpdated { get; set; }
    }
}