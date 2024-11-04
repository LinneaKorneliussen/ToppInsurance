using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TopInsuranceBL;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;


namespace TopInsuranceWPF.ViewModels
{
    // The InsuranceOverviewVM class serves as a ViewModel for the insurance overview feature in a WPF application. 
    // It manages the interaction between the view and the underlying data by utilizing controllers for private and business customers. 
    // The class implements ICommand for searching and clearing customer data, allowing users to find private and business customers through a search functionality. 
    // It contains properties and observable collections for both customer types, including their respective insurance types, and handles data updates when selections change. 
    // The Clear method resets the search fields and customer selections to their initial states. 
    // Validation is performed to ensure that search queries are not empty before executing search commands.

    public class InsuranceOverviewVM : ObservableObject
    {
        private InsuOverviewController insuOverviewController;
        private PrivateController privateController;
        private BusinessController businessController;

        public InsuranceOverviewVM()
        {
            insuOverviewController = new InsuOverviewController();
            privateController = new PrivateController();
            businessController = new BusinessController();
            FindPcustomersCommand = new RelayCommand(FindPcustomers);
            FindBCcustomersCommand = new RelayCommand(FindBCcustomers);
            ClearCommand = new RelayCommand(Clear);

        }


        #region Properties and observable collections for private customer
        private string _searchPrivateCustomer;
        public string SearchPrivateCustomers
        {
            get { return _searchPrivateCustomer; }
            set
            {
                if (_searchPrivateCustomer != value)
                {
                    _searchPrivateCustomer = value;
                    OnPropertyChanged(nameof(SearchPrivateCustomers));
                }
            }
        }

        private PrivateCustomer _selectedPcustomers;
        public PrivateCustomer SelectedPcustomers
        {
            get { return _selectedPcustomers; }
            set
            {
                if (_selectedPcustomers != value)
                {
                    _selectedPcustomers = value;
                    OnPropertyChanged(nameof(SelectedPcustomers));
                    DataGridsPrivate();
                }
            }
        }

        private ObservableCollection<PrivateCustomer> _pCustomers;
        public ObservableCollection<PrivateCustomer> Pcustomers
        {
            get { return _pCustomers; }
            set
            {
                if (_pCustomers != value)
                {
                    _pCustomers = value;
                    OnPropertyChanged(nameof(Pcustomers));
                }
            }
        }

        private LifeInsurance _lifeInsurance;
        public LifeInsurance LifeInsurance
        {
            get { return _lifeInsurance; }
            set
            {
                if (_lifeInsurance != value)
                {
                    _lifeInsurance = value;
                    OnPropertyChanged(nameof(LifeInsurance));
                }
            }
        }


        private ObservableCollection<SicknessAccidentInsurance> _sicknessInsurances;
        public ObservableCollection<SicknessAccidentInsurance> SicknessInsurances
        {
            get { return _sicknessInsurances; }
            set
            {
                if (_sicknessInsurances != value)
                {
                    _sicknessInsurances = value;
                    OnPropertyChanged(nameof(SicknessInsurances));
                }
            }
        }
        #endregion
        
        #region Properties and observale collections for Business customer
        private string _searchBusinessCustomer;
        public string SearchBusinessCustomers
        {
            get { return _searchBusinessCustomer; }
            set
            {
                if (_searchBusinessCustomer != value)
                {
                    _searchBusinessCustomer = value;
                    OnPropertyChanged(nameof(SearchBusinessCustomers));
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
                    DataGridsBusiness();
                }
            }
        }

