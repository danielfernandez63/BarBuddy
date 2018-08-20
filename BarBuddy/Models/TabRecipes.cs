using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BarBuddy.Models
{
    public class TabRecipes
    {
        [Key]
        public int TabRecipe { get; set; }

        [ForeignKey("Recipe")]
        public int? RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        [ForeignKey("Tab")]
        public int? TabId { get; set; }
        public Tab Tab { get; set; }
    }
}