using System.Windows.Input;
using System.Windows;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    public class MenuEconomicAssistantVM : ObservableObject
    {
        public MenuEconomicAssistantVM(Employee user)
        {
            userFirstName = user.FirstName;
            userLastName = user.LastName;
            userRole = user.EmployeeRole.ToString();
            CurrentViewModel = new MenuEconomicAssistantVM();
            HomePageCommand = new RelayCommand(ShowHomePage);
            ShowStatisticsCommand = new RelayCommand(ShowStatisticsBTN);
            ShowCommissionCommand = new RelayCommand(ShowCommissionBTN);
            ShowInvoiceCommand = new RelayCommand(ShowInvoiceBTN);
            InsuranceOverviewCommand = new RelayCommand(ShowInsuranceOverviewBTN);

            LogOffCommand = new RelayCommand(LogOffBTN);
        }
        public MenuEconomicAssistantVM() { }

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
        public ICommand ShowCommissionCommand { get; }
        public ICommand ShowInvoiceCommand { get; }
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

        private void ShowCommissionBTN()
        {
            CurrentViewModel = new CommissionView();
        }
        private void ShowInsuranceOverviewBTN()
        {
            CurrentViewModel = new InsuranceOverview();
        }

        private void ShowInvoiceBTN()
        {
            CurrentViewModel = new InvoiceView();
        }

        private void LogOffBTN()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            currentWindow?.Close();
        }
        #endregion
    }
}
