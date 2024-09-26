using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    public class PrivateController
    {
        private PrivateRepository privateRepository;

        public PrivateController()
        {
            privateRepository = new PrivateRepository();
        }

        #region Register new patient Method
        public void CreateNewPrivateCustomer(string name, string phoneNumber, string emailAddress, string address, int zipCode, string city, string ssn, string workPhonenumber)
        {
            privateRepository.CreateNewPrivateCustomer(name, phoneNumber, emailAddress, address, zipCode, city, ssn, workPhonenumber);
        }
        #endregion

        #region Get all private customers Method
        public List<PrivateCustomer> GetAllPrivateCustomers()
        {
            return privateRepository.GetAllPrivateCustomers();
        }

        #endregion

        #region Update private customers Method
        public void UpdatePrivateCustomers(PrivateCustomer privateCustomersToUpdate, string choice, string newValue)
        {
            privateRepository.UpdatePrivateCustomers(privateCustomersToUpdate, choice, newValue);
        }
        #endregion
    }
}