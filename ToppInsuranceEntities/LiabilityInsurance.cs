using System;
using System.Collections.Generic;
using System.Linq;

namespace TopInsuranceEntities
{
    public class LiabilityInsurance : Insurance
    {
        public string ContactPerson { get; set; }
        public string ContactPhNo { get; set; }
        public int BusinessCustomerId { get; set; }
        public BusinessCustomer BusinessCustomer { get; private set; }

        public LiabilityInsurance(BusinessCustomer customer, DateTime startDate, DateTime endDate, InsuranceType type, 
            Paymentform paymentform, string note,
            string contactPerson, string contactPhNo, Employee user) : 
            base(startDate, endDate, type, paymentform, note, user)
        {
            BusinessCustomer = customer;
            ContactPerson = contactPerson;
            ContactPhNo = contactPhNo;
        }

        public LiabilityInsurance() {}


    }
}
