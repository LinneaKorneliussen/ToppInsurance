using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceEntities;

namespace TopInsuranceDL
{
    public class LifeInsuranceRepository
    {
        private UnitOfWork unitOfWork;
        private List<int> baseAmounts;
        public LifeInsuranceRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
            baseAmounts = new List<int> { 300000, 400000, 500000 }; 
            UpdateBaseAmounts();
        }

        #region Update base amount Method
        private void UpdateBaseAmounts()
        {
            int currentYear = DateTime.Now.Year;
            int baseIncrease = 50000;
            for (int i = 0; i < baseAmounts.Count; i++)
            {
                int yearsPassed = currentYear - 2023;
                baseAmounts[i] = baseAmounts[i] + (baseIncrease * yearsPassed);
            }
        }
        #endregion

        #region Get Base Amounts Method
        public List<int> GetBaseAmounts()
        { 
            return baseAmounts;
        }
        #endregion

        #region Search Private Customers Method
        public List<PrivateCustomer> SearchPrivateCustomers(string searchTerm)
        {
            List<PrivateCustomer> allCustomers = unitOfWork.PCRepository.GetAll().ToList();

            var matchingCustomers = allCustomers.Where(c =>
                c.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                c.SSN.Contains(searchTerm)).ToList();

            return matchingCustomers;
        }
        #endregion

        #region Check If Customer Already Has Insurance Method
        public bool CustomerHasInsurance(PrivateCustomer customer)
        {   
            List<LifeInsurance> lifeInsurances = unitOfWork.LifeInsuranceRepository.GetAll().ToList();
            return lifeInsurances.Any(l => l.PrivateCustomerId == customer.PersonId);
        }
        #endregion

        #region Add life insurance Method for private customer 
        public void AddLifeInsurance(PrivateCustomer p, DateTime startDate, DateTime endDate, InsuranceType insuranceType, 
            Paymentform paymentform, int baseAmount, string note, Employee user)
        {
            LifeInsurance lifeInsurance = new LifeInsurance(p, startDate, endDate, insuranceType, paymentform, note, baseAmount, user);
            unitOfWork.LifeInsuranceRepository.Add(lifeInsurance);
            unitOfWork.Save();

        }
        #endregion
    }
}
