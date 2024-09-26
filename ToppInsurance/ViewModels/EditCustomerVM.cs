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
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    public class EditCustomerVM : ObservableObject
    {
        private BusinessController businessController;
        private PrivateController privateController;

        public EditCustomerVM() 
        {

            businessController = new BusinessController();
            UpdateBCcustomersCommand = new RelayCommand(UpdateBCcustomers);
            List<BusinessCustomer> customers = businessController.GetAllBusinessCustomers();
            BCcustomers = new ObservableCollection<BusinessCustomer>(customers);


            privateController = new PrivateController();
            UpdatePcustomersCommand = new RelayCommand(UpdatePcustomers);
            FindPcustomersCommand = new RelayCommand(FindPcustomers);
            List<PrivateCustomer> privateCustomers = privateController.GetAllPrivateCustomers();
            Pcustomers = new ObservableCollection<PrivateCustomer>(privateCustomers);
        }

        

        

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

        #region Find Business customer Command and Methods

        public ICommand FindBCcustomersCommand { get; }
        private void FindBCcustomers()
        {
            List<BusinessCustomer> businessCustomers = businessController.GetAllBusinessCustomers();
            BCcustomers = new ObservableCollection<BusinessCustomer>(businessCustomers);
      
        }
        #endregion

        #region Update Business customer Command and Methods
        public ICommand UpdateBCcustomersCommand { get; }
        private void UpdateBCcustomers()
        {
            string errorMessage = ValidateField("NewName");
            errorMessage ??= ValidateField("NewPhoneNumber");
            errorMessage ??= ValidateField("NewEmailadress");
            errorMessage ??= ValidateField("NewAddress");
            errorMessage ??= ValidateField("NewZipcode");
            errorMessage ??= ValidateField("NewCity");
            errorMessage ??= ValidateField("NewCompanyName");

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage);
                return;
            }

            int zipcode = int.Parse(NewZipcode);


            if (SelectedBCcustomers != null && !string.IsNullOrWhiteSpace(NewValue) && SelectedFieldToUpdate != null)
            {
                if (SelectedFieldToUpdate == "PhoneNumber")
                {

                    if (!IsValidPhoneNumber(NewValue))
                    {
                        MessageBox.Show("Invalid phonenumber format.\nPlease make sure the phonenumber follows the format (xxx-xxxxxxx)");
                        return;
                    }
                }

                businessController.UpdateBusinessCustomers(SelectedBCcustomers, SelectedFieldToUpdate, NewValue);
                
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
                case "NewEmailadress":
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

        #region Find private customer Command and Methods

        public ICommand FindPcustomersCommand { get; }
        private void FindPcustomers()
        {
            List<PrivateCustomer> privateCustomers = privateController.GetAllPrivateCustomers();
            Pcustomers = new ObservableCollection<PrivateCustomer>(privateCustomers);

        }

        #endregion

        #region Update private customer Command and Methods
        public ICommand UpdatePcustomersCommand { get; }
        private void UpdatePcustomers()
        {
            string errorMessage = ValidateField("NewName");
            errorMessage ??= ValidateField("NewPhoneNumber");
            errorMessage ??= ValidateField("NewEmailadress");
            errorMessage ??= ValidateField("NewAddress");
            errorMessage ??= ValidateField("NewZipcode");
            errorMessage ??= ValidateField("NewCity");
            errorMessage ??= ValidateField("NewWorkPhonenumber");

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage);
                return;
            }

            int zipcode = int.Parse(NewZipcode);


            if (SelectedPcustomers != null && !string.IsNullOrWhiteSpace(NewValue) && SelectedFieldToUpdate != null)
            {
                if (SelectedFieldToUpdate == "PhoneNumber")
                {

                    if (!IsValidPhoneNumber(NewValue))
                    {
                        MessageBox.Show("Invalid phonenumber format.\nPlease make sure the phonenumber follows the format (xxx-xxxxxxx)");
                        return;
                    }
                }

                privateController.UpdatePrivateCustomers(SelectedPcustomers, SelectedFieldToUpdate, NewValue);

                MessageBox.Show($"Följande uppgifter har uppdaterats!\n\n" +
                $"Här är detaljerna:\n" +
                $"-------------------------\n" +
                $"Namn: {NewName}\n" +
                $"Telefonnummer: {NewPhoneNumber}\n" +
                $"E-post: {NewEmailadress}\n" +
                $"Adress: {NewAddress}\n" +
                $"Postnummer: {NewZipcode}\n" +
                $"Stad: {NewCity}\n" +
                $"Jobbtelefon: {NewWorkPhoneNumber}\n" +
                $"-------------------------\n");
                NewValue = string.Empty;
            }
            else
            {
                MessageBox.Show("Please make sure to select a patient, choose a field to update, and provide a new value.");
            }

        }

        #endregion

       
    }


}

