using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class RealEstateInsurance : Insurance
    {
        public string RealEstateAddress { get; set; }
        public double RealEstateValue { get; set; }
        public int RealEstatePremium { get; set; }
        public int BusinessCustomerId { get; set; }
        public BusinessCustomer BusinessCustomer { get; private set; }
        public ICollection<Inventory> Inventories { get; private set; } = new List<Inventory>();
        public RealEstateInsurance(BusinessCustomer customer, DateTime startDate, DateTime endDate, InsuranceType type, 
            Paymentform paymentform, int premium, int baseAmount, Status status, string note,
            string realEstateAddress, double realEstateValue, int realEstatePremium) : 
            base(startDate, endDate, type, paymentform, premium, baseAmount, status, note)
        {
            BusinessCustomer = customer;
            RealEstateAddress = realEstateAddress;
            RealEstateValue = realEstateValue;
            RealEstatePremium = realEstatePremium;
        }

        public RealEstateInsurance() {}

    }
}
