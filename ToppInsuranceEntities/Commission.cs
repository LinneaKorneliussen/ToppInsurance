using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class Commission
    {
        public int CommissionId { get; init; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalCommission { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public Commission(DateTime startDate, DateTime endDate, Employee employee)
        {
            StartDate = startDate;
            EndDate = endDate;
            Employee = employee;
        }

        public Commission() {}
    }
}
