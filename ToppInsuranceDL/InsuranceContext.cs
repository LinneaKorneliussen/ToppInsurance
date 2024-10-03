using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TopInsuranceEntities;
using System.IO;
using System.Numerics;

namespace TopInsuranceDL
{
    public class InsuranceContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PrivateCustomer> PCustomers { get; set; }
        public DbSet<BusinessCustomer> BCustomers { get; set; }
        public DbSet<BusinessInsurance> BusinessInsurances { get; set; }
        public DbSet<VehicleInsurance> VehicleInsurances { get; set; }
        public DbSet<RealEstateInsurance> RealEstateInsurances{ get; set; }
        public DbSet<LifeInsurance> LifeInsurances { get; set; }
        public DbSet<SicknessAndAccidentInsurance> SicknessAndAccidentInsurances { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        public InsuranceContext()
        {
           //ResetSeed();
        }

        #region OnConfiguring 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("InsuranceMS.json", true, true)
                .Build()
                .GetConnectionString("InsuranceMS"));
            base.OnConfiguring(optionsBuilder);    
        }
        #endregion

        #region OnModelCreating 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Employee>().HasIndex(e => e.AgencyNumber).IsUnique();

            modelBuilder.Entity<PrivateCustomer>().ToTable("PrivateCustomer");
            modelBuilder.Entity<PrivateCustomer>().HasIndex(p => p.SSN).IsUnique();

            modelBuilder.Entity<BusinessCustomer>().ToTable("BusinessCustomer");
            modelBuilder.Entity<BusinessCustomer>().HasIndex(b => b.Organizationalnumber).IsUnique();

            modelBuilder.Entity<BusinessInsurance>().ToTable("BusinessInsurance");
            modelBuilder.Entity<BusinessCustomer>().HasMany(b => b.BusinessInsurances).WithOne(b => b.BusinessCustomer).HasForeignKey(b => b.BusinessCustomerId);

            modelBuilder.Entity<VehicleInsurance>().ToTable("VehicleInsurance");
            modelBuilder.Entity<BusinessCustomer>().HasMany(b => b.VehicleInsurances).WithOne(v => v.BusinessCustomer).HasForeignKey(v => v.BusinessCustomerId);
            modelBuilder.Entity<VehicleInsurance>().HasOne(i => i.Vehicle).WithOne(v => v.VehicleInsurance).HasForeignKey<Vehicle>(v => v.VechileInsuranceId);

            modelBuilder.Entity<RealEstateInsurance>().ToTable("RealEstateInsurance");
            modelBuilder.Entity<BusinessCustomer>().HasMany(b => b.RealEstateInsurances).WithOne(r => r.BusinessCustomer).HasForeignKey(r => r.BusinessCustomerId);
            modelBuilder.Entity<RealEstateInsurance>().HasMany(i => i.Inventories).WithOne(r => r.RealEstateInsurance).HasForeignKey(r => r.RealEstateInsuranceId);

            modelBuilder.Entity<LifeInsurance>().ToTable("LifeInsurance");
            modelBuilder.Entity<PrivateCustomer>().HasOne(p => p.LifeInsurance).WithOne(l => l.PrivateCustomer).HasForeignKey<LifeInsurance>(l => l.PrivateCustomerId);

            modelBuilder.Entity<SicknessAndAccidentInsurance>().ToTable("SicknessAndAccidentInsurance");
            modelBuilder.Entity<PrivateCustomer>().HasMany(p => p.SicknessAndAccidentInsurances).WithOne(s => s.PrivateCustomer).HasForeignKey(s => s.PrivateCustomerId);

        }
        #endregion

        #region Reset seed 
        public void ResetSeed()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();

            //#region Employees
            //// Employee
            //Employees.Add(new Employee("Linnea", "Korneliussen", "072-2136462", "Linnea@hotmail.com", "Västanvindsgatan 6", 41717, "Göteborg", EmployeeRole.Säljare, "Hej123"));
            //Employees.Add(new Employee("Irene", "Johansson", "098-7653872", "IreneJohansson@toppförsäkringar.com", "Stengatan 5", 93874, "Oslo", EmployeeRole.Säljare, "Irene"));
            //Employees.Add(new Employee("Karin", "Sundberg", "098-7453882", "Karin.sundberg@toppförsäkringar.com", "Lagercrantz plats 9", 50431, "Borås", EmployeeRole.Försäljningsassistent, "Kakan"));
            //Employees.Add(new Employee("Vigo", "Persson", "078-7345382", "Vigge@toppförsäkringar.com", "Odengatan 23", 50620, "Borås", EmployeeRole.Säljare, "Viggo"));
            //Employees.Add(new Employee("Birgitta", "Frisk", "070-0958328", "Birgittafrisk@toppförsäkringar.com", "Druveforsvägen 11A", 50420, "Borås", EmployeeRole.Säljare, "Birgitta"));
            //Employees.Add(new Employee("Boris", "Alm", "073-86745933", "Borisalm@toppförsäkringar.com", "Göteborgsvägen 2", 51820, "Borås", EmployeeRole.Säljare, "Boris"));
            //Employees.Add(new Employee("Linda", "Jonsson", "070-7575411", "Lindajonsson@toppförsäkringar.com", "Salmeniigatan 3", 50325, "Borås", EmployeeRole.Säljare, "Linda"));
            //Employees.Add(new Employee("Malin", "Nilsdotter", "076-5176729", "Malinnilsdotter@toppförsäkringar.com", "Körsbärsvägen 16", 53212, "Borås", EmployeeRole.Säljare, "Malin"));
            //Employees.Add(new Employee("Mikael", "Lund", "078-7238382", "Mikaellund@toppförsäkringar.com", "Roseniigatan 13", 50421, "Borås", EmployeeRole.Säljare, "Micke"));
            //Employees.Add(new Employee("Patrik", "Hedman", "070-7124386", "Patrikhedman@toppförsäkringar.com", "Makgrillsgatan 83", 51820, "Borås", EmployeeRole.Säljare, "Patrik"));
            //Employees.Add(new Employee("Sten", "Hård", "070-7121001", "Stenhård@toppförsäkringar.com", "Strandvägen 10", 50550, "Stockholm", EmployeeRole.VD, "Lösenord"));
            //Employees.Add(new Employee("Ann-Sofie", "Larsson", "073-5296398", "Annsofielarsson@toppförsäkringar.com", "Senapsgatan 154", 50539, "Borås", EmployeeRole.Ekonomiassistent, "Annsofie"));
            //Employees.Add(new Employee("Iren", "Panik", "073-1142646", "Irenpanik@toppförsäkringar.com", "Öresjövägen 29", 51821, "Borås", EmployeeRole.Försäljningschef, "Irenpanik"));
            //#endregion

            //#region Private customers and Insurances 
            //// Private Customers
            //PrivateCustomer pcJeanette = new PrivateCustomer("Jeanette", "Karlsson", "076-4907867", "Jeanette@gmail.com", "Sveavägen 6", 65789, "Göteborg", "19940518-6460", "0368976543");
            //PrivateCustomer pcErik = new PrivateCustomer("Erik", "Svensson", "073-6789012", "Erik@gmail.com", "Huvudgatan 2", 65345, "Malmö", "19900214-1234", "0456789012");
            //PrivateCustomer pcAnna = new PrivateCustomer("Anna", "Larsson", "070-2345678", "Anna@gmail.com", "Fikagatan 4", 67234, "Stockholm", "19850622-5678", "0987654321");

            //PCustomers.Add(pcJeanette);
            //PCustomers.Add(pcErik);
            //PCustomers.Add(pcAnna);

            //// Life Insurance for private customers 

            //LifeInsurance lifeInsuranceJeanette = new LifeInsurance(pcJeanette, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1), InsuranceType.Livförsäkring, Paymentform.Månad, 500, 100000, Status.Aktiv, "Jeanette's life insurance note");
            //LifeInsurance lifeInsuranceErik = new LifeInsurance(pcErik, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1), InsuranceType.Livförsäkring, Paymentform.Månad, 600, 120000, Status.Aktiv, "Erik's life insurance note");

            //LifeInsurance lifeInsuranceJeanette = new LifeInsurance(pcJeanette, new DateTime(2024, 2, 11), new DateTime(2025, 1, 1), InsuranceType.Livförsäkring, Paymentform.Månad, Status.Aktiv, "Jeanette's life insurance note", 550000);
            //LifeInsurance lifeInsuranceErik = new LifeInsurance(pcErik, new DateTime(2024, 4, 17), new DateTime(2025, 1, 1), InsuranceType.Livförsäkring, Paymentform.Månad, Status.Aktiv, "Erik's life insurance note", 450000);


            //LifeInsurances.Add(lifeInsuranceJeanette);
            //LifeInsurances.Add(lifeInsuranceErik);

            //// Sickness and accident Insurance for private customers
            //SicknessAndAccidentInsurance sicknessAndAccidentInsuranceJeanette = new SicknessAndAccidentInsurance(pcJeanette,
            // new DateTime(2024, 1, 1),
            // new DateTime(2025, 1, 1),
            // InsuranceType.SjukOchOlycksfallsförsäkringBARN,
            // Paymentform.Månad,
            // 300,
            // 50000,
            // Status.Aktiv,
            // "Jeanettes sjuk- och olycksfallsförsäkring",
            // "Jeanette",
            // "Karlsson",
            // "19940518-6460",
            // AdditionalInsurance.ErsättningVidLångvarigSjukskrivning);


            //SicknessAndAccidentInsurance sicknessAndAccidentInsuranceErik = new SicknessAndAccidentInsurance(pcErik,
            //    new DateTime(2024, 1, 1),
            //    new DateTime(2025, 1, 1),
            //    InsuranceType.SjukOchOlycksfallsförsäkringVUXEN,
            //    Paymentform.Månad,
            //    350,
            //    60000,
            //    Status.Aktiv,
            //    "Eriks sjuk- och olycksfallsförsäkring",
            //    "Erik",
            //    "Svensson",
            //    "19900214-1234",
            //    AdditionalInsurance.Ingen);


            //SicknessAndAccidentInsurance sicknessAndAccidentInsuranceAnna = new SicknessAndAccidentInsurance(pcAnna,
            //    new DateTime(2024, 1, 1),
            //    new DateTime(2025, 1, 1),
            //    InsuranceType.SjukOchOlycksfallsförsäkringVUXEN,
            //    Paymentform.Månad,
            //    400,
            //    70000,
            //    Status.Aktiv,
            //    "Annars sjuk- och olycksfallsförsäkring",
            //    "Anna",
            //    "Larsson",
            //    "19850622-5678",
            //    AdditionalInsurance.InvaliditetVidOlycksfall);

            //SicknessAndAccidentInsurances.Add(sicknessAndAccidentInsuranceJeanette);
            //SicknessAndAccidentInsurances.Add(sicknessAndAccidentInsuranceErik);
            //SicknessAndAccidentInsurances.Add(sicknessAndAccidentInsuranceAnna);
            //#endregion

            //#region Business customers and Insurances 
            //// Business Customers
            //BusinessCustomer businessCustomerSven = new BusinessCustomer("Sven", "Göransson", "076-8792345", "Sven@icabanken.se", "Ica vägen 6", 76890, "Borås", "Ica banken", 89567349, 46);
            //BusinessCustomer businessCustomerOlof = new BusinessCustomer("Olof", "Persson", "073-1234567", "Olof@perssonbygg.se", "Byggvägen 8", 41100, "Göteborg", "Persson Bygg", 87654321, 10);

            //BCustomers.Add(businessCustomerSven);
            //BCustomers.Add(businessCustomerOlof);

            //// Real Estate Insurance for business customers 
            //RealEstateInsurance realEstateInsuranceSven = new RealEstateInsurance(businessCustomerSven,
            //    new DateTime(2024, 1, 1),
            //    new DateTime(2025, 1, 1),
            //    InsuranceType.FastighetsOchInventarieförsäkring,
            //    Paymentform.År,
            //    1500,
            //    2000000,
            //    Status.Aktiv,
            //    "Svens fastighetsförsäkring",
            //    "Ica vägen 6, Borås",
            //    3500000,
            //    1800);

            //RealEstateInsurances.Add(realEstateInsuranceSven);

            //// Inventory for real estate insurance 
            //Inventory inventorySven1 = new Inventory(50000, 200);
            //Inventory inventorySven2 = new Inventory(60000, 300);

            //Inventories.Add(inventorySven1);
            //Inventories.Add(inventorySven2);

            //realEstateInsuranceSven.Inventories.Add(inventorySven1);
            //realEstateInsuranceSven.Inventories.Add(inventorySven2);

            //inventorySven1.RealEstateInsurance = realEstateInsuranceSven;
            //inventorySven2.RealEstateInsurance = realEstateInsuranceSven;

            //// Vehivle for vehicle Insurance for business customers 
            //Vehicle vehicle1 = new Vehicle(123456, "Volvo", 2020);
            //Vehicle vehicle2 = new Vehicle(789012, "Scania", 2019);
            //Vehicle vehicle3 = new Vehicle(345678, "Mercedes", 2021);

            //Vehicles.Add(vehicle1);
            //Vehicles.Add(vehicle2);
            //Vehicles.Add(vehicle3);

            //// Vehicle Insurance for business insurance 
            //VehicleInsurance vehicleInsurance1 = new VehicleInsurance(businessCustomerSven, vehicle1, "VTR", new DateTime(2024, 1, 1), new DateTime(2025, 1, 1),
            //    InsuranceType.Fordonsförsäkring, Paymentform.År, 2000, 300000, Status.Aktiv, "Volvo försäkring", "Ja");

            //VehicleInsurance vehicleInsurance2 = new VehicleInsurance(businessCustomerOlof, vehicle2, "VTR", new DateTime(2024, 1, 1), new DateTime(2025, 1, 1),
            //    InsuranceType.Fordonsförsäkring, Paymentform.År, 2500, 350000, Status.Aktiv, "Scania försäkring", "Ja");

            //VehicleInsurance vehicleInsurance3 = new VehicleInsurance(businessCustomerOlof, vehicle3, "VTR", new DateTime(2024, 1, 1), new DateTime(2025, 1, 1),
            //    InsuranceType.Fordonsförsäkring, Paymentform.År, 2200, 320000, Status.Aktiv, "Mercedes försäkring", "Ja");

            //VehicleInsurances.Add(vehicleInsurance1);
            //VehicleInsurances.Add(vehicleInsurance2);
            //VehicleInsurances.Add(vehicleInsurance3);

            //vehicle1.VehicleInsurance = vehicleInsurance1;
            //vehicle2.VehicleInsurance = vehicleInsurance2;
            //vehicle3.VehicleInsurance = vehicleInsurance3;

            //vehicleInsurance1.Vehicle = vehicle1;
            //vehicleInsurance2.Vehicle = vehicle2;
            //vehicleInsurance3.Vehicle = vehicle3;

            //// Business Insurance for business customers 
            //BusinessInsurance businessInsuranceSven = new BusinessInsurance(
            //    businessCustomerSven,
            //    new DateTime(2024, 1, 1),
            //    new DateTime(2025, 1, 1),
            //    InsuranceType.Företagsförsäkring,
            //    Paymentform.Månad,
            //    3000,
            //    1000000,
            //    Status.Aktiv,
            //    "Försäkring för Ica banken",
            //    "Sven Göransson",
            //    "076-8900123");

            //BusinessInsurance businessInsuranceOlof = new BusinessInsurance(
            //    businessCustomerOlof,
            //    new DateTime(2024, 2, 1),
            //    new DateTime(2025, 2, 1),
            //    InsuranceType.Företagsförsäkring,
            //    Paymentform.År,
            //    5000,
            //    2000000,
            //    Status.Aktiv,
            //    "Försäkring för Persson Bygg",
            //    "Olof Persson",
            //    "073-1234567");

            //BusinessInsurances.Add(businessInsuranceSven);
            //BusinessInsurances.Add(businessInsuranceOlof);
            //#endregion


            //SaveChanges();
        }
        #endregion

    }
}