        private ObservableCollection<BusinessCustomer> _bcCustomers;
        public ObservableCollection<BusinessCustomer> BCcustomers
        {
            get { return _bcCustomers; }
            set
            {
                if (_bcCustomers != value)
                {
                    _bcCustomers = value;
                    OnPropertyChanged(nameof(BCcustomers));
                }
            }
        }
        private ObservableCollection<LiabilityInsurance> _liabilityInsurances;
        public ObservableCollection<LiabilityInsurance> LiabilityInsurances
        {
            get { return _liabilityInsurances; }
            set
            {
                if (_liabilityInsurances != value)
                {
                    _liabilityInsurances = value;
                    OnPropertyChanged(nameof(LiabilityInsurances));
                }
            }
        }
        private ObservableCollection<VehicleInsurance> _vehicleInsurances;
        public ObservableCollection<VehicleInsurance> VehicleInsurances
        {
            get { return _vehicleInsurances; }
            set
            {
                if (_vehicleInsurances != value)
                {
                    _vehicleInsurances = value;
                    OnPropertyChanged(nameof(VehicleInsurances));
                }
            }
        }
        private ObservableCollection<RealEstateInsurance> _realestateInsurances;
        public ObservableCollection<RealEstateInsurance> RealEstateInsurances
        {
            get { return _realestateInsurances; }
            set
            {
                if (_realestateInsurances != value)
                {
                    _realestateInsurances = value;
                    OnPropertyChanged(nameof(RealEstateInsurances));
                }
            }
        }
        #endregion

        #region ICommands 
        public ICommand FindPcustomersCommand { get; }
        public ICommand FindBCcustomersCommand { get; }
        public ICommand ClearCommand { get; }
        #endregion

        #region Find customer Methods
        private void FindPcustomers()
        {
            if (string.IsNullOrWhiteSpace(SearchPrivateCustomers))
            {
                MessageBox.Show("Sökning misslyckades. Ange söktext.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var filteredPrivateCustomers = privateController.SearchPrivateCustomers(SearchPrivateCustomers);

            if (filteredPrivateCustomers == null || !filteredPrivateCustomers.Any())
            {
                MessageBox.Show("Inga resultat hittades för den angivna söktexten.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Pcustomers = new ObservableCollection<PrivateCustomer>(filteredPrivateCustomers);
            }
        }
        private void FindBCcustomers()
        {
            if (string.IsNullOrWhiteSpace(SearchBusinessCustomers))
            {
                MessageBox.Show("Sökning misslyckades. Ange söktext.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var filteredBusinessCustomers = businessController.SearchBusinessCustomers(SearchBusinessCustomers);

            if (filteredBusinessCustomers == null || !filteredBusinessCustomers.Any())
            {
                MessageBox.Show("Inga resultat hittades för den angivna söktexten.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                BCcustomers = new ObservableCollection<BusinessCustomer>(filteredBusinessCustomers);
            }
        }
        #endregion

        #region Data for grids Methods
        private void DataGridsPrivate()
        {
            if (SelectedPcustomers != null)
            {
                var lifeinsurance = insuOverviewController.GetLifeInsurance(SelectedPcustomers);
                LifeInsurance = lifeinsurance;

                var sicknessAccidentInsurances = insuOverviewController.GetAllSicknessInsurances(SelectedPcustomers);
                SicknessInsurances = new ObservableCollection<SicknessAccidentInsurance>(sicknessAccidentInsurances);
            }
        }
        private void DataGridsBusiness()
        {
            if (SelectedBCcustomer != null)
            {
                var liabilityInsurance = insuOverviewController.GetAllLiabilityInsurances(SelectedBCcustomer);
                LiabilityInsurances = new ObservableCollection<LiabilityInsurance>(liabilityInsurance);

                var vehicleInsurances = insuOverviewController.GetAllVehicleInsurances(SelectedBCcustomer);
                VehicleInsurances = new ObservableCollection<VehicleInsurance>(vehicleInsurances);

                var realEstateInsurances = insuOverviewController.GetAllRealEstateInsurances(SelectedBCcustomer);
                RealEstateInsurances = new ObservableCollection<RealEstateInsurance>(realEstateInsurances);
            }
        }
        #endregion

        #region Clear view Method
        private void Clear()
        {
            SearchPrivateCustomers = string.Empty;
            SearchBusinessCustomers = string.Empty;

            SelectedPcustomers = null;
            SelectedBCcustomer = null;
            Pcustomers = new ObservableCollection<PrivateCustomer>();
            BCcustomers = new ObservableCollection<BusinessCustomer>();

            LifeInsurance = null;
            SicknessInsurances = new ObservableCollection<SicknessAccidentInsurance>();
            LiabilityInsurances = new ObservableCollection<LiabilityInsurance>();
            VehicleInsurances = new ObservableCollection<VehicleInsurance>();
            RealEstateInsurances = new ObservableCollection<RealEstateInsurance>();

        }
        #endregion
    }
}
