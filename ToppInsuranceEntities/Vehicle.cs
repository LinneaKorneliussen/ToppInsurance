using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class Vehicle
    {
        public int RegistrationNumber { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }

        public Vehicle(int registrationNumber, string brand, int year)
        {
            RegistrationNumber = registrationNumber;
            Brand = brand;
            Year = year;
        }
    }
}
