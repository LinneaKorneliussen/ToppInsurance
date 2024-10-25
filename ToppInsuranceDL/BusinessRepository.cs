using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceEntities;
using System.Reflection;
using Microsoft.EntityFrameworkCore;


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

        #region Search Business Customers Method 
        public List<BusinessCustomer> SearchBusinessCustomers(string searchBusinessCustomers)
        {
            List<BusinessCustomer> allBusinessCustomers = unitOfWork.BCRepository.GetAll().ToList();

            bool isNumericSearch = int.TryParse(searchBusinessCustomers, out int searchNumber);

            var matchingCustomers = allBusinessCustomers.Where(c =>
                c.CompanyName.Contains(searchBusinessCustomers, StringComparison.OrdinalIgnoreCase) ||
                (isNumericSearch && c.Organizationalnumber.ToString().Contains(searchBusinessCustomers))
            ).ToList();

            return matchingCustomers;
        }
        #endregion

        #region Get Business Customer with only one insurance Method 
        public List<BusinessCustomer> GetBusinessCustomerProspects()
        {
            List<BusinessCustomer> businessProspects = new List<BusinessCustomer>();
            var businessCustomers = unitOfWork.BCRepository.GetAll().ToList();

            var activeLiabilityInsurance = unitOfWork.LiabilityInsuranceRepository.GetAll()
                .Where(l => l.Status == Status.Aktiv)
                .ToList();

            var activeVehicleInsurance = unitOfWork.VehicleInsuranceRepository.GetAll()
                .Where(s => s.Status == Status.Aktiv)
                .ToList();

            var activeRealEstateInsurance = unitOfWork.RealEstateInsuranceRepository.GetAll()
                .Where(s => s.Status == Status.Aktiv)
                .ToList();

            foreach (var customer in businessCustomers)
            {
                int totalInsuranceCount = 0;

                totalInsuranceCount += activeLiabilityInsurance
                    .Count(l => l.BusinessCustomerId == customer.PersonId);

                totalInsuranceCount += activeVehicleInsurance
                    .Count(s => s.BusinessCustomerId == customer.PersonId);

                totalInsuranceCount += activeRealEstateInsurance
                    .Count(s => s.BusinessCustomerId == customer.PersonId);


                if (totalInsuranceCount <= 1)
                {
                    businessProspects.Add(customer);
                }
            }

            return businessProspects;
        }
        #endregion


    }
}
