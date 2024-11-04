using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    /// <summary>
    /// LifeInsuranceController class provides methods for managing life insurance policies for private customers.
    /// It includes functionality to retrieve available base amounts, check if a customer already holds a life insurance policy, 
    /// and add new life insurance policies with relevant details such as policy dates, base amount, and payment options.
    /// This class acts as an intermediary between the presentation layer and data access layer for handling life insurances.
    /// </summary>
    public class LifeInsuranceController
    {
        private LifeInsuranceRepository lifeInsuranceRepository;

        public LifeInsuranceController()
        {
            lifeInsuranceRepository = new LifeInsuranceRepository();
        }

        #region Get Base Amounts Method
        public List<int> GetBaseAmounts()
        {
            return lifeInsuranceRepository.GetBaseAmounts();
        }
        #endregion

        #region Check If Customer Already Has Insurance Method
        public bool CustomerHasInsurance(PrivateCustomer customer)
        {
            return lifeInsuranceRepository.CustomerHasInsurance(customer);
        }
        #endregion

        #region Add Life Insurance Method for private customer
        public void AddLifeInsurance(PrivateCustomer p, DateTime startDate, DateTime endDate, InsuranceType insuranceType,
            Paymentform paymentform, int baseAmount, string note, Employee user)
        {
            lifeInsuranceRepository.AddLifeInsurance(p, startDate, endDate, insuranceType, paymentform, baseAmount, note, user);
        }
        #endregion
    }

}
