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
    public class ComissionController
    {
        private ComissionRepository comissionRepository;

        public ComissionController()
        {
            comissionRepository = new ComissionRepository();
        }

        public Comission CalculateAndCreateComission(Employee employee, DateTime startDate, DateTime endDate)
        {
            return comissionRepository.CalculateAndCreateComission(employee, startDate, endDate);
        }

        public void SaveComissionToJson(Employee employee, DateTime startDate, DateTime endDate)
        {
          comissionRepository.SaveComissionToJson(employee, startDate, endDate);
        }
    }
}
