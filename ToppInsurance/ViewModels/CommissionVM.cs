using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TopInsuranceBL;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    public class CommissionVM : ObservableObject, IDataErrorInfo
    {
        private CommissionController commissionController;
        private EmployeeController employeeController;
        public List<string> AvailableMonthsYears { get; }
        public CommissionVM()
        {
            var currentDate = DateTime.Now;
            NewStartDate = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(-1);
            NewEndDate = new DateTime(currentDate.Year, currentDate.Month, 1).AddDays(-1);
            commissionController = new CommissionController();
            employeeController = new EmployeeController();
            FindEmployeeCommand = new RelayCommand(FindEmployee);
            AddCommissionCommand = new RelayCommand(AddCommission);
            RefreshCommand = new RelayCommand(RefreshCommissionData);
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
                    LoadCommissions();
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

        private ObservableCollection<dynamic> _loadedCommissions;
        public ObservableCollection<dynamic> LoadedCommissions
        {
            get { return _loadedCommissions; }
            set
            {
                if (_loadedCommissions != value)
                {
                    _loadedCommissions = value;
                    OnPropertyChanged(nameof(LoadedCommissions));
                }
            }
        }
        #endregion

        #region Commands
        public ICommand FindEmployeeCommand { get; }
        public ICommand AddCommissionCommand { get; }
        public ICommand RefreshCommand { get; }
        #endregion

        #region Find Employee Method
        private void FindEmployee()
        {
            if (!string.IsNullOrWhiteSpace(SearchEmployee))
            {
                var filteredEmployees = employeeController.GetSalespersonsByLastNameOrAgencyNumber(SearchEmployee);
                Employees = new ObservableCollection<Employee>(filteredEmployees);
                SearchEmployee = string.Empty;
            }
            else
            {
                MessageBox.Show("Sökning misslyckades. Ange söktext.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion

        #region Add Commission for employee 
        private void AddCommission()
        {
            if (!string.IsNullOrEmpty(Error))
            {
                MessageBox.Show(Error, "Valideringsfel", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var (commission, errorMessage) = commissionController.CalculateAndCreateCommission(SelectedEmployee, NewStartDate, NewEndDate);

            if (errorMessage != null)
            {
                MessageBox.Show(errorMessage, "Valideringsfel", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }

            if (commission != null)
            {
                MessageBox.Show("Provisionsunderlaget har lagts till framgångsrikt.", "Bekräftelse", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #endregion

        #region Load Commission data Method
        public void LoadCommissions()
        {
            var commissionDataList = commissionController.LoadCommissionsFromJson();
            if (SelectedMonthYear != null)
            {
                var parts = SelectedMonthYear.Split(' ');
                if (parts.Length == 2 && int.TryParse(parts[1], out int year))
                {
                    int month = DateTime.ParseExact(parts[0], "MMMM", CultureInfo.CurrentCulture).Month;
                    DateTime startDate = new DateTime(year, month, 1);
                    DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                    var filteredCommissions = commissionDataList
                        .Where(c => DateTime.TryParse(c.StartDate.ToString(), out DateTime commissionStartDate) &&
                                     commissionStartDate >= startDate &&
                                     commissionStartDate <= endDate)
                        .ToList();

                    LoadedCommissions = new ObservableCollection<dynamic>(filteredCommissions);
                }
            }
            else
            {
                LoadedCommissions = new ObservableCollection<dynamic>(commissionDataList);
            }
        }
        #endregion

        #region Refresh sales person 
        private void RefreshCommissionData()
        {
            var commissionDataList = commissionController.LoadCommissionsFromJson();
            LoadedCommissions = new ObservableCollection<dynamic>(commissionDataList);
        }
        #endregion

        #region Validation IDataErrorInfo
        public string Error
        {
            get
            {
                string[] properties = { nameof(SelectedEmployee), nameof(NewStartDate), nameof(NewEndDate) };
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
            var currentDate = DateTime.Now;
            var previousMonthStartDate = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(-1);
            var previousMonthEndDate = new DateTime(currentDate.Year, currentDate.Month, 1).AddDays(-1);

            switch (columnName)
            {
                case nameof(SelectedEmployee):
                    if (SelectedEmployee == null)
                    {
                        errorMessage = "Vänligen välj en säljare från listan";
                    }
                    break;
                case nameof(NewStartDate):
                    if (NewStartDate != previousMonthStartDate)
                    {
                        errorMessage = "Provisionsunderlag kan endast genereras från förgående månads första dag";
                    }
                    break;
                case nameof(NewEndDate):
                    if (NewEndDate != previousMonthEndDate)
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
