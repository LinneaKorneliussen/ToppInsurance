using System.Windows.Input;
using System.Windows;
using TopInsuranceWPF.Commands;
using TopInsuranceEntities;
using TopInsuranceBL;
using System.Collections.ObjectModel;

namespace TopInsuranceWPF.ViewModels
{
    public class LifeInsuranceVM : ObservableObject

    {
        private LifeInsuranceController lifeInsuranceController;
        public IEnumerable<Paymentform> Paymentforms {  get; }
        private Employee user;

        public LifeInsuranceVM()
        {
            user = UserContext.Instance.LoggedInUser; 
            lifeInsuranceController = new LifeInsuranceController();
            List<int> baseamounts = lifeInsuranceController.GetBaseAmounts();
            BaseAmount = new ObservableCollection<int>(baseamounts); 
            Paymentforms = Enum.GetValues(typeof(Paymentform)) as IEnumerable<Paymentform>;
            FindCustomerCommand = new RelayCommand(FindCustomer);
            AddLifeInsuranceCommand = new RelayCommand(AddLifeInsurance);

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

        #endregion

        #region Find Customer Method
        private void FindCustomer()
        {
            var filteredCustomers = lifeInsuranceController.SearchPrivateCustomers(SearchText);

            PrivateCustomers = new ObservableCollection<PrivateCustomer>(filteredCustomers);
            SearchText = string.Empty; 
        }
        #endregion

        #region Add life Insurance Method
        private void AddLifeInsurance()
        {
            if (SelectedCustomer != null)
            {
                if (!lifeInsuranceController.CustomerHasInsurance(SelectedCustomer))
                {
                    InsuranceType insurance = InsuranceType.Livförsäkring;

                    lifeInsuranceController.AddLifeInsurance(SelectedCustomer, NewStartDate, NewEndDate,
                                                             insurance, SelectedPaymentForm, SelectedBaseAmount, Note, user);
                    MessageBox.Show($"Du har registrerat en livförsäkring för {SelectedCustomer.FirstName} {SelectedCustomer.LastName}");
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

    }
}
