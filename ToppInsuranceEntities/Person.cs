using System.ComponentModel.DataAnnotations;

namespace TopInsuranceEntities
{
    public abstract class Person
    {
        [Key]
        public int PersonId { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phonenumber { get; set; }
        public string Emailaddress { get; set; }
        public string Address { get; set; }
        public int Zipcode { get; set; }
        public string City { get; set; }

        public Person(string firstName, string lastName, string phonenumber, string emailaddress, string address, int zipcode, string city)
        {
            FirstName = firstName;
            LastName = lastName;
            Phonenumber = phonenumber;
            Address = address;
            Emailaddress = emailaddress;
            Zipcode = zipcode;
            City = city;
        }

        public Person() { }
    }
}
