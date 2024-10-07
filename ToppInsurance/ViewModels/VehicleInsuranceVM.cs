using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TopInsuranceBL;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    class VehicleInsuranceVM : ObservableObject
    {
        private BusinessController businessController;
        private VehicleController vehicleController;
        public IEnumerable<Paymentform> Paymentforms { get; }
        public IEnumerable<AdditionalInsurance> AdditionalInsurances { get; }
        public List<int> baseAmounts;
        private Employee user;

        public VehicleInsuranceVM()
        {
            user = UserContext.Instance.LoggedInUser;
            vehicleController = new VehicleController();
            Paymentforms = Enum.GetValues(typeof(Paymentform)) as IEnumerable<Paymentform>;
            AdditionalInsurances = Enum.GetValues(typeof(AdditionalInsurance)) as IEnumerable<AdditionalInsurance>;
            //Deductible = Enum.GetValues(typeof(Deductible)) as IEnumerable<Deductible>;
            //CoverageType = Enum.GetValues(typeof(CoverageType)) as IEnumerable<CoverageType>;
            //AddVehicleInsuranceCommand = new RelayCommand(AddVehicleInsurance);
            businessController = new BusinessController();
            List<BusinessCustomer> customers = businessController.GetAllBusinessCustomers();
            FindBCcustomersCommand = new RelayCommand(FindBCcustomers);
            BCcustomers = new ObservableCollection<BusinessCustomer>(customers);

        }

        #region Properties
        private PrivateCustomer _selectedBCcustomers;
        public PrivateCustomer SelectedBCcustomers
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

        private string _yearModel;
        public string YearModel
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

        //private Deductible _selectedDeductible;
        //public Deductible SelectedDeductible
        //{
        //    get { return _selectedDeductible; }
        //    set
        //    {
        //        if (_selectedDeductible != value)
        //        {
        //            _selectedDeductible = value;
        //            OnPropertyChanged(nameof(SelectedDeductible));
        //        }
        //    }
        //}

        //private CoverageType _selectedCoverageType;
        //public CoverageType SelectedCoverageType
        //{
        //    get { return _selectedCoverageType; }
        //    set
        //    {
        //        if (_selectedCoverageType != value)
        //        {
        //            _selectedCoverageType = value;
        //            OnPropertyChanged(nameof(SelectedCoverageType));
        //        }
        //    }
        //}
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

        #region Commands
        public ICommand AddVehicleInsuranceCommand { get; }
        public ICommand FindBCcustomersCommand { get; }
        #endregion

        #region Add vehicleinsurance Method
        //private void AddVehicleInsurance()
        //{
           
        //}

        #endregion

        #region Search business customers

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


        private void FindBCcustomers()
        {
            List<BusinessCustomer> businessCustomers = businessController.GetAllBusinessCustomers();

            if (string.IsNullOrWhiteSpace(SearchBusinessCustomer))
            {
                BCcustomers = new ObservableCollection<BusinessCustomer>(businessCustomers);
            }
            else
            {
                bool isNumber = int.TryParse(SearchBusinessCustomer, out int organizationalNumber);

                var filteredBusinessCustomers = businessCustomers
                    .Where(c =>
                        (isNumber && c.Organizationalnumber == organizationalNumber) ||
                        (!isNumber && c.CompanyName.Contains(SearchBusinessCustomer, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                BCcustomers = new ObservableCollection<BusinessCustomer>(filteredBusinessCustomers);
            }

            SearchBusinessCustomer = string.Empty;
        }

        #endregion

        #region Get all business customers
        private ObservableCollection<BusinessCustomer> _BCcustomers;
        public ObservableCollection<BusinessCustomer> BCcustomers
        {
            get { return _BCcustomers; }
            set
            {
                _BCcustomers = value;
                OnPropertyChanged(nameof(BCcustomers));
            }
        }

        #endregion

        #region Validation

        
        #endregion

    }
}
