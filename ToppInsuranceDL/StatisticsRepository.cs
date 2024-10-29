using TopInsuranceEntities;
using ClosedXML.Excel;


namespace TopInsuranceDL
{
    public class StatisticsRepository
    {
        private UnitOfWork unitOfWork;
        private string folderPath;

        public StatisticsRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();

            folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExcelExports");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
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

        #region Get Sales Data For All Employees
        public List<EmployeeSalesSummary> GetSalesDataForAllEmployees(int selectedYear)
        {
            var employees = unitOfWork.EmployeeRepository.GetAll()
                     .Where(e => e.EmployeeRole == EmployeeRole.Säljare)
                     .ToList();

            var lifeInsurances = unitOfWork.LifeInsuranceRepository.GetAll()
                .Where(i => i.Status == Status.Aktiv && i.StartDate.Year == selectedYear)
                .ToList();

            var sicknessAccidentInsurances = unitOfWork.SicknessAccidentInsuranceRepository.GetAll()
                .Where(i => i.Status == Status.Aktiv && i.StartDate.Year == selectedYear)
                .ToList();

            var adultSicknessInsurances = new List<SicknessAccidentInsurance>();
            var childSicknessInsurances = new List<SicknessAccidentInsurance>();

            foreach (var insurance in sicknessAccidentInsurances)
            {
                if (insurance.Type == InsuranceType.SjukOchOlycksfallsförsäkringVUXEN)
                {
                    adultSicknessInsurances.Add(insurance);
                }
                else if (insurance.Type == InsuranceType.SjukOchOlycksfallsförsäkringBARN)
                {
                    childSicknessInsurances.Add(insurance);
                }
            }

            var liabilityInsurances = unitOfWork.LiabilityInsuranceRepository.GetAll()
                .Where(i => i.Status == Status.Aktiv && i.StartDate.Year == selectedYear)
                .ToList();

            var vehicleInsurances = unitOfWork.VehicleInsuranceRepository.GetAll()
                .Where(i => i.Status == Status.Aktiv && i.StartDate.Year == selectedYear)
                .ToList();

            var realEstateInsurances = unitOfWork.RealEstateInsuranceRepository.GetAll()
                .Where(i => i.Status == Status.Aktiv && i.StartDate.Year == selectedYear)
                .ToList();

            var salesData = new List<EmployeeSalesSummary>();

            foreach (var employee in employees)
            {
                var summary = new EmployeeSalesSummary
                {
                    Employee = employee,
                    Year = selectedYear,
                    MonthlySalesByType = new Dictionary<InsuranceType, int[]>(),
                    TotalSales = 0,  
                    AveragePerMonth = 0.0
                };

                foreach (InsuranceType insuranceType in Enum.GetValues(typeof(InsuranceType)))
                {
                    summary.MonthlySalesByType[insuranceType] = new int[12];  
                }

                GroupAndProcessInsuranceData(lifeInsurances, employee, summary, InsuranceType.Livförsäkring);
                GroupAndProcessInsuranceData(adultSicknessInsurances, employee, summary, InsuranceType.SjukOchOlycksfallsförsäkringVUXEN);
                GroupAndProcessInsuranceData(childSicknessInsurances, employee, summary, InsuranceType.SjukOchOlycksfallsförsäkringBARN);
                GroupAndProcessInsuranceData(liabilityInsurances, employee, summary, InsuranceType.Ansvarsförsäkring);
                GroupAndProcessInsuranceData(vehicleInsurances, employee, summary, InsuranceType.Fordonsförsäkring);
                GroupAndProcessInsuranceData(realEstateInsurances, employee, summary, InsuranceType.FastighetsOchInventarieförsäkring);

                summary.TotalSales = summary.MonthlySalesByType.Sum(kv => kv.Value.Sum());
                summary.AveragePerMonth = summary.TotalSales / 12.0;

                salesData.Add(summary);
            }

            return salesData;
        }


        private void GroupAndProcessInsuranceData<TInsurance>(List<TInsurance> insurances, Employee employee, EmployeeSalesSummary summary, InsuranceType insuranceType)
            where TInsurance : Insurance
        {
            var groupedData = insurances
                .Where(i => i.EmployeeId == employee.PersonId) 
                .GroupBy(i => i.StartDate.Month) 
                .ToList();

            foreach (var group in groupedData)
            {
                int month = group.Key - 1; 
                summary.MonthlySalesByType[insuranceType][month] += group.Count(); 
            }
        }
        #endregion

        #region Save Sales Data To Excel
        public void SaveSalesDataToExcel(int selectedYear)
        {
            var salesData = GetSalesDataForAllEmployees(selectedYear); // Get sales data

            string filePath = Path.Combine(folderPath, $"SalesData_{selectedYear}.xlsx");

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sales Data");

                // Write column headers
                worksheet.Cell(1, 1).Value = "Agenturnr";
                worksheet.Cell(1, 2).Value = "Förnamn";
                worksheet.Cell(1, 3).Value = "Efternamn";
                worksheet.Cell(1, 4).Value = "Rapportens år";
                worksheet.Cell(1, 5).Value = "Försäkringstyp";
                worksheet.Cell(1, 6).Value = "Total försäljning";
                worksheet.Cell(1, 7).Value = "Medel/månad";

                // Write headers for months
                for (int i = 1; i <= 12; i++)
                {
                    worksheet.Cell(1, 7 + i).Value = $"Månad {i}"; 
                }

                int currentRow = 2; 

                foreach (var summary in salesData)
                {
                    foreach (var insuranceType in summary.MonthlySalesByType.Keys)
                    {
                        worksheet.Cell(currentRow, 1).Value = summary.Employee.AgencyNumber;
                        worksheet.Cell(currentRow, 2).Value = summary.Employee.FirstName;
                        worksheet.Cell(currentRow, 3).Value = summary.Employee.LastName;
                        worksheet.Cell(currentRow, 4).Value = summary.Year;
                        worksheet.Cell(currentRow, 5).Value = insuranceType.ToString();
                        worksheet.Cell(currentRow, 6).Value = summary.TotalSales;
                        worksheet.Cell(currentRow, 7).Value = summary.AveragePerMonth;

                        for (int month = 0; month < 12; month++)
                        {
                            worksheet.Cell(currentRow, 8 + month).Value = summary.MonthlySalesByType[insuranceType][month];
                        }

                        currentRow++;
                    }
                }

                workbook.SaveAs(filePath);
            }
        }
        #endregion
    }

}

