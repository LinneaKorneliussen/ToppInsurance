using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using TopInsuranceBL;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    public class VehicleInsuranceVM : ObservableObject, IDataErrorInfo
    {
        private VehicleController vehicleController;
        private CityRiskZoneManager cityRiskZoneManager;
        private Employee user;

        public IEnumerable<Paymentform> Paymentforms { get; }
        public List<int> DeductibleVehicle { get; }
        public IEnumerable<CoverageType> CoverageType { get; }

        public VehicleInsuranceVM()
        {
            NewStartDate = DateTime.Now;
            NewEndDate = DateTime.Now.AddYears(1);
            user = UserContext.Instance.LoggedInUser;
            vehicleController = new VehicleController();
            cityRiskZoneManager = new CityRiskZoneManager();
            Cities = new ObservableCollection<string>(cityRiskZoneManager.CityRiskZones.Keys);

            Paymentforms = Enum.GetValues(typeof(Paymentform)).Cast<Paymentform>();
            DeductibleVehicle = Enum.GetValues(typeof(DeductibleVehicle)).Cast<DeductibleVehicle>().Select(d => (int)d).ToList();
            CoverageType = Enum.GetValues(typeof(CoverageType)).Cast<CoverageType>();
            AddVehicleInsuranceCommand = new RelayCommand(AddVehicleInsurance);
            FindBCcustomersCommand = new RelayCommand(FindBCcustomers);
            ClearCommand = new RelayCommand(ClearFields);
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

        private BusinessCustomer _selectedBCcustomer;
        public BusinessCustomer SelectedBCcustomer
        {
            get { return _selectedBCcustomer; }
            set
            {
                if (_selectedBCcustomer != value)
                {
                    _selectedBCcustomer = value;
                    OnPropertyChanged(nameof(SelectedBCcustomer));
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

        private int _selectedDeductible;
        public int SelectedDeductible
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
        public ICommand ClearCommand { get; }
        #endregion

        #region Add Vehicle Insurance Method
        private void AddVehicleInsurance()
        {
            try
            {
                if (SelectedBCcustomer == null)
                {
                    MessageBox.Show("Vänligen välj en kund från listan.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (SelectedPaymentForm == 0)
                {
                    MessageBox.Show("Vänligen välj en betalningsform.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(SelectedCity))
                {
                    MessageBox.Show("Vänligen välj en stad.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                RiskZone selectedRiskZone = cityRiskZoneManager.GetRiskZoneByCity(SelectedCity);

                if (string.IsNullOrEmpty(Brand) || RegistrationNumber <= 0 || YearModel <= 0)
                {
                    MessageBox.Show("Vänligen ange giltiga registreringsnummer, varumärke och årsmodell.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Vehicle vehicle = new Vehicle(RegistrationNumber, Brand, YearModel);

                DeductibleVehicle deductible = (DeductibleVehicle)SelectedDeductible;

                vehicleController.AddVehicleInsurance(
                    SelectedBCcustomer, vehicle, deductible, SelectedCoverageType,
                    selectedRiskZone, NewStartDate, NewEndDate,
                    InsuranceType.Fordonsförsäkring, SelectedPaymentForm, Note, user);

                MessageBox.Show("Fordonsförsäkringen har lagts till framgångsrikt.", "Bekräftelse", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearFields();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ett oväntat fel inträffade: " + ex.Message, "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Search business customers
        private void FindBCcustomers()
        {
            if (SearchBusinessCustomer != null)
            {
                var filteredCustomers = vehicleController.SearchBusinessCustomer(SearchBusinessCustomer);
                BusinessCustomers = new ObservableCollection<BusinessCustomer>(filteredCustomers);
                SearchBusinessCustomer = string.Empty;
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
            string error = string.Empty;

            if (columnName == nameof(NewStartDate) && NewStartDate < DateTime.Today)
            {
                error = "Startdatum kan inte vara i det förflutna.";
            }

            if (columnName == nameof(NewEndDate) && NewEndDate < NewStartDate)
            {
                error = "Slutdatum kan inte vara tidigare än startdatum.";
            }

            return error;
        }
        #endregion

        #region Clear Fields Method
        private void ClearFields()
        {
            SelectedBCcustomer = null;
            RegistrationNumber = 0;
            Brand = string.Empty;
            YearModel = 0;
            SelectedDeductible = 0;
            SelectedCoverageType = 0;
            SelectedPaymentForm = 0;
            Note = string.Empty;
            SelectedCity = null;
            NewStartDate = DateTime.Now;
            NewEndDate = DateTime.Now.AddDays(365);
        }
        #endregion
    }



}
