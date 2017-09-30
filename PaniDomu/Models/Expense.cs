using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaniDomu.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime DateTime { get; set; }

        public string Category { get; set; }
        [Required]
        public byte CategoryId { get; set; }
    }
}