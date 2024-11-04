using TopInsuranceBL;
using TopInsuranceWPF.Commands;
using System.ComponentModel;
using System.Collections.ObjectModel;
using TopInsuranceEntities;
using System.Windows.Input;
using System.Windows;
using System.Text.RegularExpressions;


namespace TopInsuranceWPF.ViewModels
{
    // ViewModel for managing the registration of new employees in the TopInsurance application.
    // This class handles user input for employee details, validation, and the addition of new employees to the system.
    // It utilizes the EmployeeController for data operations and implements IDataErrorInfo for validation feedback.

    public class RegisterEmployeeVM : ObservableObject, IDataErrorInfo
    {
        private EmployeeController employeeController;

        public RegisterEmployeeVM()
        {
            employeeController = new EmployeeController();

            AddEmployeeCommand = new RelayCommand(AddEmployee);
            Clearfieldscommand = new RelayCommand(ClearFields);
            Refreshcommand = new RelayCommand(RefreshSalesPerson);

            List<Employee> employees = employeeController.GetAllSalesEmployees();
            Employees = new ObservableCollection<Employee>(employees);
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

        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                if (_newPassword != value)
                {
                    _newPassword = value;
                    OnPropertyChanged(nameof(NewPassword));
                }
            }
        }
        #endregion

        #region Observable employers
        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Add employee command and Method
        public ICommand AddEmployeeCommand { get; }

        private void AddEmployee()
        {

            string error = this.Error;
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
                return;
            }
            if (!IsValidPersonalNumber(NewSSN))
            {
                MessageBox.Show("Felformat på personummret, skriv i formatet (YYYYMMDD-XXXX)!");
                return;
            }
            if (!employeeController.SSNUnique(NewSSN))
            {
                MessageBox.Show("Personen finns redan registrerad");
                return;
            }
            if (!IsValidPhoneNumber(NewPhoneNumber))
            {
                MessageBox.Show("Felformat på telefonnummer!");
                return;
            }
            if (!ValidateZipcode(out int parsedZipcode))
            {
                return;
            }



            EmployeeRole defaultRole = EmployeeRole.Säljare;


            employeeController.AddEmployee(NewFirstName, NewLastName, NewSSN, NewPhoneNumber, NewEmailAddress, NewAddress, parsedZipcode, NewCity, defaultRole, NewPassword);

            MessageBox.Show($"Säljaren har registrerats korrekt!\n\n" +
                             $"Förnamn: {NewFirstName}\n" +
                             $"Efternamn: {NewLastName}\n" +
                             $"Telefonnummer: {NewPhoneNumber}\n" +
                             $"E-post: {NewEmailAddress}\n" +
                             $"Adress: {NewAddress}\n" +
                             $"Postnummer: {NewZipcode}\n" +
                             $"Stad: {NewCity}\n");


            Employees.Add(new Employee
            {
                FirstName = NewFirstName,
                LastName = NewLastName,
                Phonenumber = NewPhoneNumber,
                Emailaddress = NewEmailAddress,
                Address = NewAddress,
                Zipcode = parsedZipcode,
                City = NewCity,
            });

            ClearFields();


        }
        #endregion

        #region Refresh sales person 
        public ICommand Refreshcommand { get; }
      
        private void RefreshSalesPerson()
        {
            List<Employee> salesPersons = employeeController.GetAllSalesEmployees();
            Employees = new ObservableCollection<Employee>(salesPersons);
        }
        #endregion

        #region Clear fields Command and Method
        public ICommand Clearfieldscommand { get; }
        private void ClearFields()
        {
            NewFirstName = string.Empty;
            NewLastName = string.Empty;
            NewSSN = string.Empty;
            NewPhoneNumber = string.Empty;
            NewEmailAddress = string.Empty;
            NewAddress = string.Empty;
            NewZipcode = string.Empty;
            NewCity = string.Empty;
            NewPassword = string.Empty;


        }
        #endregion

        #region Validation IDataErrorInfo 
        public string Error
        {
            get
            {
                string[] properties = { nameof(NewFirstName), nameof(NewLastName), nameof(NewSSN), nameof(NewPhoneNumber), nameof(NewEmailAddress), nameof(NewAddress), nameof(NewZipcode), nameof(NewCity), nameof(NewPassword) };
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
                case nameof(NewSSN):
                    if (string.IsNullOrWhiteSpace(NewLastName))
                    {
                        errorMessage = "Personnummer är obligatoriskt.";
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
                case nameof(NewPassword):
                    if (string.IsNullOrWhiteSpace(NewPassword))
                    {
                        errorMessage = "Lösenord är obligatoriskt.";
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
        public bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\d{3}-\d{7}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
        public bool IsValidPersonalNumber(string personalNumber)
        {
            return Regex.IsMatch(personalNumber, @"^\d{8}-\d{4}$");
        }

        #endregion

    }
}