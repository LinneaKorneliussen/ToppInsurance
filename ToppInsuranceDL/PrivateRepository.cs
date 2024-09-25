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

        #region Register new private customer Method
        public void CreateNewPrivateCustomer(string ssn, int workPhonenumber, string name, int phoneNumber, 
            string emailAddress, string address, int zipCode, string city)
        {
            PrivateCustomer privateCustomer = new PrivateCustomer(ssn, workPhonenumber, name, phoneNumber, emailAddress, address, zipCode, city);
            //unitOfWork.PrivateRepository.Add(privateCustomer);
            unitOfWork.Save();
        }
        #endregion
    }
}
