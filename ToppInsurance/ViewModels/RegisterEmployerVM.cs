using TopInsuranceBL;
using TopInsuranceWPF.Commands;
using System.ComponentModel;
using System.Collections.ObjectModel;
using TopInsuranceEntities;
using System.Windows.Input;
using System.Windows;



namespace TopInsuranceWPF.ViewModels
{
    public class RegisterEmployerVM : ObservableObject, IDataErrorInfo
    {
        private EmployerController employerController;

        public RegisterEmployerVM()
        {
            employerController = new EmployerController();

            AddEmployerCommand = new RelayCommand(AddEmployer);
            Clearfieldscommand = new RelayCommand(ClearFields);

            List<Employee> employees = employerController.GetAllEmployers();
            Employers = new ObservableCollection<Employee>(employees);
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

        #region Observable employers
        private ObservableCollection<Employee> _employers;
        public ObservableCollection<Employee> Employers
        {
            get { return _employers; }
            set
            {
                _employers = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Add empoloyee command and Method
        public ICommand AddEmployerCommand { get; }

        private void AddEmployer()
        {

            string errorMessage = ValidateField("NewName");
            errorMessage ??= ValidateField("NewPhoneNumber");
            errorMessage ??= ValidateField("NewEmailAddress");
            errorMessage ??= ValidateField("NewAddress");
            errorMessage ??= ValidateField("NewZipcode");
            errorMessage ??= ValidateField("NewCity");
            errorMessage ??= ValidateField("NewPassword");


            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage);
                return;
            }
            if (!ValidateNumericFields(out int zipcode))
            {
                return;
            }

            EmployeeRole defaultRole = EmployeeRole.Säljare;


            employerController.AddEmployer(NewName, NewPhoneNumber, NewEmailAddress, NewAddress, zipcode, NewCity, defaultRole, NewPassword);

            MessageBox.Show($"Säljaren har registrerats korrekt!\n\n" +
                             $"Namn: {NewName}\n" +
                             $"Telefonnummer: {NewPhoneNumber}\n" +
                             $"E-post: {NewEmailAddress}\n" +
                             $"Adress: {NewAddress}\n" +
                             $"Postnummer: {NewZipcode}\n" +
                             $"Stad: {NewCity}\n");


            Employers.Add(new Employee
            {
                Name = NewName,
                Phonenumber = NewPhoneNumber,
                Emailaddress = NewEmailAddress,
                Address = NewAddress,
                Zipcode = zipcode,
                City = NewCity,
            });

            ClearFields();


        }
        #endregion

        #region Clear fields Command and Method
        public ICommand Clearfieldscommand { get; }
        private void ClearFields()
        {
            NewName = string.Empty;
            NewPhoneNumber = string.Empty;
            NewEmailAddress = string.Empty;
            NewAddress = string.Empty;
            NewZipcode = string.Empty;
            NewCity = string.Empty;
            NewPassword = string.Empty;


        }
        #endregion

        #region Validation IDataErrorInfo 
        public string Error => null;

        public string this[string columnName]
        {
            get => ValidateField(columnName);
        }

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


        private bool ValidateNumericFields(out int zipcode)
        {
            zipcode = 0;


            if (!int.TryParse(NewZipcode, out zipcode))
            {
                MessageBox.Show("Postnumret måste vara ett giltigt nummer.");
                return false;
            }

            return true;
        }
        #endregion

    }
}