using ControlzEx.Standard;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TopInsuranceBL;
using TopInsuranceDL;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    public class EditCustomerVM : ObservableObject, IDataErrorInfo
    {
        private BusinessController businessController;
        private PrivateController privateController;

        public EditCustomerVM()
        {
            businessController = new BusinessController();
            privateController = new PrivateController();
            UpdateBCcustomersCommand = new RelayCommand(UpdateBusinessCustomers);
            UpdatePcustomersCommand = new RelayCommand(UpdatePrivateCustomer);
            List<BusinessCustomer> customers = businessController.GetAllBusinessCustomers();
            List<PrivateCustomer> privateCustomers = privateController.GetAllPrivateCustomers();
            FindPcustomersCommand = new RelayCommand(FindPcustomers);
            BCcustomers = new ObservableCollection<BusinessCustomer>(customers);
            Pcustomers = new ObservableCollection<PrivateCustomer>(privateCustomers);
            ClearCommand = new RelayCommand(ClearFields);
            FindBCcustomersCommand = new RelayCommand(FindBCcustomers);
        }

        #region Search
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                }
            }
        }

        public ICommand FindBCcustomersCommand { get; }
        private void FindBCcustomers()
        {
            List<BusinessCustomer> businessCustomers = businessController.GetAllBusinessCustomers();

            // Visar alla kunder om Search textboxen är tom 
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                BCcustomers = new ObservableCollection<BusinessCustomer>(businessCustomers);
            }
            else
            {
                // Filtrera kunderna baserat på söktexten (söker på namn, e-post och telefonnummer) 
                var filteredBusinessCustomers = businessCustomers
                    .Where(c => c.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                c.Emailaddress.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                c.Phonenumber.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                // Uppdatera ObservableCollection med filtrerade kunder
                BCcustomers = new ObservableCollection<BusinessCustomer>(filteredBusinessCustomers);
            }
        }

        public ICommand FindPcustomersCommand { get; }
        private void FindPcustomers()
        {
            List<PrivateCustomer> privateCustomers = privateController.GetAllPrivateCustomers();

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Pcustomers = new ObservableCollection<PrivateCustomer>(privateCustomers);
            }
            else
            {
                var filteredCustomers = privateCustomers
                    .Where(c => c.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                c.Emailaddress.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                c.Phonenumber.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                Pcustomers = new ObservableCollection<PrivateCustomer>(filteredCustomers);
            }
        }

        private void RefreshSelectedCustomer()
        {
            if (SelectedPcustomers != null)
            {
                var updatedCustomer = Pcustomers.FirstOrDefault(c => c.Name == SelectedPcustomers.Name);

                if (updatedCustomer != null)
                {
                    SelectedPcustomers.Name = updatedCustomer.Name;
                    SelectedPcustomers.Address = updatedCustomer.Address;
                    SelectedPcustomers.Phonenumber = updatedCustomer.Phonenumber;
                    SelectedPcustomers.Emailaddress = updatedCustomer.Emailaddress;
                    SelectedPcustomers.Zipcode = updatedCustomer.Zipcode;
                    SelectedPcustomers.City = updatedCustomer.City;
                    SelectedPcustomers.WorkPhonenumber = updatedCustomer.WorkPhonenumber;

                    OnPropertyChanged(nameof(SelectedPcustomers));
                }
            }
        }

        
        #endregion

        #region Get all business customers
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

        #region Get all private customers
        private ObservableCollection<PrivateCustomer> _Pcustomers;
        public ObservableCollection<PrivateCustomer> Pcustomers
        {
            get { return _Pcustomers; }
            set
            {
                _Pcustomers = value;
                OnPropertyChanged(nameof(Pcustomers));
            }
        }
        #endregion

        #region Properties update Business customer
        private string _newName;
        public string NewName
        {
            get { return _newName; }
            set
            {
                if (_newName != value)
                {
                    _newName = value;
                    OnPropertyChanged(nameof(NewName));
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

        private string _newEmailadress;
        public string NewEmailadress
        {
            get { return _newEmailadress; }
            set
            {
                if (_newEmailadress != value)
                {
                    _newEmailadress = value;
                    OnPropertyChanged(nameof(NewEmailadress));
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

        private string _newCompanyName;
        public string NewCompanyName
        {
            get { return _newCompanyName; }
            set
            {
                if (_newCompanyName != value)
                {
                    _newCompanyName = value;
                    OnPropertyChanged(nameof(NewCompanyName));
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
                    OnPropertyChanged(nameof(NewCountryCode));
                }
            }
        }
        #endregion

        #region Properties update private customer
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

        #region Update Business Properties
        //private string _newValue;
        //public string NewValue
        //{
        //    get { return _newValue; }
        //    set
        //    {
        //        if (_newValue != value)
        //        {
        //            _newValue = value;
        //            OnPropertyChanged(nameof(NewValue));
        //        }
        //    }
        //}

        private BusinessCustomer _selectedBCcustomers;
        public BusinessCustomer SelectedBCcustomers
        {
            get { return _selectedBCcustomers; }
            set
            {
                _selectedBCcustomers = value;
                OnPropertyChanged(nameof(SelectedBCcustomers));
            }
        }

        //private ObservableCollection<string> fieldsToUpdate;
        //public ObservableCollection<string> FieldsToUpdate
        //{
        //    get { return fieldsToUpdate; }
        //    set { fieldsToUpdate = value; OnPropertyChanged(nameof(FieldsToUpdate)); }
        //}

        //private string selectedFieldToUpdate;
        //public string SelectedFieldToUpdate
        //{
        //    get { return selectedFieldToUpdate; }
        //    set { selectedFieldToUpdate = value; OnPropertyChanged(nameof(SelectedFieldToUpdate)); }
        //}
        #endregion

        #region Update private Properties
        private PrivateCustomer _selectedPcustomers;
        public PrivateCustomer SelectedPcustomers
        {
            get { return _selectedPcustomers; }
            set
            {
                _selectedPcustomers = value;
                OnPropertyChanged(nameof(SelectedPcustomers));
            }
        }
        #endregion

        #region Update Business customer Command and Methods
        public ICommand UpdateBCcustomersCommand { get; }
        private void UpdateBusinessCustomers()
        {
            string error = this.Error;
            if (SelectedBCcustomers != null)
            {
                // Validera och uppdatera varje fält om det är angivet
                if (!string.IsNullOrEmpty(NewName))
                {
                    SelectedBCcustomers.Name = NewName;
                }
                if (!string.IsNullOrEmpty(NewPhoneNumber))
                {
                    SelectedBCcustomers.Phonenumber = NewPhoneNumber;
                }
                if (!string.IsNullOrEmpty(NewEmailadress))
                {
                    SelectedBCcustomers.Emailaddress = NewEmailadress;
                }
                if (!string.IsNullOrEmpty(NewAddress))
                {
                    SelectedBCcustomers.Address = NewAddress;
                }
                //if (!ValidateNumericFields(string input))
                //{
                //    SelectedBCcustomers.Zipcode = NewZipcode;
                //}
                if (!string.IsNullOrEmpty(NewCity))
                {
                    SelectedBCcustomers.City = NewCity;
                }
                if (!string.IsNullOrEmpty(NewCompanyName))
                {
                    SelectedBCcustomers.CompanyName = NewCompanyName;
                }

                // Spara alla ändringar
                businessController.UpdateBusinessCustomers(SelectedBCcustomers);
                ClearFields();  
            }
        }

        #endregion

        #region Update private customer Command and Methods
        public ICommand UpdatePcustomersCommand { get; }
        private void UpdatePrivateCustomer()
        {
            string error = this.Error;
            if (SelectedPcustomers != null)
            {
                // Validera och uppdatera varje fält om det är angivet
                if (!string.IsNullOrEmpty(NewName))
                {
                    SelectedPcustomers.Name = NewName;
                }
                if (!string.IsNullOrEmpty(NewPhoneNumber))
                {
                    SelectedPcustomers.Phonenumber = NewPhoneNumber;
                }
                if (!string.IsNullOrEmpty(NewEmailadress))
                {
                    SelectedPcustomers.Emailaddress = NewEmailadress;
                }
                if (!string.IsNullOrEmpty(NewAddress))
                {
                    SelectedPcustomers.Address = NewAddress;
                }
                //if (!ValidateNumericFields(string input))
                //{
                //    SelectedBCcustomers.Zipcode = NewZipcode;
                //}
                if (!string.IsNullOrEmpty(NewCity))
                {
                    SelectedPcustomers.City = NewCity;
                }
                if (!string.IsNullOrEmpty(NewWorkPhoneNumber))
                {
                    SelectedPcustomers.WorkPhonenumber = NewWorkPhoneNumber;
                }

                // Spara alla ändringar
                privateController.UpdatePrivateCustomer(SelectedPcustomers);
                ClearFields();
            }
        }
        #endregion

        #region Clear Command
        public ICommand ClearCommand { get; }
        private void ClearFields()
        {
            NewName = string.Empty;
            NewEmailadress = string.Empty;
            NewPhoneNumber = string.Empty;
            NewAddress = string.Empty;
            NewZipcode = string.Empty;
            NewCity = string.Empty;
            NewCompanyName = string.Empty;
            NewCountryCode = string.Empty;
            NewWorkPhoneNumber = string.Empty;
        }
        #endregion

        #region Validation IDataErrorInfo
      
        public string Error => null;

        public string ValidateField(string columnName)
        {
            string errorMessage = null;

            switch (columnName)
            {
                case nameof(NewPhoneNumber):
                    if (!string.IsNullOrWhiteSpace(NewPhoneNumber) && !IsValidPhoneNumber(NewPhoneNumber))
                    {
                        errorMessage = "Telefonnummer är ogiltigt. Formatet ska vara 'XXX-XXXXXXX'.";
                    }
                    break;
                case nameof(NewZipcode):
                    if (!string.IsNullOrWhiteSpace(NewZipcode) && !ValidateNumericFields(NewZipcode))
                    {
                        errorMessage = "Postnumret måste vara ett giltigt nummer.";
                    }
                    break;
                default:
                    errorMessage = "Ogiltigt kolumnnamn.";
                    break;
            }

            return errorMessage;
        }
        public bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\d{3}-\d{7}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        private bool ValidateNumericFields(string input)
        {
            return int.TryParse(input, out _);
        }

        public string this[string columnName]
        {
            get
            {
                return ValidateField(columnName);
            }
        }

        #endregion

    }


}

