using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BarBuddy.Models
{
    public class Bartender
    {
        [Key]
        public int WorkerId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Restaurant")]
        public int? RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        [ForeignKey("Tab")]
        public int? TabId { get; set; }
        public Tab Tab { get; set; }

        [ForeignKey("Manager")]
        [Display(Name = "My manager")]
        public int? ManagerId { get; set; }
        public Manager Manager { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Daily Till")]
        public double DailyTill { get; set; }

    }
}