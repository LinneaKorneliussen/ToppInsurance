using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class BusinessInsurance : Insurance
    {
        public string ContactPerson { get; set; }
        public int ContactPersonPhonenumber { get; set; }

        public BusinessInsurance(string contactPerson, int contactPersonPhonenumber, int internalSerialNumber, string prefix, DateTime startDate, DateTime endDate, 
            InsuranceType type, Paymentform paymentform, int premium, int baseAmount, Status status, string note) : 
            base(internalSerialNumber, prefix, startDate, endDate, type, paymentform, premium, baseAmount, status, note)
        {
            ContactPerson = contactPerson;
            ContactPersonPhonenumber = contactPersonPhonenumber;
        }


    }
}
