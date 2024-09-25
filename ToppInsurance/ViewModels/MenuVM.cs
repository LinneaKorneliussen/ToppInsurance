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
using System.Windows.Controls;
using System.Net.NetworkInformation;
using System.Windows;
using System.Numerics;
using ControlzEx.Standard;


namespace TopInsuranceWPF.ViewModels
{
    public class MenuVM : ObservableObject
    {
        #region Constructor for ViewModel
        public MenuVM(Employee user)
        {
            userName = user.Name;
            userRole = user.EmployeeRole.ToString();
            BusinessAddCommand = new RelayCommand(AddBusinessCustomerBTN);
        }
        public MenuVM() { }
        #endregion

        // Logged in user
        #region Logged in user
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

        public string UserInfo
        {
            get { return $"{UserName} - {UserRole}"; } // Returnerar både namn och roll
        }
        #endregion


        #region MenuVM Commands
        public ICommand BusinessAddCommand { get; }
        #endregion

        private void AddBusinessCustomerBTN()
        {
            RegisterBusinessCustomer registerBusinessCustomer = new RegisterBusinessCustomer();
            registerBusinessCustomer.ShowDialog();
        }


        // Implementera den abstrakta indexeraren från ObservableObject
        public override string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case nameof(UserName):
                        if (string.IsNullOrWhiteSpace(UserName))
                            result = "Användarnamnet får inte vara tomt.";
                        break;
                        // Lägg till fler valideringar 
                }
                return result;
            }
        }

        public override string Error => null;
    }
}
