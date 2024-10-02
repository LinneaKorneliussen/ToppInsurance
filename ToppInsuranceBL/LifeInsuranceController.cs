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

        #region Get all private customers Method
        public List<PrivateCustomer> GetAllPrivateCustomers()
        {
            return lifeInsuranceRepository.GetAllPrivateCustomers();
        }
        #endregion

        #region Add LifeInsurance Method for private customer
        public void AddLifeInsurance(PrivateCustomer p, DateTime startDate, DateTime endDate, InsuranceType insuranceType,
            Paymentform paymentform, int baseAmount, Status status, string note)
        {
            lifeInsuranceRepository.AddLifeInsurance(p, startDate, endDate, insuranceType, paymentform, baseAmount, status, note);
        }
        #endregion

    }
}
