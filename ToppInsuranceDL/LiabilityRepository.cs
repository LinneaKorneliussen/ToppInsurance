using TopInsuranceEntities;

namespace TopInsuranceDL
{
    /// <summary>
    /// The LiabilityRepository class is responsible for managing liability insurance 
    /// policies for business customers within the insurance system. It provides methods 
    /// to add new liability insurance entries, ensuring that all relevant details 
    /// are captured and stored effectively.
    /// </summary>
    
    public class LiabilityRepository
    {
        private UnitOfWork unitOfWork;

        public LiabilityRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }

        #region Add Sickness and Accident Insurance Method for Business Customer
        public void AddLiabilityInsurance(BusinessCustomer customer, DateTime startDate, DateTime endDate, InsuranceType type,
            Paymentform paymentform, string note, string contactPerson, string contactPhNo, DeductibleLiability deductible, InsuranceAmount insuranceAmount, Employee user)
        {
            LiabilityInsurance liabilityInsurance = new LiabilityInsurance(customer, startDate, endDate, type, paymentform, note, contactPerson, 
                contactPhNo, deductible, insuranceAmount, user);

            unitOfWork.LiabilityInsuranceRepository.Add(liabilityInsurance);
            unitOfWork.Save();
        }
        #endregion
    }
}
