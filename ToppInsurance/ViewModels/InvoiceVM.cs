using ControlzEx.Standard;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TopInsuranceBL;
using TopInsuranceDL;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    public class InvoiceVM : ObservableObject, IDataErrorInfo
    {
        private BusinessController businessController;
        private PrivateController privateController;
        private InvoiceController invoiceController;


        public InvoiceVM()
        {
            NewInvoiceDate = DateTime.Today;
            businessController = new BusinessController();
            privateController = new PrivateController();
            invoiceController = new InvoiceController();

            List<BusinessCustomer> customers = businessController.GetAllBusinessCustomers();
            List<PrivateCustomer> privateCustomers = privateController.GetAllPrivateCustomers();

            FindPcustomersCommand = new RelayCommand(FindPcustomers);
            FindBCcustomersCommand = new RelayCommand(FindBCcustomers);
            AddPrivateInvoiceCommand = new RelayCommand(AddPrivateInvoice);
            AddBusinessInvoiceCommand = new RelayCommand(AddBusinessInvoice);
            BCcustomers = new ObservableCollection<BusinessCustomer>(customers);
            Pcustomers = new ObservableCollection<PrivateCustomer>(privateCustomers);

            //ClearCommand = new RelayCommand(ClearFields);

        }

        #region Properties for search

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

        #region Properties for selected customer

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
                    LoadInvoices();

                }
            }
        }

        #endregion

        #region Properties

        private DateTime _invoiceDate;
        public DateTime NewInvoiceDate
        {
            get { return _invoiceDate; }
            set
            {
                if (_invoiceDate != value)
                {
                    _invoiceDate = value;
                    OnPropertyChanged(nameof(NewInvoiceDate));

                }
            }
        }

        #endregion

        #region Add Invoice method 
        private void AddPrivateInvoice()
        {

            var resultMessage = invoiceController.CalculateCreatePrivateInvoiceDocuments(SelectPrivateCustomer, NewInvoiceDate);

            // Kontrollera om det returnerades ett felmeddelande eller om fakturan skapades framgångsrikt
            if (resultMessage.Contains("Inga fakturor"))
            {
                MessageBox.Show(resultMessage, "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show(resultMessage, "Bekräftelse", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddBusinessInvoice()
        {

            var resultMessage = invoiceController.CalculateCreateBusinessInvoiceDocuments(SelectBusinessCustomer, NewInvoiceDate);

            // Kontrollera om det returnerades ett felmeddelande eller om fakturan skapades framgångsrikt
            if (resultMessage.Contains("Inga fakturor"))
            {
                MessageBox.Show(resultMessage, "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show(resultMessage, "Bekräftelse", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #endregion

        public void LoadInvoices()
        {
            var invoiceDataList = invoiceController.LoadInvoicesFromJson();

            LoadedInvoices = new ObservableCollection<dynamic>(invoiceDataList);
        }


        #region Find customer Methods
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
            }
            else
            {
                BCcustomers = new ObservableCollection<BusinessCustomer>(filteredBusinessCustomers);
            }
        }

        private void FindPcustomers()
        {
            if (string.IsNullOrWhiteSpace(SearchPrivateCustomer))
            {
                MessageBox.Show("Sökning misslyckades. Ange söktext.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var filteredPrivateCustomers = privateController.SearchPrivateCustomers(SearchPrivateCustomer);

            if (filteredPrivateCustomers == null || !filteredPrivateCustomers.Any())
            {
                MessageBox.Show("Inga resultat hittades för den angivna söktexten.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Pcustomers = new ObservableCollection<PrivateCustomer>(filteredPrivateCustomers);
            }
        }
        #endregion


        #region Observable Collection
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

        private ObservableCollection<dynamic> _loadedInvoices;
        public ObservableCollection<dynamic> LoadedInvoices
        {
            get { return _loadedInvoices; }
            set
            {
                if (_loadedInvoices != value)
                {
                    _loadedInvoices = value;
                    OnPropertyChanged(nameof(LoadedInvoices));
                }
            }
        }
        #endregion

        #region Commands
        public ICommand FindBCcustomersCommand { get; }
        public ICommand FindPcustomersCommand { get; }
        public ICommand AddPrivateInvoiceCommand { get; }
        public ICommand AddBusinessInvoiceCommand { get; }
        public ICommand ClearCommand { get; }
        #endregion

        #region Validation IDataErrorInfo
        public string Error
        {
            get
            {
                string[] properties = { nameof(NewInvoiceDate) };
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
            var currentDate = DateTime.Today;

            switch (columnName)
            {

                case nameof(NewInvoiceDate):
                    if (NewInvoiceDate != currentDate)
                    {
                        errorMessage = "Provisionsunderlag kan endast generas till föregående månads sista dag";
                    }
                    break;
                default:
                    errorMessage = "Vänligen välj";
                    break;
            }

            return errorMessage;
        }
        #endregion

    }

}