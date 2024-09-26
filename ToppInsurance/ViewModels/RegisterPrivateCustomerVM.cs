using System.Windows.Input;
using System.Windows;
using TopInsuranceBL;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    public class RegisterPrivateCustomerVM : ObservableObject
    {
        private PrivateController privateController;

        public RegisterPrivateCustomerVM()
        {
            privateController = new PrivateController();
            AddPrivateCustomerCommand = new RelayCommand(AddPrivateCustomer);
        }

        #region Properties Add privatecustomer

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

        private string _newEmailAddress;
        public string NewEmailAddress
        {
            get { return _newEmailAddress; }
            set
            {
                if (_newEmailAddress != value)
                {
                    _newEmailAddress = value;
                    OnPropertyChanged(NewEmailAddress);
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

        private string _newSSN;
        public string NewSSN
        {
            get { return _newSSN; }
            set
            {
                if (_newSSN != value)
                {
                    _newSSN = value;
                    OnPropertyChanged(NewSSN);
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
                    OnPropertyChanged(NewWorkPhoneNumber);
                }
            }
        }

        #endregion

        #region Registrate new business customer Command and Methods 
        public ICommand AddPrivateCustomerCommand { get; }
        private void AddPrivateCustomer()
        {
            string errorMessage = ValidateField("NewName");
            errorMessage ??= ValidateField("NewPhoneNumber");
            errorMessage ??= ValidateField("NewEmailAddress");
            errorMessage ??= ValidateField("NewAddress");
            errorMessage ??= ValidateField("NewZipcode");
            errorMessage ??= ValidateField("NewCity");
            errorMessage ??= ValidateField("NewSSN");
            errorMessage ??= ValidateField("NewWorkPhoneNumber");

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage);
                return;
            }

            // Parse the zipcode and date for customer creation
            int zipcode = int.Parse(NewZipcode);
          


            privateController.CreateNewPrivateCustomer(NewName, NewPhoneNumber, NewEmailAddress, NewAddress, zipcode, NewCity, NewSSN, NewWorkPhoneNumber);

            MessageBox.Show($"Kunden har registrerats framgångsrikt!\n\n" +
                   $"Namn: {NewName}\n" +
                   $"E-post: {NewEmailAddress}\n" +
                   $"Telefon: {NewPhoneNumber}\n" +
                   $"Adress: {NewAddress}\n" +
                   $"Postnummer: {NewZipcode}\n" +
                   $"Stad: {NewCity}\n" +
                   $"Personnummer: {NewSSN}");

            ClearFields();
        }
        #endregion

        #region Clear fields Method
        private void ClearFields()
        {
            NewName = string.Empty;
            NewPhoneNumber = string.Empty;
            NewEmailAddress = string.Empty;
            NewAddress = string.Empty;
            NewZipcode = string.Empty;
            NewCity = string.Empty;
            NewSSN = string.Empty;
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
                case "NewEmailAddress":
                    if (string.IsNullOrWhiteSpace(NewEmailAddress))
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
                case "NewSSN":
                    if (string.IsNullOrWhiteSpace(NewSSN)) 
                    {
                        errorMessage = "Date is required."; 
                    }
                    break;
                case "NewWorkPhoneNumber":
                    if (string.IsNullOrEmpty(NewWorkPhoneNumber))
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
