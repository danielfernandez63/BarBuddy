using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BarBuddy.Models
{
    public class Tab
    {
        [Key]
        public int TabId { get; set; }

        //[ForeignKey("Recipe")]
        //public int? RecipeId { get; set; }
        //public Recipe Recipe { get; set; }

        [Display(Name = "Tab Total")]
        public double Total { get; set; }


        [Display(Name = "Customer Name")]
        public string Name { get; set; }

        [Display(Name = "Check Out Completed")]
        public bool CheckOut { get; set; }

        [ForeignKey("Bartender")]
        public int? WorkerId { get; set; }
        public Bartender Bartender { get; set; }
        
        public IEnumerable<Recipe> TabItems { get; set; }

    }
}