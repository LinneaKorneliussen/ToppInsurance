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

        #region Register new patient Method
        public void CreateNewPrivateCustomer(string ssn, int workPhonenumber, string name, int phoneNumber, string emailAddress, string address, int zipCode, string city)
        {
            privateRepository.CreateNewPrivateCustomer(ssn, workPhonenumber, name, phoneNumber, emailAddress, address, zipCode, city);
        }
        #endregion
    }
}