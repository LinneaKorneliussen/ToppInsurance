using System.Windows.Input;
using System.Windows;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    /// <summary>
    /// The MenuResponsibleVM class serves as the view model for the VD and the Sales manager in the TopInsurance WPF application, 
    /// inheriting from ObservableObject to manage property change notifications. It initializes with an Employee object to 
    /// populate user details such as first name, last name, and role. The class manages the CurrentViewModel property to enable 
    /// navigation between different views, including statistics, customer prospects, and insurance overviews, based on user commands.
    /// </summary>
    public class MenuResponsibleVM : ObservableObject
    {
        public MenuResponsibleVM(Employee user)
        {
            userFirstName = user.FirstName;
            userLastName = user.LastName;
            userRole = user.EmployeeRole.ToString();
            CurrentViewModel = new MenuResponsibleVM();
            HomePageCommand = new RelayCommand(ShowHomePage);
            ShowStatisticsCommand = new RelayCommand(ShowStatisticsBTN);
            CustomerProspectCommand = new RelayCommand(ShowCustomerProspectBTN);
            InsuranceOverviewCommand = new RelayCommand(ShowInsuranceOverviewBTN);

            LogOffCommand = new RelayCommand(LogOffBTN);
        }
        public MenuResponsibleVM() { }

        #region Propertys 
        private string userFirstName;
        public string UserFirstName
        {
            get { return userFirstName; }
            set
            {
                userFirstName = value;
                OnPropertyChanged(nameof(UserFirstName));
                OnPropertyChanged(nameof(UserInfo));
            }
        }
        private string userLastName;
        public string UserLastName
        {
            get { return userLastName; }
            set
            {
                userLastName = value;
                OnPropertyChanged(nameof(UserLastName));
                OnPropertyChanged(nameof(UserInfo));
            }
        }

        private string userRole;
        public string UserRole
        {
            get { return userRole; }
            set
            {
                userRole = value;
                OnPropertyChanged(nameof(UserRole));
                OnPropertyChanged(nameof(UserInfo));
            }
        }

        private object _currentViewModel;
        public object CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public string UserInfo
        {
            get { return $"{UserFirstName} {UserLastName} - {UserRole}"; }
        }
        #endregion

        #region MenuVM Commands
        public ICommand HomePageCommand { get; }
        public ICommand ShowStatisticsCommand { get; }
        public ICommand CustomerProspectCommand { get; }
        public ICommand InsuranceOverviewCommand { get; }
        public ICommand LogOffCommand { get; }
        #endregion

        #region Command Methods
        private void ShowHomePage()
        {
            CurrentViewModel = null;
        }
        private void ShowStatisticsBTN()
        {
            CurrentViewModel = new Statistics();
        }
        private void ShowCustomerProspectBTN()
        {
            CurrentViewModel = new CustomerProspect();
        }
        private void ShowInsuranceOverviewBTN()
        {
            CurrentViewModel = new InsuranceOverview();
        }

        private void LogOffBTN()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            currentWindow?.Close();
        }
        #endregion

    }
}
