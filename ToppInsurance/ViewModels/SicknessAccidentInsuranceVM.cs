using TopInsuranceWPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceEntities;
using TopInsuranceBL;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace TopInsuranceWPF.ViewModels
{
    public class SicknessAccidentInsuranceVM : ObservableObject, IDataErrorInfo
    {
        private SicknessAccidentController sicknessAccidentController;
        private PrivateController privateController;
        public List<double> baseAmounts;
        private Employee user;
        public IEnumerable<Paymentform> Paymentforms { get; }
        public IEnumerable<AdditionalInsurance> AdditionalInsurances { get; }

        public SicknessAccidentInsuranceVM()
        {
            NewStartDate = DateTime.Now;
            NewEndDate = DateTime.Now.AddYears(1);
            user = UserContext.Instance.LoggedInUser;
            sicknessAccidentController = new SicknessAccidentController();
            privateController = new PrivateController();
            Paymentforms = Enum.GetValues(typeof(Paymentform)) as IEnumerable<Paymentform>;
            AdditionalInsurances = Enum.GetValues(typeof(AdditionalInsurance)) as IEnumerable<AdditionalInsurance>;
            AdultsCommand = new RelayCommand(AdultCommand);
            ChildsCommand = new RelayCommand(ChildCommand);
            FindCustomerCommand = new RelayCommand(FindCustomer);
            AddSicknessAccidentInsuranceCommand = new RelayCommand(AddSicknessAccidentInsurance);
            ClearFieldsCommand = new RelayCommand(ClearFields);
            ClearCommand = new RelayCommand(ClearFields);
        }

        #region Properties
        private PrivateCustomer _selectedCustomer;
        public PrivateCustomer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    OnPropertyChanged(nameof(SelectedCustomer));
                }
            }
        }

        private DateTime _newStartDate;
        public DateTime NewStartDate
        {
            get { return _newStartDate; }
            set
            {
                if (_newStartDate != value)
                {
                    _newStartDate = value;
                    OnPropertyChanged(nameof(NewStartDate));
                }
            }
        }

        private DateTime _newEndDate;
        public DateTime NewEndDate
        {
            get { return _newEndDate; }
            set
            {
                if (_newEndDate != value)
                {
                    _newEndDate = value;
                    OnPropertyChanged(nameof(NewEndDate));
                }
            }
        }

        private Paymentform _selectedPaymentForm;
        public Paymentform SelectedPaymentForm
        {
            get { return _selectedPaymentForm; }
            set
            {
                if (_selectedPaymentForm != value)
                {
                    _selectedPaymentForm = value;
                    OnPropertyChanged(nameof(SelectedPaymentForm));
                }
            }
        }

        private AdditionalInsurance _selectedAdditional;
        public AdditionalInsurance SelectedAdditional
        {
            get { return _selectedAdditional; }
            set
            {
                if (_selectedAdditional != value)
                {
                    _selectedAdditional = value;
                    OnPropertyChanged(nameof(SelectedAdditional));
                }
            }
        }

        private double _selectedBaseAmount;
        public double SelectedBaseAmount
        {
            get { return _selectedBaseAmount; }
            set
            {
                if (_selectedBaseAmount != value)
                {
                    _selectedBaseAmount = value;
                    OnPropertyChanged(nameof(SelectedBaseAmount));
                }
            }
        }

        private string _note;
        public string Note
        {
            get { return _note; }
            set
            {
                if (_note != value)
                {
                    _note = value;
                    OnPropertyChanged(nameof(Note));
                }
            }
        }

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

        private string _firstNameChild;
        public string FirstNameChild
        {
            get { return _firstNameChild; }
            set
            {
                if (_firstNameChild != value)
                {
                    _firstNameChild = value;
                    OnPropertyChanged(nameof(FirstNameChild));
                }
            }
        }


        private string _lastNameChild;
        public string LastNameChild
        {
            get { return _lastNameChild; }
            set
            {
                if (_lastNameChild != value)
                {
                    _lastNameChild = value;
                    OnPropertyChanged(nameof(LastNameChild));
                }
            }
        }

        private string _ssnChild;
        public string SSNChild
        {
            get { return _ssnChild; }
            set
            {
                if (_ssnChild != value)
                {
                    _ssnChild = value;
                    OnPropertyChanged(nameof(SSNChild));
                }
            }
        }

        private bool isAdultOptionSelected;
        public bool IsAdultOptionSelected
        {
            get { return isAdultOptionSelected; }
            set
            {
                if (isAdultOptionSelected != value)
                {
                    isAdultOptionSelected = value;
                    OnPropertyChanged(nameof(IsAdultOptionSelected));

                }
            }
        }

        private bool isChildInsuranceSelected;
        public bool IsChildInsuranceSelected
        {
            get { return isChildInsuranceSelected; }
            set
            {
                if (isChildInsuranceSelected != value)
                {
                    isChildInsuranceSelected = value;
                    OnPropertyChanged(nameof(IsChildInsuranceSelected));

                }
            }
        }
        #endregion

        #region Observable collection 
        private ObservableCollection<double> _currentBaseAmounts;
        public ObservableCollection<double> CurrentBaseAmounts
        {
            get { return _currentBaseAmounts; }
            set
            {
                if (_currentBaseAmounts != value)
                {
                    _currentBaseAmounts = value;
                    OnPropertyChanged(nameof(CurrentBaseAmounts));
                }
            }
        }
        private ObservableCollection<PrivateCustomer> _privateCustomers;
        public ObservableCollection<PrivateCustomer> PrivateCustomers
        {
            get { return _privateCustomers; }
            set
            {
                if (_privateCustomers != value)
                {
                    _privateCustomers = value;
                    OnPropertyChanged(nameof(PrivateCustomers));
                }
            }
        }
        #endregion

        #region Commands 
        public ICommand FindCustomerCommand { get; }
        public ICommand ChildsCommand { get; }
        public ICommand AdultsCommand { get; }
        public ICommand AddSicknessAccidentInsuranceCommand { get; }
        public ICommand ClearFieldsCommand { get; }
        #endregion

        #region Child and Adult base amount Methods
        private void ChildCommand()
        {
            baseAmounts = sicknessAccidentController.GetBaseAmountsChild();
            CurrentBaseAmounts = new ObservableCollection<double>(baseAmounts);

        }
        private void AdultCommand()
        {
            baseAmounts = sicknessAccidentController.GetBaseAmountsAdult();
            CurrentBaseAmounts = new ObservableCollection<double>(baseAmounts);
        }
        #endregion

        #region Find Customer Method
        private void FindCustomer()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var filteredCustomers = privateController.SearchPrivateCustomer(SearchText);

                PrivateCustomers = new ObservableCollection<PrivateCustomer>(filteredCustomers);
                SearchText = string.Empty; 
            }
            else
            {
                MessageBox.Show("Sökning misslyckades. Ange söktext.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                PrivateCustomers = new ObservableCollection<PrivateCustomer>();
            }

        }
        #endregion

        #region Add Sickness Accident Insurance
        private void AddSicknessAccidentInsurance()
        {
            if (SelectedCustomer == null)
            {
                ShowMessage("Vänligen välj en kund innan du lägger till en försäkring.", "Ingen kund vald");
                return;
            }

            if (!IsAdultOptionSelected && !IsChildInsuranceSelected)
            {
                ShowMessage("Vänligen välj om försäkringen gäller vuxen eller barn.", "Ingen försäkringskategori vald");
                return;
            }

            string error = this.Error;
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Valideringsfel", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (IsAdultOptionSelected)
            {
                AddInsuranceForAdult();
                ClearFields();
            }
            
            else if (IsChildInsuranceSelected)
            {
                if (!ValidateChildInfo())
                {
                    return;
                }
                AddInsuranceForChild();
                ClearFields();
            }
        }
        #endregion

        #region Validation Methods 
        public bool IsValidPersonalNumber(string personalNumber)
        {
            return Regex.IsMatch(personalNumber, @"^\d{8}-\d{4}$");
        }

        private bool ValidateChildInfo()
        {
            if (!IsValidPersonalNumber(SSNChild))
            {
                ShowMessage("Ogiltigt personnummer. Vänligen kontrollera att personnumret är i formatet YYYYMMDD-XXXX.", "Ogiltigt personnummer");
                return false;
            }
            return true;
        }
        #endregion

        #region Add insurance for child or adult Method
        private void AddInsuranceForAdult()
        {
            sicknessAccidentController.AddSicknessAccidentInsurance(SelectedCustomer,NewStartDate,NewEndDate,InsuranceType.SjukOchOlycksfallsförsäkringVUXEN,SelectedPaymentForm,SelectedBaseAmount,
                Note,null,null,null,SelectedAdditional,user
            );
            ShowMessage("Sjuk- och olycksfallsförsäkring för vuxen har lagts till.", "Försäkring tillagd", MessageBoxImage.Information);
        }

        private void AddInsuranceForChild()
        {
            sicknessAccidentController.AddSicknessAccidentInsurance(SelectedCustomer,NewStartDate,NewEndDate,InsuranceType.SjukOchOlycksfallsförsäkringBARN,SelectedPaymentForm,
                SelectedBaseAmount,Note,FirstNameChild,LastNameChild,SSNChild,SelectedAdditional,user
            );
            ShowMessage("Sjuk- och olycksfallsförsäkring för barn har lagts till.", "Försäkring tillagd", MessageBoxImage.Information);
        }
        #endregion

        #region Show message Method
        private void ShowMessage(string message, string caption, MessageBoxImage icon = MessageBoxImage.Warning)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, icon);
        }
        #endregion

        #region Validation IDataErrorInfo
        public string Error
        {
            get
            {
                string[] properties = IsChildInsuranceSelected
               ? new string[] { nameof(NewStartDate), nameof(NewEndDate), nameof(SelectedPaymentForm), nameof(SelectedBaseAmount), nameof(SelectedAdditional),
                    nameof(FirstNameChild), nameof(LastNameChild), nameof(SSNChild) }
               : new string[] { nameof(NewStartDate), nameof(NewEndDate), nameof(SelectedPaymentForm), nameof(SelectedBaseAmount), nameof(SelectedAdditional) };
                foreach (var property in properties)
                {
                    string error = this[property];
                    if (!string.IsNullOrEmpty(error))
                    {
                        return error;
                    }
                }
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                return ValidateField(columnName);
            }
        }

        private string ValidateField(string columnName)
        {
            string errorMessage = null;

            switch (columnName)
            {
                case nameof(NewStartDate):
                    if (NewStartDate < DateTime.Today)
                    {
                        errorMessage = "Går inte att teckna en försäkring för redan passerade datum";
                    }
                    break;
                case nameof(NewEndDate):
                    if (NewEndDate < DateTime.Today)
                    {
                        errorMessage = "Går inte att teckna försäkring för redan passerade datum";
                    }
                    else if (NewEndDate < NewStartDate)
                    {
                        errorMessage = "Slutdatum kan inte vara före startdatum.";
                    }
                    break;
                case nameof(SelectedPaymentForm):
                    if (SelectedPaymentForm == 0)
                    {
                        errorMessage = "Vänligen välj ett betalningssätt";
                    }
                    break;
                case nameof(SelectedBaseAmount):
                    if (SelectedBaseAmount <= 0)
                    {
                        errorMessage = "Vänligen välj ett basbelopp";
                    }
                    break;
                case nameof(SelectedAdditional):
                    if (SelectedAdditional <= 0)
                    {
                        errorMessage = "Vänligen välj eventuellt tillval";
                    }
                    break;
                case nameof(FirstNameChild):
                    if (string.IsNullOrWhiteSpace(FirstNameChild))
                    {
                        errorMessage = "Försäkrades förnamn måste anges";
                    }
                    break;
                case nameof(LastNameChild):
                    if (string.IsNullOrWhiteSpace(LastNameChild))
                    {
                        errorMessage = "Försäkrades efternamn måste anges";
                    }
                    break;
                case nameof(SSNChild):
                    if (string.IsNullOrWhiteSpace(SSNChild))
                    {
                        errorMessage = "Försäkrades personnummer måste anges";
                    }
                    break;
                default:
                    errorMessage = "Ogiltigt kolumnnamn.";
                    break;
            }

            return errorMessage;
        }
        #endregion

        #region Clear Command
        public ICommand ClearCommand { get; }
        private void ClearFields()
        {
            IsAdultOptionSelected = false;
            IsChildInsuranceSelected = false; 
            Note = string.Empty;
            PrivateCustomers.Clear();
            SelectedCustomer = null;
            NewStartDate = DateTime.Today;
            NewEndDate = DateTime.Today.AddYears(1);
            SelectedAdditional = 0;
            SelectedBaseAmount = 0;
            SelectedPaymentForm = 0;
            SSNChild = string.Empty;
            FirstNameChild = string.Empty;
            LastNameChild = string.Empty;
        }
        #endregion

    }

}
