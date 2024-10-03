using System;
using System.Collections.Generic;
using System.Linq;

namespace TopInsuranceEntities
{
    public class BusinessInsurance : Insurance
    {
        public string ContactPerson { get; set; }
        public string ContactPhNo { get; set; }
        public int BusinessCustomerId { get; set; }
        public BusinessCustomer BusinessCustomer { get; private set; }

        public BusinessInsurance(BusinessCustomer customer, DateTime startDate, DateTime endDate, InsuranceType type, 
            Paymentform paymentform, Status status, string note,
            string contactPerson, string contactPhNo) : 
            base(startDate, endDate, type, paymentform, status, note)
        {
            BusinessCustomer = customer;
            ContactPerson = contactPerson;
            ContactPhNo = contactPhNo;
        }

        public BusinessInsurance() {}


    }
}
