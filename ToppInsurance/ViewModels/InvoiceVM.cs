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

        private BusinessCustomer _selectedBusinessCustomer;
        public BusinessCustomer SelectedBusinessCustomer
        {
            get { return _selectedBusinessCustomer; }
            set
            {
                if (_selectedBusinessCustomer != value)
                {
                    _selectedBusinessCustomer = value;
                    OnPropertyChanged(nameof(SelectedBusinessCustomer));
                    LoadInvoices();

                }

            }
        }

        private PrivateCustomer _selectedPrivateCustomer;
        public PrivateCustomer SelectedPrivateCustomer
        {
            get { return _selectedPrivateCustomer; }
            set
            {
                if (_selectedPrivateCustomer != value)
                {
                    _selectedPrivateCustomer = value;
                    OnPropertyChanged(nameof(SelectedPrivateCustomer));
                    LoadInvoices();

                }
            }
        }

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

        #region Add Invoice methods
        private void AddPrivateInvoice()
        {
            if (SelectedPrivateCustomer == null)
            {
                MessageBox.Show("Vänligen välj en privatkund för att fortsätta", "Valideringsfel", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!string.IsNullOrEmpty(Error))
            {
                MessageBox.Show(Error, "Valideringsfel", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var resultMessage = invoiceController.CalculateCreatePrivateInvoiceDocuments(SelectedPrivateCustomer, NewInvoiceDate);

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
            if (SelectedBusinessCustomer == null)
            {
                MessageBox.Show("Vänligen välj en företagskund för att fortsätta", "Valideringsfel", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!string.IsNullOrEmpty(Error))
            {
                MessageBox.Show(Error, "Valideringsfel", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var resultMessage = invoiceController.CalculateCreateBusinessInvoiceDocuments(SelectedBusinessCustomer, NewInvoiceDate);

            if (resultMessage.Contains("Inga fakturor"))
            {
                MessageBox.Show(resultMessage, "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show(resultMessage, "Bekräftelse", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void LoadInvoices()
        {
            var invoiceDataList = invoiceController.LoadInvoicesFromJson();

            LoadedInvoices = new ObservableCollection<dynamic>(invoiceDataList);
        }

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
            var earlierDate = DateTime.Today.AddMonths(-1);

            switch (columnName)
            {
                case nameof(NewInvoiceDate):
                    if (NewInvoiceDate < earlierDate)
                    {
                        errorMessage = "Faktureringsunderlag kan maximalt genereras en månad tillbaka i tiden";
                    }
                    break;

                default:
                    errorMessage = "Ogiltig egenskap";
                    break;
            }

            return errorMessage;
        }
        #endregion

    }
}