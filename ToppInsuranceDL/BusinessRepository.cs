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
        public void CreateNewBusinessCustomer(string companyName, int organizationalNumber, int countryCode,
                          string name, int phoneNumber, string emailAddress, string address,
                          int zipCode, string city)
        {
            BusinessCustomer businessCustomer = new BusinessCustomer(companyName, organizationalNumber, countryCode,
                          name, phoneNumber, emailAddress, address,
                          zipCode, city);
            //unitOfWork.BusinessRepository.Add(businessCustomer);
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
