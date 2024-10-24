using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceBL;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;
using System.Windows;



namespace TopInsuranceWPF.ViewModels
{
    public class MenuSalespersonVM : ObservableObject
    {
        public MenuSalespersonVM(Employee user)
        {
            userFirstName = user.FirstName;
            userLastName = user.LastName;
            userRole = user.EmployeeRole.ToString();
            CurrentViewModel = new MenuSalespersonVM();
            HomePageCommand = new RelayCommand(ShowHomePage);
            BusinessAddCommand = new RelayCommand(AddBusinessCustomerBTN);
            PrivateAddCommand = new RelayCommand(AddPrivateCustomerBTN);
            LifeInsuranceCommand = new RelayCommand(LifeInsuranceBTN);
            SicknessAccidentCommand = new RelayCommand(SicknessAccidentBTN);
            LiabilityInsuranceCommand = new RelayCommand(LiabilityInsuranceBTN);
            VehicleInsuranceCommand = new RelayCommand (VehicleInsuranceBTN);
            RealEstateInsuranceCommand = new RelayCommand(RealEstateInsuranceBTN);
            EditCustomerCommand = new RelayCommand(EditCustomerBTN);
            CustomerProspectCommand = new RelayCommand(ShowCustomerProspectBTN);
            InsuranceOverviewCommand = new RelayCommand(ShowInsuranceOverviewBTN);

            LogOffCommand = new RelayCommand(LogOffBTN);
        }
        public MenuSalespersonVM() { }

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
                userFirstName = value;
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
            get { return $"{UserFirstName} {userLastName} - {UserRole}"; } // Returnerar både namn och roll
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
        public ICommand CustomerProspectCommand { get; }
        public ICommand InsuranceOverviewCommand { get; }
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
            CurrentViewModel = new LifeInsuranceView();
        }

        private void SicknessAccidentBTN()
        {
            CurrentViewModel = new SicknessAccidentView();  
        }

        private void LiabilityInsuranceBTN() 
        {
            CurrentViewModel = new LiabilityInsuranceView();
        }

        private void VehicleInsuranceBTN() 
        {
            CurrentViewModel = new VehicleInsuranceView();
        }

        private void RealEstateInsuranceBTN() 
        {
            CurrentViewModel= new RealEstateInsuranceView();    
        }

        private void EditCustomerBTN()
        {
            CurrentViewModel = new EditCustomer();
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
