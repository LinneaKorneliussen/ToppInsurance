using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using TopInsuranceBL;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    class RealEstateInsuranceVM : ObservableObject /* IDataErrorInfo*/
    {
        private RealEstateController realEstateController;
        private BusinessController businessController;
        List<Inventory> inventories = new List<Inventory>();



        public IEnumerable<Paymentform> Paymentforms { get; }

        private Employee user;

        public RealEstateInsuranceVM()
        {
            NewStartDate = DateTime.Now;
            NewEndDate = DateTime.Now.AddYears(1);
            user = UserContext.Instance.LoggedInUser;
            realEstateController = new RealEstateController();

            Paymentforms = Enum.GetValues(typeof(Paymentform)) as IEnumerable<Paymentform>;

            Inventories = new ObservableCollection<Inventory>();
            AddInventoryCommand = new RelayCommand(AddInventory);

            FindCustomerCommand = new RelayCommand(FindCustomer);
            //AddInventoryCommand = new RelayCommand(AddRealEstateInsurance);

            RemoveInventoryCommand = new RelayCommand<Inventory>(RemoveInventory);
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

        private int _valueRealEstate;
        public int ValueRealEstate
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

        private int _valueInventory;
        public int ValueInventory
        {
            get { return _valueInventory; }
            set
            {
                if (_valueInventory != value)
                {
                    _valueInventory = value;
                    OnPropertyChanged(nameof(ValueInventory));
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

        
        #endregion

        #region Observable collection 

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
        #endregion

        #region Commands 
        public ICommand FindCustomerCommand { get; }

        public ICommand AddInventoryCommand { get; }

        public ICommand RemoveInventoryCommand { get; }
        public ICommand ClearFieldsCommand { get; }


        #endregion

        #region Find Customer Method
        private void FindCustomer()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var filteredBusinessCustomers = realEstateController.SearchBusinessCustomer(SearchText);
                BusinessCustomers = new ObservableCollection<BusinessCustomer>(filteredBusinessCustomers);
            }
            else
            {
                MessageBox.Show("Sökning misslyckades. Ange söktext.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                BusinessCustomers = new ObservableCollection<BusinessCustomer>();
            }
        }
        #endregion

       
        #region Inventory methods
        private void AddInventory()
        {
            Inventories.Add(new Inventory(0, 0));
        }

        private void RemoveInventory(Inventory inventory)
        {
            // Ta bort vald inventarie
            if (Inventories.Contains(inventory))
            {
                Inventories.Remove(inventory);
            }
        }

        #endregion

        #region Validation

        //private string ValidateField(string columnName)
        //{
        //    string errorMessage = null;

        //    switch (columnName)
        //    {
        //        case nameof(NewStartDate):
        //            if (NewStartDate < DateTime.Today)
        //            {
        //                errorMessage = "Går inte att teckna en försäkring för redan passerade datum";
        //            }
        //            break;
        //        case nameof(NewEndDate):
        //            if (NewEndDate < DateTime.Today)
        //            {
        //                errorMessage = "Går inte att teckna försäkring för redan passerade datum";
        //            }
        //            else if (NewEndDate < NewStartDate)
        //            {
        //                errorMessage = "Slutdatum kan inte vara före startdatum.";
        //            }
        //            break;
        //        case nameof(SelectedPaymentForm):
        //            if (SelectedPaymentForm.ToString() == "Vänligen välj")
        //            {
        //                errorMessage = "Betalningsform måste väljas.";
        //            }
        //            break;
        //        default:
        //            errorMessage = "Ogiltigt kolumnnamn.";
        //            break;
        //    }

        //    return errorMessage;
        //}
        //public string this[string columnName]
        //{
        //    get
        //    {
        //        return ValidateField(columnName);
        //    }
        //}

        //private bool ValidateNumericFields(out int zipcode)
        //{
        //    zipcode = 0;

        //    if (!int.TryParse(CompanyZipcode, out zipcode))
        //    {
        //        MessageBox.Show("Postnumret måste vara ett giltigt nummer.");
        //        return false;
        //    }

        //    return true;
        //}
        #endregion

    }
}
