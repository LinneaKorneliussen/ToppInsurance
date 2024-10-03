using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceEntities;

namespace TopInsuranceWPF.ViewModels
{
    public class UserContext
    {
        private static UserContext _instance;

        // Den inloggade användaren
        public Employee LoggedInUser { get; private set; }

        // Privat konstruktor för att förhindra instansiering utanför klassen
        private UserContext() { }

        // Singleton instans
        public static UserContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserContext();
                }
                return _instance;
            }
        }

        // Metod för att sätta den inloggade användaren
        public void SetUser(Employee user)
        {
            LoggedInUser = user;
        }

        // Metod för att logga ut användaren
        public void Logout()
        {
            LoggedInUser = null;
        }
    }

}
