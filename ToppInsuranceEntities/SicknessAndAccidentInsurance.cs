using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class SicknessAndAccidentInsurance : Insurance
    {
        public string InsuranceFirstName { get; set; }
        public string InsuranceLastName { get; set; }
        public string InsuranceSSN { get; set; }
        public AdditionalInsurance AdditionalInsurance { get; set; }

        public SicknessAndAccidentInsurance(string insuranceFirstName, string insuranceLastName, string insuranceSSN,
        AdditionalInsurance additionalInsurance, int internalSerialNumber, string prefix, DateTime startDate,
        DateTime endDate, InsuranceType type, Paymentform paymentform, int premium, int baseAmount, Status status,
        string note)
        : base(internalSerialNumber, prefix, startDate, endDate, type, paymentform, premium, baseAmount, status, note)
        {
            InsuranceFirstName = insuranceFirstName;
            InsuranceLastName = insuranceLastName;
            InsuranceSSN = insuranceSSN;
            AdditionalInsurance = additionalInsurance;
        }
    }

}

