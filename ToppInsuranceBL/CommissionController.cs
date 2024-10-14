using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    public class CommissionController
    {
        private CommissionRepository comissionRepository;

        public CommissionController()
        {
            comissionRepository = new CommissionRepository();
        }

        #region Calculate and create commission Method
        public Commission CalculateAndCreateCommission(Employee employee, DateTime startDate, DateTime endDate)
        {
            return comissionRepository.CalculateAndCreateCommission(employee, startDate, endDate);
        }
        #endregion
    }
}
