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
            List<BusinessCustomer> customers = businessController.GetAllBusinessCustomers();
            List<PrivateCustomer> privateCustomers = privateController.GetAllPrivateCustomers();
            FindPcustomersCommand = new RelayCommand(FindPCcustomers);
            FindBCcustomersCommand = new RelayCommand(FindBCcustomers);
            BCcustomers = new ObservableCollection<BusinessCustomer>(customers);



            Pcustomers = new ObservableCollection<PrivateCustomer>();

            //ClearCommand = new RelayCommand(ClearFields);

        }



        #region Search properties

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

        #endregion

        #region Properties

        private string _selectBusinessCustomer;
        public string SelectBusinessCustomer
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


        private string _selectPrivateCustomer;
        public string SelectPrivateCustomer
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

        private string _salesperson;
        public string Salesperson
        {
            get { return _salesperson; }
            set
            {
                if (_salesperson != value)
                {
                    _salesperson = value;
                    OnPropertyChanged(nameof(Salesperson));
                }
            }
        }

        private int _agencyNumber;
        public int AgencyNumber
        {
            get { return _agencyNumber; }
            set
            {
                if (_agencyNumber != value)
                {
                    _agencyNumber = value;
                    OnPropertyChanged(nameof(AgencyNumber));
                }
            }
        }
        #endregion

        #region Find method 

        private void FindBCcustomers()
        {
            var filteredBusinessCustomers = businessController.SearchBusinessCustomer(SearchBusinessCustomer);

            BCcustomers = new ObservableCollection<BusinessCustomer>(filteredBusinessCustomers);
            SearchBusinessCustomer = string.Empty;
        }

        //private void FindPCcustomers()
        //{
        //    var filteredPrivateCustomers = privateController.SearchPrivateCustomer(SearchPrivateCustomer);

        //    // Kontrollera att vi har några filtrerade kunder
        //    if (filteredPrivateCustomers != null && filteredPrivateCustomers.Any())
        //    {
        //        // Filtrera kunder som har aktiva försäkringar
        //        var customersWithActiveInsurance = filteredPrivateCustomers

        //            .Where(customer => customer.LifeInsurance == null).ToList();

        //        // Skapa en ObservableCollection av de filtrerade kunderna
        //        Pcustomers = new ObservableCollection<PrivateCustomer>(customersWithActiveInsurance);
        //    }
        //    else
        //    {
        //        // Om inga kunder hittas, sätt Pcustomers till en tom ObservableCollection
        //        Pcustomers = new ObservableCollection<PrivateCustomer>();

        //        Console.WriteLine("Inga kunder hittades.");

        //    }

        //    // Rensa sökterm
        //    SearchPrivateCustomer = string.Empty;

        //}



        //(customer.SicknessAndAccidentInsurances != null && customer.SicknessAndAccidentInsurances.Count > 0) ||
        //(customer.LifeInsurance != null))


        //TILL SENARE 
        private void FindPCcustomers()
        {
            var filteredPrivateCustomers = privateController.SearchPrivateCustomer(SearchPrivateCustomer);

            Pcustomers = new ObservableCollection<PrivateCustomer>(filteredPrivateCustomers);
            SearchPrivateCustomer = string.Empty;
        }

        #endregion

        #region Commands
        public ICommand FindBCcustomersCommand { get; }
        public ICommand FindPcustomersCommand { get; }
        public ICommand ClearCommand { get; }

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
    }
}
