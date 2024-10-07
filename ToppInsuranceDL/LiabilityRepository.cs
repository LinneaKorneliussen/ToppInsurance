using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceEntities;

namespace TopInsuranceDL
{
    public class LiabilityRepository
    {
        private UnitOfWork unitOfWork;

        public LiabilityRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }

        #region Search Business Customer Method 
        public List<BusinessCustomer> SearchBusinessCustomer(string searchTerm)
        {
            List<BusinessCustomer> allCustomers = unitOfWork.BCRepository.GetAll().ToList();

            bool isNumericSearch = int.TryParse(searchTerm, out int searchNumber);

            var matchingCustomers = allCustomers.Where(c =>
                c.CompanyName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                (isNumericSearch && c.Organizationalnumber.ToString().Contains(searchTerm))
            ).ToList();

            return matchingCustomers;
        }
        #endregion

        #region Add Sickness and Accident Insurance Method for Private Customer
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
