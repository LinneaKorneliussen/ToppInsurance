using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class LifeInsurance : Insurance
    {
        public LifeInsurance(int internalSerialNumber, string prefix, DateTime startDate, DateTime endDate, InsuranceType type, 
            Paymentform paymentform, int premium, int baseAmount, Status status, string note) : 
            base(internalSerialNumber, prefix, startDate, endDate, type, paymentform, premium, baseAmount, status, note)
        {
        }
    }
}
