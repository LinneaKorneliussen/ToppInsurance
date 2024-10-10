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
        public Employee LoggedInUser { get; private set; }

        private UserContext() { }

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

        public void SetUser(Employee user)
        {
            LoggedInUser = user;
        }

        public void Logout()
        {
            LoggedInUser = null;
        }
    }

}
