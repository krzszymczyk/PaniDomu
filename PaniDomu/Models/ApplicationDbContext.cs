﻿using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PaniDomu.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}