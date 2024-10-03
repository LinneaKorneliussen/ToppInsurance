using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
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

        #region Search Private Customers Method
        public List<PrivateCustomer> SearchPrivateCustomers(string searchTerm)
        {
            return lifeInsuranceRepository.SearchPrivateCustomers(searchTerm);
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
