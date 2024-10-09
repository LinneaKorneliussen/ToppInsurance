using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    public class RealEstateController
    {
        private RealEstateRepository realEstateRepository;

        public RealEstateController()
        {
            realEstateRepository = new RealEstateRepository();
        }

        #region Search Business Customers Method
        public List<BusinessCustomer> SearchBusinessCustomer(string searchTerm)
        {
            return realEstateRepository.SearchBusinessCustomer(searchTerm);
        }
        #endregion

        #region Add real estate insurance Method 
        public void AddRealEstateInsurance(BusinessCustomer customer, DateTime startDate, DateTime endDate, InsuranceType type,
            Paymentform paymentform, string note, string realEstateAddress, int zipcode, string city, int realEstateValue, List<Inventory> inventories, Employee user)
        {
            realEstateRepository.AddRealEstateInsurance(customer, startDate, endDate, type, paymentform, note, realEstateAddress, zipcode, city, realEstateValue, inventories, user);
        }
        #endregion

    }
}
