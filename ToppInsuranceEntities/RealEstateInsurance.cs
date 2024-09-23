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

        public RealEstateInsurance(string realEstateAddress, double realEstateValue, 
            int realEstatePremium, int internalSerialNumber, string prefix, DateTime startDate, 
            DateTime endDate, InsuranceType type, Paymentform paymentform, int premium, int baseAmount, Status status, string note) : 
            base(internalSerialNumber, prefix, startDate, endDate, type, paymentform, premium, baseAmount, status, note)
        {
            RealEstateAddress = realEstateAddress;
            RealEstateValue = realEstateValue;
            RealEstatePremium = realEstatePremium;
        }

    }
}
