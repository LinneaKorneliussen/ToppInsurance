using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class ProspectInformation
    {
        public int ProspectInformationId { get; set; }
        public DateTime SigningDate { get; set; }
        public string Note { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int? PrivateCustomerId { get; set; }
        public PrivateCustomer PrivateCustomer { get; set; }

        public int? BusinessCustomerId { get; set; }
        public BusinessCustomer BusinessCustomer { get; set; }

        public ProspectInformation(string note, Employee employee, PrivateCustomer? privateCustomer, BusinessCustomer? businessCustomer) 
        {
            SigningDate = DateTime.Now;
            Note = note;
            Employee = employee;
            PrivateCustomer = privateCustomer;
            BusinessCustomer = businessCustomer; 
        }

        public ProspectInformation() { }
    }
}
