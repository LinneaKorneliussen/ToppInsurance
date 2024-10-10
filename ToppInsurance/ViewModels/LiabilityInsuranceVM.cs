using System.Windows.Input;
using System.Windows;
using TopInsuranceWPF.Commands;
using TopInsuranceEntities;
using TopInsuranceBL;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace TopInsuranceWPF.ViewModels
{
    public class LiabilityInsuranceVM : ObservableObject, IDataErrorInfo

    {
        private LiabilityController liabilityController;
        private BusinessController businessController;
        private Employee user;
        public IEnumerable<Paymentform> Paymentforms { get; }
        public IEnumerable<DeductibleLiability> Deductibleliabilities { get; }
        public IEnumerable<InsuranceAmount> Insuranceamounts { get; }

        public LiabilityInsuranceVM()
        {
            NewStartDate = DateTime.Now;
            NewEndDate = DateTime.Now.AddYears(1);
            user = UserContext.Instance.LoggedInUser;
            liabilityController = new LiabilityController();
            businessController = new BusinessController();
            Paymentforms = Enum.GetValues(typeof(Paymentform)).Cast<Paymentform>();
            Insuranceamounts = Enum.GetValues(typeof(InsuranceAmount)).Cast<InsuranceAmount>();
            Deductibleliabilities = Enum.GetValues(typeof(DeductibleLiability)).Cast<DeductibleLiability>();
            FindBcustomerCommand = new RelayCommand(FindCustomer);
            AddLiabilityInsuranceCommand = new RelayCommand(AddLiabilityInsurance);
            ClearCommand = new RelayCommand(ClearFields);
        }

        #region Properties
        private BusinessCustomer _selectedCustomer;
        public BusinessCustomer SelectedCustomer
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

        private string _contactPerson;
        public string ContactPerson
        {
            get { return _contactPerson; }
            set
            {
                if (_contactPerson != value)
                {
                    _contactPerson = value;
                    OnPropertyChanged(nameof(ContactPerson));
                }
            }
        }

        private string _contactPersonPhNo;
        public string ContactPersonPhNo
        {
            get { return _contactPersonPhNo; }
            set
            {
                if (_contactPersonPhNo != value)
                {
                    _contactPersonPhNo = value;
                    OnPropertyChanged(nameof(ContactPersonPhNo));
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

        private DeductibleLiability _selectedDeductible;
        public DeductibleLiability SelectedDeductible
        {
            get { return _selectedDeductible; }
            set
            {
                if (_selectedDeductible != value)
                {
                    _selectedDeductible = value;
                    OnPropertyChanged(nameof(SelectedDeductible));
                }
            }
        }

        private InsuranceAmount _selectedAmount;
        public InsuranceAmount SelectedAmount
        {
            get { return _selectedAmount; }
            set
            {
                if (_selectedAmount != value)
                {
                    _selectedAmount = value;
                    OnPropertyChanged(nameof(SelectedAmount));
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

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }
        #endregion

        #region Observable Collection 

        private ObservableCollection<BusinessCustomer> _businessCustomers;
        public ObservableCollection<BusinessCustomer> BusinessCustomers
        {
            get { return _businessCustomers; }
            set
            {
                if (_businessCustomers != value)
                {
                    _businessCustomers = value;
                    OnPropertyChanged(nameof(BusinessCustomers));
                }
            }
        }
        #endregion

        #region Commands 
        public ICommand FindBcustomerCommand { get; }
        public ICommand AddLiabilityInsuranceCommand { get; }
        public ICommand ClearCommand { get; }

        #endregion

        #region Find Customer Method
        private void FindCustomer()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var filteredCustomers = businessController.SearchBusinessCustomer(SearchText);
                BusinessCustomers = new ObservableCollection<BusinessCustomer>(filteredCustomers);
            }
            else 
            {
                MessageBox.Show("Sökning misslyckades. Ange söktext.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                BusinessCustomers = new ObservableCollection<BusinessCustomer>();
            }
        }
        #endregion

        #region Add Liability Insurance Method
        private void AddLiabilityInsurance()
        {
            try
            {
                if (SelectedCustomer == null)
                {
                    MessageBox.Show("Vänligen välj en kund från listan.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string error = this.Error;
                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show(error, "Varning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!IsValidPhoneNumber(ContactPersonPhNo))
                {
                    MessageBox.Show("Felaktigt format på telefonnummer!","Varning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                liabilityController.AddLiabilityInsurance(SelectedCustomer, NewStartDate, NewEndDate, InsuranceType.Ansvarsförsäkring, SelectedPaymentForm, Note, ContactPerson, ContactPersonPhNo, SelectedDeductible, SelectedAmount, user);
                MessageBox.Show($"Ansvarsförsäkring tecknad framgångsfullt {SelectedCustomer.FirstName} {SelectedCustomer.LastName}");
                ClearFields();
            }
            catch (ArgumentException ex) 
            {
                MessageBox.Show(ex.Message, "Varning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ett oväntat fel inträffade: " + ex.Message, "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Validation IDataErrorInfo
        public string Error
        {
            get
            {
                string[] properties = { nameof(ContactPerson), nameof(ContactPersonPhNo), nameof(NewStartDate), nameof(NewEndDate), nameof(SelectedPaymentForm)};
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
                case nameof(ContactPerson):
                    if (string.IsNullOrWhiteSpace(ContactPerson))
                    {
                        errorMessage = "Kontaktperson måste anges";
                    }
                    break;
                case nameof(ContactPersonPhNo):
                    if (string.IsNullOrWhiteSpace(ContactPersonPhNo))
                    {
                        errorMessage = "Telefonnummer till kontaktperson måste anges";
                    }
                    break;
                case nameof(SelectedPaymentForm):
                    if (SelectedPaymentForm == 0)
                    {
                        errorMessage = "Vänligen välj ett betalningssätt";
                    }
                    break;
                default:
                    errorMessage = "Vänligen välj";
                    break;
            }

            return errorMessage;
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\d{3}-\d{7}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
        #endregion

        #region Clear Field Method
        private void ClearFields()
        {
            SelectedCustomer = null;
            SelectedAmount = 0;
            SelectedDeductible = 0;
            SelectedPaymentForm = 0;
            Note = string.Empty;
            SearchText = string.Empty;
            NewStartDate = DateTime.Today;  
            NewEndDate = DateTime.Today.AddYears(1);
            ContactPerson = string.Empty;
            ContactPersonPhNo = string.Empty;
            BusinessCustomers.Clear();
        }
        #endregion
    }
}
