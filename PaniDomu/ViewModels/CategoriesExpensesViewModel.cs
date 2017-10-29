using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PaniDomu.Models;

namespace PaniDomu.ViewModels
{
    public class CategoriesExpensesViewModel
    {
        public IEnumerable<Expense> Expenses { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        [Display(Name = "Data początkowa:")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "Data końcowa:")]
        public DateTime? EndDate { get; set; }
    }
}