using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    public class SicknessAccidentController
    {
        private SicknessAccidentRepository sicknessAccidentRepository;

        public SicknessAccidentController()
        {
            sicknessAccidentRepository = new SicknessAccidentRepository();
        }

        #region Get Base Amounts Adult Method
        public List<int> GetBaseAmountsAdult()
        {
            return sicknessAccidentRepository.GetBaseAmountsAdults();
        }
        #endregion

        #region Get Base Amounts Child Method
        public List<int> GetBaseAmountsChild()
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
