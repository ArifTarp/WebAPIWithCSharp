using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnginDemirog.WebApiDemo.Entities;
using EnginDemirog.WebApiDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EnginDemirog.WebApiDemo.DataAccess
{
    public class NorthwindDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=Northwind; Trusted_Connection=true");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
