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
        private string _searchBusinessCustomer;
        public string SearchBusinessCustomer
        {
            get { return _searchBusinessCustomer; }
            set
            {
                if (_searchBusinessCustomer != value)
                {
                    _searchBusinessCustomer = value;
                    OnPropertyChanged(nameof(SearchBusinessCustomer));
                }
            }
        }

        private string _searchPrivateCustomer;
        public string SearchPrivateCustomer
        {
            get { return _searchPrivateCustomer; }
            set
            {
                if (_searchPrivateCustomer != value)
                {
                    _searchPrivateCustomer = value;
                    OnPropertyChanged(nameof(SearchPrivateCustomer));
                }
            }
        }

        public ICommand FindBCcustomersCommand { get; }
        private void FindBCcustomers()
        {
            List<BusinessCustomer> businessCustomers = businessController.GetAllBusinessCustomers();

            if (string.IsNullOrWhiteSpace(SearchBusinessCustomer))
            {
                BCcustomers = new ObservableCollection<BusinessCustomer>(businessCustomers);
            }
            else
            {
                bool isNumber = int.TryParse(SearchBusinessCustomer, out int organizationalNumber);

                var filteredBusinessCustomers = businessCustomers
                    .Where(c =>
                        (isNumber && c.Organizationalnumber == organizationalNumber) ||
                        (!isNumber && c.CompanyName.Contains(SearchBusinessCustomer, StringComparison.OrdinalIgnoreCase))) 
                    .ToList();

                BCcustomers = new ObservableCollection<BusinessCustomer>(filteredBusinessCustomers);
            }

            SearchBusinessCustomer = string.Empty;
        }


        public ICommand FindPcustomersCommand { get; }
        private void FindPcustomers()
        {
            List<PrivateCustomer> privateCustomers = privateController.GetAllPrivateCustomers();

            if (string.IsNullOrWhiteSpace(SearchPrivateCustomer))
            {
                Pcustomers = new ObservableCollection<PrivateCustomer>(privateCustomers);
            }
            else
            {
                var filteredCustomers = privateCustomers
                    .Where(c => c.FirstName.Contains(SearchPrivateCustomer, StringComparison.OrdinalIgnoreCase) ||
                                c.LastName.Contains(SearchPrivateCustomer, StringComparison.OrdinalIgnoreCase) ||
                                c.SSN.Contains(SearchPrivateCustomer, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                Pcustomers = new ObservableCollection<PrivateCustomer>(filteredCustomers);
            }
            SearchPrivateCustomer = string.Empty;
        }

        #endregion

        #region Get all business and private customers
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

        #region Properties update customer
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

        #region Code selected customers
       
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

        #region Update private customer Command and Methods
        public ICommand UpdatePcustomersCommand { get; }
        private void UpdatePrivateCustomer()
        {
            
            if (SelectedPcustomers != null)
            {
                if (!string.IsNullOrEmpty(NewFirstName))
                {
                    SelectedPcustomers.FirstName = NewFirstName;
                }
                if (!string.IsNullOrEmpty(NewLastName))
                {
                    SelectedPcustomers.LastName = NewLastName;
                }

                if (!string.IsNullOrEmpty(NewPhoneNumber))
                {
                    if (IsValidPhoneNumber(NewPhoneNumber))
                    {
                        SelectedPcustomers.Phonenumber = NewPhoneNumber;
                    }
                    else
                    {
                        MessageBox.Show("Ogiltigt telefonnummer. Vänligen ange ett telefonnummer i formatet XXX-XXXXXXX.");
                        return; 
                    }
                }

                if (!string.IsNullOrEmpty(NewEmailadress))
                {
                    SelectedPcustomers.Emailaddress = NewEmailadress;
                }

                if (!string.IsNullOrEmpty(NewAddress))
                {
                    SelectedPcustomers.Address = NewAddress;
                }

                if (!string.IsNullOrEmpty(NewZipcode))
                {
                    if (ValidateZipcode(NewZipcode))
                    {
                        SelectedPcustomers.Zipcode = int.Parse(NewZipcode);
                    }
                    else
                    {
                        MessageBox.Show("Postnumret måste vara ett giltigt nummer");
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(NewCity))
                {
                    SelectedPcustomers.City = NewCity;
                }

                if (!string.IsNullOrEmpty(NewWorkPhoneNumber))
                {
                    if (IsValidPhoneNumber(NewWorkPhoneNumber))
                    {
                        SelectedPcustomers.WorkPhonenumber = NewWorkPhoneNumber;
                    }
                    else
                    {
                        MessageBox.Show("Ogiltigt jobbtelefonnummer. Vänligen ange ett telefonnummer i formatet XXX-XXXXXXX.");
                        return;
                    }
                }


                

                // Spara alla ändringar
                privateController.UpdatePrivateCustomer(SelectedPcustomers);
                ClearFields();

                MessageBox.Show($"Kundens uppgifter har uppdaterats!\n\n" +
                $"Förnamn: {SelectedPcustomers.FirstName}\n" +
                $"Efternamn: {SelectedPcustomers.LastName}\n" +
                $"E-post: {SelectedPcustomers.Emailaddress}\n" +
                $"Telefon: {SelectedPcustomers.Phonenumber}\n" +
                $"Adress: {SelectedPcustomers.Address}\n" +
                $"Postnummer: {SelectedPcustomers.Zipcode}\n" +
                $"Stad: {SelectedPcustomers.City}\n" +
                $"Arbetsnummer: {SelectedPcustomers.WorkPhonenumber}");


                
            }
            

        }
        #endregion

        #region Update Business customer Command and Methods
        public ICommand UpdateBCcustomersCommand { get; }
        private void UpdateBusinessCustomers()
        {

            if (SelectedBCcustomers != null)
            {
                if (!string.IsNullOrEmpty(NewFirstName))
                {
                    SelectedBCcustomers.FirstName = NewFirstName;
                }
                if (!string.IsNullOrEmpty(NewLastName))
                {
                    SelectedBCcustomers.LastName = NewLastName;
                }

                if (!string.IsNullOrEmpty(NewPhoneNumber))
                {
                    if (IsValidPhoneNumber(NewPhoneNumber))
                    {
                        SelectedBCcustomers.Phonenumber = NewPhoneNumber;
                    }
                    else
                    {
                        MessageBox.Show("Ogiltigt telefonnummer. Vänligen ange ett telefonnummer i formatet XXX-XXXXXXX.");
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(NewEmailadress))
                {
                    SelectedBCcustomers.Emailaddress = NewEmailadress;
                }

                if (!string.IsNullOrEmpty(NewAddress))
                {
                    SelectedBCcustomers.Address = NewAddress;
                }

                if (!string.IsNullOrEmpty(NewZipcode))
                {
                    if (ValidateZipcode(NewZipcode))
                    {
                        SelectedBCcustomers.Zipcode = int.Parse(NewZipcode);
                    }
                    else
                    {
                        MessageBox.Show("Postnumret måste vara ett giltigt nummer");
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(NewCity))
                {
                    SelectedBCcustomers.City = NewCity;
                }
                if (!string.IsNullOrEmpty(NewCompanyName))
                {
                    SelectedBCcustomers.CompanyName = NewCompanyName;
                }

                businessController.UpdateBusinessCustomers(SelectedBCcustomers);
                ClearFields();


                MessageBox.Show($"Kundens uppgifter har uppdaterats!\n\n" +
                $"Förnamn: {SelectedBCcustomers.FirstName}\n" +
                $"Efternamn: {SelectedBCcustomers.LastName}\n" +
                $"E-post: {SelectedBCcustomers.Emailaddress}\n" +
                $"Telefon: {SelectedBCcustomers.Phonenumber}\n" +
                $"Adress: {SelectedBCcustomers.Address}\n" +
                $"Postnummer: {SelectedBCcustomers.Zipcode}\n" +
                $"Stad: {SelectedBCcustomers.City}\n" +
                $"Företagsnamn: {SelectedBCcustomers.CompanyName}\n" +
                $"Landskod: {SelectedBCcustomers.CountryCode}\n" +
                $"OrganisationNr: {SelectedBCcustomers.Organizationalnumber}");

            }



        }

        #endregion

        #region Clear Command
        public ICommand ClearCommand { get; }
        private void ClearFields()
        {
            NewFirstName = string.Empty;
            NewLastName = string.Empty;
            NewEmailadress = string.Empty;
            NewPhoneNumber = string.Empty;
            NewAddress = string.Empty;
            NewZipcode = string.Empty;
            NewCity = string.Empty;
            NewCompanyName = string.Empty;
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
                        errorMessage = "Formatet ska vara 'XXX-XXXXXXX'.";
                    }
                    break;
                case nameof(NewWorkPhoneNumber):
                    if (!string.IsNullOrWhiteSpace(NewWorkPhoneNumber) && !IsValidPhoneNumber(NewWorkPhoneNumber))
                    {
                        errorMessage = "Formatet ska vara 'XXX-XXXXXXX'.";
                    }
                    break;
            }

            return errorMessage;
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\d{3}-\d{7}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        private bool ValidateZipcode(string zipcode)
        {
            return int.TryParse(zipcode, out _);
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

