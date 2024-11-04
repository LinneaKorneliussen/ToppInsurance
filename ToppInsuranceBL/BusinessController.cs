using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    /// <summary>
    /// BusinessController class manages business customer-related operations for the TopInsurance system.
    /// This class connects the presentation layer with the data access layer by providing methods for creating, 
    /// updating, searching, and retrieving business customers and checking the uniqueness of organizational numbers.
    /// </summary>
    public class BusinessController
    {
        private BusinessRepository businessRepository;

        public BusinessController()
        {
            businessRepository = new BusinessRepository();
        }

        #region Register new business customer Method
        public void CreateNewBusinessCustomer(string firstName, string lastName, string phoneNumber, string emailAddress,
            string address, int zipCode, string city, string companyName, int organizationalNumber, int countryCode)
        {
            businessRepository.CreateNewBusinessCustomer(firstName, lastName, phoneNumber, emailAddress, address, zipCode, city, companyName, organizationalNumber, countryCode);
        }
        #endregion

        #region Business unique Method
        public bool IsOrganizationalnumberUnique(int orgNumber)
        {
            return businessRepository.IsOrganizationalnumberUnique(orgNumber);
        }
        #endregion

        #region Search Business Customers Method
        public List<BusinessCustomer> SearchBusinessCustomers(string searchBusinessCustomers)
        {
            return businessRepository.SearchBusinessCustomers(searchBusinessCustomers);
        }
        #endregion

        #region Get all business customers Method
        public List<BusinessCustomer> GetAllBusinessCustomers()
        {
            return businessRepository.GetAllBusinessCustomers();
        }

        #endregion

        #region Update business Customers 
        public void UpdateBusinessCustomers(BusinessCustomer businessCustomersToUpdate)
        {
            businessRepository.UpdateBusinessCustomers(businessCustomersToUpdate);
        }
        #endregion

    }
}
