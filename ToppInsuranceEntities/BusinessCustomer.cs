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
        [Required]
        public int Organizationalnumber { get; set; }   
        public int CountryCode { get; set; }

        public BusinessCustomer(string name, string phoneNumber, string emailAddress, string address,
                          int zipCode, string city, string companyName, int organizationalNumber, int countryCode)
      : base(name, phoneNumber, emailAddress, address, zipCode, city) 
        {
            CompanyName = companyName;
            Organizationalnumber = organizationalNumber;
            CountryCode = countryCode;
        }

        public BusinessCustomer() { }

    }
}
