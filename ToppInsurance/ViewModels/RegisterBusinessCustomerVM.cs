using System;
using System.Windows.Input;
using System.Windows;
using TopInsuranceWPF.Commands;
using TopInsuranceBL;
using System.Collections.ObjectModel;
using System.Numerics;
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

        #region Properties Add business customer

        private string _newName;
        public string NewName
        {
            get { return _newName; }
            set
            {
                if (_newName != value)
                {
                    _newName = value;
                    OnPropertyChanged(NewName);
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
                    OnPropertyChanged(NewPhoneNumber);
                }
            }
        }

        private string _newEmailadress;
        public string NewEmailadress
        {
            get { return _newEmailadress; }
            set
            {
                if (_newEmailadress != value)
                {
                    _newEmailadress = value;
                    OnPropertyChanged(NewEmailadress);
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
                    OnPropertyChanged(NewAddress);
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
                    OnPropertyChanged(NewZipcode);
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
                    OnPropertyChanged(NewCity);
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
                    OnPropertyChanged(NewCompanyName);
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
                    OnPropertyChanged(NewOrganizationalnumber);
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
                    OnPropertyChanged(NewCountryCode);
                }
            }
        }


        #endregion

        #region Registrate new business customer Command and Methods 
        public ICommand AddBusinessCustomerCommand { get; }
        private void AddBusinessCustomer()
        {
            string errorMessage = ValidateField("NewName");
            errorMessage ??= ValidateField("NewPhoneNumber");
            errorMessage ??= ValidateField("NewEmailadress");
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

            int orgNumber = int.Parse(NewOrganizationalnumber);
            int zipcode = int.Parse(NewZipcode);
            int countrycode = int.Parse(NewCountryCode);

            businessController.CreateNewBusinessCustomer(NewName, NewPhoneNumber, NewEmailadress, NewAddress, zipcode, NewCity, NewCompanyName, orgNumber, countrycode);

            MessageBox.Show($"Företagskunden har registrerats korrekt!\n\n" +
                $"Här är detaljerna:\n" +
                $"-------------------------\n" +
                $"Namn: {NewName}\n" +
                $"Telefonnummer: {NewPhoneNumber}\n" +
                $"E-post: {NewEmailadress}\n" +
                $"Adress: {NewAddress}\n" +
                "Postnummer: {zipcode}\n" +
                $"Stad: {NewCity}\n" +
                $"Företagsnamn: {NewCompanyName}\n" +
                $"Organisationsnummer: {orgNumber}\n" +
                $"Landkod: {countrycode}\n" +
                $"-------------------------\n" +
                $"Tack för att du registrerade en ny företagskund!\n" +
                $"Vi ser fram emot att hjälpa er vidare.");

            ClearFields();


            //if (!IsValidPhoneNumber(NewPhoneNumber))
            //{
            //    MessageBox.Show("Invalid phonenumber format.\nPlease make sure the phonenumber follows the format (xxx-xxxxxxx)");
            //    return;
            //}

            //if (!businessController.IsOrganizationalnumberUnique(NewOrganizationalnumber))
            //{
            //    MessageBox.Show("Organizational number is not unique.");
            //    return;
            //}
        }
        #endregion

        #region Get all business customer
        private ObservableCollection<BusinessCustomer> _BCcustomers;
        public ObservableCollection<BusinessCustomer> BCcustomers
        {
            get { return _BCcustomers; }
            set
            {
                _BCcustomers = value;
                OnPropertyChanged(nameof(BCcustomers));
            }
        }
        #endregion

        #region Clear fields
        private void ClearFields()
        {
            NewName = string.Empty;
            NewPhoneNumber = string.Empty;
            NewEmailadress = string.Empty;
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
                case "NewName":
                    if (string.IsNullOrWhiteSpace(NewName))
                    {
                        errorMessage = "Namn är obligatoriskt.";
                    }
                    break;
                case "NewPhoneNumber":
                    if (string.IsNullOrWhiteSpace(NewPhoneNumber))
                    {
                        errorMessage = "Telefonnummer är obligatoriskt.";
                    }
                    break;
                case "NewEmailadress":
                    if (string.IsNullOrWhiteSpace(NewEmailadress))
                    {
                        errorMessage = "E-post är obligatoriskt.";
                    }
                    break;
                case "NewAddress":
                    if (string.IsNullOrWhiteSpace(NewAddress))
                    {
                        errorMessage = "Adress är obligatoriskt.";
                    }
                    break;
                case "NewZipcode":
                    if (string.IsNullOrWhiteSpace(NewZipcode))
                    {
                        errorMessage = "Postnummer är obligatoriskt.";
                    }
                    break;
                case "NewCity":
                    if (string.IsNullOrEmpty(NewCity))
                    {
                        errorMessage = "Stad är obligatoriskt.";
                    }
                    break;
                case "NewCompanyName":
                    if (string.IsNullOrEmpty(NewCompanyName))
                    {
                        errorMessage = "Företagsnamn är obligatoriskt.";
                    }
                    break;
                case "NewOrganizationalnumber": // Ändrat här för att matcha fältet korrekt
                    if (string.IsNullOrEmpty(NewOrganizationalnumber))
                    {
                        errorMessage = "Org.nummer är obligatoriskt.";
                    }
                    break;
                case "NewCountryCode":
                    if (string.IsNullOrEmpty(NewCountryCode))
                    {
                        errorMessage = "Landskod är obligatoriskt.";
                    }
                    break;
                default:
                    errorMessage = "Felaktigt.";
                    break;
            }

            return errorMessage;
        }


        public override string this[string columnName]
        {
            get
            {
                return ValidateField(columnName);
            }
        }
        #endregion



    }

}
