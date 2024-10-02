using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class LifeInsurance : Insurance
    {
        public int PrivateCustomerId { get; set; }
        public PrivateCustomer PrivateCustomer { get; private set; }
        public LifeInsurance(PrivateCustomer customer, DateTime startDate, DateTime endDate, InsuranceType type, 
            Paymentform paymentform, int premium, int baseAmount, Status status, string note) : 
            base(startDate, endDate, type, paymentform, premium, baseAmount, status, note)
        {
            PrivateCustomer = customer;
        }

        public LifeInsurance() { }
    }
}
