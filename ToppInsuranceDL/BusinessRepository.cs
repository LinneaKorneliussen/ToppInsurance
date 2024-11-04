using TopInsuranceEntities;

namespace TopInsuranceDL
{
    /// <summary>
    /// The BusinessRepository class manages business customer data within the system.
    /// It provides functionality to create, retrieve, update, and search for business customers.
    /// This class interacts with the data layer through the unit of work pattern, ensuring efficient 
    /// database operations and maintaining data integrity.
    /// </summary>
    
    public class BusinessRepository
    {   
        private UnitOfWork unitOfWork;
        public BusinessRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }

        #region Register new business customer Method
        public void CreateNewBusinessCustomer(string firstName, string lastName, string phoneNumber, string emailAddress, string address,
                          int zipCode, string city, string companyName, int organizationalNumber, int countryCode)
        {
            BusinessCustomer businessCustomer = new BusinessCustomer(firstName, lastName, phoneNumber, emailAddress, address, zipCode, city, companyName, organizationalNumber, countryCode);
            unitOfWork.BCRepository.Add(businessCustomer);
            unitOfWork.Save();
        }
        #endregion

        #region Org.number unique check Method
        public bool IsOrganizationalnumberUnique(int organizationalnumber)
        {
            bool isUnique = !unitOfWork.BCRepository.Any(p => p.Organizationalnumber == organizationalnumber);
            return isUnique;
        }
        #endregion

        #region Get all business customers Method
        public List<BusinessCustomer> GetAllBusinessCustomers()
        {
            return unitOfWork.BCRepository.GetAll().ToList();
        }

        #endregion

        #region Update Business customer
        public void UpdateBusinessCustomers(BusinessCustomer businessCustomersToUpdate)
        {
            if(businessCustomersToUpdate != null)
            {
                unitOfWork.Save();
            }

        }
        #endregion

        #region Search Business Customers Method 
        public List<BusinessCustomer> SearchBusinessCustomers(string searchBusinessCustomers)
        {
            List<BusinessCustomer> allBusinessCustomers = unitOfWork.BCRepository.GetAll().ToList();

            bool isNumericSearch = int.TryParse(searchBusinessCustomers, out int searchNumber);

            var matchingCustomers = allBusinessCustomers.Where(c =>
                c.CompanyName.Contains(searchBusinessCustomers, StringComparison.OrdinalIgnoreCase) ||
                (isNumericSearch && c.Organizationalnumber.ToString().Contains(searchBusinessCustomers))
            ).ToList();

            return matchingCustomers;
        }
        #endregion
    }
}
