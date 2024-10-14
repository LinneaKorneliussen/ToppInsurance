using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TopInsuranceEntities;
using Newtonsoft.Json;


namespace TopInsuranceDL
{
    public class ComissionRepository
    {
        private UnitOfWork unitOfWork;

        public ComissionRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }
        public Comission CalculateAndCreateComission(Employee employee, DateTime startDate, DateTime endDate)
        {
            double totalPremium = 0;

            totalPremium += employee.LifeInsurances
                .Where(l => l.StartDate >= startDate && l.StartDate <= endDate && l.Status == Status.Aktiv)
                .Sum(l => l.Premium);

            totalPremium += employee.AccidentInsurances
                .Where(a => a.StartDate >= startDate && a.StartDate <= endDate && a.Status == Status.Aktiv)
                .Sum(a => a.Premium);

            totalPremium += employee.LiabilityInsurances
                .Where(l => l.StartDate >= startDate && l.StartDate <= endDate && l.Status == Status.Aktiv)
                .Sum(l => l.Premium);

            totalPremium += employee.VehicleInsurances
                .Where(v => v.StartDate >= startDate && v.StartDate <= endDate && v.Status == Status.Aktiv)
                .Sum(v => v.Premium);

            foreach (var realEstateInsurance in employee.RealEstateInsurances
                .Where(re => re.StartDate >= startDate && re.StartDate <= endDate && re.Status == Status.Aktiv))
            {
                totalPremium += realEstateInsurance.Premium;

                totalPremium += realEstateInsurance.Inventories
                    .Sum(inv => inv.InvPremium);
            }

            double comissionAmount = CalculateCommission(totalPremium);

            Comission comission = new Comission(startDate, endDate, employee)
            {
                TotalComission = comissionAmount 
            };

            unitOfWork.ComissionRepository.Add(comission);
            unitOfWork.Save(); 

            return comission;
        }
        public double CalculateCommission(double totalPremium)
        {
            return totalPremium * 0.12; 
        }

        public void SaveComissionToJson(Employee employee, DateTime startDate, DateTime endDate)
        {
            var comission = CalculateAndCreateComission(employee, startDate, endDate);

            var comissionData = new
            {
                AgencyNumber = employee.AgencyNumber,
                //PersonalNumber = employee.SSN, 
                FullName = $"{employee.FirstName} {employee.LastName}",
                Period = $"{startDate.ToShortDateString()} - {endDate.ToShortDateString()}",
                TotalComission = comission.TotalComission
            };

            string json = JsonConvert.SerializeObject(comissionData, Newtonsoft.Json.Formatting.Indented);

            // Spara till fil
            string filePath = "comissionReport.json"; // Anpassa sökvägen efter behov
            File.WriteAllText(filePath, json);

            // Visa innehållet för användaren (använd MVVM om du inte vill ha logik i code-behind)
            //ShowJsonContentToUser(json);
        }

        //private void ShowJsonContentToUser(string json)
        //{
        //    // Du kan använda en TextBox eller RichTextBox i ditt WPF-fönster
        //    JsonContentWindow jsonWindow = new JsonContentWindow();
        //    jsonWindow.JsonTextBox.Text = json; // visa JSON i ett TextBox-kontroll
        //    jsonWindow.ShowDialog(); // öppnar ett nytt fönster där JSON visas
        //}
    }
}

