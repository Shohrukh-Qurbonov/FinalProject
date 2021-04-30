using FinalProject.Context.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Home> Homes { get; set; }
        public DbSet<HomeImage> HomeImages { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<City>().HasData(
                new City { Id = 1, Name = "Худжанд" },
                new City { Id = 2, Name = "Душанбе" }
                );
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Новостройка"},
                new Category { Id = 2, Name = "Дача"},
                new Category { Id = 3, Name = "Офис"}
                );
        }
    }
}
