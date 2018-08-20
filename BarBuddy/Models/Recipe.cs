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


        [Display(Name = "Drink Name")]
        public string Name { get; set; }

        [Display(Name = "Drink Description")]
        public string Description { get; set; }


        [Display(Name = "Liquor Type in Recipe")]
        public string Type { get; set; }

        [Display(Name = "Quantity in Recipe")]
        public double Amount { get; set; }

        [Display(Name = "Drink Order Price")]
        public double Price { get; set; }

    }
}