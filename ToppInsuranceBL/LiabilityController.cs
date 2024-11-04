using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    /// <summary>
    /// LiabilityController class provides methods for managing liability insurance policies for business customers.
    /// This class allows adding new liability insurance policies, capturing essential details such as customer information, 
    /// coverage dates, payment method, and policy specifications. This class acts as an intermediary 
    /// between the presentation layer and data access layer for handling liability insurance records.
    /// </summary>
    public class LiabilityController
    {
        private LiabilityRepository liabilityRepository;

        public LiabilityController()
        {
           liabilityRepository = new LiabilityRepository();
        }

        #region Add Liability Insurance Method
        public void AddLiabilityInsurance(BusinessCustomer customer, DateTime startDate, DateTime endDate, InsuranceType type,
            Paymentform paymentform, string note, string contactPerson, string contactPhNo, DeductibleLiability deductible, InsuranceAmount insuranceAmount, Employee user)
        {

            liabilityRepository.AddLiabilityInsurance(customer, startDate, endDate, type, paymentform, note, contactPerson, contactPhNo, deductible, insuranceAmount, user);
        }

        #endregion

    }
}
