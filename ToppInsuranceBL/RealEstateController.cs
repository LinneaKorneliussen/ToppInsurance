using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    /// <summary>
    /// RealEstateController class provides methods for managing real estate insurance for business customers. 
    /// It allows for the addition of real estate insurance policies, including details such as coverage type, 
    /// payment form, and property information. This class acts as an intermediary between the presentation layer 
    /// and the data access layer for handling real estate insurance.
    /// </summary>
    public class RealEstateController
    {
        private RealEstateRepository realEstateRepository;

        public RealEstateController()
        {
            realEstateRepository = new RealEstateRepository();
        }

        #region Add real estate insurance Method 
        public void AddRealEstateInsurance(BusinessCustomer customer, DateTime startDate, DateTime endDate, InsuranceType type,
            Paymentform paymentform, string note, string realEstateAddress, int zipcode, string city, double realEstateValue, List<Inventory> inventories, Employee user)
        {
            realEstateRepository.AddRealEstateInsurance(customer, startDate, endDate, type, paymentform, note, realEstateAddress, zipcode, city, realEstateValue, inventories, user);
        }
        #endregion

    }
}
