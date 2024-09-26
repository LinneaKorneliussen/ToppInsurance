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
        public void CreateNewPrivateCustomer(string name, string phoneNumber, 
            string emailAddress, string address, int zipCode, string city, string ssn, string workPhonenumber)
        {
            PrivateCustomer privateCustomer = new PrivateCustomer(name, phoneNumber, emailAddress, address, zipCode, city, ssn, workPhonenumber);
            unitOfWork.PCRepository.Add(privateCustomer);
            unitOfWork.Save();
        }
        #endregion
    }
}
