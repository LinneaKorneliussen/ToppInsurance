namespace ToppInsuranceEntities
{
    public abstract class Person
    {
        public string Name { get; set; }
        public int Phonenumber { get; set; }
        public string Emailadress { get; set; }
        public int Zipcode { get; set; }
        public string City { get; set; }

        public Person(string name, int phonenumber, string emailadress, int zipcode, string city)
        {
            Name = name;
            Phonenumber = phonenumber;
            Emailadress = emailadress;
            Zipcode = zipcode;
            City = city;
        }

    }
}
