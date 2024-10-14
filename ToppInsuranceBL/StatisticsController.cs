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

        #region Get Sales Data For Employee Method
        public (Dictionary<string, List<int>> PrivateSalesData, Dictionary<string, List<int>> BusinessSalesData) GetSalesDataForEmployee(Employee employee)
        {
            return statisticsRepository.GetSalesDataForEmployee(employee);
        }
        #endregion
    }
}
