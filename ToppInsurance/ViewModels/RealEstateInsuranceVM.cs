using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using TopInsuranceBL;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    public class RealEstateInsuranceVM : ObservableObject, IDataErrorInfo
    {
        private RealEstateController realEstateController;
        private BusinessController businessController;
        private Employee user;

        public IEnumerable<Paymentform> Paymentforms { get; }
        public IEnumerable<InventoryType> Types { get; }

        public RealEstateInsuranceVM()
        {
            NewStartDate = DateTime.Today;
            NewEndDate = DateTime.Now.AddYears(1);
            user = UserContext.Instance.LoggedInUser;
            realEstateController = new RealEstateController();
            businessController = new BusinessController();

            Paymentforms = Enum.GetValues(typeof(Paymentform)).Cast<Paymentform>();
            Types = Enum.GetValues(typeof(InventoryType)).Cast<InventoryType>();
            Inventories = new ObservableCollection<Inventory>();

            FindBCcustomersCommand = new RelayCommand(FindBCcustomers);
            AddInventoryCommand = new RelayCommand(AddInventory);
            RemoveInventoryCommand = new RelayCommand<Inventory>(RemoveInventory);
            AddRealEstateInsuranceCommand = new RelayCommand(AddRealEstateInsurance);
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

        private string _companyAddress;
        public string CompanyAddress
        {
            get { return _companyAddress; }
            set
            {
                if (_companyAddress != value)
                {
                    _companyAddress = value;
                    OnPropertyChanged(nameof(CompanyAddress));
                }
            }
        }

        private string _companyCity;
        public string CompanyCity
        {
            get { return _companyCity; }
            set
            {
                if (_companyCity != value)
                {
                    _companyCity = value;
                    OnPropertyChanged(nameof(CompanyCity));
                }
            }
        }

        private string _companyZipcode;
        public string CompanyZipcode
        {
            get { return _companyZipcode; }
            set
            {
                if (_companyZipcode != value)
                {
                    _companyZipcode = value;
                    OnPropertyChanged(nameof(CompanyZipcode));
                }
            }
        }

        private double _valueRealEstate;
        public double ValueRealEstate
        {
            get { return _valueRealEstate; }
            set
            {
                if (_valueRealEstate != value)
                {
                    _valueRealEstate = value;
                    OnPropertyChanged(nameof(ValueRealEstate));
                }
            }
        }

        private bool isInventorySelected;
        public bool IsInventorySelected
        {
            get { return isInventorySelected; }
            set
            {
                if (isInventorySelected != value)
                {
                    isInventorySelected = value;
                    OnPropertyChanged(nameof(IsInventorySelected));
                }
            }
        }
        private string _searchBusinessCustomer;
        public string SearchBusinessCustomer
        {
            get { return _searchBusinessCustomer; }
            set
            {
                if (_searchBusinessCustomer != value)
                {
                    _searchBusinessCustomer = value;
                    OnPropertyChanged(nameof(SearchBusinessCustomer));
                }
            }
        }

        #endregion

        #region Observable Collections

        private ObservableCollection<Inventory> _inventories;
        public ObservableCollection<Inventory> Inventories
        {
            get { return _inventories; }
            set
            {
                if (_inventories != value)
                {
                    _inventories = value;
                    OnPropertyChanged(nameof(Inventories));
                }
            }
        }
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

        #region ICommand 
        public ICommand FindBCcustomersCommand { get; }
        public ICommand AddInventoryCommand { get; }
        public ICommand RemoveInventoryCommand { get; }
        public ICommand AddRealEstateInsuranceCommand { get; }
        public ICommand ToggleInventorySelectionCommand { get; }

        public ICommand ClearCommand { get; }

        #endregion

        #region Find business customers
        private void FindBCcustomers()
        {
            if (!string.IsNullOrWhiteSpace(SearchBusinessCustomer))
            {
                var filteredCustomers = businessController.SearchBusinessCustomers(SearchBusinessCustomer);
                BusinessCustomers = new ObservableCollection<BusinessCustomer>(filteredCustomers);
                SearchBusinessCustomer = string.Empty;
            }
            else
            {
                MessageBox.Show("Sökning misslyckades. Ange söktext.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                BusinessCustomers = new ObservableCollection<BusinessCustomer>();
            }
        }

        #endregion

        #region Methods for Inventory
        private void AddInventory()
        {
            var newInventory = new Inventory();
            Inventories.Add(newInventory);
        }

        private void RemoveInventory(Inventory inventory)
        {
            if (Inventories.Contains(inventory))
            {
                Inventories.Remove(inventory);
            }
        }

        #endregion

        #region Method for Adding Real Estate Insurance
        private void AddRealEstateInsurance()
        {
            if (SelectedCustomer == null)
            {
                MessageBox.Show("Vänligen välj en kund från listan. ", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string error = this.Error;
            if (ValueRealEstate <= 0)
            {
                MessageBox.Show("Vänligen ange ett giltigt värde för fastigheten.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SelectedPaymentForm == 0)
            {
                MessageBox.Show("Vänligen välj ett betalningssätt.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (IsInventorySelected)
            {
                if (!Inventories.Any())
                {
                    MessageBox.Show("Vänligen lägg till minst en inventarie.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                foreach (var inventory in Inventories)
                {
                    if (inventory.InventoryType == 0)
                    {
                        MessageBox.Show("Vänligen ange en giltig inventarietyp för alla inventarier.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (inventory.InvValue <= 0)
                    {
                        MessageBox.Show("Vänligen ange ett giltigt värde för inventarier.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(CompanyAddress))
            {
                MessageBox.Show("Vänligen ange företagsadress. ", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(CompanyZipcode))
            {
                MessageBox.Show("Vänligen ange ett postnummer.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(CompanyCity))
            {
                MessageBox.Show("Vänligen ange stad. ", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                realEstateController.AddRealEstateInsurance(
                    SelectedCustomer,
                    NewStartDate,
                    NewEndDate,
                    InsuranceType.FastighetsOchInventarieförsäkring, 
                    SelectedPaymentForm,
                    Note,
                    CompanyAddress,
                    int.Parse(CompanyZipcode),
                    CompanyCity,
                    ValueRealEstate,
                    Inventories.ToList(), 
                    user
                );

                MessageBox.Show("Fastighetsförsäkring har lagts till.", "Framgång", MessageBoxButton.OK, MessageBoxImage.Information);

                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ett fel uppstod vid tillägg av försäkringen: {ex.Message}", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }

        #endregion

        #region Validation IDataErrorInfo
        public string Error
        {
            get
            {
                string[] properties = { nameof(NewStartDate), nameof(NewEndDate), nameof(SelectedPaymentForm), nameof(CompanyAddress), nameof(CompanyZipcode), nameof(CompanyCity), nameof(ValueRealEstate)};
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
                case nameof(CompanyAddress):
                    if (string.IsNullOrWhiteSpace(CompanyAddress))
                    {
                        errorMessage = "Företagsadress måste anges";
                    }
                    break;
                case nameof(CompanyZipcode):
                    if (string.IsNullOrWhiteSpace(CompanyZipcode))
                    {
                        errorMessage = "Postnummer måste anges";
                    }
                    break;
                case nameof(CompanyCity):
                    if (string.IsNullOrWhiteSpace(CompanyCity))
                    {
                        errorMessage = "Stad måste anges"; 
                    }
                    break;
                case nameof(ValueRealEstate):
                    if (ValueRealEstate == 0)
                    {
                        errorMessage = "Värde fastigheter måste anges";
                    }
                    break;
                default:
                    errorMessage = "Vänligen välj";
                    break;
            }

            return errorMessage;
        }

        #region Clear Field Method
        private void ClearFields()
        {
            IsInventorySelected = false;
            SelectedCustomer = null;
            SearchBusinessCustomer = null;
            ValueRealEstate = 0;
            SelectedPaymentForm = 0;
            Note = string.Empty;
            CompanyAddress = string.Empty;
            NewStartDate = DateTime.Today;
            NewEndDate = DateTime.Today.AddYears(1);
            CompanyZipcode = string.Empty;
            CompanyCity = string.Empty;
            BusinessCustomers.Clear();
        }
        #endregion

        #endregion
    }

}
