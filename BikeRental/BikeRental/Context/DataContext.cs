using BikeRental.Model;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Context
{
    public class DataContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BikeRentalDB;Trusted_Connection=True");
        }
    }
}
