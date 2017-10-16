using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaniDomu.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using PaniDomu.ViewModels;

namespace PaniDomu.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize]
        public ActionResult Index()
        {
            var categories = _context.Categories.ToList();
            var expenses = _context.Expenses.Include(m => m.Category).ToList().Where(x => x.UserId == User.Identity.GetUserId());
            var viewModel = new CategoriesExpensesViewModel
            {
                Categories = categories.OrderBy(n => n.Name),
                Expenses = expenses
            };
            return View(viewModel);
        }
     

       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}