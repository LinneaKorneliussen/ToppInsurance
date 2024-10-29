using System.Windows.Input;
using System.Windows;
using TopInsuranceBL;
using TopInsuranceWPF.Commands;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using TopInsuranceEntities;

namespace TopInsuranceWPF.ViewModels
{
    public class RegisterPrivateCustomerVM : ObservableObject, IDataErrorInfo
    {
        private PrivateController privateController;

        public RegisterPrivateCustomerVM()
        {
            privateController = new PrivateController();
            AddPrivateCustomerCommand = new RelayCommand(AddPrivateCustomer);
            ClearFieldsCommand = new RelayCommand(ClearFields);
            RefreshPrivateCommand = new RelayCommand(RefreshPrivateCustomer);
            List<PrivateCustomer> customers = privateController.GetAllPrivateCustomers();
            PCustomers = new ObservableCollection<PrivateCustomer>(customers);
        }

        #region Properties

        private string _newFirstName;
        public string NewFirstName
        {
            get { return _newFirstName; }
            set
            {
                if (_newFirstName != value)
                {
                    _newFirstName = value;
                    OnPropertyChanged(nameof(NewFirstName));
                }
            }
        }

        private string _newLastName;
        public string NewLastName
        {
            get { return _newLastName; }
            set
            {
                if (_newLastName != value)
                {
                    _newLastName = value;
                    OnPropertyChanged(nameof(NewLastName));
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
                    OnPropertyChanged(nameof(NewPhoneNumber));
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
                    OnPropertyChanged(nameof(NewEmailAddress));
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
                    OnPropertyChanged(nameof(NewAddress));
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
                    OnPropertyChanged(nameof(NewZipcode));
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
                    OnPropertyChanged(nameof(NewCity));
                }
            }
        }

        private string _newSSN;
        public string NewSSN
        {
            get { return _newSSN; }
            set
            {
                if (_newSSN != value)
                {
                    _newSSN = value;
                    OnPropertyChanged(nameof(NewSSN));
                }
            }
        }

        private string _newWorkPhoneNumber;
        public string NewWorkPhoneNumber
        {
            get { return _newWorkPhoneNumber; }
            set
            {
                if (_newWorkPhoneNumber != value)
                {
                    _newWorkPhoneNumber = value;
                    OnPropertyChanged(nameof(NewWorkPhoneNumber));
                }
            }
        }

        #endregion

        #region Collection for business customers

        private ObservableCollection<PrivateCustomer> _PCustomers;
        public ObservableCollection<PrivateCustomer> PCustomers
        {
            get { return _PCustomers; }
            set
            {
                _PCustomers = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands
        public ICommand AddPrivateCustomerCommand { get; }
        public ICommand ClearFieldsCommand { get; }
        public ICommand RefreshPrivateCommand { get; }
        #endregion

        #region Add private customer Method
        private void AddPrivateCustomer()
        {
            string error = this.Error;
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
                return;
            }
            if (!ValidateZipcode(out int parsedZipcode))
            {
                return;
            }
            if(!IsValidPhoneNumber(NewPhoneNumber) || !IsValidPhoneNumber(NewWorkPhoneNumber))
            {
                MessageBox.Show("Felformat på telefonnummer!");
                return;
            }
            if (!privateController.SSNUnique(NewSSN))
            {
                MessageBox.Show("Personen finns redan registrerad");
                return;
            }

            
            privateController.CreateNewPrivateCustomer(NewFirstName, NewLastName, NewPhoneNumber, NewEmailAddress, NewAddress, parsedZipcode, NewCity, NewSSN, NewWorkPhoneNumber);

            
            MessageBox.Show($"Kunden har registrerats framgångsrikt!\n\n" +
                            $"Förnamn: {NewFirstName}\n" +
                            $"Efternamn: {NewLastName}\n" +
                            $"E-post: {NewEmailAddress}\n" +
                            $"Telefon: {NewPhoneNumber}\n" +
                            $"Adress: {NewAddress}\n" +
                            $"Postnummer: {NewZipcode}\n" +
                            $"Stad: {NewCity}\n" +
                            $"Personnummer: {NewSSN}");

            PCustomers.Add(new PrivateCustomer
            {
                FirstName = NewFirstName,
                LastName = NewLastName,
                Phonenumber = NewPhoneNumber,
                Emailaddress = NewEmailAddress,
                Address = NewAddress,
                Zipcode = parsedZipcode,
                City = NewCity,
                SSN = NewSSN,
                WorkPhonenumber = NewWorkPhoneNumber
               
            });
            ClearFields();
        }
        #endregion

        #region Refresh Private Customers Method 
        private void RefreshPrivateCustomer()
        {
            List<PrivateCustomer> privateCustomers = privateController.GetAllPrivateCustomers();
            PCustomers = new ObservableCollection<PrivateCustomer>(privateCustomers);
        }
        #endregion

        #region Validation IDataErrorInfo
        public string Error
        {
            get
            {
                string[] properties = { nameof(NewFirstName), nameof(NewLastName), nameof(NewPhoneNumber), nameof(NewEmailAddress), nameof(NewAddress), nameof(NewZipcode), nameof(NewCity), nameof(NewSSN), nameof(NewWorkPhoneNumber) };
                foreach (var property in properties)
                {
                    string error = this[property];
                    if (!string.IsNullOrEmpty(error))
                    {
                        return error;
                    }
                }
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                return ValidateField(columnName);
            }
        }

        private string ValidateField(string columnName)
        {
            string errorMessage = null;

            switch (columnName)
            {
                case nameof(NewFirstName):
                    if (string.IsNullOrWhiteSpace(NewFirstName))
                    {
                        errorMessage = "Förnamn är obligatoriskt.";
                    }
                    break;
                case nameof(NewLastName):
                    if (string.IsNullOrWhiteSpace(NewLastName))
                    {
                        errorMessage = "Efternamn är obligatoriskt.";
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
                case nameof(NewSSN):
                    if (string.IsNullOrWhiteSpace(NewSSN))
                    {
                        errorMessage = "Personnummer är obligatoriskt.";
                    }
                    break;
                case nameof(NewWorkPhoneNumber):
                    if (string.IsNullOrWhiteSpace(NewWorkPhoneNumber))
                    {
                        errorMessage = "Arbetstelefon är obligatoriskt.";
                    }
                    break;
                default:
                    errorMessage = "Ogiltigt kolumnnamn.";
                    break;
            }

            return errorMessage;
        }

        private bool ValidateZipcode(out int parsedZipcode)
        {
            parsedZipcode = 0;

            string zipcodePattern = @"^\d{3}\s?\d{2}$";
            if (!Regex.IsMatch(NewZipcode, zipcodePattern))
            {
                MessageBox.Show("Postnumret måste vara i formatet 'xxxxx' eller 'xxx xx'.");
                return false;
            }

            parsedZipcode = int.Parse(NewZipcode.Replace(" ", ""));

            return true;

        }

        public bool IsValidPersonalNumber(string personalNumber)
        {
            return Regex.IsMatch(personalNumber, @"^\d{8}-\d{4}$");
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\d{3}-\d{7}$"; 
            return Regex.IsMatch(phoneNumber, pattern);
        }

        #endregion

        #region Clear Field Method
        private void ClearFields()
        {
            NewFirstName = string.Empty;
            NewLastName = string.Empty;
            NewPhoneNumber = string.Empty;
            NewEmailAddress = string.Empty;
            NewAddress = string.Empty;
            NewZipcode = string.Empty;
            NewCity = string.Empty;
            NewSSN = string.Empty;
            NewWorkPhoneNumber = string.Empty;
        }
        #endregion
    }

}
