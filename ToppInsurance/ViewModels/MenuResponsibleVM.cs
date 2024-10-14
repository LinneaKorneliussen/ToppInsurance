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
            userFirstName = user.FirstName;
            userLastName = user.LastName;
            userRole = user.EmployeeRole.ToString();
            CurrentViewModel = new MenuResponsibleVM();
            HomePageCommand = new RelayCommand(ShowHomePage);
            BusinessAddCommand = new RelayCommand(AddBusinessCustomerBTN);
            PrivateAddCommand = new RelayCommand(AddPrivateCustomerBTN);
            LifeInsuranceCommand = new RelayCommand(LifeInsuranceBTN);
            SicknessAccidentCommand = new RelayCommand(SicknessAccidentBTN);
            LiabilityInsuranceCommand = new RelayCommand(LiabilityInsuranceBTN);
            VehicleInsuranceCommand = new RelayCommand(VehicleInsuranceBTN);
            RealEstateInsuranceCommand = new RelayCommand(RealEstateInsuranceBTN);
            EditCustomerCommand = new RelayCommand(EditCustomerBTN);
            RegisterEmployerCommand = new RelayCommand(RegisterEmployerBTN);
            ShowStatisticsCommand = new RelayCommand(ShowStatisticsBTN);
            
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
        public ICommand BusinessAddCommand { get; }
        public ICommand PrivateAddCommand { get; }
        public ICommand LifeInsuranceCommand { get; }
        public ICommand SicknessAccidentCommand { get; }
        public ICommand LiabilityInsuranceCommand { get; }
        public ICommand VehicleInsuranceCommand { get; }
        public ICommand RealEstateInsuranceCommand { get; }
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
        private void LifeInsuranceBTN()
        {
            CurrentViewModel = new LifeInsurance();
        }

        private void SicknessAccidentBTN()
        {
            CurrentViewModel = new SicknessAccident();
        }

        private void LiabilityInsuranceBTN()
        {
            CurrentViewModel = new LiabilityInsurance();
        }

        private void VehicleInsuranceBTN()
        {
            CurrentViewModel = new VehicleInsurance();
        }

        private void RealEstateInsuranceBTN()
        {
            CurrentViewModel = new RealEstateInsurance();
        }

        private void EditCustomerBTN()
        {
            CurrentViewModel = new EditCustomer();
        }

        private void RegisterEmployerBTN()
        {
            CurrentViewModel = new RegisterEmployee();
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
