using System.ComponentModel.DataAnnotations;

namespace TopInsuranceEntities
{
    public abstract class Insurance
    {
        [Key] 
        public int InsuranceId { get; init; }
        public DateTime SigningDate { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public InsuranceType Type { get; set; }
        public Paymentform Paymentform { get; set; }
        public double Premium {  get; set; }
        public Status Status { get; set; }
        public string? Note { get; set; }
        public int EmployeeId { get; set; }  
        public Employee Employee { get; set; }

        public Insurance( DateTime startDate, DateTime endDate,InsuranceType type, Paymentform paymentform, string? note, Employee employee)
        {
            SigningDate = DateTime.Now;
            StartDate = startDate;
            EndDate = endDate;
            Type = type;
            Paymentform = paymentform;
            Note = note;
            Employee = employee;
            Status = Status.Förfrågan;
            UpdateStatus();
        }

        public Insurance() { }

        public void UpdateStatus()
        {
            DateTime today = DateTime.Now;

            if (today < StartDate)
            {
                Status = Status.Förfrågan; 
            }
            else if (today >= StartDate && today <= EndDate)
            {
                Status = Status.Aktiv;  
            }
            else if (today > EndDate)
            {
                Status = Status.Avslutad; 
            }
        }
    }
}
