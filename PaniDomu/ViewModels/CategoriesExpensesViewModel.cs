using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaniDomu.Models;

namespace PaniDomu.ViewModels
{
    public class CategoriesExpensesViewModel
    {
        public IEnumerable<Expense> Expenses { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}