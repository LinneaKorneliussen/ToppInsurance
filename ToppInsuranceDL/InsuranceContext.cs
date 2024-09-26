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

            //Employees.Add(new Employee("Linnea Korneliussen", "0722136462", "Linnea@hotmail.com", "Västanvindsgatan 6", 41717, "Göteborg", EmployeeRole.Säljare, "Hej123"));

            ////Personal
            //Employees.Add(new Employee("Irene Johansson", "098765387", "IreneJohansson@toppförsäkringar.com", "Stengatan 5", 93874, "Oslo", EmployeeRole.Säljare, "Irene"));
            //Employees.Add(new Employee("Karin Sundberg", "098745388", "Karin.sundberg@toppförsäkringar.com", "Lagercrantz plats 9", 50431, "Borås", EmployeeRole.Försäljningsassistent, "Kakan"));
            //Employees.Add(new Employee("Vigo Persson", "078735382", "Vigge@toppförsäkringar.com", "Odengatan 23", 50620, "Borås", EmployeeRole.Säljare, "Viggo"));
            //Employees.Add(new Employee("Birgitta frisk", "0700908328", "Birgittafrisk@toppförsäkringar.com", "Druveforsvägen 11A", 50420, "Borås", EmployeeRole.Säljare, "Birgitta"));
            //Employees.Add(new Employee("Boris Alm", "0738745933", "Borisalm@toppförsäkringar.com", "Göteborgsvägen 2", 51820, "Borås", EmployeeRole.Säljare, "Boris"));
            //Employees.Add(new Employee("Linda Jonsson", "0707575411", "Lindajonsson@toppförsäkringar.com", "Salmeniigatan 3", 50325, "Borås", EmployeeRole.Säljare, "Linda"));
            //Employees.Add(new Employee("Malin Nilsdotter", "0761726729", "Malinnilsdotter@toppförsäkringar.com", "Körsbärsvägen 16", 53212, "Borås", EmployeeRole.Säljare, "Malin"));
            //Employees.Add(new Employee("Mikael Lund", "078723382", "Mikaellund@toppförsäkringar.com", "Roseniigatan 13", 50421, "Borås", EmployeeRole.Säljare, "Micke"));
            //Employees.Add(new Employee("Patrik Hedman", "070712386", "Patrikhedman@toppförsäkringar.com", "Makgrillsgatan 83", 51820, "Borås", EmployeeRole.Säljare, "Patrik"));
            //Employees.Add(new Employee("Sten Hård", "0707121001", "Stenhård@toppförsäkringar.com", "Strandvägen 10", 50550, "Stockholm", EmployeeRole.VD, "Lösenord"));
            //Employees.Add(new Employee("Ann-Sofie Larsson", "0735296398", "Annsofielarsson@toppförsäkringar.com", "Senapsgatan 154", 50539, "Borås", EmployeeRole.Ekonomiassistent, "Annsofie"));
            //Employees.Add(new Employee("Iren Panik", "073112646", "Irenpanik@toppförsäkringar.com", "Öresjövägen 29", 51821, "Borås", EmployeeRole.Försäljningschef, "Irenpanik"));


            //// Private Customer
            //PCustomers.Add(new PrivateCustomer("Jeanette Karlsson", "0764907867", "Jeanette@gmail.com", "Sveavägen 6", 65789, "Göteborg", "8905286789", "0368976543"));


            //// Business Customer 
            //BCustomers.Add(new BusinessCustomer("Sven Göransson", "0768792345", "Sven@icabanken.se", "Ica vägen 6", 76890, "Borås", "Ica banken", 89020201, 46));

            SaveChanges();
        }
    }
}
