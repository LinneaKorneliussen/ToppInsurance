namespace ToppInsuranceEntities
{
    public class Person
    {
        public string Name { get; set; }
        public int Phonenumber { get; set; }
        public string Emailadress { get; set; }
        public string Address { get; set; }
        public int Zipcode { get; set; }
        public string City { get; set; }

        public Person(string name, int phonenumber, string emailadress, string address, int zipcode, string city)
        {
            Name = name;
            Phonenumber = phonenumber;
            Address = address;
            Emailadress = emailadress;
            Zipcode = zipcode;
            City = city;
        }

    }
}
