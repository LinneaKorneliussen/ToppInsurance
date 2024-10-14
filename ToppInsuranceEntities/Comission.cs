using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class Comission
    {
        public int ComissionId { get; init; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalComission { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public Comission(DateTime startDate, DateTime endDate, Employee employee )
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public Comission()
        {
            
        }
    }
}
