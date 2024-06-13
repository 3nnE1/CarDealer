using CarDealer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Data
{
    public class CarDealerContext : DbContext
    {
        public DbSet<AvailableCar> AvailableCars { get; set; }
        public DbSet<SoldCar> SoldCars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet <SalesManager> SalesManagers { get; set; }

        public CarDealerContext(DbContextOptions<CarDealerContext> options) : base(options) { }
    }
}
