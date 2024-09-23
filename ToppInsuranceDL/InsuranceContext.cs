using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using TopInsuranceEntities;

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
            //modelBuilder.Entity<Patient>().HasIndex(p => p.PersonalNumber).IsUnique();
            //modelBuilder.Entity<Doctor>().ToTable("Doctors");
            //modelBuilder.Entity<Nurse>().ToTable("Nurses");
            //modelBuilder.Entity<Doctor>().HasMany(d => d.appointments).WithOne(a => a.Doctor).HasForeignKey(a => a.DoctorId);
        }

        public void ResetSeed()
        {
            ////Database.EnsureDeleted();
            //Database.EnsureCreated();

            //Employee p = new Employee(
            //    agencyNuber: 123,
            //    employeeRole: EmployeeRole.SalesPerson,
            //    password: "Hej123",
            //    name: "Linnea Korneliussen",
            //    phoneNumber: 0722136462,
            //    emailAddress: "Linnea@hotmail.com",
            //    address: "Västanvindsgatan 6",
            //    zipCode: 41717,
            //    city: "Göteborg");

            //Employees.Add(p);

            //SaveChanges();
        }
    }
}
