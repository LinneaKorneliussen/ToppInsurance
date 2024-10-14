using TopInsuranceEntities;
using Newtonsoft.Json;

namespace TopInsuranceDL
{
    public class CommissionRepository
    {
        private UnitOfWork unitOfWork;

        public CommissionRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }

        #region Calculate and create commission Method
        public Commission CalculateAndCreateCommission(Employee employee, DateTime startDate, DateTime endDate)
        {
            double totalPremium = 0;

            var existingCommission = unitOfWork.CommissionRepository.GetAll()
                .FirstOrDefault(c => c.EmployeeId == employee.PersonId &&
                                     c.StartDate >= startDate &&
                                     c.EndDate <= endDate);

            if (existingCommission != null)
            {
                return existingCommission;
            }

            var activeLifeInsurances = employee.LifeInsurances
                .Where(l => l.StartDate >= startDate && l.StartDate <= endDate && l.Status == Status.Aktiv && l.EmployeeId == employee.PersonId)
                .Select(l => l.Premium);

            var activeAccidentInsurances = employee.AccidentInsurances
                .Where(a => a.StartDate >= startDate && a.StartDate <= endDate && a.Status == Status.Aktiv && a.EmployeeId == employee.PersonId)
                .Select(a => a.Premium);

            var activeLiabilityInsurances = employee.LiabilityInsurances
                .Where(l => l.StartDate >= startDate && l.StartDate <= endDate && l.Status == Status.Aktiv && l.EmployeeId == employee.PersonId)
                .Select(l => l.Premium);

            var activeVehicleInsurances = employee.VehicleInsurances
                .Where(v => v.StartDate >= startDate && v.StartDate <= endDate && v.Status == Status.Aktiv && v.EmployeeId == employee.PersonId)
                .Select(v => v.Premium);

            var realEstateInsurances = employee.RealEstateInsurances
                .Where(re => re.StartDate >= startDate && re.StartDate <= endDate && re.Status == Status.Aktiv && re.EmployeeId == employee.PersonId);

            totalPremium += activeLifeInsurances.Sum();
            totalPremium += activeAccidentInsurances.Sum();
            totalPremium += activeLiabilityInsurances.Sum();
            totalPremium += activeVehicleInsurances.Sum();

            foreach (var realEstateInsurance in realEstateInsurances)
            {
                totalPremium += realEstateInsurance.Premium;
                totalPremium += realEstateInsurance.Inventories.Sum(inv => inv.InvPremium);
            }

            double commissionAmount = CalculateCommission(totalPremium);

            Commission commission = new Commission(startDate, endDate, employee)
            {
                TotalCommission = commissionAmount
            };

            unitOfWork.CommissionRepository.Add(commission);
            unitOfWork.Save();

            SaveComissionToJson(commission); 

            return commission;
        }

        public double CalculateCommission(double totalPremium)
        {
            return totalPremium * 0.12;
        }
        #endregion

        public void SaveComissionToJson(Commission commission)
        {
            var comissionData = new
            {
                AgencyNumber = commission.Employee.AgencyNumber,
                SSN = commission.Employee.SSN,
                FirstName = commission.Employee.FirstName,
                LastName = commission.Employee.LastName,
                StartDate = commission.StartDate,
                EndDate = commission.EndDate,
                TotalComission = commission.TotalCommission
            };

            string json = JsonConvert.SerializeObject(comissionData, Newtonsoft.Json.Formatting.Indented);

            string filePath = "commissionReport.json"; 
            File.WriteAllText(filePath, json);
        }

        public List<dynamic> LoadComissionsFromJson()
        {
            string filePath = "comissionReport.json"; 

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            string json = File.ReadAllText(filePath);

            var comissionDataList = JsonConvert.DeserializeObject<List<dynamic>>(json);

            return comissionDataList;
        }
    }
}
