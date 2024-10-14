using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    public class StatisticsController
    {
        private StatisticsRepository statisticsRepository;

        public StatisticsController()
        {
            statisticsRepository = new StatisticsRepository();
        }

        #region Get Salesperson by Last Name or Agency Number
        public List<Employee> GetSalespersonsByLastNameOrAgencyNumber(string searchText)
        {
            return statisticsRepository.GetSalespersonsByLastNameOrAgencyNumber(searchText);
        }
        #endregion

        #region Get Sales Data For Employee Method
        public (Dictionary<string, List<int>> PrivateSalesData, Dictionary<string, List<int>> BusinessSalesData) GetSalesDataForEmployee(Employee employee)
        {
            return statisticsRepository.GetSalesDataForEmployee(employee);
        }
        #endregion
    }
}
