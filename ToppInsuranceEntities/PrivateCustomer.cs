using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class PrivateCustomer : Person
    {
        [Required]
        public string SSN { get; set; }
        public string WorkPhonenumber { get; set; }

        public LifeInsurance LifeInsurance { get; set; }
        public ICollection<SicknessAndAccidentInsurance> SicknessAndAccidentInsurances { get; set; }
        public PrivateCustomer(string name, string phoneNumber, string emailAddress, string address, int zipCode, string city, string ssn, string workPhonenumber)
            : base(name, phoneNumber, emailAddress, address, zipCode, city) 
        {
            SSN = ssn;
            WorkPhonenumber = workPhonenumber;
        }

        public PrivateCustomer() { }
    }


}
