using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class LifeInsurance : Insurance
    {
        public int BaseAmount { get; set; }
        public DateTime CalenderYear { get; set; }
        public int PrivateCustomerId { get; set; }
        public PrivateCustomer PrivateCustomer { get; private set; }
        public LifeInsurance(PrivateCustomer customer, DateTime startDate, DateTime endDate, InsuranceType type, 
            Paymentform paymentform, Status status, string note, int baseAmount) : 
            base(startDate, endDate, type, paymentform, status, note)
        {
            PrivateCustomer = customer;
            BaseAmount = baseAmount;
            CalenderYear = new DateTime(SigningDate.Year, 1, 1);
            CalculatePremium();
        }

        public LifeInsurance() { }

        private void CalculatePremium()
        {
            Premium = (int)(BaseAmount * 0.0005);
        }
    }
}
