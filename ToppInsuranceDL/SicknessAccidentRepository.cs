using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceEntities;

namespace TopInsuranceDL
{
    public class SicknessAccidentRepository
    {
        private UnitOfWork unitOfWork;
        private List<int> baseAmountsAdult;
        private List<int> baseAmountsChild;

        public SicknessAccidentRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
            baseAmountsAdult = new List<int> { 300000, 400000, 500000 };
            baseAmountsChild = new List<int> { 700000, 900000, 1100000, 1300000 };
            UpdateBaseAmounts();
        }

        #region Update Base Amounts Method
        private void UpdateBaseAmounts()
        {
            int currentYear = DateTime.Now.Year;
            int yearsPassed = currentYear - 2023; 
            int baseIncrease = 50000; 

            for (int i = 0; i < baseAmountsAdult.Count; i++)
            {
                baseAmountsAdult[i] += (baseIncrease * yearsPassed);
            }

            for (int i = 0; i < baseAmountsChild.Count; i++)
            {
                baseAmountsChild[i] += (baseIncrease * yearsPassed);
            }
        }
        #endregion

        #region Get Base Amounts Adults Method
        public List<int> GetBaseAmountsAdults()
        {
            return baseAmountsAdult;
        }
        #endregion

        #region Get Base Amounts Child Method
        public List<int> GetBaseAmountsChild()
        {
            return baseAmountsChild;
        }
        #endregion

        #region Add Sickness and Accident Insurance Method for Private Customer
        public void AddSicknessAccidentInsurance(PrivateCustomer customer, DateTime startDate, DateTime endDate,
            InsuranceType insuranceType, Paymentform paymentform, int baseAmount, string note,
            string? insuranceFirstName, string? insuranceLastName, string? insuranceSSN,
            AdditionalInsurance additionalInsurance, Employee user)
        {
            // Skapa den nya sjuk- och olycksfallsförsäkringen
            SicknessAccidentInsurance sicknessInsurance = new SicknessAccidentInsurance(
                customer, startDate, endDate,
                insuranceType, paymentform, note, insuranceFirstName,
                insuranceLastName, insuranceSSN, additionalInsurance, baseAmount, user);

            // Lägg till försäkringen i databasen
            unitOfWork.SicknessAccidentInsuranceRepository.Add(sicknessInsurance);
            unitOfWork.Save();
        }
        #endregion

    }
}
