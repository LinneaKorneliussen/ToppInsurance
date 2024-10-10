using System.Windows.Input;
using System.Windows;
using TopInsuranceWPF.Commands;
using TopInsuranceEntities;
using TopInsuranceBL;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace TopInsuranceWPF.ViewModels
{
    public class LifeInsuranceVM : ObservableObject, IDataErrorInfo

    {
        private LifeInsuranceController lifeInsuranceController;
        private PrivateController privateController;
        public IEnumerable<Paymentform> Paymentforms {  get; }
        private Employee user;

        public LifeInsuranceVM()
        {
            NewStartDate = DateTime.Now;
            NewEndDate = DateTime.Now.AddYears(1);
            user = UserContext.Instance.LoggedInUser; 
            lifeInsuranceController = new LifeInsuranceController();
            privateController = new PrivateController();
            List<int> baseamounts = lifeInsuranceController.GetBaseAmounts();
            BaseAmount = new ObservableCollection<int>(baseamounts); 
            Paymentforms = Enum.GetValues(typeof(Paymentform)) as IEnumerable<Paymentform>;
            FindCustomerCommand = new RelayCommand(FindCustomer);
            AddLifeInsuranceCommand = new RelayCommand(AddLifeInsurance);
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

        private int _selectedBaseAmount;
        public int SelectedBaseAmount
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
        #endregion

        #region Observable Collection 
        private ObservableCollection<int> _baseAmounts;
        public ObservableCollection<int> BaseAmount
        {
            get { return _baseAmounts; }
            set
            {
                if (_baseAmounts != value)
                {
                    _baseAmounts = value;
                    OnPropertyChanged(nameof(BaseAmount));
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
        public ICommand AddLifeInsuranceCommand { get; }
        public ICommand ClearCommand { get; }

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

        #region Add life Insurance Method
        private void AddLifeInsurance()
        {
            string error = this.Error;
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
                return;
            }

            if (SelectedCustomer != null)
            {
                if (SelectedPaymentForm == 0)
                {
                    MessageBox.Show("Vänligen välj en betalningsform.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (SelectedBaseAmount <= 0)
                {
                    MessageBox.Show("Vänligen välj ett basbelopp.", "Ingen basbelopp vald", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!lifeInsuranceController.CustomerHasInsurance(SelectedCustomer))
                {
                    InsuranceType insurance = InsuranceType.Livförsäkring;

                    lifeInsuranceController.AddLifeInsurance(SelectedCustomer, NewStartDate, NewEndDate,
                                                             insurance, SelectedPaymentForm, SelectedBaseAmount, Note, user);
                    MessageBox.Show($"Du har registrerat en livförsäkring för {SelectedCustomer.FirstName} {SelectedCustomer.LastName}");
                    ClearFields();
                }
                else
                {
                    MessageBox.Show($"Kunden {SelectedCustomer.FirstName} {SelectedCustomer.LastName} har redan en livförsäkring.");
                }
            }
            else
            {
                MessageBox.Show("Ingen kund vald. Vänligen välj en kund för att registrera livförsäkringen.");
            }
        }
        #endregion

        #region Validation IDataErrorInfo
        public string Error
        {
            get
            {
                string[] properties = { nameof(NewStartDate), nameof(NewEndDate) };
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
                default:
                    errorMessage = "Ogiltigt kolumnnamn.";
                    break;
            }

            return errorMessage;
        }
        #endregion

        #region Clear Field Method
        private void ClearFields()
        {
            Note = string.Empty;
            SearchText = string.Empty ;
        }
        #endregion

    }
}
