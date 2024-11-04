using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    /// <summary>
    /// PrivateController class provides methods for managing private customers in the system.
    /// It allows checking the uniqueness of social security numbers (SSN), searching for private customers, 
    /// registering new private customers, retrieving all private customers and updating existing customer details.
    /// This class acts as an intermediary between the presentation layer and data access layer for handling private customer records.
    /// </summary>
    public class PrivateController
    {
        private PrivateRepository privateRepository;

        public PrivateController()
        {
            privateRepository = new PrivateRepository();
        }

        #region SSN unique Method
        public bool SSNUnique(string ssn)
        {
            return privateRepository.SSNUnique(ssn);
        }
        #endregion

        #region Search Private Customers Method
        public List<PrivateCustomer> SearchPrivateCustomers(string searchPrivateCustomers)
        {
            return privateRepository.SearchPrivateCustomers(searchPrivateCustomers);
        }
        #endregion

        #region Register new private customer Method
        public void CreateNewPrivateCustomer(string firstName, string lastName, string phoneNumber, string emailAddress, string address, int zipCode, string city, string ssn, string workPhonenumber)
        {
            privateRepository.CreateNewPrivateCustomer(firstName, lastName, phoneNumber, emailAddress, address, zipCode, city, ssn, workPhonenumber);
        }
        #endregion

        #region Get all private customers Method
        public List<PrivateCustomer> GetAllPrivateCustomers()
        {
            return privateRepository.GetAllPrivateCustomers();
        }

        #endregion

        #region Update private customers Method
        public void UpdatePrivateCustomer(PrivateCustomer privateCustomersToUpdate)
        {
            privateRepository.UpdatePrivateCustomer(privateCustomersToUpdate);
        }
        #endregion
    }
}