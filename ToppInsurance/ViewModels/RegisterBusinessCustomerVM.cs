using System.Windows.Input;
using System.Windows;
using TopInsuranceWPF.Commands;
using TopInsuranceBL;
using System.Collections.ObjectModel;
using TopInsuranceEntities;
using System.ComponentModel;
using System.Text.RegularExpressions;



namespace TopInsuranceWPF.ViewModels
{
    public class RegisterBusinessCustomerVM : ObservableObject, IDataErrorInfo
    {
        private BusinessController businessController;

        public RegisterBusinessCustomerVM()
        {
            businessController = new BusinessController();
            AddBusinessCustomerCommand = new RelayCommand(AddBusinessCustomer);
            ClearFieldsCommand = new RelayCommand(ClearFields);
            List<BusinessCustomer> customers = businessController.GetAllBusinessCustomers();
            BCcustomers = new ObservableCollection<BusinessCustomer>(customers);
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
                    OnPropertyChanged(nameof(_newFirstName));
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
                    OnPropertyChanged(nameof(_newLastName));
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
                    OnPropertyChanged(nameof(_newPhoneNumber));
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
                    OnPropertyChanged(nameof(_newEmailAddress));
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
                    OnPropertyChanged(nameof(_newAddress));
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
                    OnPropertyChanged(nameof(_newZipcode);
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
                    OnPropertyChanged(nameof(_newCity));
                }
            }
        }

        private string _newCompanyName;
        public string NewCompanyName
        {
            get { return _newCompanyName; }
            set
            {
                if (_newCompanyName != value)
                {
                    _newCompanyName = value;
                    OnPropertyChanged(nameof(_newCompanyName));
                }
            }
        }

        private string _newOrganizationalnumber;
        public string NewOrganizationalnumber
        {
            get { return _newOrganizationalnumber; }
            set
            {
                if (_newOrganizationalnumber != value)
                {
                    _newOrganizationalnumber = value;
                    OnPropertyChanged(nameof(_newOrganizationalnumber));
                }
            }
        }

        private string _newCountryCode;
        public string NewCountryCode
        {
            get { return _newCountryCode; }
            set
            {
                if (_newCountryCode != value)
                {
                    _newCountryCode = value;
                    OnPropertyChanged(nameof(_newCountryCode));
                }
            }
        }

        #endregion

        #region Collection for business customers

        private ObservableCollection<BusinessCustomer> _BCcustomers;
        public ObservableCollection<BusinessCustomer> BCcustomers
        {
            get { return _BCcustomers; }
            set
            {
                _BCcustomers = value;
                OnPropertyChanged(nameof(_BCcustomers));
            }
        }

        #endregion

        #region Commands
        public ICommand AddBusinessCustomerCommand { get; }
        public ICommand ClearFieldsCommand { get; }
        #endregion

        #region Add Business Customer Methods
        private void AddBusinessCustomer()
        {
            if (!ValidateAllFields())
            {
                return;
            }

            if (!ValidateNumericFields(out int orgNumber, out int zipcode, out int countrycode))
            {
                return;
            }
            if (!IsValidPhoneNumber(NewPhoneNumber))
            {
                MessageBox.Show("Telefonnummer är inte i rätt format!");
                return;
            }
            if (!businessController.IsOrganizationalnumberUnique(orgNumber))
            {
                MessageBox.Show("Organisationsnumret är inte unikt.");
                return;
            }

            businessController.CreateNewBusinessCustomer(NewFirstName, NewLastName, NewPhoneNumber, NewEmailAddress, NewAddress, zipcode, NewCity, NewCompanyName, orgNumber, countrycode);
            ShowMessage(NewFirstName, NewPhoneNumber, NewEmailAddress, NewAddress, zipcode, NewCity, NewCompanyName, orgNumber, countrycode);

            BCcustomers.Add(new BusinessCustomer
            {
                FirstName = NewFirstName,
                LastName = NewLastName,
                Phonenumber = NewPhoneNumber,
                Emailaddress = NewEmailAddress,
                Address = NewAddress,
                Zipcode = zipcode,
                City = NewCity,
                CompanyName = NewCompanyName,
                Organizationalnumber = orgNumber,
                CountryCode = countrycode
            });
            ClearFields();
        }
        #endregion

