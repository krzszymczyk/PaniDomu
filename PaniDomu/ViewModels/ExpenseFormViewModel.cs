using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PaniDomu.Models;

namespace PaniDomu.ViewModels
{
    public class ExpenseFormViewModel
    {
        [Required]
        [Display(Name = "Data zakupów:")]
        public string Date { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
        [Required]
        [Display(Name="Kategoria:")]
        public byte CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        [Required]
        [Display(Name = "Kwota:")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }
        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, "0:00:01"));
        }

    }
}