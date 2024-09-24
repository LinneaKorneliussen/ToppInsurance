using System;
using System.Text;
using System.Security.Cryptography;



namespace TopInsuranceEntities
{
    public class Employee : Person
    {
        public EmployeeRole EmployeeRole { get; set; }
        public string PasswordHash { get; private set; }

        public Employee(string name, int phoneNumber, string emailAddress, string address, int zipCode, string city, EmployeeRole employeeRole, string password)
            : base(name, phoneNumber, emailAddress, address, zipCode, city)
        {
            EmployeeRole = employeeRole;
            PasswordHash = HashFunction(password);
            
        }

        public Employee() { }

        public string GenerateUsername()
        {
            string username = $"{PersonId}{Name}";
            return username;
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
