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

        public List<string> AvailableMonthsYears { get; }

        public InvoiceVM()
        {
            NewInvoiceDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
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
            int currentYear = DateTime.Now.Year;
            AvailableMonthsYears = Enumerable.Range(0, 9)
                .Select(offset =>
                {
                    int month = (9 + offset) % 12;
                    int year = currentYear + (9 + offset) / 12;
                    return $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month == 0 ? 12 : month)} {year}";
                })
                .ToList();
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

        private string _selectedMonthYear;
        public string SelectedMonthYear
        {
            get { return _selectedMonthYear; }
            set
            {
                if (_selectedMonthYear != value)
                {
                    _selectedMonthYear = value;
                    OnPropertyChanged(nameof(SelectedMonthYear));
                    LoadInvoices();
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

            if (resultMessage.Contains("existerar redan"))
            {
                MessageBox.Show(resultMessage, "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (resultMessage.Contains("Inga fakturor"))
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

            if (resultMessage.Contains("existerar redan"))
            {
                MessageBox.Show(resultMessage, "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (resultMessage.Contains("Inga fakturor"))
            {
                MessageBox.Show(resultMessage, "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show(resultMessage, "Bekräftelse", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion

        #region Load Invoices
        public void LoadInvoices()
        {
            var invoiceDataList = invoiceController.LoadInvoicesFromJson();

            if (SelectedMonthYear != null)
            {
                var parts = SelectedMonthYear.Split(' ');
                if (parts.Length == 2 && int.TryParse(parts[1], out int year))
                {
                    int month = DateTime.ParseExact(parts[0], "MMMM", CultureInfo.CurrentCulture).Month;
                    DateTime startDate = new DateTime(year, month, 1);
                    DateTime endDate = startDate.AddMonths(1).AddDays(-1);

                    var filteredInvoices = invoiceDataList
                        .Where(i => DateTime.TryParse(i.InvoiceDate.ToString(), out DateTime invoiceDate) &&
                                    invoiceDate >= startDate &&
                                    invoiceDate <= endDate)
                        .ToList();

                    LoadedInvoices = new ObservableCollection<dynamic>(filteredInvoices);
                }
            }
            else
            {
                LoadedInvoices = new ObservableCollection<dynamic>(invoiceDataList);
            }
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
            var monthLastDay = new DateTime(NewInvoiceDate.Year, NewInvoiceDate.Month, DateTime.DaysInMonth(NewInvoiceDate.Year, NewInvoiceDate.Month));

            switch (columnName)
            {
                case nameof(NewInvoiceDate):
                    if (NewInvoiceDate != monthLastDay)
                    {
                        errorMessage = "Faktureringsdatumet måste vara den sista dagen i månaden.";
                    }
                    break;

                default:
                    errorMessage = "Ogiltig egenskap.";
                    break;
            }

            return errorMessage;
        }
        #endregion

    }
}