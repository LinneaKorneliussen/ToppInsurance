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
        public string? InsuranceFirstName { get; set; }
        public string? InsuranceLastName { get; set; }
        public string? InsuranceSSN { get; set; }
        public AdditionalInsurance AdditionalInsurance { get; set; }
        public int PrivateCustomerId { get; set; }
        public PrivateCustomer PrivateCustomer { get; set; }

        public SicknessAndAccidentInsurance(PrivateCustomer customer, DateTime startDate, DateTime endDate, InsuranceType type,
            Paymentform paymentform, string note, string? insuranceFirstName, string? insuranceLastName, string? 
            insuranceSSN, AdditionalInsurance additionalInsurance, int baseAmount, Employee user)
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

        public SicknessAndAccidentInsurance() {}

        private void CalculatePremium()
        {
            Premium = (int)(BaseAmount * 0.0005);

            if (AdditionalInsurance == AdditionalInsurance.InvaliditetVidOlycksfall)
            {
                Premium += (int)(BaseAmount * 0.0003);
            }
            if (AdditionalInsurance == AdditionalInsurance.ErsättningVidLångvarigSjukskrivning)
            {
                Premium += (int)(BaseAmount * 0.0005);
            }
            if (AdditionalInsurance == AdditionalInsurance.Båda)
            {
                Premium += (int)(BaseAmount * 0.0003);
                Premium += (int)(BaseAmount * 0.0005);
            }

        }
    }

}

