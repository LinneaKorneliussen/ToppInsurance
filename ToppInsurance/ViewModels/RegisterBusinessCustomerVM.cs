using System;
using System.Windows.Input;
using System.Windows;
using TopInsuranceWPF.Commands;
using TopInsuranceBL;
using TopInsuranceEntities;
using ControlzEx.Standard;


namespace TopInsuranceWPF.ViewModels
{
    public class RegisterBusinessCustomerVM : ObservableObject
    {
        private BusinessController businessController;

        public RegisterBusinessCustomerVM()
        {
            businessController = new BusinessController();
            AddBusinessCustomerCommand = new RelayCommand(AddBusinessCustomer);
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


            int orgNumber = int.Parse(NewOrganizationalnumber);
            int zipcode = int.Parse(NewZipcode);
            int countrycode = int.Parse(NewCountryCode);

            businessController.CreateNewBusinessCustomer(NewName, NewPhoneNumber, NewEmailadress, NewAddress, zipcode, NewCity, NewCompanyName, orgNumber, countrycode);

            MessageBox.Show($"Patient registered successfully, See details below:\n Name: {NewName}\n SSN: {NewName}\n Address: {NewAddress}\n Phone: {NewPhoneNumber}\n Email: {NewEmailadress}");

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
                case "NewOrganizationalnumber": // Ändrat här för att matcha fältet korrekt
                    if (string.IsNullOrEmpty(NewOrganizationalnumber))
                    {
                        errorMessage = "Field is required.";
                    }
                    break;
                case "NewCountryCode":
                    if (string.IsNullOrEmpty(NewCountryCode))
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
