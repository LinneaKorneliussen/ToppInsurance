using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections;



namespace TopInsuranceEntities
{
    public class Employee : Person
    {
        public string SSN { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
        public string PasswordHash { get; private set; }
        public string AgencyNumber { get; private set; }

        private static List<string> GeneratedAgencyNumbers = new List<string>();
        public ICollection<LifeInsurance> LifeInsurances { get; set; } = new List<LifeInsurance>();
        public ICollection<SicknessAccidentInsurance> AccidentInsurances { get; set; } = new List<SicknessAccidentInsurance>();
        public ICollection<LiabilityInsurance> LiabilityInsurances { get; set; } = new List<LiabilityInsurance>();
        public ICollection<VehicleInsurance> VehicleInsurances { get; set; } = new List<VehicleInsurance>();
        public ICollection<RealEstateInsurance> RealEstateInsurances { get; set; } = new List<RealEstateInsurance>();
        public ICollection<Comission> Comissions { get; set; } = new List<Comission>();


        public Employee(string firstName, string lastName, string ssn, string phoneNumber, string emailAddress, string address, int zipCode, string city, EmployeeRole employeeRole, string password)
            : base(firstName, lastName, phoneNumber, emailAddress, address, zipCode, city)
        {
            SSN = ssn;
            EmployeeRole = employeeRole;
            PasswordHash = HashFunction(password);
            AgencyNumber = GenerateUniqueAgencyNumber();
            
        }

        public Employee() { }

        private string GenerateUniqueAgencyNumber()
        {
            Random random = new Random();
            string newNumber;

            do
            {
                newNumber = random.Next(1000, 9999).ToString(); 
            }
            while (GeneratedAgencyNumbers.Contains(newNumber));

            GeneratedAgencyNumbers.Add(newNumber);
            return newNumber;
        }


        public string GetHashedPassword(string password)
        {
            return HashFunction(password);
        }

        public string HashFunction(string input)
        {
            return ComputeSHA256Hash(input);
        }

        private string ComputeSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).ToLower().Replace("-", string.Empty);
            }
        }
    }

}
