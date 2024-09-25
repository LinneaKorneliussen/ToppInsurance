using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
