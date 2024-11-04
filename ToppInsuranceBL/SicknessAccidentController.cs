using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    /// <summary>
    /// SicknessAccidentController class provides methods for managing sickness and accident insurance for private customers.
    /// It allows retrieval of base amounts for both adults and children, as well as the addition of new insurance policies.
    /// This class acts as an intermediary between the presentation layer and the data access layer for handling sickness and accident insurance.
    /// </summary>
    public class SicknessAccidentController
    {
        private SicknessAccidentRepository sicknessAccidentRepository;

        public SicknessAccidentController()
        {
            sicknessAccidentRepository = new SicknessAccidentRepository();
        }

        #region Get Base Amounts Adult Method
        public List<double> GetBaseAmountsAdult()
        {
            return sicknessAccidentRepository.GetBaseAmountsAdults();
        }
        #endregion

        #region Get Base Amounts Child Method
        public List<double> GetBaseAmountsChild()
        {
            return sicknessAccidentRepository.GetBaseAmountsChild();
        }
        #endregion

        #region Add Sickness and Accident Insurance Method for private customer
        public void AddSicknessAccidentInsurance(PrivateCustomer customer, DateTime startDate, DateTime endDate,
            InsuranceType insuranceType, Paymentform paymentform, double baseAmount, string note,
            string? insuranceFirstName, string? insuranceLastName, string? insuranceSSN,
            AdditionalInsurance additionalInsurance, Employee user)
        {
           
            sicknessAccidentRepository.AddSicknessAccidentInsurance(customer, startDate, endDate, insuranceType, paymentform, baseAmount, note,
                insuranceFirstName, insuranceLastName, insuranceSSN, additionalInsurance, user);
        }
        #endregion
    }
}
