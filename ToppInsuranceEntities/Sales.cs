using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class Sales
    {
        public int SalesId { get; set; }
        public int InsuranceId { get; set; }
        public string AgencyNumber { get; set; }
        public int Amount { get; set; }
        public double CommissionRate { get; set; }
        public Sales(int insuranceId, string agencyNumber, int amount, double commissionRate)

        {
            InsuranceId = insuranceId;
            AgencyNumber = agencyNumber;
            Amount = amount;
            CommissionRate = commissionRate;
            GetCommission();
        }

        public double GetCommission()

        {

            return Amount * CommissionRate;


        }



    }
}
