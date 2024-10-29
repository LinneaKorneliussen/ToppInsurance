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
        private Employee user;
        public IEnumerable<Paymentform> Paymentforms {  get; }

        public LifeInsuranceVM()
        {
            NewStartDate = DateTime.Now;
            NewEndDate = DateTime.Now.AddYears(1);
            user = UserContext.Instance.LoggedInUser; 
            lifeInsuranceController = new LifeInsuranceController();
            privateController = new PrivateController();
            List<int> baseamounts = lifeInsuranceController.GetBaseAmounts();
            BaseAmount = new ObservableCollection<int>(baseamounts);
            Paymentforms = Enum.GetValues(typeof(Paymentform)).Cast<Paymentform>();
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
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                MessageBox.Show("Sökning misslyckades. Ange söktext.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var filteredPrivateCustomers = privateController.SearchPrivateCustomers(SearchText);

            if (filteredPrivateCustomers == null || !filteredPrivateCustomers.Any())
            {
                MessageBox.Show("Inga resultat hittades för den angivna söktexten.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                PrivateCustomers = new ObservableCollection<PrivateCustomer>(filteredPrivateCustomers);
            }

        }
        
#endregion

        #region Add life Insurance Method
        private void AddLifeInsurance()
                {
                    if (SelectedCustomer == null)
                    {
                        MessageBox.Show("Vänligen välj en kund från listan.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    string error = this.Error;
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show(error, "Valideringsfel", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (lifeInsuranceController.CustomerHasInsurance(SelectedCustomer))
                    {
                        MessageBox.Show($"Kunden {SelectedCustomer.FirstName} {SelectedCustomer.LastName} har redan en livförsäkring.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    lifeInsuranceController.AddLifeInsurance(SelectedCustomer, NewStartDate, NewEndDate, InsuranceType.Livförsäkring, SelectedPaymentForm, SelectedBaseAmount, Note, user);
                    MessageBox.Show($"Livförsäkring har skapat för: {SelectedCustomer.FirstName} {SelectedCustomer.LastName}.", "Toppenbra!", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearFields();
                }
                #endregion

        #region Validation IDataErrorInfo
        public string Error
        {
            get
            {
                string[] properties = { nameof(NewStartDate), nameof(NewEndDate), nameof(SelectedPaymentForm), nameof(SelectedBaseAmount) };
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
                        errorMessage = "Går inte att teckna försäkring för redan passerade datum.";
                    }
                    else if (NewEndDate < NewStartDate)
                    {
                        errorMessage = "Slutdatum kan inte vara före startdatum.";
                    }
                    else
                    {
                        int daysBetween = (NewEndDate - NewStartDate).Days;
                        switch (SelectedPaymentForm)
                        {
                            case Paymentform.År:
                                if (daysBetween < 365)
                                    errorMessage = "För årsbetalning måste perioden vara minst 365 dagar.";
                                break;
                            case Paymentform.Halvår:
                                if (daysBetween < 182)
                                    errorMessage = "För halvårsbetalning måste perioden vara minst 182 dagar.";
                                break;
                            case Paymentform.Kvartal:
                                if (daysBetween < 91)
                                    errorMessage = "För kvartalsbetalning måste perioden vara minst 91 dagar.";
                                break;
                            case Paymentform.Månad:
                                if (daysBetween < 30)
                                    errorMessage = "För månadsbetalning måste perioden vara minst 30 dagar.";
                                break;
                        }
                    }
                    break;
                case nameof(SelectedPaymentForm):
                    if (SelectedPaymentForm == 0)
                    {
                        errorMessage = "Vänligen välj betalningsform";
                    }
                    break;
                case nameof(SelectedBaseAmount):
                    if(SelectedBaseAmount <= 0) 
                    {
                        errorMessage = "Vänligen välj ett basbelopp";
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
            SelectedCustomer = null;
            SearchText = string.Empty;
            NewStartDate = DateTime.Today;
            NewEndDate = DateTime.Today.AddYears(1);
            SelectedBaseAmount = 0;
            SelectedPaymentForm = 0;
            Note = string.Empty;
            SearchText = string.Empty;
            PrivateCustomers = new ObservableCollection<PrivateCustomer>();
        }
        #endregion
    }
}
