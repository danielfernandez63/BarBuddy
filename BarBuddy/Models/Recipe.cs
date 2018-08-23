using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BarBuddy.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }

        [ForeignKey("Restaurant")]
        public int? RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        //[ForeignKey("Tab")]
        //public int? TabId { get; set; }
        //public Tab Tab { get; set; }

        [ForeignKey("Inventory")]
        public int? InventoryId { get; set; }
        public Inventory Inventory { get; set; }

        [Display(Name = "Drink Name")]
        public string Name { get; set; }

        [Display(Name = "Drink Description")]
        public string Description { get; set; }

        [Display(Name = "Extra to delete")]
        public string Type { get; set; }

        [Display(Name = "Quantity in Recipe in Ounces")]
        public double Amount { get; set; }

        [Display(Name = "Drink Order Price")]
        public double Price { get; set; }

        [Display(Name = "Reduced From Inventory")]
        public double ReducedFromInventory { get; set; }

        [Display(Name = "Seasonal Item")]
        public bool IsSeasonal { get; set; }

    }
}