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
        

        public static UnitOfWork GetInstance()
        {
            if (instance == null)
            {
                instance = new UnitOfWork();
                instance.context = new InsuranceContext();
                instance.EmployeeRepository = new Repository<Employee>(instance.context);
                instance.PCRepository = new Repository<PrivateCustomer>(instance.context);
                instance.BCRepository = new Repository<BusinessCustomer>(instance.context);
               
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
