using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToppInsuranceEntities
{
    public class PrivateCustomer : Person
    {
        public int SSN { get; set; }
        public int WorkPhonenumber { get; set; }

        public PrivateCustomer(int ssn, int workPhonenumber, string name, int phoneNumber, string emailAddress, int zipCode, string city)
            : base(name, phoneNumber, emailAddress, zipCode, city) 
        {
            SSN = ssn;
            WorkPhonenumber = workPhonenumber;
        }
    }


}
