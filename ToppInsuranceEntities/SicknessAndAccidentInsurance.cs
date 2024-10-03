using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class SicknessAndAccidentInsurance : Insurance
    {
        public int BaseAmount { get; set; }
        public DateTime CalenderYear { get; set; }
        public string? InsuranceFirstName { get; set; }
        public string? InsuranceLastName { get; set; }
        public string? InsuranceSSN { get; set; }
        public AdditionalInsurance AdditionalInsurance { get; set; }
        public int PrivateCustomerId { get; set; }
        public PrivateCustomer PrivateCustomer { get; set; }

        public SicknessAndAccidentInsurance(PrivateCustomer customer, DateTime startDate, DateTime endDate, InsuranceType type,
            Paymentform paymentform, Status status, string note, string? insuranceFirstName, string? insuranceLastName, string? 
            insuranceSSN, AdditionalInsurance additionalInsurance, int baseAmount)
        : base(startDate, endDate, type, paymentform, status, note)
        {
            PrivateCustomer = customer;
            InsuranceFirstName = insuranceFirstName;
            InsuranceLastName = insuranceLastName;
            InsuranceSSN = insuranceSSN;
            AdditionalInsurance = additionalInsurance;
            BaseAmount = baseAmount;
            CalenderYear = new DateTime(SigningDate.Year, 1, 1);
            CalculatePremium();
        }

        public SicknessAndAccidentInsurance() {}

        private void CalculatePremium()
        {
            Premium = (int)(BaseAmount * 0.0005);
        }
    }

}

