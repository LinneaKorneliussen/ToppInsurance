using TopInsuranceEntities;

namespace TopInsuranceDL
{
    /// <summary>
    /// The PrivateRepository class manages private customer data within the system.
    /// It provides functionality to check SSN uniqueness, create new private customers, retrieve all private customers,
    /// update customer information, and search for private customers by name or SSN.
    /// This class interacts with the data layer through the unit of work pattern, ensuring efficient 
    /// database operations and maintaining data integrity.
    /// </summary>
    
    public class PrivateRepository
    {
        private UnitOfWork unitOfWork;
        public PrivateRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }

        #region SSN unique check Method
        public bool SSNUnique(string ssn)
        {
            bool isUnique = !unitOfWork.PCRepository.Any(p => p.SSN == ssn);
            return isUnique;
        }
        #endregion

        #region Register new private customer Method
        public void CreateNewPrivateCustomer(string firstName, string lastName, string phoneNumber, 
            string emailAddress, string address, int zipCode, string city, string ssn, string workPhonenumber)
        {
            PrivateCustomer privateCustomer = new PrivateCustomer(firstName, lastName, phoneNumber, emailAddress, address, zipCode, city, ssn, workPhonenumber);
            unitOfWork.PCRepository.Add(privateCustomer);
            unitOfWork.Save();
        }
        #endregion

        #region Get all private customers Method
        public List<PrivateCustomer> GetAllPrivateCustomers()
        {
            return unitOfWork.PCRepository.GetAll().ToList();
        }

        #endregion

        #region Update private customer Method
        public void UpdatePrivateCustomer(PrivateCustomer privateCustomersToUpdate)
        {
            if (privateCustomersToUpdate != null)
            {
                unitOfWork.Save();
            }
        }
        #endregion

        #region Search Private Customers Method 
        public List<PrivateCustomer> SearchPrivateCustomers(string searchPrivateCustomers)
        {
            List<PrivateCustomer> allPrivateCustomers = unitOfWork.PCRepository.GetAll().ToList();

            bool isNumericSearch = int.TryParse(searchPrivateCustomers, out int searchNumber);
            var matchingCustomers = allPrivateCustomers.Where(c =>
                (c.FirstName.Contains(searchPrivateCustomers, StringComparison.OrdinalIgnoreCase) ||
                c.LastName.Contains(searchPrivateCustomers, StringComparison.OrdinalIgnoreCase)) ||
                (isNumericSearch && c.SSN.ToString().Contains(searchPrivateCustomers))
            ).ToList();

            return matchingCustomers;
        }
        #endregion

    }
}
