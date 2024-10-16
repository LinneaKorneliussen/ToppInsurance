using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class BusinessCustomer : Person
    {
        public string CompanyName { get; set; }
        public int Organizationalnumber { get; set; }   
        public int CountryCode { get; set; }

        public ICollection<LiabilityInsurance> BusinessInsurances { get; set; }
        public ICollection<RealEstateInsurance> RealEstateInsurances { get; set; }
        public ICollection<VehicleInsurance> VehicleInsurances{ get; set; }
        public ICollection<ProspectInformation> ProspectInformationList { get; set; }

        public BusinessCustomer(string firstName, string lastName, string phoneNumber, string emailAddress, string address,
                          int zipCode, string city, string companyName, int organizationalNumber, int countryCode)
      : base(firstName, lastName, phoneNumber, emailAddress, address, zipCode, city) 
        {
            CompanyName = companyName;
            Organizationalnumber = organizationalNumber;
            CountryCode = countryCode;
        }

        public BusinessCustomer() { }

    }
}
