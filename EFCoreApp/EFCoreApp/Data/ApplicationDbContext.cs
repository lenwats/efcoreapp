using EFCoreApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>().ToTable(nameof(Address));
            modelBuilder.Entity<Appointment>().ToTable(nameof(Appointment));
            modelBuilder.Entity<City>().ToTable(nameof(City));
            modelBuilder.Entity<Country>().ToTable(nameof(Country));
            modelBuilder.Entity<Customer>().ToTable(nameof(Customer));
            modelBuilder.Entity<AppUser>().ToTable(nameof(AppUser));

            modelBuilder.Entity<Customer>().HasMany(c => c.Appointments).WithOne(a => a.Customer).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
