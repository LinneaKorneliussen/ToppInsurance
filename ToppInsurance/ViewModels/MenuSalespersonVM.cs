﻿using System.Windows.Input;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;
using System.Windows;

namespace TopInsuranceWPF.ViewModels
{
    /// <summary>
    /// The MenuSalespersonVM class acts as the view model for the salesperson in the TopInsurance WPF application, inheriting from ObservableObject 
    /// to facilitate property change notifications. It is initialized with an Employee object to set user details, including first name, last name, and role. 
    /// The class manages the CurrentViewModel property to switch between various views related to customer management and insurance products based on user commands.
    /// </summary>
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
            ShowInvoiceCommand = new RelayCommand(ShowInvoiceBTN);
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
        public ICommand ShowInvoiceCommand { get; }
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
        private void ShowInvoiceBTN()
        {
            CurrentViewModel = new InvoiceView();
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
