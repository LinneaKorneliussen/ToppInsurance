using System;
using System.Collections.Generic;
using System.Linq;
using TopInsuranceEntities;

namespace TopInsuranceEntities
{
    public class EmployeeSalesSummary
    {
        public Employee Employee { get; set; }
        public int Year { get; set; }
        public Dictionary<InsuranceType, int[]> MonthlySalesByType { get; set; }
        public int TotalSales { get; set; }
        public double AveragePerMonth { get; set; }

    }
}

