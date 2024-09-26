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

        #region Get all private customers Method
        public List<PrivateCustomer> GetAllPrivateCustomers()
        {
            return unitOfWork.PCRepository.GetAll().ToList();
        }

        #endregion

        #region Update private customer Method
        public void UpdatePrivateCustomers(PrivateCustomer privateCustomersToUpdate, string choice, string newValue)
        {
            if (privateCustomersToUpdate != null)
            {
                switch (choice)
                {
                    case "Name":
                        privateCustomersToUpdate.Name = newValue;
                        unitOfWork.Save();
                        break;
                    case "Phonenumber":
                        privateCustomersToUpdate.Phonenumber = newValue;
                        unitOfWork.Save();
                        break;
                    case "Emailaddress":
                        privateCustomersToUpdate.Emailaddress = newValue;
                        unitOfWork.Save();
                        break;
                    case "Address":
                        privateCustomersToUpdate.Address = newValue;
                        unitOfWork.Save();
                        break;
                    //case "Zipcode":
                    //    businessCustomersToUpdate.Zipcode = newValue;
                    //    unitOfWork.Save();
                    //    break;
                    case "City":
                        privateCustomersToUpdate.City = newValue;
                        unitOfWork.Save();
                        break;
                    case "WorkPhonenumber":
                        privateCustomersToUpdate.WorkPhonenumber = newValue;
                        unitOfWork.Save();
                        break;
                    default:
                        break;

                }

            }

        }
        #endregion
    }
}
