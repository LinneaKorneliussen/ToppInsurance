using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceEntities;

namespace TopInsuranceDL
{
    public class BusinessRepository
    {
        private UnitOfWork unitOfWork;
        public BusinessRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }

        #region Register new business customer Method
        public void CreateNewBusinessCustomer(string name, string phoneNumber, string emailAddress, string address,
                          int zipCode, string city, string companyName, int organizationalNumber, int countryCode)
        {
            BusinessCustomer businessCustomer = new BusinessCustomer(name, phoneNumber, emailAddress, address, zipCode, city, companyName, organizationalNumber, countryCode);
            unitOfWork.BCRepository.Add(businessCustomer);
            unitOfWork.Save();
        }
        #endregion

        #region Patient peronalnumber unique Method
        //public bool IsOrganizationalnumberUnique(string organizationalnumber)
        //{
        //    bool isUnique = !unitOfWork.BusinessRepository.Any(p => p.Organizationalnumber == organizationalnumber);
        //    return isUnique;
        //}
        #endregion

        #region Get all business customers Method
        public List<BusinessCustomer> GetAllBusinessCustomers()
        {
            return unitOfWork.BCRepository.GetAll().ToList();
        }

        #endregion

        public void UpdateBusinessCustomers(BusinessCustomer businessCustomersToUpdate, string choice, string newValue)
        {
            if (businessCustomersToUpdate != null)
            {
                switch (choice)
                {
                    case "Name":
                        businessCustomersToUpdate.Name = newValue;
                        unitOfWork.Save();
                        break;
                    case "Phonenumber":
                        businessCustomersToUpdate.Phonenumber = newValue;
                        unitOfWork.Save();
                        break;
                    case "Emailaddress":
                        businessCustomersToUpdate.Emailaddress = newValue;
                        unitOfWork.Save();
                        break;
                    case "Address":
                        businessCustomersToUpdate.Address = newValue;
                        unitOfWork.Save();
                        break;
                    //case "Zipcode":
                    //    businessCustomersToUpdate.Zipcode = newValue;
                    //    unitOfWork.Save();
                    //    break;
                    case "City":
                        businessCustomersToUpdate.City = newValue;
                        unitOfWork.Save();
                        break;
                    case "CompanyName":
                        businessCustomersToUpdate.CompanyName = newValue;
                        unitOfWork.Save();
                        break;
                    default:
                        break;

                }

            }

        }

    }
}
