using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TopInsuranceEntities;
using System.IO;

namespace TopInsuranceDL
{
    public class InsuranceContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PrivateCustomer> PCustomers { get; set; }
        public DbSet<BusinessCustomer> BCustomers { get; set; }
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
            modelBuilder.Entity<Employee>().HasIndex(e => e.AgencyNumber).IsUnique();

            modelBuilder.Entity<PrivateCustomer>().ToTable("PrivateCustomer");
            modelBuilder.Entity<PrivateCustomer>().HasIndex(p => p.SSN).IsUnique();

            modelBuilder.Entity<BusinessCustomer>().ToTable("BusinessCustomer");
            modelBuilder.Entity<BusinessCustomer>().HasIndex(b => b.Organizationalnumber).IsUnique();
        }

        public void ResetSeed()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();

            //Employees.Add(new Employee("Linnea Korneliussen", "072-2136462", "Linnea@hotmail.com", "Västanvindsgatan 6", 41717, "Göteborg", EmployeeRole.Säljare, "Hej123"));

            ////Personal
            //Employees.Add(new Employee("Irene Johansson", "098-7653872", "IreneJohansson@toppförsäkringar.com", "Stengatan 5", 93874, "Oslo", EmployeeRole.Säljare, "Irene"));
            //Employees.Add(new Employee("Karin Sundberg", "098-7453882", "Karin.sundberg@toppförsäkringar.com", "Lagercrantz plats 9", 50431, "Borås", EmployeeRole.Försäljningsassistent, "Kakan"));
            //Employees.Add(new Employee("Vigo Persson", "078-7345382", "Vigge@toppförsäkringar.com", "Odengatan 23", 50620, "Borås", EmployeeRole.Säljare, "Viggo"));
            //Employees.Add(new Employee("Birgitta frisk", "070-0958328", "Birgittafrisk@toppförsäkringar.com", "Druveforsvägen 11A", 50420, "Borås", EmployeeRole.Säljare, "Birgitta"));
            //Employees.Add(new Employee("Boris Alm", "073-86745933", "Borisalm@toppförsäkringar.com", "Göteborgsvägen 2", 51820, "Borås", EmployeeRole.Säljare, "Boris"));
            //Employees.Add(new Employee("Linda Jonsson", "070-7575411", "Lindajonsson@toppförsäkringar.com", "Salmeniigatan 3", 50325, "Borås", EmployeeRole.Säljare, "Linda"));
            //Employees.Add(new Employee("Malin Nilsdotter", "076-5176729", "Malinnilsdotter@toppförsäkringar.com", "Körsbärsvägen 16", 53212, "Borås", EmployeeRole.Säljare, "Malin"));
            //Employees.Add(new Employee("Mikael Lund", "078-7238382", "Mikaellund@toppförsäkringar.com", "Roseniigatan 13", 50421, "Borås", EmployeeRole.Säljare, "Micke"));
            //Employees.Add(new Employee("Patrik Hedman", "070-7124386", "Patrikhedman@toppförsäkringar.com", "Makgrillsgatan 83", 51820, "Borås", EmployeeRole.Säljare, "Patrik"));
            //Employees.Add(new Employee("Sten Hård", "070-7121001", "Stenhård@toppförsäkringar.com", "Strandvägen 10", 50550, "Stockholm", EmployeeRole.VD, "Lösenord"));
            //Employees.Add(new Employee("Ann-Sofie Larsson", "073-5296398", "Annsofielarsson@toppförsäkringar.com", "Senapsgatan 154", 50539, "Borås", EmployeeRole.Ekonomiassistent, "Annsofie"));
            //Employees.Add(new Employee("Iren Panik", "073-1142646", "Irenpanik@toppförsäkringar.com", "Öresjövägen 29", 51821, "Borås", EmployeeRole.Försäljningschef, "Irenpanik"));


            //// Private Customer
            //PCustomers.Add(new PrivateCustomer("Jeanette Karlsson", "076-4907867", "Jeanette@gmail.com", "Sveavägen 6", 65789, "Göteborg", "19940518-6460", "0368976543"));


            //// Business Customer 
            //BCustomers.Add(new BusinessCustomer("Sven Göransson", "076-8792345", "Sven@icabanken.se", "Ica vägen 6", 76890, "Borås", "Ica banken", 20010519 - 6734, 46));

            //SaveChanges();
        }
    }
}
