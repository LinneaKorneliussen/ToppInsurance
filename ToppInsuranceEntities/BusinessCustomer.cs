using System;
using System.Collections.Generic;
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

        public BusinessCustomer(string companyName, int organizationalNumber, int countryCode,
                          string name, int phoneNumber, string emailAddress, string address,
                          int zipCode, string city)
      : base(name, phoneNumber, emailAddress, address, zipCode, city) 
        {
            CompanyName = companyName;
            Organizationalnumber = organizationalNumber;
            CountryCode = countryCode;
        }

    }
}
