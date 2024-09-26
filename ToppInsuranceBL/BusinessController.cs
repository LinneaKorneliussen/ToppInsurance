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
        public void CreateNewBusinessCustomer(string name, string phoneNumber, 
            string emailAddress, string address, int zipCode, string city, string companyName, int organizationalNumber, int countryCode)
        {
            businessRepository.CreateNewBusinessCustomer(name, phoneNumber, emailAddress, address, zipCode, city, companyName, organizationalNumber, countryCode);
        }
        #endregion

        #region Business unique Method
        //public bool IsOrganizationalnumberUnique(string organizationalnumber)
        //{
        //    return businessRepository.IsOrganizationalnumberUnique(organizationalnumber);
        //}
        #endregion

        #region Get all business customers Method
        public List<BusinessCustomer> GetAllBusinessCustomers()
        {
            return businessRepository.GetAllBusinessCustomers();
        }

        #endregion


    }
}
