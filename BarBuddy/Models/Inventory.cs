using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BarBuddy.Models
{
    public class Inventory
    {
        [Key]
        [Display(Name = "Item Id")]
        public int InventoryId { get; set; }

        [ForeignKey("Restaurant")]
        public int? RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        [Display(Name = "Type of Spirit")]
        public string Type { get; set; }

        [Display(Name = "Amount in Inventory Whole Store")]
        public double Stock { get; set; }

        //[Display(Name = "Brand of Bottle")]
        //public string Brand { get; set; }

        [Display(Name = "Bottle Size in ML")]
        public double BottleSize { get; set; }

        [Display(Name = "Bottle Price")]
        public double Price { get; set; }


    }
}