using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BarBuddy.Models
{
    public class Vodka
    {
        [Key]
        public int VodkaId { get; set; }


        [ForeignKey("Inventory")]
        public int? InventoryId { get; set; }
        public Inventory Inventory { get; set; }

        [Display(Name = "Amount in Stock")]
        public double Stock { get; set; }

        [Display(Name = "Bottle Size")]
        public double BottleSize { get; set; }

        [Display(Name = "Bottle Price")]
        public double Price { get; set; }
    }
}