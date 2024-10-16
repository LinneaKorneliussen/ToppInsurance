using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class CustomerProspectVM : ObservableObject
    {
        private BusinessController businessController;
        private PrivateController privateController;

        public ObservableCollection<Insurance> ActiveInsurances { get; set; }

        public CustomerProspectVM()
        {
            NewStartDate = DateTime.Now;
            businessController = new BusinessController();
            privateController = new PrivateController();

            FindPcustomersCommand = new RelayCommand(FindPcustomers);
            FindBCcustomersCommand = new RelayCommand(FindBCcustomers);

            FindPrivatCustomerProspect();
            FindBusinessCustomerProspect();

        }

        #region Search
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

        private string _searchPrivateCustomer;
        public string SearchPrivateCustomer
        {
            get { return _searchPrivateCustomer; }
            set
            {
                if (_searchPrivateCustomer != value)
                {
                    _searchPrivateCustomer = value;
                    OnPropertyChanged(nameof(SearchPrivateCustomer));
                }
            }
        }

        private void FindBCcustomers()
        {
            var filteredBusinessCustomers = businessController.SearchBusinessCustomers(SearchBusinessCustomer);

            BCcustomers = new ObservableCollection<BusinessCustomer>(filteredBusinessCustomers);
            SearchBusinessCustomer = string.Empty;
        }

        private void FindPcustomers()
        {
            var filteredPrivateCustomers = privateController.SearchPrivateCustomers(SearchPrivateCustomer);

            Pcustomers = new ObservableCollection<PrivateCustomer>(filteredPrivateCustomers);
            SearchPrivateCustomer = string.Empty;
        }

        #endregion

        #region Properties

        private BusinessCustomer _selectBusinessCustomer;
        public BusinessCustomer SelectBusinessCustomer
        {
            get { return _selectBusinessCustomer; }
            set
            {
                if (_selectBusinessCustomer != value)
                {
                    _selectBusinessCustomer = value;
                    OnPropertyChanged(nameof(SelectBusinessCustomer));
                }
            }
        }


        private PrivateCustomer _selectPrivateCustomer;
        public PrivateCustomer SelectPrivateCustomer
        {
            get { return _selectPrivateCustomer; }
            set
            {
                if (_selectPrivateCustomer != value)
                {
                    _selectPrivateCustomer = value;
                    OnPropertyChanged(nameof(SelectPrivateCustomer));
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

        private string _outcome;
        public string Outcome
        {
            get { return _outcome; }
            set
            {
                if (_outcome != value)
                {
                    _outcome = value;
                    OnPropertyChanged(nameof(Outcome));
                }
            }
        }
        #endregion

        #region Get all business and private customers
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

        private ObservableCollection<PrivateCustomer> _Pcustomers;
        public ObservableCollection<PrivateCustomer> Pcustomers
        {
            get { return _Pcustomers; }
            set
            {
                _Pcustomers = value;
                OnPropertyChanged(nameof(Pcustomers));
            }
        }
        #endregion

        #region Find method 
        private void FindPrivatCustomerProspect()
        {
            var privateProspects = privateController.GetPrivateCustomerProspects();
            Pcustomers = new ObservableCollection<PrivateCustomer>(privateProspects);
        }

        private void FindBusinessCustomerProspect()
        {
            var businessProspects = businessController.GetBusinessCustomerProspects();
            BCcustomers = new ObservableCollection<BusinessCustomer>(businessProspects);
        }

        #endregion

        #region Commands
        public ICommand ClearCommand { get; }
        public ICommand FindBCcustomersCommand { get; }
        public ICommand FindPcustomersCommand { get; }

        #endregion


    }
}
