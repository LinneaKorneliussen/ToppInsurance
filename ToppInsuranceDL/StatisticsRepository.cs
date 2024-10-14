using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceEntities;

namespace TopInsuranceDL
{
    public class StatisticsRepository
    {
        private UnitOfWork unitOfWork;

        public StatisticsRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }

        #region Get Sales Data For Employee Method
        public (Dictionary<string, List<int>> PrivateSalesData, Dictionary<string, List<int>> BusinessSalesData) GetSalesDataForEmployee(Employee employee)
        {
            var privateSalesData = new Dictionary<string, List<int>>
            {
                { "Livförsäkring", new List<int> { 0, 0, 0, 0, 0 } },
                { "Sjuk- och olycksfallsförsäkring", new List<int> { 0, 0, 0, 0, 0 } }
            };

            var businessSalesData = new Dictionary<string, List<int>> {
                { "Ansvarsförsäkring", new List<int> { 0, 0, 0, 0, 0 } },
                { "Fordonsförsäkring", new List<int> { 0, 0, 0, 0, 0 } },
                { "Fastighetsförsäkring", new List<int> { 0, 0, 0, 0, 0 } }
            };

            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;

            var insuranceTypes = new List<(string Type, Func<IEnumerable<Insurance>> GetInsurances)>
            {
                ("Livförsäkring", () => unitOfWork.LifeInsuranceRepository.GetAll()),
                ("Sjuk- och olycksfallsförsäkring", () => unitOfWork.SicknessAccidentInsuranceRepository.GetAll()),
                ("Ansvarsförsäkring", () => unitOfWork.LiabilityInsuranceRepository.GetAll()),
                ("Fordonsförsäkring", () => unitOfWork.VehicleInsuranceRepository.GetAll()),
                ("Fastighetsförsäkring", () => unitOfWork.RealEstateInsuranceRepository.GetAll())
            };

            foreach (var insuranceType in insuranceTypes)
            {
                var sales = insuranceType.GetInsurances()
                    .Where(i => i.EmployeeId == employee.PersonId && i.Status == Status.Aktiv)
                    .ToList();

                foreach (var insurance in sales)
                {
                    if (insurance.StartDate.Year == currentYear)
                    {
                        int monthIndex = currentMonth - insurance.StartDate.Month;
                        if (monthIndex >= 0 && monthIndex < 5)
                        {
                            if (insuranceType.Type == "Livförsäkring" || insuranceType.Type == "Sjuk- och olycksfallsförsäkring")
                            {
                                privateSalesData[insuranceType.Type][4 - monthIndex]++;
                            }
                            else
                            {
                                businessSalesData[insuranceType.Type][4 - monthIndex]++;
                            }
                        }
                    }
                }
            }

            return (privateSalesData, businessSalesData);
        }
        #endregion

    }
}
