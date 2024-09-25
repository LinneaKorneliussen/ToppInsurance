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
        public void CreateNewBusinessCustomer(string companyName, int organizationalNumber, int countryCode,
                          string name, int phoneNumber, string emailadress, string address,
                          int zipCode, string city)
        {
            businessRepository.CreateNewBusinessCustomer(companyName, organizationalNumber, countryCode,
                          name, phoneNumber, emailadress, address, zipCode, city);
        }
        #endregion

        #region Business unique Method
        //public bool IsOrganizationalnumberUnique(string organizationalnumber)
        //{
        //    return businessRepository.IsOrganizationalnumberUnique(organizationalnumber);
        //}
        #endregion
    }
}
