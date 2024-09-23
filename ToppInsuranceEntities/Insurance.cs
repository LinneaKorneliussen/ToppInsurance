using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public abstract class Insurance
    {
        public int InternalSerialNumber { get; set; }
        public string Prefix { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public InsuranceType Type { get; set; }
        public Paymentform Paymentform { get; set; }
        public int Premium {  get; set; }
        public int BaseAmount { get; set; }
        public Status Status { get; set; }
        public string Note { get; set; }

        public Insurance(int internalSerialNumber, string prefix, DateTime startDate, DateTime endDate, 
            InsuranceType type, Paymentform paymentform, int premium, int baseAmount, Status status, string note)
        {
            InternalSerialNumber = internalSerialNumber;
            Prefix = prefix;
            StartDate = startDate;
            EndDate = endDate;
            Type = type;
            Paymentform = paymentform;
            Premium = premium;
            BaseAmount = baseAmount;
            Status = status;
            Note = note;
        }
    }
}
