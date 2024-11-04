using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using TopInsuranceBL;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    /// <summary>
    /// ViewModel for managing statistics in the Top Insurance WPF application.
    /// This class handles the display and processing of sales data for employees, including both private and 
    /// business insurance statistics. It initializes necessary controllers for data retrieval and binds the data 
    /// to visual elements like charts. The ViewModel supports commands to find employees, save sales data to 
    /// Excel, and load sales data based on user selections.
    /// The class maintains properties for employee selection, sales summaries and the current year, and it 
    /// validates user input. It also features observable collections for real-time updates in the UI and 
    /// retrieves data for visualization using LiveCharts.
    /// </summary>
    public class StatisticsVM : ObservableObject
    {
        private StatisticsController statisticsController;
        private EmployeeController employeeController;
        private Employee employee;
        public SeriesCollection BarPrivateSeriesCollection { get; set; }
        public SeriesCollection BarBusinessSeriesCollection { get; set; }
        public SeriesCollection LinePrivateSeriesCollection { get; set; }
        public SeriesCollection LineBusinessSeriesCollection { get; set; }
        public string[] Labels { get; set; }

        public StatisticsVM()
        {
            employee = UserContext.Instance.LoggedInUser;
            statisticsController = new StatisticsController();
            employeeController = new EmployeeController();
            FindEmployeeCommand = new RelayCommand(FindEmployee);
            SaveToExcelCommand = new RelayCommand(SaveToExcel);

            BarPrivateSeriesCollection = new SeriesCollection();
            BarBusinessSeriesCollection = new SeriesCollection();
            LinePrivateSeriesCollection = new SeriesCollection();
            LineBusinessSeriesCollection = new SeriesCollection();
            Labels = GetLastFiveMonths();
        }

        #region Properties
        public bool CanSaveToExcel
        {
            get
            {
                return employee.EmployeeRole == EmployeeRole.VD || employee.EmployeeRole == EmployeeRole.Försäljningschef;
            }
        }
        private string _searchEmployee;
        public string SearchEmployees
        {
            get { return _searchEmployee; }
            set
            {
                if (_searchEmployee != value)
                {
                    _searchEmployee = value;
                    OnPropertyChanged(nameof(SearchEmployees));
                }
            }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                if (_selectedEmployee != value)
                {
                    _selectedEmployee = value;
                    OnPropertyChanged(nameof(SelectedEmployee));
                    LoadSalesDataForEmployee();
                }
            }
        }

        private EmployeeSalesSummary _selectedEmployeeSummary;
        public EmployeeSalesSummary SelectedEmployeeSummary
        {
            get => _selectedEmployeeSummary;
            set
            {
                _selectedEmployeeSummary = value;
                OnPropertyChanged(nameof(SelectedEmployeeSummary));
            }
        }

        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set
            {
                _selectedTabIndex = value;
                OnPropertyChanged(nameof(SelectedTabIndex));
            }
        }

        private int _selectedYear;
        public int SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged(nameof(SelectedYear));
                    LoadSalesData(SelectedYear);
                }
            }
        }
        public List<int> AvailableYears { get; } = Enumerable.Range(DateTime.Now.Year - 5, 6).ToList();
        #endregion

        #region Observable collections
        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                if (_employees != value)
                {
                    _employees = value;
                    OnPropertyChanged(nameof(Employees));
                }
            }
        }

        private ObservableCollection<EmployeeSalesSummary> _employeesSalesData;
        public ObservableCollection<EmployeeSalesSummary> EmployeeSalesData
        {
            get { return _employeesSalesData; }
            set
            {
                if (_employeesSalesData != value)
                {
                    _employeesSalesData = value;
                    OnPropertyChanged(nameof(EmployeeSalesData));
                }
            }
        }
        #endregion

        #region Commands
        public ICommand FindEmployeeCommand { get; }
        public ICommand LoadSalesDataCommand { get; }
        public ICommand SaveToExcelCommand { get; }
        #endregion

        #region Find Employee Method
        private void FindEmployee()
        {
            if (!string.IsNullOrEmpty(SearchEmployees))
            {
                var filteredEmployees = employeeController.GetSalesEmployees(SearchEmployees);
                Employees = new ObservableCollection<Employee>(filteredEmployees);
                SearchEmployees = string.Empty;
            }
        }
        #endregion

        #region Load Sales Data for Employee Method
        private void LoadSalesDataForEmployee()
        {
            if (SelectedEmployee != null)
            {
                BarPrivateSeriesCollection.Clear();
                BarBusinessSeriesCollection.Clear();
                LinePrivateSeriesCollection.Clear();
                LineBusinessSeriesCollection.Clear();

                var (privateSalesData, businessSalesData) = statisticsController.GetSalesDataForEmployee(SelectedEmployee);

                foreach (var insuranceType in privateSalesData.Keys)
                {
                    var values = privateSalesData[insuranceType];

                    BarPrivateSeriesCollection.Add(new ColumnSeries
                    {
                        Title = insuranceType,
                        Values = new ChartValues<int>(values)
                    });
                    LinePrivateSeriesCollection.Add(new LineSeries
                    {
                        Title = insuranceType,
                        Values = new ChartValues<int>(values)
                    });
                }

                foreach (var insuranceType in businessSalesData.Keys)
                {
                    var values = businessSalesData[insuranceType];

                    BarBusinessSeriesCollection.Add(new ColumnSeries
                    {
                        Title = insuranceType,
                        Values = new ChartValues<int>(values)
                    });
                    LineBusinessSeriesCollection.Add(new LineSeries
                    {
                        Title = insuranceType,
                        Values = new ChartValues<int>(values)
                    });
                }
            }
        }
        #endregion

        #region Helper Method for Last Five Months
        private string[] GetLastFiveMonths()
        {
            var months = new string[5];
            DateTimeFormatInfo dtInfo = new CultureInfo("sv-SE", false).DateTimeFormat;
            DateTime currentDate = DateTime.Now;

            for (int i = 0; i < 5; i++)
            {
                DateTime month = currentDate.AddMonths(-i);
                months[4 - i] = dtInfo.GetMonthName(month.Month);
            }

            return months;
        }
        #endregion

        #region Load Sales Data for Selected Year Method
        public void LoadSalesData(int selectedYear)
        {
            var salesData = statisticsController.GetSalesDataForAllEmployees(selectedYear);
            EmployeeSalesData = new ObservableCollection<EmployeeSalesSummary>(salesData);
        }
        #endregion

        #region Save To Excel Method
        private void SaveToExcel()
        {
            if (SelectedYear > 0)
            {
                MessageBoxResult result = MessageBox.Show($"Är du säker på att du vill exportera försäljningsdata till Excel för året {SelectedYear}?",
                                                          "Bekräfta Export",
                                                          MessageBoxButton.OKCancel,
                                                          MessageBoxImage.Question);

                if (result == MessageBoxResult.OK)
                {
                    statisticsController.SaveSalesDataToExcel(SelectedYear);
                    MessageBox.Show("Excel-exporten har initierats för året: " + SelectedYear);
                }
                else
                {
                    MessageBox.Show("Exporten avbröts.");
                }
            }
            else
            {
                MessageBox.Show("Vänligen välj ett giltigt år.");
            }
        }
        #endregion

    }
}
