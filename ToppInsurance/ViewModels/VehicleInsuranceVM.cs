using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
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
        private BusinessController businessController;
        private Employee user;

        public IEnumerable<Paymentform> Paymentforms { get; }
        public List<int> DeductibleVehicle { get; }
        public IEnumerable<CoverageType> CoverageType { get; }

        public VehicleInsuranceVM()
        {
            NewStartDate = DateTime.Today;
            NewEndDate = DateTime.Today.AddYears(1);
            user = UserContext.Instance.LoggedInUser;
            vehicleController = new VehicleController();
            businessController = new BusinessController();
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

        private string _registrationNumber;
        public string RegistrationNumber
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

                string error = this.Error;
                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show(error, "Valideringsfel", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!IsValidRegistrationNumber(RegistrationNumber))
                {
                    MessageBox.Show("Ogiltigt registreringsnummer. Vänligen kontrollera att det är i formaten ABC123 eller ABC12A.", "Valideringsfel", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                RiskZone selectedRiskZone = cityRiskZoneManager.GetRiskZoneByCity(SelectedCity);

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

        #region Validation method
        private bool IsValidRegistrationNumber(string registrationNumber)
        {
            string pattern = @"^[A-Za-z]{3}\d{3}$|^[A-Za-z]{3}\d{2}[A-Za-z]{1}$";
            return Regex.IsMatch(registrationNumber, pattern);
        }
        #endregion

        #region Search business customers
        private void FindBCcustomers()
        {
            if (string.IsNullOrWhiteSpace(SearchBusinessCustomer))
            {
                MessageBox.Show("Sökning misslyckades. Ange söktext.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var filteredBusinessCustomers = businessController.SearchBusinessCustomers(SearchBusinessCustomer);

            if (filteredBusinessCustomers == null || !filteredBusinessCustomers.Any())
            {
                MessageBox.Show("Inga resultat hittades för den angivna söktexten.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                BusinessCustomers = new ObservableCollection<BusinessCustomer>();
            }
            else
            {
                BusinessCustomers = new ObservableCollection<BusinessCustomer>(filteredBusinessCustomers);
            }

        }
        #endregion

        #region Validation IDataErrorInfo
        public string Error
        {
            get
            {
                string[] properties = { nameof(NewStartDate), nameof(NewEndDate), nameof(SelectedPaymentForm), nameof(RegistrationNumber),
                    nameof(Brand), nameof(YearModel), nameof(SelectedCity), nameof(SelectedDeductible), nameof(SelectedCoverageType)};
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
                        errorMessage = "Vänligen välj ett betalningssätt";
                    }
                    break;
                case nameof(RegistrationNumber):
                    if (string.IsNullOrWhiteSpace(RegistrationNumber))
                    {
                        errorMessage = "Ange fordonets reg.nr";
                    }
                    break;
                case nameof(Brand):
                    if (string.IsNullOrWhiteSpace(Brand))
                    {
                        errorMessage = "Ange fordonsmärke";
                    }
                    break;
                case nameof(YearModel):
                    if (YearModel <= 0)
                    {
                        errorMessage = "Ange fordonets årsmodell";
                    }
                    break;
                case nameof(SelectedCity):
                    if (string.IsNullOrWhiteSpace(SelectedCity))
                    {
                        errorMessage = "Ange fordonets primära hemmastad";
                    }
                    break;
                case nameof(SelectedDeductible):
                    if (SelectedDeductible == 0)
                    {
                        errorMessage = "Vänligen välj en självrisk";
                    }
                    break;
                case nameof(SelectedCoverageType):
                    if (SelectedCoverageType == 0)
                    {
                        errorMessage = "Vänligen välj en trafikförsäkring";
                    }
                    break;
                default:
                    errorMessage = "Vänligen välj";
                    break;
            }

            return errorMessage;
        }
    
        #endregion

        #region Clear Fields Method
        private void ClearFields()
        {
            SelectedBCcustomer = null;
            RegistrationNumber = string.Empty;
            Brand = string.Empty;
            YearModel = 0;
            SelectedDeductible = 0;
            SelectedCoverageType = 0;
            SelectedPaymentForm = 0;
            Note = string.Empty;
            SelectedCity = null;
            NewStartDate = DateTime.Now;
            NewEndDate = DateTime.Now.AddYears(1);
        }
        #endregion
    }

}
