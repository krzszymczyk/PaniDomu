using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Provider;
using PaniDomu.Models;
using PaniDomu.ViewModels;
using System.Data.Entity;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace PaniDomu.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpensesController()
        {
            _context = new ApplicationDbContext();
        }


        public ActionResult Index()
        {
            return View();
        }
      
        public ActionResult Create(int id)
        {
            var viewModel = new ExpenseFormViewModel
            {
                Categories = _context.Categories.Where(c=>c.Id==id).ToList(),
                Date = DateTime.Now.ToShortDateString()

            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult Create(ExpenseFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _context.Categories.Where(x=>x.Id==viewModel.CategoryId).ToList();
                return View("Create", viewModel);
            }


            var expense = new Expense
            {
                UserId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.CategoryId,
                Amount = viewModel.Amount,
                Details = viewModel.Details
                
            };
            _context.Expenses.Add(expense);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");

        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult ShowExpenses(int? id)
        {
            IEnumerable<Category> categories;
            IEnumerable<Expense> expenses;
            string viewName;
            if (id == null)
            {
                categories = _context.Categories.ToList();
                expenses = _context.Expenses.Include(m => m.Category).OrderByDescending(x => x.DateTime).ToList().Where(x=>x.UserId==User.Identity.GetUserId());
                viewName = "ShowExpenses";
            }
            else
            {
                expenses = _context.Expenses.Include(m => m.Category).Where(x => x.CategoryId == id).OrderByDescending(x=>x.DateTime).Take(5).ToList().Where(x => x.UserId == User.Identity.GetUserId());
                categories = _context.Categories.Where(x => x.Id == id).ToList();
                viewName = "ShowExpensesForCategory";
            }


            var viewModel = new CategoriesExpensesViewModel
            {
                Categories = categories.OrderBy(n => n.Name),
                Expenses = expenses
            };
            return PartialView(viewName,viewModel);
        }
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]

        public ActionResult SumByCategory(byte id)
        {
            DateTime startDate, endDate;
            if ((DateTime.Now.Day < 14))
            {
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month,14);
                endDate = new DateTime(DateTime.Now.Year,DateTime.Now.Month,13); 
            }
            else
            {
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 14);
                endDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month, 13);
            }
            decimal sum = 0.0m;
            string userId = User.Identity.GetUserId();
            string category = _context.Categories.First(x => x.Id == id).Name;
            var expense = _context.Expenses
                .Where(x => x.CategoryId == id)
                .Where(x => x.UserId == userId)
                .Where(x=>x.DateTime >= startDate && x.DateTime<=endDate)
                .ToList();

            sum = expense.Sum(x => x.Amount);


            var view = new SumCategoryViewModel
            {
                CategoryName = category,
                TotalAmount = sum
            };
            return PartialView(view);
        }

        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = _context.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Expense expense = _context.Expenses.Find(id);
            _context.Expenses.Remove(expense ?? throw new InvalidOperationException("Nie ma takiego wydatku"));
            _context.SaveChanges();
            return RedirectToAction("Index","Home");
        }

    }
}