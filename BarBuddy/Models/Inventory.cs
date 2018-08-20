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
        public int IventoryId { get; set; }

        [ForeignKey("Restaurant")]
        public int? ResturantId { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}