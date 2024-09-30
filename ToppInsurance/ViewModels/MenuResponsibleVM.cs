using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    public class MenuResponsibleVM : ObservableObject
    {
        public MenuResponsibleVM(Employee user)
        {
            userName = user.Name;
            userRole = user.EmployeeRole.ToString();
            CurrentViewModel = new MenuResponsibleVM();
            HomePageCommand = new RelayCommand(ShowHomePage);
            BusinessAddCommand = new RelayCommand(AddBusinessCustomerBTN);
            PrivateAddCommand = new RelayCommand(AddPrivateCustomerBTN);
            NewInsuranceCommand = new RelayCommand(NewInsuranceBTN);
            EditCustomerCommand = new RelayCommand(EditCustomerBTN);
            RegisterEmployerCommand = new RelayCommand(RegisterEmployerBTN);
            ShowStatisticsCommand = new RelayCommand(ShowStatisticsBTN);
            
            LogOffCommand = new RelayCommand(LogOffBTN);
        }
        public MenuResponsibleVM() { }

        #region Propertys 
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
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
            get { return $"{UserName} - {UserRole}"; }
        }
        #endregion

        #region MenuVM Commands
        public ICommand HomePageCommand { get; }
        public ICommand BusinessAddCommand { get; }
        public ICommand PrivateAddCommand { get; }
        public ICommand NewInsuranceCommand { get; }
        public ICommand EditCustomerCommand { get; }
        public ICommand ShowStatisticsCommand { get; }
        public ICommand RegisterEmployerCommand { get; }
        public ICommand LogOffCommand { get; }
        #endregion

        #region Command Methods
        private void ShowHomePage()
        {
            CurrentViewModel = null;
        }
        private void AddBusinessCustomerBTN()
        {
            CurrentViewModel = new RegisterBusinessCustomer();
        }

        private void AddPrivateCustomerBTN()
        {
            CurrentViewModel = new RegisterPrivateCustomer();
        }

        private void NewInsuranceBTN()
        {
            CurrentViewModel = new NewInsurance();
        }

        private void EditCustomerBTN()
        {
            CurrentViewModel = new EditCustomer();
        }

        private void RegisterEmployerBTN()
        {
            CurrentViewModel = new RegisterEmployer();
        }

        private void ShowStatisticsBTN()
        {
            CurrentViewModel = new Statistics();
        }

        private void LogOffBTN()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            currentWindow?.Close();
        }
        #endregion

    }
}
