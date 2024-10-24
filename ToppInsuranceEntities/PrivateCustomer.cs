
namespace TopInsuranceEntities
{
    public class PrivateCustomer : Person
    {
        public string SSN { get; set; }
        public string WorkPhonenumber { get; set; }

        public LifeInsurance LifeInsurance { get; set; }
        public ICollection<SicknessAccidentInsurance> SicknessAndAccidentInsurances { get; set; } = new List<SicknessAccidentInsurance>();
        public ICollection<ProspectInformation> ProspectInformationList { get; set; }

        public PrivateCustomer(string firstName, string lastName, string phoneNumber, string emailAddress, string address, int zipCode, string city, string ssn, string workPhonenumber)
            : base(firstName, lastName, phoneNumber, emailAddress, address, zipCode, city) 
        {
            SSN = ssn;
            WorkPhonenumber = workPhonenumber;
            ProspectInformationList = new List<ProspectInformation>();
        }

        public PrivateCustomer() 
        {
            ProspectInformationList = new List<ProspectInformation>();
        }
    }


}
