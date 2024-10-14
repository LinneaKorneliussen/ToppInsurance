using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using TopInsuranceBL;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    public class StatisticsVM : ObservableObject
    {
        private StatisticsController statisticsController;
        private EmployeeController employeeController;
        public SeriesCollection BarPrivateSeriesCollection { get; set; }
        public SeriesCollection BarBusinessSeriesCollection { get; set; }
        public SeriesCollection LinePrivateSeriesCollection { get; set; }
        public SeriesCollection LineBusinessSeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public StatisticsVM()
        {
            statisticsController = new StatisticsController();
            employeeController = new EmployeeController();
            FindEmployeeCommand = new RelayCommand(FindEmployee);

            BarPrivateSeriesCollection = new SeriesCollection();
            BarBusinessSeriesCollection = new SeriesCollection();
            LinePrivateSeriesCollection = new SeriesCollection();
            LineBusinessSeriesCollection = new SeriesCollection();

            Labels = GetLastFiveMonths();
            Formatter = value => ((int)value).ToString();
        }

        #region Properties
        private string _searchEmployee;
        public string SearchEmployee
        {
            get { return _searchEmployee; }
            set
            {
                if (_searchEmployee != value)
                {
                    _searchEmployee = value;
                    OnPropertyChanged(nameof(SearchEmployee));
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
        #endregion

        #region Commands
        public ICommand FindEmployeeCommand { get; }
        #endregion

        #region Find Employee Method
        private void FindEmployee()
        {
            if (!string.IsNullOrEmpty(SearchEmployee))
            {
                var filteredEmployees = employeeController.GetSalespersonsByLastNameOrAgencyNumber(SearchEmployee);
                Employees = new ObservableCollection<Employee>(filteredEmployees);
                SearchEmployee = string.Empty;
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
    }
}
