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

namespace TopInsuranceWPF.ViewModels
{
    public class SicknessAccidentInsuranceVM : ObservableObject
    {
        private SicknessAccidentController sicknessAccidentController;
        public IEnumerable<Paymentform> Paymentforms { get; }
        public IEnumerable<AdditionalInsurance> AdditionalInsurances { get; }
        public List<int> baseAmounts;
        private Employee user;

        public SicknessAccidentInsuranceVM()
        {
            user = UserContext.Instance.LoggedInUser;
            sicknessAccidentController = new SicknessAccidentController();
            Paymentforms = Enum.GetValues(typeof(Paymentform)) as IEnumerable<Paymentform>;
            AdditionalInsurances = Enum.GetValues(typeof(AdditionalInsurance)) as IEnumerable<AdditionalInsurance>;
            AdultsCommand = new RelayCommand(AdultCommand);
            ChildsCommand = new RelayCommand(ChildCommand);
            FindCustomerCommand = new RelayCommand(FindCustomer);
            AddSicknessAccidentInsuranceCommand = new RelayCommand(AddSicknessAccidentInsurance);
            ClearFieldsCommand = new RelayCommand(ClearFields);
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
        private ObservableCollection<int> _currentBaseAmounts;
        public ObservableCollection<int> CurrentBaseAmounts
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
            CurrentBaseAmounts = new ObservableCollection<int>(baseAmounts);

        }
        private void AdultCommand()
        {
            baseAmounts = sicknessAccidentController.GetBaseAmountsAdult();
            CurrentBaseAmounts = new ObservableCollection<int>(baseAmounts);
        }
        #endregion

        #region Find Customer Method
        private void FindCustomer()
        {
            var filteredCustomers = sicknessAccidentController.SearchPrivateCustomers(SearchText);

            PrivateCustomers = new ObservableCollection<PrivateCustomer>(filteredCustomers);
            SearchText = string.Empty;
        }
        #endregion

        #region Add life Insurance Method
        private void AddSicknessAccidentInsurance()
        {
            if (SelectedCustomer == null)
            {
                MessageBox.Show("Vänligen välj en kund innan du lägger till en försäkring.", "Ingen kund vald", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (IsAdultOptionSelected)
            {
                sicknessAccidentController.AddSicknessAccidentInsurance(SelectedCustomer, NewStartDate, NewEndDate, InsuranceType.SjukOchOlycksfallsförsäkringVUXEN,
                    SelectedPaymentForm, SelectedBaseAmount, Note, null, null, null, SelectedAdditional, user);
                MessageBox.Show("Sjuk- och olycksfallsförsäkring för vuxen har lagts till.", "Försäkring tillagd", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (IsChildInsuranceSelected)
            {
                if (string.IsNullOrWhiteSpace(FirstNameChild) || string.IsNullOrWhiteSpace(LastNameChild) || string.IsNullOrWhiteSpace(SSNChild))
                {
                    MessageBox.Show("Vänligen fyll i alla fält för barnets information (förnamn, efternamn, personnummer).", "Ofullständig information", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                sicknessAccidentController.AddSicknessAccidentInsurance(SelectedCustomer, NewStartDate, NewEndDate, InsuranceType.SjukOchOlycksfallsförsäkringBARN, 
                    SelectedPaymentForm, SelectedBaseAmount, Note, FirstNameChild, LastNameChild, SSNChild, SelectedAdditional, user);

                MessageBox.Show("Sjuk- och olycksfallsförsäkring för barn har lagts till.", "Försäkring tillagd", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion

        #region Clear Fields Method
        private void ClearFields()
        {
            SearchText = string.Empty;
        }
        #endregion
    }

}
