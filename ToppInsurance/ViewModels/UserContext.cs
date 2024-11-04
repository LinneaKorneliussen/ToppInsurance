using TopInsuranceEntities;

namespace TopInsuranceWPF.ViewModels
{
    /// <summary>
    /// Singleton class that manages the logged in users context in the Top Insurance WPF application.
    /// This class stores information about the currently logged in employee and provides access to it throughout 
    /// the application. It ensures that there is only one instance of the user context, allowing easy retrieval 
    /// of the logged-in user's details.
    /// The class includes a method to set the logged-in user, ensuring that the user information is consistently 
    /// accessible wherever needed in the application.
    /// </summary>
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
    }

}
