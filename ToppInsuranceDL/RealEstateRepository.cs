using TopInsuranceEntities;

namespace TopInsuranceDL
{
    /// <summary>
    /// The RealEstateRepository class manages real estate insurance data within the system.
    /// It provides functionality to create new real estate insurance policies for business customers, 
    /// including adding associated inventories. This class interacts with the data layer through 
    /// the unit of work pattern, ensuring efficient database operations and data integrity.
    /// </summary>
    public class RealEstateRepository
    {
        private UnitOfWork unitOfWork;

        public RealEstateRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }

        #region Add Real Estate Insurance Method
        public void AddRealEstateInsurance(BusinessCustomer customer, DateTime startDate, DateTime endDate, InsuranceType type,
            Paymentform paymentform, string note, string realEstateAddress, int zipcode, string city, double realEstateValue,
            List<Inventory> inventories, Employee user)
        {
            RealEstateInsurance realEstateInsurance = new RealEstateInsurance(customer, startDate, endDate, type,
                paymentform, note, realEstateAddress, zipcode, city, realEstateValue, user);

            if (inventories != null && inventories.Any())
            {
                AddInventories(realEstateInsurance, inventories); 
            }

            unitOfWork.RealEstateInsuranceRepository.Add(realEstateInsurance);
            unitOfWork.Save();
        }
        #endregion

        #region Add Range for Inventories
        private void AddInventories(RealEstateInsurance insurance, List<Inventory> inventories)
        {
            foreach (var inventory in inventories)
            {
                insurance.Inventories.Add(inventory);
            }
        }
        #endregion

    }
}