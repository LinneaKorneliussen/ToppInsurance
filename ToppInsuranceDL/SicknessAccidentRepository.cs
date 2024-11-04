using TopInsuranceEntities;

namespace TopInsuranceDL
{
    /// <summary>
    /// The SicknessAccidentRepository class manages sickness and accident insurance policies within the system.
    /// It provides functionality to add new policies for private customers, update base amounts for adults and children 
    /// based on annual increments, and retrieve the current base amounts. This class uses the unit of work pattern to 
    /// ensure efficient database operations and data consistency.
    /// </summary>
    
    public class SicknessAccidentRepository
    {
        private UnitOfWork unitOfWork;
        private List<double> baseAmountsAdult;
        private List<double> baseAmountsChild;

        public SicknessAccidentRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
            baseAmountsAdult = new List<double> { 300000, 400000, 500000 };
            baseAmountsChild = new List<double> { 700000, 900000, 1100000, 1300000 };
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
        public List<double> GetBaseAmountsAdults()
        {
            return baseAmountsAdult;
        }
        #endregion

        #region Get Base Amounts Child Method
        public List<double> GetBaseAmountsChild()
        {
            return baseAmountsChild;
        }
        #endregion

        #region Add Sickness and Accident Insurance Method for Private Customer
        public void AddSicknessAccidentInsurance(PrivateCustomer customer, DateTime startDate, DateTime endDate,
            InsuranceType insuranceType, Paymentform paymentform, double baseAmount, string note,
            string? insuranceFirstName, string? insuranceLastName, string? insuranceSSN,
            AdditionalInsurance additionalInsurance, Employee user)
        {
            SicknessAccidentInsurance sicknessInsurance = new SicknessAccidentInsurance(
                customer, startDate, endDate,
                insuranceType, paymentform, note, insuranceFirstName,
                insuranceLastName, insuranceSSN, additionalInsurance, baseAmount, user);

            unitOfWork.SicknessAccidentInsuranceRepository.Add(sicknessInsurance);
            unitOfWork.Save();
        }
        #endregion

    }
}
