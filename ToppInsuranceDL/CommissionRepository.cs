using TopInsuranceEntities;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace TopInsuranceDL
{
    /// <summary>
    /// The CommissionRepository class is responsible for calculating and managing 
    /// commissions for insurance sales representatives. It interacts with various 
    /// insurance repositories to compute total premiums based on active policies 
    /// within a specified date range. The class also checks for existing commissions 
    /// to avoid duplicates, creates new commission records, and saves commission 
    /// data in a JSON format for reporting purposes.
    /// </summary>
    
    public class CommissionRepository
    {
        private UnitOfWork unitOfWork;

        public CommissionRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }

        #region Calculate and create commission Method
        public (Commission, string) CalculateAndCreateCommission(Employee employee, DateTime startDate, DateTime endDate)
        {
            double totalPremium = 0;

            var existingCommission = unitOfWork.CommissionRepository.GetAll()
                .FirstOrDefault(c => c.EmployeeId == employee.PersonId &&
                                     c.StartDate == startDate &&
                                     c.EndDate == endDate);

            if (existingCommission != null)
            {
                return (null, "Provisionsunderlag för säljaren under denna period existerar redan.");
            }

            var activeLifeInsurances = unitOfWork.LifeInsuranceRepository.GetAll()
                .Where(l => l.EmployeeId == employee.PersonId &&
                            l.StartDate >= startDate && 
                            l.StartDate <= endDate &&   
                            l.Status == Status.Aktiv);

            var activeAccidentInsurances = unitOfWork.SicknessAccidentInsuranceRepository.GetAll()
                .Where(a => a.EmployeeId == employee.PersonId &&
                            a.StartDate >= startDate &&
                            a.StartDate <= endDate &&
                            a.Status == Status.Aktiv);

            var activeLiabilityInsurances = unitOfWork.LiabilityInsuranceRepository.GetAll()
                .Where(l => l.EmployeeId == employee.PersonId &&
                            l.StartDate >= startDate &&
                            l.StartDate <= endDate &&
                            l.Status == Status.Aktiv);

            var activeVehicleInsurances = unitOfWork.VehicleInsuranceRepository.GetAll()
                .Where(v => v.EmployeeId == employee.PersonId &&
                            v.StartDate >= startDate &&
                            v.StartDate <= endDate &&
                            v.Status == Status.Aktiv);

            totalPremium += activeLifeInsurances.Sum(l => l.Premium);
            totalPremium += activeAccidentInsurances.Sum(a => a.Premium);
            totalPremium += activeLiabilityInsurances.Sum(l => l.Premium);
            totalPremium += activeVehicleInsurances.Sum(v => v.Premium);

            var realEstateInsurances = unitOfWork.RealEstateInsuranceRepository.GetAll()
                .Where(re => re.EmployeeId == employee.PersonId &&
                             re.StartDate >= startDate &&
                             re.StartDate <= endDate &&
                             re.Status == Status.Aktiv)
                .ToList();

            foreach (var realEstateInsurance in realEstateInsurances)
            {
                var inventories = unitOfWork.InventoryRepository.GetAll()
                    .Where(inv => inv.RealEstateInsuranceId == realEstateInsurance.InsuranceId);

                totalPremium += realEstateInsurance.Premium;
                totalPremium += inventories.Sum(inv => inv.InvPremium);
            }

            double commissionAmount = CalculateCommission(totalPremium);
            int commissionAsInt = (int)Math.Round(commissionAmount);

            Commission commission = new Commission(startDate, endDate, employee)
            {
                TotalCommission = commissionAsInt
            };

            unitOfWork.CommissionRepository.Add(commission);
            unitOfWork.Save();
            SaveCommissionToJson(commission);
            return (commission, null);
        }

        public double CalculateCommission(double totalPremium)
        {
            return totalPremium * 0.12;
        }
        #endregion

        #region Save Commission to Json Method
        public void SaveCommissionToJson(Commission commission)
        {
            string filePath = "commissionReport.json";
            List<dynamic> commissionsList = new List<dynamic>();

            if (File.Exists(filePath))
            {
                string existingJson = File.ReadAllText(filePath);

                if (!string.IsNullOrEmpty(existingJson))
                {
                    commissionsList = JsonConvert.DeserializeObject<List<dynamic>>(existingJson) ?? new List<dynamic>();
                }
            }

            var commissionData = new
            {
                AgencyNumber = commission.Employee.AgencyNumber,
                SSN = commission.Employee.SSN,
                FirstName = commission.Employee.FirstName,
                LastName = commission.Employee.LastName,
                StartDate = commission.StartDate,
                EndDate = commission.EndDate,
                TotalCommission = commission.TotalCommission
            };

            commissionsList.Add(commissionData);

            string json = JsonConvert.SerializeObject(commissionsList, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        #endregion

        #region Get Commission Method 
        public List<Commission> GetCommissions()
        {
            return unitOfWork.CommissionRepository.GetAllQueryable()
                .Include(c => c.Employee)
                .ToList();
        }
        #endregion


    }
}
