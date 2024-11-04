using TopInsuranceEntities;
using Microsoft.EntityFrameworkCore;

namespace TopInsuranceDL
{
    /// <summary>
    /// The UnitOfWork class centralizes database operations and handles status updates for insurance entities.
    /// It uses the singleton pattern to ensure only one instance is created, simplifying repository management
    /// and ensuring consistency across the application.
    /// </summary>
    public class UnitOfWork
    {
        private static UnitOfWork instance;
        private InsuranceContext context;

        public Repository<Employee> EmployeeRepository { get; private set; }
        public Repository<PrivateCustomer> PCRepository { get; private set; }
        public Repository<BusinessCustomer> BCRepository { get; private set; }
        public Repository<LiabilityInsurance> LiabilityInsuranceRepository { get; private set; }
        public Repository<VehicleInsurance> VehicleInsuranceRepository { get; private set; }
        public Repository<RealEstateInsurance> RealEstateInsuranceRepository { get; private set; }
        public Repository<LifeInsurance> LifeInsuranceRepository { get; private set; }
        public Repository<SicknessAccidentInsurance> SicknessAccidentInsuranceRepository { get; private set; }
        public Repository<Vehicle> VehicleRepository { get; private set; }
        public Repository<Inventory> InventoryRepository { get; private set; }
        public Repository<Commission> CommissionRepository { get; private set; }
        public Repository<ProspectInformation> ProspectRepository { get; private set; }
        public Repository<Invoice> InvoiceRepository { get; private set; }


        public static UnitOfWork GetInstance()
        {
            if (instance == null)
            {
                instance = new UnitOfWork();
                instance.context = new InsuranceContext();
                instance.EmployeeRepository = new Repository<Employee>(instance.context);
                instance.PCRepository = new Repository<PrivateCustomer>(instance.context);
                instance.BCRepository = new Repository<BusinessCustomer>(instance.context);
                instance.LiabilityInsuranceRepository = new Repository<LiabilityInsurance>(instance.context);
                instance.VehicleInsuranceRepository = new Repository<VehicleInsurance>(instance.context);
                instance.RealEstateInsuranceRepository = new Repository<RealEstateInsurance>(instance.context);
                instance.LifeInsuranceRepository = new Repository<LifeInsurance>(instance.context);
                instance.SicknessAccidentInsuranceRepository = new Repository<SicknessAccidentInsurance>(instance.context);
                instance.VehicleRepository = new Repository<Vehicle> (instance.context);
                instance.InventoryRepository = new Repository<Inventory>(instance.context);
                instance.CommissionRepository = new Repository<Commission>(instance.context);
                instance.ProspectRepository = new Repository<ProspectInformation>(instance.context);
                instance.InvoiceRepository = new Repository<Invoice>(instance.context);
                instance.UpdateInsuranceStatuses();
            }
            return instance;
        }

        private void UpdateInsuranceStatuses()
        {
            UpdateStatusForRepository(LiabilityInsuranceRepository);
            UpdateStatusForRepository(VehicleInsuranceRepository);
            UpdateStatusForRepository(RealEstateInsuranceRepository);
            UpdateStatusForRepository(LifeInsuranceRepository);
            UpdateStatusForRepository(SicknessAccidentInsuranceRepository);
            context.SaveChanges();
        }

        private void UpdateStatusForRepository<T>(Repository<T> repository) where T : Insurance
        {
            var insurances = repository.GetAll(); 

            foreach (var insurance in insurances)
            {
                var originalStatus = insurance.Status;
                insurance.UpdateStatus();

                if (insurance.Status != originalStatus)
                {
                    context.Entry(insurance).State = EntityState.Modified;
                }
            }
        }
        private UnitOfWork() {}
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
