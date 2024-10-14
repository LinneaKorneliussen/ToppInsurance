using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class LifeInsurance : Insurance
    {
        public double BaseAmount { get; set; }
        public int PrivateCustomerId { get; set; }
        public PrivateCustomer PrivateCustomer { get; private set; }
        public LifeInsurance(PrivateCustomer customer, DateTime startDate, DateTime endDate, InsuranceType type, 
            Paymentform paymentform, string note, int baseAmount, Employee user) : 
            base(startDate, endDate, type, paymentform, note, user)
        {
            PrivateCustomer = customer;
            BaseAmount = baseAmount;
            CalculatePremium();
        }

        public LifeInsurance() { }

        private void CalculatePremium()
        {
            Premium = BaseAmount * 0.0005;
        }
    }
}
