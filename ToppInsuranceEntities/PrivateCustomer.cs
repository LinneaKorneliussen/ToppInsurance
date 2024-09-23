using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToppInsuranceEntities
{
    public class PrivateCustomer : Person
    {
        public string SSN { get; set; }
        public int WorkPhonenumber { get; set; }

        public PrivateCustomer(string ssn, int workPhonenumber, string name, int phoneNumber, string emailAddress, string address, int zipCode, string city)
            : base(name, phoneNumber, address, emailAddress, zipCode, city) 
        {
            SSN = ssn;
            WorkPhonenumber = workPhonenumber;
        }
    }


}
