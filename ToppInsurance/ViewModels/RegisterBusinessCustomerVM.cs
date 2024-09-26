using System.Windows.Input;
using System.Windows;
using TopInsuranceWPF.Commands;
using TopInsuranceBL;
using System.Collections.ObjectModel;
using TopInsuranceEntities;



namespace TopInsuranceWPF.ViewModels
{
   
    public class RegisterBusinessCustomerVM : ObservableObject
    {
        private BusinessController businessController;

        public RegisterBusinessCustomerVM()
        {
            businessController = new BusinessController();
            AddBusinessCustomerCommand = new RelayCommand(AddBusinessCustomer);

            List<BusinessCustomer> customers = businessController.GetAllBusinessCustomers();
            BCcustomers = new ObservableCollection<BusinessCustomer>(customers);
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

        private string _newCompanyName;
        public string NewCompanyName
        {
            get { return _newCompanyName; }
            set
            {
                if (_newCompanyName != value)
                {
                    _newCompanyName = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Commands
        public ICommand AddBusinessCustomerCommand { get; }
        #endregion

        #region Add Business Customer Methods
        private void AddBusinessCustomer()
        {
            string errorMessage = ValidateField("NewName");
            errorMessage ??= ValidateField("NewPhoneNumber");
            errorMessage ??= ValidateField("NewEmailAddress");
            errorMessage ??= ValidateField("NewAddress");
            errorMessage ??= ValidateField("NewZipcode");
            errorMessage ??= ValidateField("NewCity");
            errorMessage ??= ValidateField("NewCompanyName");
            errorMessage ??= ValidateField("NewOrganizationalnumber");
            errorMessage ??= ValidateField("NewCountryCode");

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage);
                return;
            }
            if (!ValidateNumericFields(out int orgNumber, out int zipcode, out int countrycode))
            {
                return; 
            }
            if (!businessController.IsOrganizationalnumberUnique(orgNumber))
            {
                MessageBox.Show("Organisationsnumret är inte unikt.");
                return; 
            }

            businessController.CreateNewBusinessCustomer(NewName, NewPhoneNumber, NewEmailAddress, NewAddress, zipcode, NewCity, NewCompanyName, orgNumber, countrycode);
            ShowMessage(NewName, NewPhoneNumber, NewEmailAddress, NewAddress, zipcode, NewCity, NewCompanyName, orgNumber, countrycode);

            BCcustomers.Add(new BusinessCustomer
            {
                Name = NewName,
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

        #region Show message 
        private void ShowMessage(string newName, string newPhoneNumber, string newEmailAddress, string newAddress, int zipcode,
                                 string newCity, string newCompanyName, int orgNumber, int countrycode)
        {
            MessageBox.Show($"Företagskunden har registrerats korrekt!\n\n" +
                             $"Namn: {newName}\n" +
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

        #region Clear fields
        private void ClearFields()
        {
            NewName = string.Empty;
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
                case nameof(NewCompanyName):
                    if (string.IsNullOrWhiteSpace(NewCompanyName))
                    {
                        errorMessage = "Företagsnamn är obligatoriskt.";
                    }
                    break;
                case nameof(NewOrganizationalnumber):
                    if (string.IsNullOrWhiteSpace(NewOrganizationalnumber))
                    {
                        errorMessage = "Org.nr är obligatoriskt.";
                    }
                    break;
                case nameof(NewCountryCode):
                    if (string.IsNullOrWhiteSpace(NewCountryCode))
                    {
                        errorMessage = "Landskod är obligatoriskt.";
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
        #endregion

        #region Collection for business customers

        private ObservableCollection<BusinessCustomer> _BCcustomers;
        public ObservableCollection<BusinessCustomer> BCcustomers
        {
            get { return _BCcustomers; }
            set
            {
                _BCcustomers = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }

}
