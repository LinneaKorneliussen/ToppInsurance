using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceDL;

namespace TopInsuranceBL
{
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

        #region Register new patient Method
        public void CreateNewPrivateCustomer(string name, string phoneNumber, string emailAddress, string address, int zipCode, string city, string ssn, string workPhonenumber)
        {
            privateRepository.CreateNewPrivateCustomer(name, phoneNumber, emailAddress, address, zipCode, city, ssn, workPhonenumber);
        }
        #endregion
    }
}