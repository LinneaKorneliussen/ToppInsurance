using System;
using System.Text;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
using ToppInsuranceEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopInsuranceEntities
{
    [Table("Employees")]
    public class Employee : Person
    {
        public EmployeeRole EmployeeRole { get; set; }
        public string PasswordHash { get; private set; }
        //public string Username { get; private set; }  // Ny egenskap för användarnamn

        public Employee(string name, int phoneNumber, string emailAddress, string address, int zipCode, string city, EmployeeRole employeeRole, string password)
            : base(name, phoneNumber, emailAddress, address, zipCode, city)
        {
            EmployeeRole = employeeRole;
            PasswordHash = HashFunction(password);
            //Username = GenerateUsername();  // Generera användarnamn vid skapande
        }

        public Employee() { }

        private string GenerateUsername()
        {
            // Skapa användarnamn med prefix "1234" följt av EmployeeId
            return $"1234{PersonId}";
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
