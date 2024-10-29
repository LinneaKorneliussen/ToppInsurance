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
        public DbSet<LiabilityInsurance> LiabilityInsurances { get; set; }
        public DbSet<VehicleInsurance> VehicleInsurances { get; set; }
        public DbSet<RealEstateInsurance> RealEstateInsurances{ get; set; }
        public DbSet<LifeInsurance> LifeInsurances { get; set; }
        public DbSet<SicknessAccidentInsurance> SicknessAccidentInsurances { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Commission> Commissions { get; set; }
        public DbSet<ProspectInformation> ProspectInformations { get; set; }

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
            // Employee Configuration
            modelBuilder.Entity<Employee>()
                .ToTable("Employee")
                .HasIndex(e => e.AgencyNumber).IsUnique();

            // PrivateCustomer Configuration
            modelBuilder.Entity<PrivateCustomer>()
                .ToTable("PrivateCustomer")
                .HasIndex(p => p.SSN).IsUnique(); 

            // BusinessCustomer Configuration
            modelBuilder.Entity<BusinessCustomer>()
                .ToTable("BusinessCustomer")
                .HasIndex(b => b.Organizationalnumber).IsUnique(); 

            // LifeInsurance Configuration
            modelBuilder.Entity<LifeInsurance>()
                .ToTable("LifeInsurance")
                .HasOne(li => li.Employee) 
                .WithMany(e => e.LifeInsurances) 
                .HasForeignKey(li => li.EmployeeId); 

            modelBuilder.Entity<PrivateCustomer>()
                .HasOne(pc => pc.LifeInsurance) 
                .WithOne(li => li.PrivateCustomer) 
                .HasForeignKey<LifeInsurance>(li => li.PrivateCustomerId); 

            // SicknessAccidentInsurance Configuration
            modelBuilder.Entity<SicknessAccidentInsurance>()
                .ToTable("SicknessAccidentInsurance")
                .HasOne(sai => sai.Employee) 
                .WithMany(e => e.AccidentInsurances) 
                .HasForeignKey(sai => sai.EmployeeId); 

            modelBuilder.Entity<PrivateCustomer>()
                .HasMany(pc => pc.SicknessAndAccidentInsurances) 
                .WithOne(sai => sai.PrivateCustomer) 
                .HasForeignKey(sai => sai.PrivateCustomerId); 

            // LiabilityInsurance Configuration
            modelBuilder.Entity<LiabilityInsurance>()
                .ToTable("LiabilityInsurance")
                .HasOne(li => li.Employee) 
                .WithMany(e => e.LiabilityInsurances)
                .HasForeignKey(li => li.EmployeeId); 

            modelBuilder.Entity<BusinessCustomer>()
                .HasMany(bc => bc.LiabilityInsurances) 
                .WithOne(li => li.BusinessCustomer) 
                .HasForeignKey(li => li.BusinessCustomerId); 

            // VehicleInsurance Configuration
            modelBuilder.Entity<VehicleInsurance>()
                .ToTable("VehicleInsurance")
                .HasOne(vi => vi.Employee) 
                .WithMany(e => e.VehicleInsurances) 
                .HasForeignKey(vi => vi.EmployeeId); 

            modelBuilder.Entity<BusinessCustomer>()
                .HasMany(bc => bc.VehicleInsurances) 
                .WithOne(vi => vi.BusinessCustomer) 
                .HasForeignKey(vi => vi.BusinessCustomerId); 

            modelBuilder.Entity<VehicleInsurance>()
                .HasOne(vi => vi.Car) 
                .WithOne(v => v.VehicleInsurance) 
                .HasForeignKey<VehicleInsurance>(vi => vi.VehicleId); 

            // RealEstateInsurance Configuration
            modelBuilder.Entity<RealEstateInsurance>()
                .ToTable("RealEstateInsurance")
                .HasOne(rei => rei.Employee) 
                .WithMany(e => e.RealEstateInsurances) 
                .HasForeignKey(rei => rei.EmployeeId); 

            modelBuilder.Entity<BusinessCustomer>()
                .HasMany(bc => bc.RealEstateInsurances) 
                .WithOne(rei => rei.BusinessCustomer) 
                .HasForeignKey(rei => rei.BusinessCustomerId); 

            modelBuilder.Entity<RealEstateInsurance>()
                .HasMany(rei => rei.Inventories) 
                .WithOne(inv => inv.RealEstateInsurance) 
                .HasForeignKey(inv => inv.RealEstateInsuranceId);

            // Commission Configuration
            modelBuilder.Entity<Commission>()
                .HasOne(c => c.Employee) 
                .WithMany(e => e.Commissions) 
                .HasForeignKey(c => c.EmployeeId); 

            // Prospectinformation Configuration
            modelBuilder.Entity<ProspectInformation>()
                .HasOne(p => p.Employee) 
                .WithMany(e => e.ProspectInformationList) 
                .HasForeignKey(p => p.EmployeeId); 

            // Prospectinformation Configuration
            modelBuilder.Entity<ProspectInformation>()
                .HasOne(p => p.PrivateCustomer) 
                .WithMany(pc => pc.ProspectInformationList)
                .HasForeignKey(p => p.PrivateCustomerId) 
                .IsRequired(false);

            // Prospectinformation Configuration
            modelBuilder.Entity<ProspectInformation>()
                .HasOne(p => p.BusinessCustomer) 
                .WithMany(bc => bc.ProspectInformationList) 
                .HasForeignKey(p => p.BusinessCustomerId) 
                .IsRequired(false);
        }

        #endregion

        #region Reset seed 
        public void ResetSeed()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();

            //// Skapa anställda som objekt med unika personnummer och telefonnummer
            //Employee linnea = new Employee("Linnea", "Korneliussen", "19940518-6464", "072-2136462", "Linnea@hotmail.com", "Västanvindsgatan 6", 41717, "Göteborg", EmployeeRole.Säljare, "Hej123");
            //Employee irene = new Employee("Irene", "Johansson", "19840525-1111", "073-7653872", "IreneJohansson@toppförsäkringar.com", "Stengatan 5", 93874, "Oslo", EmployeeRole.Säljare, "Irene");
            //Employee karin = new Employee("Karin", "Sundberg", "19900515-2222", "070-7453882", "Karin.sundberg@toppförsäkringar.com", "Lagercrantz plats 9", 50431, "Borås", EmployeeRole.Försäljningsassistent, "Kakan");
            //Employee vigo = new Employee("Vigo", "Persson", "19920312-3333", "076-7345382", "Vigge@toppförsäkringar.com", "Odengatan 23", 50620, "Borås", EmployeeRole.Säljare, "Viggo");
            //Employee birgitta = new Employee("Birgitta", "Frisk", "19880620-4444", "070-0958328", "Birgittafrisk@toppförsäkringar.com", "Druveforsvägen 11A", 50420, "Borås", EmployeeRole.Säljare, "Birgitta");
            //Employee boris = new Employee("Boris", "Alm", "19830101-5555", "073-8674593", "Borisalm@toppförsäkringar.com", "Göteborgsvägen 2", 51820, "Borås", EmployeeRole.Säljare, "Boris");
            //Employee linda = new Employee("Linda", "Jonsson", "19930415-6666", "070-7575411", "Lindajonsson@toppförsäkringar.com", "Salmeniigatan 3", 50325, "Borås", EmployeeRole.Säljare, "Linda");
            //Employee malin = new Employee("Malin", "Nilsdotter", "19871212-7777", "076-5176729", "Malinnilsdotter@toppförsäkringar.com", "Körsbärsvägen 16", 53212, "Borås", EmployeeRole.Säljare, "Malin");
            //Employee mikael = new Employee("Mikael", "Lund", "19900110-8888", "078-7238382", "Mikaellund@toppförsäkringar.com", "Roseniigatan 13", 50421, "Borås", EmployeeRole.Säljare, "Micke");
            //Employee patrik = new Employee("Patrik", "Hedman", "19850222-9999", "070-7124386", "Patrikhedman@toppförsäkringar.com", "Makgrillsgatan 83", 51820, "Borås", EmployeeRole.Säljare, "Patrik");
            //Employee sten = new Employee("Sten", "Hård", "19800101-0001", "070-7121001", "Stenhård@toppförsäkringar.com", "Strandvägen 10", 50550, "Stockholm", EmployeeRole.VD, "Lösenord");
            //Employee annSofie = new Employee("Ann-Sofie", "Larsson", "19930218-1112", "073-5296398", "Annsofielarsson@toppförsäkringar.com", "Senapsgatan 154", 50539, "Borås", EmployeeRole.Ekonomiassistent, "Annsofie");
            //Employee iren = new Employee("Iren", "Panik", "19781230-1234", "073-1142646", "Irenpanik@toppförsäkringar.com", "Öresjövägen 29", 51821, "Borås", EmployeeRole.Försäljningschef, "Irenpanik");

            //// Lägga till anställda i listan
            //Employees.Add(linnea);
            //Employees.Add(irene);
            //Employees.Add(karin);
            //Employees.Add(vigo);
            //Employees.Add(birgitta);
            //Employees.Add(boris);
            //Employees.Add(linda);
            //Employees.Add(malin);
            //Employees.Add(mikael);
            //Employees.Add(patrik);
            //Employees.Add(sten);
            //Employees.Add(annSofie);
            //Employees.Add(iren);

            //#region Private customers and Insurances 
            //// Private Customers
            //PrivateCustomer pcJeanette = new PrivateCustomer("Jeanette", "Karlsson", "076-4907867", "Jeanette@gmail.com", "Sveavägen 6", 65789, "Göteborg", "19940518-6460", "036-8976543");
            //PrivateCustomer pcErik = new PrivateCustomer("Erik", "Svensson", "073-6789012", "Erik@gmail.com", "Huvudgatan 2", 65345, "Malmö", "19900214-1234", "045-6789012");
            //PrivateCustomer pcAnna = new PrivateCustomer("Anna", "Larsson", "070-2345678", "Anna@gmail.com", "Fikagatan 4", 67234, "Stockholm", "19850622-5678", "098-7654321");

            //PCustomers.Add(pcJeanette);
            //PCustomers.Add(pcErik);
            //PCustomers.Add(pcAnna);

            //// Life Insurance for private customers 
            //LifeInsurance lifeInsuranceJeanette = new LifeInsurance(pcJeanette, new DateTime(2024, 9, 1), new DateTime(2025, 1, 1), InsuranceType.Livförsäkring, Paymentform.Månad, "Jeanette's life insurance note", 550000, linnea);
            //LifeInsurance lifeInsuranceErik = new LifeInsurance(pcErik, new DateTime(2024, 12, 24), new DateTime(2025, 1, 1), InsuranceType.Livförsäkring, Paymentform.Månad, "Erik's life insurance note", 450000, linnea);
            //LifeInsurance lifeInsuranceAnna = new LifeInsurance(pcAnna, new DateTime(2024, 9, 4), new DateTime(2025, 9, 1), InsuranceType.Livförsäkring, Paymentform.Månad, "Annas life insurance note", 450000, linnea);

            //LifeInsurances.Add(lifeInsuranceJeanette);
            //LifeInsurances.Add(lifeInsuranceErik);
            //LifeInsurances.Add(lifeInsuranceAnna);

            //// Sickness and accident Insurance for private customers
            //SicknessAccidentInsurance sicknessAndAccidentInsuranceJeanette = new SicknessAccidentInsurance(pcJeanette,
            // new DateTime(2024, 9, 30),
            // new DateTime(2025, 1, 1),
            // InsuranceType.SjukOchOlycksfallsförsäkringBARN,
            // Paymentform.År,
            // "Jeanettes sjuk- och olycksfallsförsäkring",
            // "Lena",
            // "Karlsson",
            // "19940518-6460",
            // AdditionalInsurance.ErsättningVidLångvarigSjukskrivning,
            // 750000,
            // linnea);

            //SicknessAccidentInsurance sicknessAndAccidentInsuranceErik = new SicknessAccidentInsurance(pcErik,
            //    new DateTime(2024, 9, 1),
            //    new DateTime(2025, 1, 1),
            //    InsuranceType.SjukOchOlycksfallsförsäkringVUXEN,
            //    Paymentform.Månad,
            //    "Eriks sjuk- och olycksfallsförsäkring",
            //    null, null, null,
            //    AdditionalInsurance.Ingen,
            //    350000,
            //    boris);


            //SicknessAccidentInsurance sicknessAndAccidentInsuranceAnna = new SicknessAccidentInsurance(pcAnna,
            //    new DateTime(2024, 1, 1),
            //    new DateTime(2025, 1, 1),
            //    InsuranceType.SjukOchOlycksfallsförsäkringBARN,
            //    Paymentform.Månad,
            //    "Annars sjuk- och olycksfallsförsäkring",
            //    "Lisa",
            //    "Larsson",
            //    "19850622-5678",
            //    AdditionalInsurance.Båda,
            //    950000,
            //    linnea);

            //SicknessAccidentInsurances.Add(sicknessAndAccidentInsuranceJeanette);
            //SicknessAccidentInsurances.Add(sicknessAndAccidentInsuranceErik);
            //SicknessAccidentInsurances.Add(sicknessAndAccidentInsuranceAnna);
            //#endregion

            //#region Business customers and Insurances 
            //// Business Customers
            //BusinessCustomer businessCustomerSven = new BusinessCustomer("Sven", "Göransson", "076-8792345", "Sven@icabanken.se", "Ica vägen 6", 76890, "Borås", "Ica banken", 89567349, 46);
            //BusinessCustomer businessCustomerOlof = new BusinessCustomer("Olof", "Persson", "073-1234567", "Olof@perssonbygg.se", "Byggvägen 8", 41100, "Göteborg", "Persson Bygg", 87654321, 10);

            //BCustomers.Add(businessCustomerSven);
            //BCustomers.Add(businessCustomerOlof);

            //// Business Insurance for business customers 
            //LiabilityInsurance businessInsuranceSven = new LiabilityInsurance(
            //    businessCustomerSven,
            //    new DateTime(2024, 10, 1),
            //    new DateTime(2025, 1, 1),
            //    InsuranceType.Ansvarsförsäkring,
            //    Paymentform.Månad,
            //    "Notering om ansvarsförsäkring",
            //    "Erik kontakt",
            //    "072-9867234",
            //    DeductibleLiability.HalvtPrisbasbelopp,
            //    InsuranceAmount.Tre_miljoner,
            //    birgitta);

            //LiabilityInsurance businessInsuranceOlof = new LiabilityInsurance(
            //    businessCustomerOlof,
            //    new DateTime(2024, 8, 12),
            //    new DateTime(2025, 1, 1),
            //    InsuranceType.Ansvarsförsäkring,
            //    Paymentform.Månad,
            //    "Notering om ansvarsförsäkring",
            //    "Lenas kontakt",
            //    "072-9867964",
            //    DeductibleLiability.TreFjärdedelsPrisbasbelopp,
            //    InsuranceAmount.Fem_miljoner,
            //    linnea);

            //LiabilityInsurances.Add(businessInsuranceSven);
            //LiabilityInsurances.Add(businessInsuranceOlof);

            //// Vehivle for vehicle Insurance for business customers 
            //Vehicle vehicle1 = new Vehicle("FDK123", "Volvo", 2020);
            //Vehicle vehicle2 = new Vehicle("ADE12E", "Scania", 2019);
            //Vehicle vehicle3 = new Vehicle("YPA299", "Mercedes", 2021);

            //Vehicles.Add(vehicle1);
            //Vehicles.Add(vehicle2);
            //Vehicles.Add(vehicle3);

            //// Skapa fordonförsäkringar för affärsförsäkring
            //VehicleInsurance vehicleInsurance1 = new VehicleInsurance(businessCustomerSven, vehicle1,
            //    DeductibleVehicle.DV1, CoverageType.Trafik, RiskZone.Z2,
            //    new DateTime(2024, 9, 1), new DateTime(2025, 1, 1),
            //    InsuranceType.Fordonsförsäkring, Paymentform.År, "Volvo försäkring", birgitta);

            //VehicleInsurance vehicleInsurance2 = new VehicleInsurance(businessCustomerOlof, vehicle2,
            //    DeductibleVehicle.DV2, CoverageType.Hel, RiskZone.Z1,
            //    new DateTime(2024, 10, 1), new DateTime(2025, 10, 1),
            //    InsuranceType.Fordonsförsäkring, Paymentform.År, "Scania försäkring", linnea);

            //VehicleInsurance vehicleInsurance3 = new VehicleInsurance(businessCustomerOlof, vehicle3,
            //    DeductibleVehicle.DV3, CoverageType.Halv, RiskZone.Z3,
            //    new DateTime(2024, 9, 19), new DateTime(2025, 1, 1),
            //    InsuranceType.Fordonsförsäkring, Paymentform.År, "Mercedes försäkring", malin);


            //VehicleInsurances.Add(vehicleInsurance1);
            //VehicleInsurances.Add(vehicleInsurance2);
            //VehicleInsurances.Add(vehicleInsurance3);

            //// Skapa fastighetsförsäkringar för varje kund
            //RealEstateInsurance realEstateInsuranceOlof = new RealEstateInsurance(businessCustomerOlof,
            //    new DateTime(2024, 9, 11),
            //    new DateTime(2025, 1, 1),
            //    InsuranceType.FastighetsOchInventarieförsäkring,
            //    Paymentform.År,
            //    "Olofs fastighetsförsäkring",
            //    "Huvudgatan 1",
            //    12345,
            //    "Stockholm",
            //    4500000,
            //    linnea);

            //RealEstateInsurance realEstateInsuranceSven = new RealEstateInsurance(businessCustomerSven,
            //    new DateTime(2024, 10, 1),
            //    new DateTime(2025, 1, 1),
            //    InsuranceType.FastighetsOchInventarieförsäkring,
            //    Paymentform.År,
            //    "Kalle's fastighetsförsäkring",
            //    "Bygggatan 12",
            //    98765,
            //    "Göteborg",
            //    2700000,
            //    birgitta);

            //RealEstateInsurance realEstateInsuranceOlof2 = new RealEstateInsurance(businessCustomerOlof,
            //    new DateTime(2024, 9, 17),
            //    new DateTime(2025, 1, 1),
            //    InsuranceType.FastighetsOchInventarieförsäkring,
            //    Paymentform.År,
            //    "Linas fastighetsförsäkring",
            //    "Cafévägen 3",
            //    45678,
            //    "Malmö",
            //    3200000,
            //    birgitta);

            //// Lägg till fastighetsförsäkringar till listan
            //RealEstateInsurances.Add(realEstateInsuranceOlof);
            //RealEstateInsurances.Add(realEstateInsuranceSven);
            //RealEstateInsurances.Add(realEstateInsuranceOlof2);

            //Inventory inventoryOlof1 = new Inventory(InventoryType.Anläggningsinventarier, 100000);
            //Inventory inventoryOlof2 = new Inventory(InventoryType.Anläggningsinventarier, 150000);
            //realEstateInsuranceOlof.Inventories.Add(inventoryOlof1);
            //realEstateInsuranceOlof.Inventories.Add(inventoryOlof2);
            //inventoryOlof1.RealEstateInsurance = realEstateInsuranceOlof;
            //inventoryOlof2.RealEstateInsurance = realEstateInsuranceOlof;

            //Inventory inventorySven1 = new Inventory(InventoryType.Förbrukningsinventarier, 200000);
            //Inventory inventorySven2 = new Inventory(InventoryType.Anläggningsinventarier, 300000);
            //realEstateInsuranceSven.Inventories.Add(inventorySven1);
            //realEstateInsuranceSven.Inventories.Add(inventorySven2);
            //inventorySven1.RealEstateInsurance = realEstateInsuranceSven;
            //inventorySven2.RealEstateInsurance = realEstateInsuranceSven;


            //Inventory inventory1 = new Inventory(InventoryType.Anläggningsinventarier, 80000);
            //Inventory inventory2 = new Inventory(InventoryType.Förbrukningsinventarier, 120000);
            //realEstateInsuranceOlof2.Inventories.Add(inventory1);
            //realEstateInsuranceOlof2.Inventories.Add(inventory2);
            //inventory1.RealEstateInsurance = realEstateInsuranceOlof2;
            //inventory2.RealEstateInsurance = realEstateInsuranceOlof2;
            //#endregion

            //#region Prospect Information Test Data

            //// Skapa några exempel på prospect information för privata kunder och affärskunder
            //ProspectInformation prospectInfo1 = new ProspectInformation(
            //    "Jeanette är intresserad av livförsäkring.", // Notering
            //    linnea, // Anställd som hanterar prospektet
            //    pcJeanette, // Privat kund (kan vara null för företagskunder)
            //    null); // Ingen företagskund, eftersom detta gäller en privat kund

            //ProspectInformation prospectInfo2 = new ProspectInformation(
            //    "Erik är intresserad av en kombination av liv- och sjukförsäkring.",
            //    boris,
            //    pcErik,
            //    null);

            //ProspectInformation prospectInfo3 = new ProspectInformation(
            //    "Olof från Persson Bygg vill utöka sitt ansvarsskydd.",
            //    birgitta,
            //    null,
            //    businessCustomerOlof); // Företagskund, ingen privatkund

            //ProspectInformation prospectInfo4 = new ProspectInformation(
            //    "Sven från Ica Banken är intresserad av fastighetsförsäkring.",
            //    linnea,
            //    null,
            //    businessCustomerSven);

            //// Lägg till prospect information till listan eller databasen
            //ProspectInformations.Add(prospectInfo1);
            //ProspectInformations.Add(prospectInfo2);
            //ProspectInformations.Add(prospectInfo3);
            //ProspectInformations.Add(prospectInfo4);
            //#endregion


            //SaveChanges();
        }
        #endregion

    }
}