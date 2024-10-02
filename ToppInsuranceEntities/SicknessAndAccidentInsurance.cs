using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class SicknessAndAccidentInsurance : Insurance
    {
        public string? InsuranceFirstName { get; set; }
        public string? InsuranceLastName { get; set; }
        public string? InsuranceSSN { get; set; }
        public AdditionalInsurance AdditionalInsurance { get; set; }
        public int PrivateCustomerId { get; set; }
        public PrivateCustomer PrivateCustomer { get; set; }

        public SicknessAndAccidentInsurance(PrivateCustomer customer, DateTime startDate, DateTime endDate, InsuranceType type,
            Paymentform paymentform, int premium, int baseAmount, Status status, string note,
            string? insuranceFirstName, string? insuranceLastName, string? insuranceSSN, AdditionalInsurance additionalInsurance)
        : base(startDate, endDate, type, paymentform, premium, baseAmount, status, note)
        {
            PrivateCustomer = customer;
            InsuranceFirstName = insuranceFirstName;
            InsuranceLastName = insuranceLastName;
            InsuranceSSN = insuranceSSN;
            AdditionalInsurance = additionalInsurance;
        }

        public SicknessAndAccidentInsurance() {}
    }

}

