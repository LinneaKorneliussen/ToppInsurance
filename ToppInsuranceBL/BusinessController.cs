using System.Numerics;
using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
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
        public List<BusinessCustomer> SearchBusinessCustomers(string searchTerm)
        {
            return businessRepository.SearchBusinessCustomers(searchTerm);
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

        #region Get Business Customer prospect
        public List<BusinessCustomer> GetBusinessCustomerProspects()
        {
            return businessRepository.GetBusinessCustomerProspects();
        }
        #endregion

    }
}
