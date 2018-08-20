using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BarBuddy.Models
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }

        [Display(Name = "Restaurant Total Balance")]
        public double Balance { get; set; }
    }
}