using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class SicknessAccidentInsurance : Insurance
    {
        public double BaseAmount { get; set; }
        public string? InsuranceFirstName { get; set; }
        public string? InsuranceLastName { get; set; }
        public string? InsuranceSSN { get; set; }
        public AdditionalInsurance AdditionalInsurance { get; set; }
        public int PrivateCustomerId { get; set; }
        public PrivateCustomer PrivateCustomer { get; set; }

        public SicknessAccidentInsurance(PrivateCustomer customer, DateTime startDate, DateTime endDate, InsuranceType type,
            Paymentform paymentform, string note, string? insuranceFirstName, string? insuranceLastName, string? 
            insuranceSSN, AdditionalInsurance additionalInsurance, double baseAmount, Employee user)
        : base(startDate, endDate, type, paymentform, note, user)
        {
            PrivateCustomer = customer;
            InsuranceFirstName = insuranceFirstName;
            InsuranceLastName = insuranceLastName;
            InsuranceSSN = insuranceSSN;
            AdditionalInsurance = additionalInsurance;
            BaseAmount = baseAmount;
            CalculatePremium();
        }

        public SicknessAccidentInsurance() {}

        private void CalculatePremium()
        {
            Premium = BaseAmount * 0.0005;

            if (AdditionalInsurance == AdditionalInsurance.InvaliditetVidOlycksfall)
            {
                Premium += BaseAmount * 0.0003;
            }
            if (AdditionalInsurance == AdditionalInsurance.ErsättningVidLångvarigSjukskrivning)
            {
                Premium += BaseAmount * 0.0005;
            }
            if (AdditionalInsurance == AdditionalInsurance.Båda)
            {
                Premium += BaseAmount * 0.0003;
                Premium += BaseAmount * 0.0005;
            }

        }
    }

}

