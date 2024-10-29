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
        private CommissionRepository commissionRepository;

        public CommissionController()
        {
            commissionRepository = new CommissionRepository();
        }

        #region Calculate and create commission Method
        public (Commission, string) CalculateAndCreateCommission(Employee employee, DateTime startDate, DateTime endDate)
        {
            return commissionRepository.CalculateAndCreateCommission(employee, startDate, endDate);
        }
        #endregion

        #region Get Commission Method 
        public List<Commission> GetCommissions()
        {
            return commissionRepository.GetCommissions();
        }
        #endregion
    }
}
