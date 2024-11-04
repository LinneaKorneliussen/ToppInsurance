using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    /// <summary>
    /// CommissionController class provides methods to calculate and create commissions for employees 
    /// over a specified date range and to retrieve all existing commission records. 
    /// This class acts as an intermediary between the presentation layer and data access layer for handling commissions.
    /// </summary>
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
