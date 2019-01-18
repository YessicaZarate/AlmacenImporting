using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AlmacenImporting.ViewModels.Products
{
    public class CreateProductsVM
    {
        [Key]
        public int ProdId { get; set; }

        public string Item { get; set; }

        //public string Brand { get; set; }

        public string Description { get; set; }

        public int Qty { get; set; } //In stock

        public double Cost { get; set; }

        public double Price { get; set; }

        public int LocId { get; set; }

        public int ProvId { get; set; }

        public int BrandId { get; set; }

        public int LocationId { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }

        public int Warranty { get; set; } //In weeks

        public DateTimeOffset? DateAd { get; set; } //Date when the items arrived
    }
}