using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlmacenImporting.Models
{
    public class Products
    {
        [Key]
        public int ProdId { get; set; }

        public string Item { get; set; }

        public string Brand { get; set; }

        public string Description { get; set; }

        public int Qty { get; set; } //In stock

        public double Cost { get; set; }

        public double Price { get; set; }

        //public int LocId { get; set; }

        //public int ProvId { get; set; }

        //public int BrandId { get; set; }

        public int ProvId { get; set; }
        [ForeignKey("ProvId")]
        public virtual Providers Providers { get; set; }

        public int LocId { get; set; }
        [ForeignKey("LocId")]
        public virtual Locations Locations { get; set; }

        //[ForeignKey("ProvId")]
        //public virtual Providers Providers { get; set; }

        //[ForeignKey("BrandId")]
        //public virtual Brands Brands { get; set; }

        //public string Country { get; set; } //From

        //public string Provider { get; set; }

        public int Warranty { get; set; } //In weeks

        public DateTimeOffset? DateAd { get; set; } //Date when the items arrived

        public DateTimeOffset? DateCreated { get; set; }
        //Creation date for the field

        public DateTimeOffset? DateUpdated { get; set; }
        //Updating date for the field

        public string ProviderName
        {
            get
            {
                return Providers.ProvName;
            }
        }
        public string LocationName
        {
            get
            {
                return Locations.LocName;
            }
        }
    }
}