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

        }
    }
}
