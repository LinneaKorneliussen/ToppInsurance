using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceEntities;
using System.Reflection;


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
        public void CreateNewBusinessCustomer(string firstName, string lastName, string phoneNumber, string emailAddress, string address,
                          int zipCode, string city, string companyName, int organizationalNumber, int countryCode)
        {
            BusinessCustomer businessCustomer = new BusinessCustomer(firstName, lastName, phoneNumber, emailAddress, address, zipCode, city, companyName, organizationalNumber, countryCode);
            unitOfWork.BCRepository.Add(businessCustomer);
            unitOfWork.Save();
        }
        #endregion

        #region Org.number unique check Method
        public bool IsOrganizationalnumberUnique(int organizationalnumber)
        {
            bool isUnique = !unitOfWork.BCRepository.Any(p => p.Organizationalnumber == organizationalnumber);
            return isUnique;
        }
        #endregion

        #region Get all business customers Method
        public List<BusinessCustomer> GetAllBusinessCustomers()
        {
            return unitOfWork.BCRepository.GetAll().ToList();
        }

        #endregion

        #region Update Business customer
        public void UpdateBusinessCustomers(BusinessCustomer businessCustomersToUpdate)
        {
            if(businessCustomersToUpdate != null)
            {
                unitOfWork.Save();
            }

        }
        #endregion

    }
}