        #region Show message Method
        private void ShowMessage(string newFirstName, string newPhoneNumber, string newEmailAddress, string newAddress, int zipcode,
                                 string newCity, string newCompanyName, int orgNumber, int countrycode)
        {
            MessageBox.Show($"Företagskunden har registrerats korrekt!\n\n" +
                            $"Namn: {newFirstName}\n" +
                            $"Telefonnummer: {newPhoneNumber}\n" +
                            $"E-post: {newEmailAddress}\n" +
                            $"Adress: {newAddress}\n" +
                            $"Postnummer: {zipcode}\n" +
                            $"Stad: {newCity}\n" +
                            $"Företagsnamn: {newCompanyName}\n" +
                            $"Organisationsnummer: {orgNumber}\n" +
                            $"Landkod: {countrycode}\n" +
                            $"Tack för att du registrerade en ny företagskund!");
        }
        #endregion

        #region Validation IDataErrorInfo
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(NewFirstName):
                        if (string.IsNullOrWhiteSpace(NewFirstName))
                        {
                            return "Förnamn är obligatoriskt.";
                        }
                        break;
                    case nameof(NewLastName):
                        if (string.IsNullOrWhiteSpace(NewLastName))
                        {
                            return "Efternamn är obligatoriskt.";
                        }
                        break;
                    case nameof(NewPhoneNumber):
                        if (string.IsNullOrWhiteSpace(NewPhoneNumber))
                        {
                            return "Telefonnummer är obligatoriskt.";
                        }
                        break;
                    case nameof(NewEmailAddress):
                        if (string.IsNullOrWhiteSpace(NewEmailAddress))
                        {
                            return "E-mail är obligatoriskt.";
                        }
                        break;
                    case nameof(NewAddress):
                        if (string.IsNullOrWhiteSpace(NewAddress))
                        {
                            return "Adress är obligatoriskt.";
                        }
                        break;
                    case nameof(NewZipcode):
                        if (string.IsNullOrWhiteSpace(NewZipcode))
                        {
                            return "Postnummer är obligatoriskt.";
                        }
                        break;
                    case nameof(NewCity):
                        if (string.IsNullOrWhiteSpace(NewCity))
                        {
                            return "Stad är obligatoriskt.";
                        }
                        break;
                    case nameof(NewCompanyName):
                        if (string.IsNullOrWhiteSpace(NewCompanyName))
                        {
                            return "Företagsnamn är obligatoriskt.";
                        }
                        break;
                    case nameof(NewOrganizationalnumber):
                        if (string.IsNullOrWhiteSpace(NewOrganizationalnumber))
                        {
                            return "Org.nr är obligatoriskt.";
                        }
                        break;
                    case nameof(NewCountryCode):
                        if (string.IsNullOrWhiteSpace(NewCountryCode))
                        {
                            return "Landskod är obligatoriskt.";
                        }
                        break;
                }
                return null;
            }
        }

        public string Error => null;

        #endregion

        #region Validation Method 
        private bool ValidateAllFields()
        {
            string[] fields = { nameof(NewFirstName), nameof(NewLastName), nameof(NewPhoneNumber), nameof(NewEmailAddress), nameof(NewAddress), nameof(NewZipcode), nameof(NewCity), nameof(NewCompanyName), nameof(NewOrganizationalnumber), nameof(NewCountryCode) };
            foreach (var field in fields)
            {
                if (!string.IsNullOrEmpty(this[field]))
                {
                    MessageBox.Show(this[field]);
                    return false;
                }
            }
            return true;
        }

        private bool ValidateNumericFields(out int orgNumber, out int zipcode, out int countrycode)
        {
            orgNumber = 0;
            zipcode = 0;
            countrycode = 0;

            if (!int.TryParse(NewZipcode, out zipcode))
            {
                MessageBox.Show("Postnumret måste vara ett giltigt nummer.");
                return false;
            }
            if (!int.TryParse(NewOrganizationalnumber, out orgNumber))
            {
                MessageBox.Show("Org.nr måste vara ett giltigt nummer.");
                return false;
            }
            if (!int.TryParse(NewCountryCode, out countrycode))
            {
                MessageBox.Show("Landskoden måste vara ett giltigt nummer.");
                return false;
            }

            return true;
        }
        public bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\d{3}-\d{7}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
        #endregion

        #region Clear fields Method
        private void ClearFields()
        {
            NewFirstName = string.Empty;
            NewLastName = string.Empty;
            NewPhoneNumber = string.Empty;
            NewEmailAddress = string.Empty;
            NewAddress = string.Empty;
            NewZipcode = string.Empty;
            NewCity = string.Empty;
            NewCompanyName = string.Empty;
            NewOrganizationalnumber = string.Empty;
            NewCountryCode = string.Empty;
        }
        #endregion

    }

}
