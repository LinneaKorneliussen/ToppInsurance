using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    public class LiabilityController
    {
        private LiabilityRepository liabilityRepository;

        public LiabilityController()
        {
           liabilityRepository = new LiabilityRepository();
        }

        #region Search Business Customers Method
        public List<BusinessCustomer> SearchBusinessCustomer(string searchTerm)
        {
            return liabilityRepository.SearchBusinessCustomer(searchTerm);
        }
        #endregion

        #region Add Liability Insurance Method
        public void AddLiabilityInsurance(BusinessCustomer customer, DateTime startDate, DateTime endDate, InsuranceType type,
            Paymentform paymentform, string note, string contactPerson, string contactPhNo, DeductibleLiability deductible, InsuranceAmount insuranceAmount, Employee user)
        {

            liabilityRepository.AddLiabilityInsurance(customer, startDate, endDate, type, paymentform, note, contactPerson, contactPhNo, deductible, insuranceAmount, user);
        }
        #endregion

    }
}
