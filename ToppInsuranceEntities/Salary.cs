using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class Salary
    {
        public int SalaryId { get; init; }   
        public double Commission { get; set; }

        public Salary(double commission)
        {
            Commission = commission;
        }
    }
}
