namespace CarService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CarService.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<CarService.Models.CarServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CarService.Models.CarServiceContext context)
        {
            context.Owners.AddOrUpdate(x => x.Id,
                new Owner() { Id = 1, Name = "Niko Tobiasson" },
                new Owner() { Id = 2, Name = "Heikki Hirvi" },
                new Owner() { Id = 3, Name = "Kai Vinkonen" }
                );

            context.Cars.AddOrUpdate(x => x.Id,
                new Car()
                {
                    Id = 1,
                    Title = "Mersu",
                    Year = 2014,
                    OwnerId = 1,
                    Price = 16790.00M
                },
                new Car()
                {
                    Id = 2,
                    Title = "Bemari",
                    Year = 1990,
                    OwnerId = 1,
                    Price = 12000.00M
                },
                new Car()
                {
                    Id = 3,
                    Title = "Mitsu",
                    Year = 2004,
                    OwnerId = 2,
                    Price = 12700
                },
                new Car()
                {
                    Id = 4,
                    Title = "Volvo X90",
                    Year = 2013,
                    OwnerId = 3,
                    Price = 62400
                }
                );
        }
    }
}
