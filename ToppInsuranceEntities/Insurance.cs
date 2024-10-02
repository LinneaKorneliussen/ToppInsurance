using System.ComponentModel.DataAnnotations;

namespace TopInsuranceEntities
{
    public abstract class Insurance
    {
        [Key] 
        public int InsuranceId { get; init; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public InsuranceType Type { get; set; }
        public Paymentform Paymentform { get; set; }
        public int Premium {  get; set; }
        public int BaseAmount { get; set; }
        public Status Status { get; set; }
        public string Note { get; set; }

        public Insurance( DateTime startDate, DateTime endDate, 
            InsuranceType type, Paymentform paymentform, int premium, int baseAmount, Status status, string note)
        {
            StartDate = startDate;
            EndDate = endDate;
            Type = type;
            Paymentform = paymentform;
            Premium = premium;
            BaseAmount = baseAmount;
            Status = status;
            Note = note;
        }

        public Insurance() { }
    }
}
