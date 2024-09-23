using System;
using System.Text;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
using ToppInsuranceEntities;

namespace TopInsuranceEntities
{
    public class Employee : Person
    {
        public int AgencyNumber { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
        public string PasswordHash { get; private set; }

        public Employee(int agencyNuber, EmployeeRole employeeRole, string password, string name, int phoneNumber, string emailAddress, string address, int zipCode, string city): 
            base(name, phoneNumber, address, emailAddress, zipCode, city)
        {
           AgencyNumber = agencyNuber;
           EmployeeRole = employeeRole;
           PasswordHash = HashFunction(password);
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
                return BitConverter.ToString(hash).ToLower().Replace("-", String.Empty);
            }
        }
    }
}
