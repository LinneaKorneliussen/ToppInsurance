﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TopInsuranceBL;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    // The EditCustomerVM class is a ViewModel for managing the editing of customers in an insurance system. 
    // It utilizes ICommand to handle commands for searching and updating both business and private customers. 
    // The class uses ObservableCollection to store and bind customer data to the UI. 
    // It implements IDataErrorInfo to support validation of input fields. 
    // The class includes properties to store search and edit information, as well as methods for retrieving and updating customer data. 
    // Validation for phone numbers and postal codes is performed before updating customer information.

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
            FindBCcustomersCommand = new RelayCommand(FindBCcustomers);
            BCcustomers = new ObservableCollection<BusinessCustomer>(customers);
            Pcustomers = new ObservableCollection<PrivateCustomer>(privateCustomers);
            ClearCommand = new RelayCommand(ClearFields);
            RefreshPrivateCommand = new RelayCommand(RefreshPrivateCustomer);
            RefreshBusinessCommand = new RelayCommand(RefreshBusinessCustomer);
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
        #endregion

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

        #region Properties for selected customer

        private PrivateCustomer _selectedPcustomers;
        public PrivateCustomer SelectedPcustomers
        {
            get => _selectedPcustomers;
            set
            {
                if (_selectedPcustomers != value)
                {
                    _selectedPcustomers = value;
                    OnPropertyChanged(nameof(SelectedPcustomers));

                    if (_selectedPcustomers != null)
                    {
                        NewFirstName = _selectedPcustomers.FirstName;
                        NewLastName = _selectedPcustomers.LastName;
                        NewPhoneNumber = _selectedPcustomers.Phonenumber;
                        NewEmailadress = _selectedPcustomers.Emailaddress;
                        NewAddress = _selectedPcustomers.Address;
                        NewZipcode = _selectedPcustomers.Zipcode.ToString();
                        NewCity = _selectedPcustomers.City;
                        NewWorkPhoneNumber = _selectedPcustomers.WorkPhonenumber;
                    }
                }
            }
        }

        private BusinessCustomer _selectedBCcustomers;
        public BusinessCustomer SelectedBCcustomers
        {
            get => _selectedBCcustomers;
            set
            {
                if (_selectedBCcustomers != value)
                {
                    _selectedBCcustomers = value;
                    OnPropertyChanged(nameof(SelectedBCcustomers));

                    if (_selectedBCcustomers != null)
                    {
                        NewFirstName = _selectedBCcustomers.FirstName;
                        NewLastName = _selectedBCcustomers.LastName;
                        NewPhoneNumber = _selectedBCcustomers.Phonenumber;
                        NewEmailadress = _selectedBCcustomers.Emailaddress;
                        NewAddress = _selectedBCcustomers.Address;
                        NewZipcode = _selectedBCcustomers.Zipcode.ToString();
                        NewCity = _selectedBCcustomers.City;
                        NewCompanyName = _selectedBCcustomers.CompanyName;
                    }
                }
            }
        }
        #endregion

        #region Observable Collection
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

        #region Commands
        public ICommand FindBCcustomersCommand { get; }
        public ICommand FindPcustomersCommand { get; }
        public ICommand UpdatePcustomersCommand { get; }
        public ICommand UpdateBCcustomersCommand { get; }
        public ICommand ClearCommand { get; }

        #endregion

        #region Find customer Methods
        private void FindBCcustomers()
        {
            if (string.IsNullOrWhiteSpace(SearchBusinessCustomer))
            {
                MessageBox.Show("Sökning misslyckades. Ange söktext.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var filteredBusinessCustomers = businessController.SearchBusinessCustomers(SearchBusinessCustomer);

            if (filteredBusinessCustomers == null || !filteredBusinessCustomers.Any())
            {
                MessageBox.Show("Inga resultat hittades för den angivna söktexten.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                BCcustomers = new ObservableCollection<BusinessCustomer>(filteredBusinessCustomers);
            }
        }

        private void FindPcustomers()
        {
            if (string.IsNullOrWhiteSpace(SearchPrivateCustomer))
            {
                MessageBox.Show("Sökning misslyckades. Ange söktext.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var filteredPrivateCustomers = privateController.SearchPrivateCustomers(SearchPrivateCustomer);

            if (filteredPrivateCustomers == null || !filteredPrivateCustomers.Any())
            {
                MessageBox.Show("Inga resultat hittades för den angivna söktexten.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Pcustomers = new ObservableCollection<PrivateCustomer>(filteredPrivateCustomers);
            }
        }
        #endregion

        #region Update private customer method
        private void UpdatePrivateCustomer()
        {
            if (SelectedPcustomers == null)
            {
                MessageBox.Show("Du måste välja en privatkund för att kunna uppdatera.");
                return;
            }

            if (string.IsNullOrWhiteSpace(NewFirstName) && string.IsNullOrWhiteSpace(NewLastName) &&
                string.IsNullOrWhiteSpace(NewPhoneNumber) && string.IsNullOrWhiteSpace(NewEmailadress) &&
                string.IsNullOrWhiteSpace(NewAddress) && string.IsNullOrWhiteSpace(NewZipcode) &&
                string.IsNullOrWhiteSpace(NewCity) && string.IsNullOrWhiteSpace(NewWorkPhoneNumber))
            {
                MessageBox.Show("Inga uppdateringar har gjorts.");
                return;
            }
            if (!string.IsNullOrWhiteSpace(NewFirstName))
            {
                SelectedPcustomers.FirstName = NewFirstName;
            }

            if (!string.IsNullOrWhiteSpace(NewLastName))
            {
                SelectedPcustomers.LastName = NewLastName;
            }

            if (!string.IsNullOrWhiteSpace(NewPhoneNumber))
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

            if (!string.IsNullOrWhiteSpace(NewEmailadress))
            {
                SelectedPcustomers.Emailaddress = NewEmailadress;
            }

            if (!string.IsNullOrWhiteSpace(NewAddress))
            {
                SelectedPcustomers.Address = NewAddress;
            }

            if (!string.IsNullOrWhiteSpace(NewZipcode))
            {
                string pattern = @"^\d{3}\s\d{2}$";
                if (Regex.IsMatch(NewZipcode, pattern) || ValidateZipcode(NewZipcode))
                {
                    SelectedPcustomers.Zipcode = int.Parse(NewZipcode.Replace(" ", ""));
                }
                else
                {
                    MessageBox.Show("Postnumret måste vara i formatet 'xxx xx'.");
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(NewCity))
            {
                SelectedPcustomers.City = NewCity;
            }

            if (!string.IsNullOrWhiteSpace(NewWorkPhoneNumber))
            {
                if (IsValidWorkPhoneNumber(NewWorkPhoneNumber))
                {
                    SelectedPcustomers.WorkPhonenumber = NewWorkPhoneNumber;
                }
                else
                {
                    MessageBox.Show("Ogiltigt jobbtelefonnummer. Vänligen ange ett telefonnummer i formatet XX-XXXXX eller XXX-XXXXX.");
                    return;
                }
            }

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
                $"Jobbnummer: {SelectedPcustomers.WorkPhonenumber}");
        }

        #endregion

        #region Update Business customer method
        private void UpdateBusinessCustomers()
        {
            if (SelectedBCcustomers == null)
            {
                MessageBox.Show("Du måste välja en företagskund för att kunna uppdatera.");
                return;
            }

            if (string.IsNullOrWhiteSpace(NewFirstName) && string.IsNullOrWhiteSpace(NewLastName) &&
                string.IsNullOrWhiteSpace(NewPhoneNumber) && string.IsNullOrWhiteSpace(NewEmailadress) &&
                string.IsNullOrWhiteSpace(NewAddress) && string.IsNullOrWhiteSpace(NewZipcode) &&
                string.IsNullOrWhiteSpace(NewCity) && string.IsNullOrWhiteSpace(NewCompanyName))
            {
                MessageBox.Show("Inga uppdateringar har gjorts.");
                return;
            }

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

                if (!string.IsNullOrWhiteSpace(NewZipcode))
                {
                    string pattern = @"^\d{3}\s\d{2}$";
                    if (Regex.IsMatch(NewZipcode, pattern) || ValidateZipcode(NewZipcode))
                    {
                        SelectedBCcustomers.Zipcode = int.Parse(NewZipcode.Replace(" ", ""));
                    }
                    else
                    {
                        MessageBox.Show("Postnumret måste vara i formatet 'xxx xx'.");
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

        #region Clear fields method
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

        #region Refresh 

        private ICommand refreshPrivatecommand;
        public ICommand RefreshPrivateCommand
        {
            get { return refreshPrivatecommand; }
            set { refreshPrivatecommand = value; }
        }
        private void RefreshPrivateCustomer()
        {
            List<PrivateCustomer> privateCustomers = privateController.GetAllPrivateCustomers();
            Pcustomers = new ObservableCollection<PrivateCustomer>(privateCustomers);
        }


        private ICommand refreshBusinesscommand;
        public ICommand RefreshBusinessCommand
        {
            get { return refreshBusinesscommand; }
            set { refreshBusinesscommand = value; }
        }

        private void RefreshBusinessCustomer()
        {
            List<BusinessCustomer> businessCustomers = businessController.GetAllBusinessCustomers();
            BCcustomers = new ObservableCollection<BusinessCustomer>(businessCustomers);
        }
        #endregion

        #region Validation
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
            }

            return errorMessage;
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\d{3}-\d{6,7}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        private bool ValidateZipcode(string zipcode)
        {
            return int.TryParse(zipcode, out _);
        }

        public bool IsValidWorkPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\d{2,3}-\d{5,}$";
            return Regex.IsMatch(phoneNumber, pattern);
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

