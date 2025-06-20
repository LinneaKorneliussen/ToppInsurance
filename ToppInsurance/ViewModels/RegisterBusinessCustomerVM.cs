﻿using System.Windows.Input;
using System.Windows;
using TopInsuranceWPF.Commands;
using TopInsuranceBL;
using System.Collections.ObjectModel;
using TopInsuranceEntities;
using System.ComponentModel;
using System.Text.RegularExpressions;


namespace TopInsuranceWPF.ViewModels
{
    /// <summary>
    // This class, RegisterBusinessCustomerVM, serves as the ViewModel for registering business customers in a WPF application.
    // It implements IDataErrorInfo for input validation and utilizes ICommand to handle user actions such as adding,
    // clearing fields, and refreshing the customer list. The class maintains properties for various business customer 
    // details, including name, contact information, and address. It interacts with the BusinessController to manage 
    // customer data and provides methods for validating input fields, displaying messages upon successful registration, 
    // and maintaining a collection of business customers.
    /// </summary>
    
    public class RegisterBusinessCustomerVM : ObservableObject, IDataErrorInfo
    {
        private BusinessController businessController;

        public RegisterBusinessCustomerVM()
        {
            businessController = new BusinessController();
            AddBusinessCustomerCommand = new RelayCommand(AddBusinessCustomer);
            ClearFieldsCommand = new RelayCommand(ClearFields);
            RefreshBusinessCommand = new RelayCommand(RefreshBusinessCustomer);
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

        private string _newOrganizationalnumber;
        public string NewOrganizationalnumber
        {
            get { return _newOrganizationalnumber; }
            set
            {
                if (_newOrganizationalnumber != value)
                {
                    _newOrganizationalnumber = value;
                    OnPropertyChanged(nameof(NewOrganizationalnumber));
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

        #region Collection for business customers

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

        #region Commands
        public ICommand AddBusinessCustomerCommand { get; }
        public ICommand ClearFieldsCommand { get; }
        public ICommand RefreshBusinessCommand { get; }
        #endregion

        #region Add Business Customer Methods
        private void AddBusinessCustomer()
        {
            if (!ValidateAllFields())
            {
                return;
            }

            if (!ValidateNumericFields(out int orgNumber, out int parsedZipcode, out int countrycode))
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

            businessController.CreateNewBusinessCustomer(NewFirstName, NewLastName, NewPhoneNumber, NewEmailAddress, NewAddress, parsedZipcode, NewCity, NewCompanyName, orgNumber, countrycode);
            ShowMessage(NewFirstName, NewPhoneNumber, NewEmailAddress, NewAddress, parsedZipcode, NewCity, NewCompanyName, orgNumber, countrycode);

            BCcustomers.Add(new BusinessCustomer
            {
                FirstName = NewFirstName,
                LastName = NewLastName,
                Phonenumber = NewPhoneNumber,
                Emailaddress = NewEmailAddress,
                Address = NewAddress,
                Zipcode = parsedZipcode,
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
            MessageBox.Show($"Företagskunden har registrerats framgångsrikt!\n\n" +
                            $"Namn: {newFirstName}\n" +
                            $"Telefonnummer: {newPhoneNumber}\n" +
                            $"E-post: {newEmailAddress}\n" +
                            $"Adress: {newAddress}\n" +
                            $"Postnummer: {zipcode}\n" +
                            $"Stad: {newCity}\n" +
                            $"Företagsnamn: {newCompanyName}\n" +
                            $"Organisationsnummer: {orgNumber}\n" +
                            $"Landkod: {countrycode}\n");
        }
        #endregion

        #region Refresh Business Customer Method 
        private void RefreshBusinessCustomer()
        {
            List<BusinessCustomer> businessCustomers = businessController.GetAllBusinessCustomers();
            BCcustomers = new ObservableCollection<BusinessCustomer>(businessCustomers);
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

        private bool ValidateNumericFields(out int orgNumber, out int parsedZipcode, out int countrycode)
        {
            orgNumber = 0;
            countrycode = 0;
            parsedZipcode = 0;

            if (!int.TryParse(NewCountryCode, out countrycode))
            {
                MessageBox.Show("Landskoden måste vara ett giltigt nummer.");
                return false;
            }

            string zipcodePattern = @"^\d{3}\s?\d{2}$"; 
            if (!Regex.IsMatch(NewZipcode, zipcodePattern))
            {
                MessageBox.Show("Postnumret måste vara i formatet 'xxxxx' eller 'xxx xx'.");
                return false;
            }

            parsedZipcode = int.Parse(NewZipcode.Replace(" ", ""));


            string orgNumberPattern = @"^\d{8}$";
            if (!Regex.IsMatch(NewOrganizationalnumber, orgNumberPattern))
            {
                MessageBox.Show("Organisationsnumret måste vara exakt 8 siffror (XXXXXXXX).");
                return false;
            }
            orgNumber = int.Parse(NewOrganizationalnumber);


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
