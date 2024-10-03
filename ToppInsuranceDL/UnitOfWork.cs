using TopInsuranceEntities;
using System;
using System.Collections.Generic;

namespace TopInsuranceDL
{
    public class UnitOfWork
    {
        private static UnitOfWork instance;
        private InsuranceContext context;

        public Repository<Employee> EmployeeRepository { get; private set; }
        public Repository<PrivateCustomer> PCRepository { get; private set; }
        public Repository<BusinessCustomer> BCRepository { get; private set; }
        public Repository<BusinessInsurance> BCInsuranceRepository { get; private set; }
        public Repository<VehicleInsurance> VehicleInsuranceRepository { get; private set; }
        public Repository<RealEstateInsurance> RealEstateInsuranceRepository { get; private set; }
        public Repository<LifeInsurance> LifeInsuranceRepository { get; private set; }
        public Repository<SicknessAndAccidentInsurance> SicknessAndAccidentInsuranceRepository { get; private set; }
        public Repository<Vehicle> VehicleRepository { get; private set; }
        public Repository<Inventory> InventoryRepository { get; private set; }


        public static UnitOfWork GetInstance()
        {
            if (instance == null)
            {
                instance = new UnitOfWork();
                instance.context = new InsuranceContext();
                instance.EmployeeRepository = new Repository<Employee>(instance.context);
                instance.PCRepository = new Repository<PrivateCustomer>(instance.context);
                instance.BCRepository = new Repository<BusinessCustomer>(instance.context);
                instance.BCInsuranceRepository = new Repository<BusinessInsurance>(instance.context);
                instance.VehicleInsuranceRepository = new Repository<VehicleInsurance>(instance.context);
                instance.RealEstateInsuranceRepository = new Repository<RealEstateInsurance>(instance.context);
                instance.LifeInsuranceRepository = new Repository<LifeInsurance>(instance.context);
                instance.SicknessAndAccidentInsuranceRepository = new Repository<SicknessAndAccidentInsurance>(instance.context);
                instance.VehicleRepository = new Repository<Vehicle> (instance.context);
                instance.InventoryRepository = new Repository<Inventory>(instance.context);

            }
            return instance;
        }
        private UnitOfWork() {}
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
