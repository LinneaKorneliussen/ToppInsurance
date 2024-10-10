using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceEntities;

namespace TopInsuranceDL
{
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

        #region Search Private Customer Method 
        public List<PrivateCustomer> SearchPrivateCustomer(string searchTerm)
        {
            List<PrivateCustomer> allPrivateCustomers = unitOfWork.PCRepository.GetAll().ToList();

            bool isNumericSearch = int.TryParse(searchTerm, out int searchNumber);
            var matchingCustomers = allPrivateCustomers.Where(c =>
                (c.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                c.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                (isNumericSearch && c.SSN.ToString().Contains(searchTerm))
            ).ToList();

            return matchingCustomers;
        }
        #endregion
    }
}
