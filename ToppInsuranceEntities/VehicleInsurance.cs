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
            InsuranceType type, Paymentform paymentform, Status status, string note, string trafficInsurance) : 
            base(startDate, endDate, type, paymentform, status, note)
        {
            BusinessCustomer = customer;
            Vehicle = car;
            TrafficInsurance = trafficInsurance;
        }

        public VehicleInsurance() { }

    }
}
