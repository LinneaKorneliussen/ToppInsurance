using System.Windows.Input;
using System.Windows;
using TopInsuranceBL;
using TopInsuranceWPF.Commands;



namespace TopInsuranceWPF.ViewModels
{
    public class RegisterEmployerVM : ObservableObject
    {
        private EmployerController employerController;

        public void RegisterBusinessCustomerVM()
        {
            employerController = new EmployerController();
            //AddBusinessCustomerCommand = new RelayCommand(AddBusinessCustomer);

            //List<BusinessCustomer> customers = businessController.GetAllBusinessCustomers();
            //BCcustomers = new ObservableCollection<BusinessCustomer>(customers);

            
        }

        #region Properties
        private string _newName;
        public string NewName
        {
            get { return _newName; }
            set
            {
                if (_newName != value)
                {
                    _newName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _newPhoneNumber;
        public string NewPhoneNumber
        {
            get { return _newPhoneNumber; }
            set
            {
                if (_newPhoneNumber != value)
                {
                    _newPhoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _newEmailAddress;
        public string NewEmailAddress
        {
            get { return _newEmailAddress; }
            set
            {
                if (_newEmailAddress != value)
                {
                    _newEmailAddress = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _newAddress;
        public string NewAddress
        {
            get { return _newAddress; }
            set
            {
                if (_newAddress != value)
                {
                    _newAddress = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _newZipcode;
        public string NewZipcode
        {
            get { return _newZipcode; }
            set
            {
                if (_newZipcode != value)
                {
                    _newZipcode = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _newCity;
        public string NewCity
        {
            get { return _newCity; }
            set
            {
                if (_newCity != value)
                {
                    _newCity = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _newEmployeerole;
        public string NewEmployeeRole
        {
            get { return _newEmployeerole; }
            set
            {
                if (_newEmployeerole != value)
                {
                    _newEmployeerole = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                if (_newPassword != value)
                {
                    _newPassword = value;
                    OnPropertyChanged();
                }
            }
        }


        #endregion

        #region Validation
        private string ValidateField(string columnName)
        {
            string errorMessage = null;

            switch (columnName)
            {
                case nameof(NewName):
                    if (string.IsNullOrWhiteSpace(NewName))
                    {
                        errorMessage = "Namn är obligatoriskt.";
                    }
                    break;
                case nameof(NewPhoneNumber):
                    if (string.IsNullOrWhiteSpace(NewPhoneNumber))
                    {
                        errorMessage = "Telefonnummer är obligatoriskt.";
                    }
                    break;
                case nameof(NewEmailAddress):
                    if (string.IsNullOrWhiteSpace(NewEmailAddress))
                    {
                        errorMessage = "E-mail är obligatoriskt.";
                    }
                    break;
                case nameof(NewAddress):
                    if (string.IsNullOrWhiteSpace(NewAddress))
                    {
                        errorMessage = "Adress är obligatoriskt.";
                    }
                    break;
                case nameof(NewZipcode):
                    if (string.IsNullOrWhiteSpace(NewZipcode))
                    {
                        errorMessage = "Postnummer är obligatoriskt.";
                    }
                    break;
                case nameof(NewCity):
                    if (string.IsNullOrWhiteSpace(NewCity))
                    {
                        errorMessage = "Stad är obligatoriskt.";
                    }
                    break;
                case nameof(NewEmployeeRole):
                    if (string.IsNullOrWhiteSpace(NewEmployeeRole))
                    {
                        errorMessage = "Roll är obligatoriskt.";
                    }
                    break;
                case nameof(NewPassword):
                    if (string.IsNullOrWhiteSpace(NewPassword))
                    {
                        errorMessage = "Roll är obligatoriskt.";
                    }
                    break;

                default:
                    errorMessage = "Ogiltigt kolumnnamn.";
                    break;
            }

            return errorMessage;
        }


        public override string this[string columnName]
        {
            get => ValidateField(columnName);
        }

        #endregion

    }


}