using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceEntities;

namespace TopInsuranceDL
{
    public class RealEstateRepository
    {
        private UnitOfWork unitOfWork;

        public RealEstateRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }

        #region Add Real Estate Insurance Method
        public void AddRealEstateInsurance(BusinessCustomer customer, DateTime startDate, DateTime endDate, InsuranceType type,
            Paymentform paymentform, string note, string realEstateAddress, int zipcode, string city, int realEstateValue, List<Inventory> inventories, Employee user)
        {
            //RealEstateInsurance realEstateInsurance = new RealEstateInsurance(customer, startDate, endDate, type, 
            //    paymentform, note, realEstateAddress, zipcode, city, realEstateValue, user);
          
            //if (inventories != null && inventories.Any())
            //{
            //    foreach (var inventory in inventories)
            //    {
            //        realEstateInsurance.Inventories.Add(inventory);
            //    }
            //}

            //unitOfWork.RealEstateInsuranceRepository.Add(realEstateInsurance);
            //unitOfWork.Save(); 
        }
        #endregion
    }
}