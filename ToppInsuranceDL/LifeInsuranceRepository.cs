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

        #region Get all private customers Method
        public List<PrivateCustomer> GetAllPrivateCustomers()
        {
            return unitOfWork.PCRepository.GetAll().ToList();
        }
        #endregion

        #region Add life insurance Method for private customer 
        public void AddLifeInsurance(PrivateCustomer p, DateTime startDate, DateTime endDate, InsuranceType insuranceType, 
            Paymentform paymentform, int baseAmount, Status status, string note)
        {
            LifeInsurance lifeInsurance = new LifeInsurance(p, startDate, endDate, insuranceType, paymentform, status, note, baseAmount);
            unitOfWork.LifeInsuranceRepository.Add(lifeInsurance);
            unitOfWork.Save();

        }
        #endregion
    }
}
