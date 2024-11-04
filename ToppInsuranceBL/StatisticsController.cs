using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    /// <summary>
    /// StatisticsController class provides methods for managing sales data related to employees.
    /// It allows retrieval of sales data for individual employees and for all employees in a selected year,
    /// as well as the ability to save this sales data to an Excel file. 
    /// This class acts as an intermediary between the presentation layer and the data access layer for handling sales statistics.
    /// </summary>
    public class StatisticsController
    {
        private StatisticsRepository statisticsRepository;

        public StatisticsController()
        {
            statisticsRepository = new StatisticsRepository();
        }

        #region Get Sales Data For Employee Method
        public (Dictionary<string, List<int>> PrivateSalesData, Dictionary<string, List<int>> BusinessSalesData) GetSalesDataForEmployee(Employee employee)
        {
            return statisticsRepository.GetSalesDataForEmployee(employee);
        }
        #endregion

        #region Get Sales Data For All Employees
        public List<EmployeeSalesSummary> GetSalesDataForAllEmployees(int selectedYear)
        {   
            return statisticsRepository.GetSalesDataForAllEmployees(selectedYear);
        }
        #endregion

        #region Save Sales Data To Excel
        public void SaveSalesDataToExcel(int selectedYear)
        {
            statisticsRepository.SaveSalesDataToExcel(selectedYear);
        }
        #endregion
    }
}
