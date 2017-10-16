using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Provider;
using PaniDomu.Models;
using PaniDomu.ViewModels;
using System.Data.Entity;
using System.Net;

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
                Amount = viewModel.Amount
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
                expenses = _context.Expenses.Include(m => m.Category).ToList().Where(x=>x.UserId==User.Identity.GetUserId());
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
            decimal sum = 0.0m;
            string category = _context.Categories.First(x => x.Id == id).Name;
            var expense = _context.Expenses.Where(x => x.CategoryId == id).ToList().Where(x => x.UserId == User.Identity.GetUserId());

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
            _context.Expenses.Remove(expense);
            _context.SaveChanges();
            return RedirectToAction("Index","Home");
        }

    }
}