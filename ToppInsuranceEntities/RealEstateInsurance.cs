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
        public int RealEstateZipCode { get; set; }
        public string RealEstateCity { get; set; }
        public double RealEstateValue { get; set; }
        public int BusinessCustomerId { get; set; }
        public BusinessCustomer BusinessCustomer { get; private set; }
        public ICollection<Inventory> Inventories { get; private set; } = new List<Inventory>();
        public RealEstateInsurance(BusinessCustomer customer, DateTime startDate, DateTime endDate, InsuranceType type, 
            Paymentform paymentform, string note, string realEstateAddress, int zipCode, string city, int realEstateValue, Employee user) : 
            base(startDate, endDate, type, paymentform, note, user)
        {
            BusinessCustomer = customer;
            RealEstateAddress = realEstateAddress;
            RealEstateValue = realEstateValue;
            RealEstateZipCode = zipCode;
            RealEstateCity = city;
            Premium = (int)Math.Round(CalculateRealEstatePremium());
        }

        public RealEstateInsurance() {}

        public double CalculateRealEstatePremium()
        {
            return RealEstateValue * 0.002;
        }
    }
}
