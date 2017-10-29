using PaniDomu.Models;

namespace PaniDomu.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PaniDomu.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PaniDomu.Models.ApplicationDbContext context)
        {
            context.Categories.AddOrUpdate(c=>c.Name,
              
                new Category { Name = "Artyku³y spo¿ywcze", Id=0},
                new Category { Name = "Chemia gospodarcza",Id=1},
                new Category { Name = "Benzyna i LGP",Id=2},
                new Category { Name = "Kot",Id=3},
                new Category { Name = "Mysz",Id=4},
                new Category { Name = "Julian",Id=5},
                new Category { Name = "Inne", Id = 6 }

                );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
