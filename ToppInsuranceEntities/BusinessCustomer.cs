
namespace TopInsuranceEntities
{
    public class BusinessCustomer : Person
    {
        public string CompanyName { get; set; }
        public int Organizationalnumber { get; set; }   
        public int CountryCode { get; set; }

        public ICollection<LiabilityInsurance> LiabilityInsurances { get; set; } 
        public ICollection<RealEstateInsurance> RealEstateInsurances { get; set; }  
        public ICollection<VehicleInsurance> VehicleInsurances { get; set; } 
        public ICollection<ProspectInformation> ProspectInformationList { get; set; } 

        public BusinessCustomer(string firstName, string lastName, string phoneNumber, string emailAddress, string address,
                          int zipCode, string city, string companyName, int organizationalNumber, int countryCode)
      : base(firstName, lastName, phoneNumber, emailAddress, address, zipCode, city) 
        {
            CompanyName = companyName;
            Organizationalnumber = organizationalNumber;
            CountryCode = countryCode;
            ProspectInformationList = new List<ProspectInformation>(); 

        }

        public BusinessCustomer() 
        {
            ProspectInformationList = new List<ProspectInformation>();

        }

    }
}
