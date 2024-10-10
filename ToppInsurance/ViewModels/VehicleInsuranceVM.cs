using System.Collections.ObjectModel;
using System.Windows.Input;
using TopInsuranceBL;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    class VehicleInsuranceVM : ObservableObject
    {
        private VehicleController vehicleController;
        private CityRiskZoneManager cityRiskZoneManager;
        private Employee user;

        public IEnumerable<Paymentform> Paymentforms { get; }
        public IEnumerable<DeductibleVehicle> DeductibleVehicle { get; }
        public IEnumerable<CoverageType> CoverageType { get; }

        public VehicleInsuranceVM()
        {
            user = UserContext.Instance.LoggedInUser;
            vehicleController = new VehicleController();
            cityRiskZoneManager = new CityRiskZoneManager();
            Cities = new ObservableCollection<string>(cityRiskZoneManager.CityRiskZones.Keys);

            Paymentforms = Enum.GetValues(typeof(Paymentform)) as IEnumerable<Paymentform>;
            DeductibleVehicle = Enum.GetValues(typeof(DeductibleVehicle)) as IEnumerable<DeductibleVehicle>;
            CoverageType = Enum.GetValues(typeof(CoverageType)) as IEnumerable<CoverageType>;
            AddVehicleInsuranceCommand = new RelayCommand(AddVehicleInsurance);
            FindBCcustomersCommand = new RelayCommand(FindBCcustomers);

        }

        #region Properties

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
        private BusinessCustomer _selectedBCcustomers;
        public BusinessCustomer SelectedBCcustomers
        {
            get { return _selectedBCcustomers; }
            set
            {
                if (_selectedBCcustomers != value)
                {
                    _selectedBCcustomers = value;
                    OnPropertyChanged(nameof(SelectedBCcustomers));
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
        private DeductibleVehicle _selectedDeductible;
        public DeductibleVehicle SelectedDeductible
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

        private CoverageType _selectedCoverageType;
        public CoverageType SelectedCoverageType
        {
            get { return _selectedCoverageType; }
            set
            {
                if (_selectedCoverageType != value)
                {
                    _selectedCoverageType = value;
                    OnPropertyChanged(nameof(SelectedCoverageType));
                }
            }
        }

        private string _selectedCity;
        public string SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                if (_selectedCity != value)
                {
                    _selectedCity = value;
                    OnPropertyChanged(nameof(SelectedCity));
                }
            }
        }

        private int _registrationNumber;
        public int RegistrationNumber
        {
            get { return _registrationNumber; }
            set
            {
                if (_registrationNumber != value)
                {
                    _registrationNumber = value;
                    OnPropertyChanged(nameof(RegistrationNumber));
                }
            }
        }

        private string _brand;
        public string Brand
        {
            get { return _brand; }
            set
            {
                if (_brand != value)
                {
                    _brand = value;
                    OnPropertyChanged(nameof(Brand));
                }
            }
        }

        private int _yearModel;
        public int YearModel
        {
            get { return _yearModel; }
            set
            {
                if (_yearModel != value)
                {
                    _yearModel = value;
                    OnPropertyChanged(nameof(YearModel));
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
        private ObservableCollection<string> _cities;
        public ObservableCollection<string> Cities
        {
            get { return _cities; }
            set
            {
                if (_cities != value)
                {
                    _cities = value;
                    OnPropertyChanged(nameof(Cities));
                }
            }
        }
        #endregion

        #region Commands
        public ICommand AddVehicleInsuranceCommand { get; }
        public ICommand FindBCcustomersCommand { get; }
        #endregion

        #region Add vehicleinsurance Method
        private void AddVehicleInsurance()
        {
            RiskZone selectedRiskZone = cityRiskZoneManager.GetRiskZoneByCity(SelectedCity);


            Vehicle vehicle = new Vehicle(RegistrationNumber, Brand, YearModel);

            if (vehicle != null)
            {
                vehicleController.AddVehicleInsurance(SelectedBCcustomers, vehicle, SelectedDeductible, SelectedCoverageType, selectedRiskZone, NewStartDate,
               NewEndDate, InsuranceType.Fordonsförsäkring, SelectedPaymentForm, Note, user);

            }

        }

        #endregion

        #region Search business customers
        private void FindBCcustomers()
        {
            var filteredCustomers = vehicleController.SearchBusinessCustomer(SearchBusinessCustomer);

            BusinessCustomers = new ObservableCollection<BusinessCustomer>(filteredCustomers);
            SearchBusinessCustomer = string.Empty;
        }

        #endregion


        #region Validation


        #endregion

    }
}
