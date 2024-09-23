using TopInsuranceEntities;

namespace TopInsuranceDL
{
    public class UnitOfWork
    {
        private static UnitOfWork instance;
        private InsuranceContext context;

        public Repository<Employee> Employees { get; private set; }
        public Repository<LifeInsurance> LifeInsuranceRepository { get; private set; }  

        public static UnitOfWork GetInstance()
        {
            if (instance == null)
            {
                instance = new UnitOfWork();
                instance.context = new InsuranceContext();
                instance.Employees = new Repository<Employee>(instance.context);
                instance.LifeInsuranceRepository = new Repository<LifeInsurance>(instance.context);
            }
            return instance;
        }
        private UnitOfWork()
        {

        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
