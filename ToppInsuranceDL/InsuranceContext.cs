using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TopInsuranceEntities;
using System.IO;

namespace TopInsuranceDL
{
    public class InsuranceContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public InsuranceContext()
        {
           //ResetSeed();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("InsuranceMS.json", true, true)
                .Build()
                .GetConnectionString("InsuranceMS"));
            base.OnConfiguring(optionsBuilder);    
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees");
        }

        public void ResetSeed()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();

            //var employee1 = new Employee(
            //    "Linnea Korneliussen",
            //    0722136462,
            //    "Linnea@hotmail.com",
            //    "Västanvindsgatan 6",
            //    41717,
            //    "Göteborg",
            //    EmployeeRole.Säljare,
            //    "Hej123"
            //);

            //Employees.Add(employee1);

            //var employee2 = new Employee(
            //    "Lisa Nilsson",
            //    098765387,
            //    "LisaNilsson@gmail.com",
            //    "Stengatan 5",
            //    93874,
            //    "Oslo",
            //    EmployeeRole.Säljare,
            //    "1234"
            //);

            //Employees.Add(employee2);


            SaveChanges();
        }
    }
}
