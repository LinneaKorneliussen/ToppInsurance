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
            ResetSeed();
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
            //modelBuilder.Entity<Employee>().HasIndex(e => e.EmployeeId).IsUnique();
            modelBuilder.Entity<Employee>().ToTable("Employee");
            //modelBuilder.Entity<Nurse>().ToTable("Nurses");
            //modelBuilder.Entity<Doctor>().HasMany(d => d.appointments).WithOne(a => a.Doctor).HasForeignKey(a => a.DoctorId);
        }

        public void ResetSeed()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

            Employees.Add(new Employee(
                "Linnea Korneliussen",      
                0722136462,                 
                "Linnea@hotmail.com",       
                "Västanvindsgatan 6",       
                41717,                      
                "Göteborg",                 
                EmployeeRole.SalesPerson,   
                "Hej123"                   
            ));

            Employees.Add(new Employee(
                "Lisa Nilsson",
                098765387,
                "LisaNilsson@gmail.com",
                "Stengatan 5",
                93874,
                "Oslo",
                EmployeeRole.SalesPerson,
                "1234"));

            SaveChanges();
        }
    }
}
