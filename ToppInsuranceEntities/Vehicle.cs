using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class Vehicle
    {
        public int VehicleId { get; init; }  
        public string RegistrationNumber { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public VehicleInsurance VehicleInsurance { get; set; }

        public Vehicle(string registrationNumber, string brand, int year)
        {
            RegistrationNumber = registrationNumber;
            Brand = brand;
            Year = year;
        }
    }
}
