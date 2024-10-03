using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class VehicleInsurance : Insurance
    {
        public string TrafficInsurance {  get; set; }
        public Vehicle Vehicle { get; set; }
        public int BusinessCustomerId { get; set; }
        public BusinessCustomer BusinessCustomer { get; private set; }

        public VehicleInsurance(BusinessCustomer customer, Vehicle car, string prefix, DateTime startDate, DateTime endDate, 
            InsuranceType type, Paymentform paymentform, string note, string trafficInsurance, Employee user) : 
            base(startDate, endDate, type, paymentform, note, user)
        {
            BusinessCustomer = customer;
            Vehicle = car;
            TrafficInsurance = trafficInsurance;
        }

        public VehicleInsurance() { }

    }
}
