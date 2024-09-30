using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TopInsuranceBL;
using TopInsuranceDL;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    public class EditCustomerVM : ObservableObject
    {
        private BusinessController businessController;
        private PrivateController privateController;
        private PrivateRepository privateRepository;
        public EditCustomerVM() 
        {

            businessController = new BusinessController();
            privateController = new PrivateController();
            UpdateBCcustomersCommand = new RelayCommand(UpdateBCcustomers);
            FieldsToUpdate = new ObservableCollection<string> { "Name", "Address", "PhoneNumber", "EmailAddress" };
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
                    OnPropertyChanged(SearchText);
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
                //Filtrera på det vi vill att man ska söka på
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
            // Hämta alla privata kunder från kontrollern
            List<PrivateCustomer> privateCustomers = privateController.GetAllPrivateCustomers();

            // Om SearchText är tom, visa alla kunder
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Pcustomers = new ObservableCollection<PrivateCustomer>(privateCustomers);
            }
            else
            {
                // Filtrera kunderna baserat på söktexten (söker på namn, e-post och telefonnummer) 
                //Filtrera på det vi vill att man ska söka på
                var filteredCustomers = privateCustomers
                    .Where(c => c.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                c.Emailaddress.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                c.Phonenumber.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                // Uppdatera ObservableCollection med filtrerade kunder
                Pcustomers = new ObservableCollection<PrivateCustomer>(filteredCustomers);
            }
        }

        private void RefreshSelectedCustomer()
        {
            if (SelectedPcustomers != null)
            {
                // Hitta och uppdatera den valda kunden i listan
                var updatedCustomer = Pcustomers.FirstOrDefault(c => c.Name == SelectedPcustomers.Name); // Exempel på hur du hittar kunden

                if (updatedCustomer != null)
                {
                    // Uppdatera egenskaper
                    SelectedPcustomers.Name = updatedCustomer.Name;
                    SelectedPcustomers.Address = updatedCustomer.Address;
                    SelectedPcustomers.Phonenumber = updatedCustomer.Phonenumber;
                    SelectedPcustomers.Emailaddress = updatedCustomer.Emailaddress;
                    SelectedPcustomers.Zipcode = updatedCustomer.Zipcode;
                    SelectedPcustomers.City = updatedCustomer.City;
                    SelectedPcustomers.WorkPhonenumber = updatedCustomer.WorkPhonenumber;

                    // Meddela att egenskaper har förändrats om du behöver
                    OnPropertyChanged(nameof(SelectedPcustomers));
                }
            }
        }
        private void RefreshSelectedBusinessCustomer()
        {
            if (SelectedPcustomers != null)
            {
                // Hitta och uppdatera den valda kunden i listan
                var updatedBusinessCustomer = BCcustomers.FirstOrDefault(c => c.Name == SelectedBCcustomers.Name); // Exempel på hur du hittar kunden

                if (updatedBusinessCustomer != null)
                {
                    // Uppdatera egenskaper
                    SelectedBCcustomers.Name = updatedBusinessCustomer.Name;
                    SelectedBCcustomers.Address = updatedBusinessCustomer.Address;
                    SelectedBCcustomers.Phonenumber = updatedBusinessCustomer.Phonenumber;
                    SelectedBCcustomers.Emailaddress = updatedBusinessCustomer.Emailaddress;
                    SelectedBCcustomers.Zipcode = updatedBusinessCustomer.Zipcode;
                    SelectedBCcustomers.City = updatedBusinessCustomer.City;
                    SelectedBCcustomers.CompanyName = updatedBusinessCustomer.CompanyName;

                    // Meddela att egenskaper har förändrats om du behöver
                    OnPropertyChanged(nameof(SelectedBCcustomers));
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
                    OnPropertyChanged(NewWorkPhoneNumber);
                }
            }
        }

        #endregion

        #region Update Business 
        private string _newValue;
        public string NewValue
        {
            get { return _newValue; }
            set
            {
                if (_newValue != value)
                {
                    _newValue = value;
                    OnPropertyChanged(NewValue);
                }
            }
        }

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

        private ObservableCollection<string> fieldsToUpdate;
        public ObservableCollection<string> FieldsToUpdate
        {
            get { return fieldsToUpdate; }
            set { fieldsToUpdate = value; OnPropertyChanged(nameof(FieldsToUpdate)); }
        }

        private string selectedFieldToUpdate;
        public string SelectedFieldToUpdate
        {
            get { return selectedFieldToUpdate; }
            set { selectedFieldToUpdate = value; OnPropertyChanged(nameof(SelectedFieldToUpdate)); }
        }
        #endregion

        #region Update private  

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
        private void UpdateBCcustomers()
        {
            
            if (SelectedBCcustomers != null && !string.IsNullOrWhiteSpace(NewValue) && SelectedFieldToUpdate != null)
            {
                // Uppdatera privatkundens information
                businessController.UpdateBusinessCustomers(SelectedBCcustomers, SelectedFieldToUpdate, NewValue);

                // Uppdatera egenskapen direkt (t.ex. namn)
                if (SelectedFieldToUpdate == "Name")
                {
                    SelectedBCcustomers.Name = NewName; // Ställ in det nya värdet direkt
                }
                if (SelectedFieldToUpdate == "Phonenumber")
                {
                    SelectedBCcustomers.Phonenumber = NewPhoneNumber; // Ställ in det nya värdet direkt
                }
                if (SelectedFieldToUpdate == "Emailaddress")
                {
                    SelectedBCcustomers.Emailaddress = NewEmailadress; // Ställ in det nya värdet direkt
                }
                // Upprepa för andra fält som kan uppdateras
                RefreshSelectedBusinessCustomer();
                MessageBox.Show($"Följande uppgifter har uppdaterats!\n\n" +
                $"Här är detaljerna:\n" +
                $"-------------------------\n" +
                $"Namn: {NewName}\n" +
                $"Telefonnummer: {NewPhoneNumber}\n" +
                $"E-post: {NewEmailadress}\n" +
                $"Adress: {NewAddress}\n" +
                $"Postnummer: {NewZipcode}\n" +
                $"Stad: {NewCity}\n" +
                $"Företagsnamn: {NewCompanyName}\n" +
                $"-------------------------\n");
                NewValue = string.Empty;
            }
            else
            {
                MessageBox.Show("Please make sure to select a patient, choose a field to update, and provide a new value.");
            }

        }

        #endregion

        #region Update private customer Command and Methods
        public ICommand UpdatePcustomersCommand { get; }

        private void UpdatePrivateCustomer()
        {
            if (SelectedPcustomers != null && !string.IsNullOrWhiteSpace(NewValue) && SelectedFieldToUpdate != null)
            {
                // Uppdatera privatkundens information
                privateController.UpdatePrivateCustomers(SelectedPcustomers, SelectedFieldToUpdate, NewValue);

                // Uppdatera egenskapen direkt (t.ex. namn)
                if (SelectedFieldToUpdate == "Name")
                {
                    SelectedPcustomers.Name = NewValue; // Ställ in det nya värdet direkt
                }
                if (SelectedFieldToUpdate == "Phonenumber")
                {
                    SelectedPcustomers.Phonenumber = NewValue; // Ställ in det nya värdet direkt
                }
                if (SelectedFieldToUpdate == "Emailaddress")
                {
                    SelectedPcustomers.Emailaddress = NewValue; // Ställ in det nya värdet direkt
                }
                // Upprepa för andra fält som kan uppdateras
                RefreshSelectedCustomer();

                // Visa bekräftelsemeddelande
                MessageBox.Show($"Följande uppgifter har uppdaterats!\n\n" +
                    $"Här är detaljerna:\n" +
                    $"-------------------------\n" +
                    $"Namn: {SelectedPcustomers.Name}\n" +
                    $"Telefonnummer: {SelectedPcustomers.Phonenumber}\n" +
                    $"E-post: {SelectedPcustomers.Emailaddress}\n" +
                    $"Adress: {SelectedPcustomers.Address}\n" +
                    $"Postnummer: {SelectedPcustomers.Zipcode}\n" +
                    $"Stad: {SelectedPcustomers.City}\n" +
                    $"Jobbtelefon: {SelectedPcustomers.WorkPhonenumber}\n" +
                    $"-------------------------\n");

                // Återställ NewValue för att förbereda för nästa uppdatering
                NewValue = string.Empty;
            }
            else
            {
                MessageBox.Show("Please make sure to select a patient, choose a field to update, and provide a new value.");
            }
        }


        #endregion

        #region Clear and exit command 
        public ICommand ClearCommand { get; }
        private void ClearFields()
        {
            NewName = string.Empty;
            NewPhoneNumber = string.Empty;
            NewEmailadress = string.Empty;
            NewAddress = string.Empty;
            NewZipcode = string.Empty;
            NewCity = string.Empty;
            NewCompanyName = string.Empty;
            NewWorkPhoneNumber = string.Empty;  
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
                        errorMessage = "Field is required.";
                    }
                    break;
                case "NewPhoneNumber":
                    if (string.IsNullOrWhiteSpace(NewPhoneNumber))
                    {
                        errorMessage = "Field is required.";
                    }
                    break;
                case "Emailaddress":
                    if (string.IsNullOrWhiteSpace(NewEmailadress))
                    {
                        errorMessage = "Field is required.";
                    }
                    break;
                case "NewAddress":
                    if (string.IsNullOrWhiteSpace(NewAddress))
                    {
                        errorMessage = "Field is required.";
                    }
                    break;
                case "NewZipcode":
                    if (string.IsNullOrWhiteSpace(NewZipcode))
                    {
                        errorMessage = "Field is required.";
                    }
                    break;
                case "NewCity":
                    if (string.IsNullOrEmpty(NewCity))
                    {
                        errorMessage = "Field is required.";

                    }
                    break;
                case "NewCompanyName":
                    if (string.IsNullOrEmpty(NewCompanyName))
                    {
                        errorMessage = "Field is required.";

                    }
                    break;
                default:
                    errorMessage = "Invalid column name.";
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

