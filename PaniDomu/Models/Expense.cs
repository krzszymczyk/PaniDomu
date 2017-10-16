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
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }

        public Category Category { get; set; }
        [Required]
        public byte CategoryId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }
    }
}