using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Mini_Project_2
{
    internal class AssetDbContext : DbContext
    {
        string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=assets1;Trusted_Connection=True;MultipleActiveResultSets=true";

        public DbSet<Phone> phones { get; set; }
        public DbSet<Computer> computers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // We tell the app to use the connectionstring
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // test list
            /*
            List<Asset> assets = new List<Asset>()
            {
                new Phone("iPhone", "8", "Spain", Convert.ToDateTime("2019-11-05"), 970),
                new Computer("HP", "Elitebook", "Spain", Convert.ToDateTime("2022-05-01"), 1423),
                new Phone("iPhone", "11", "Spain", Convert.ToDateTime("2022-04-25"), 990),
                new Phone("iPhone", "X", "Sweden", Convert.ToDateTime("2019-08-05"), 1245),
                new Phone("Motorola", "Razr", "Sweden", Convert.ToDateTime("2019-09-06"), 970),
                new Computer("HP", "Elitebook", "Sweden", Convert.ToDateTime("2019-10-07"), 588),
                new Computer("Asus", "W234", "USA", Convert.ToDateTime("2019-07-21"), 1200),
                new Computer("Lenovo", "Yoga 730", "USA", Convert.ToDateTime("2019-09-28"), 835),
                new Computer("Lenovo", "Yoga 530", "USA", Convert.ToDateTime("2019-11-21"), 1030)
            };
            */
            modelBuilder.Entity<Phone>().HasData(new Phone(1, "iPhone", "8", "Spain", Convert.ToDateTime("2019-11-05"), 970));
            modelBuilder.Entity<Phone>().HasData(new Phone(2, "iPhone", "11", "Spain", Convert.ToDateTime("2022-04-25"), 990));
            modelBuilder.Entity<Phone>().HasData(new Phone(3, "iPhone", "X", "Sweden", Convert.ToDateTime("2019-08-05"), 1245));
            modelBuilder.Entity<Phone>().HasData(new Phone(4, "Motorola", "Razr", "Sweden", Convert.ToDateTime("2019-09-06"), 970));

            modelBuilder.Entity<Computer>().HasData(new Computer(1, "HP", "Elitebook", "Spain", Convert.ToDateTime("2022-05-01"), 1423));
            modelBuilder.Entity<Computer>().HasData(new Computer(2, "HP", "Elitebook", "Sweden", Convert.ToDateTime("2019-10-07"), 588));
            modelBuilder.Entity<Computer>().HasData(new Computer(3, "Asus", "W234", "USA", Convert.ToDateTime("2019-07-21"), 1200));
            modelBuilder.Entity<Computer>().HasData(new Computer(4, "Lenovo", "Yoga 730", "USA", Convert.ToDateTime("2019-09-28"), 835));
            modelBuilder.Entity<Computer>().HasData(new Computer(5, "Lenovo", "Yoga 530", "USA", Convert.ToDateTime("2019-11-21"), 1030));
        }
    }
}
