using System.Collections.ObjectModel;
using System.Windows.Input;
using TopInsuranceBL;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    public class CommissionVM : ObservableObject
    {
        private CommissionController commissionController;
        private EmployeeController employeeController;
        public CommissionVM()
        {
            commissionController = new CommissionController();
            employeeController = new EmployeeController();
            FindEmployeeCommand = new RelayCommand(FindEmployee);
            AddCommissionCommand = new RelayCommand(AddCommission);
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
        public ICommand AddCommissionCommand { get; }
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

        #region Add Commission for employee 
        private void AddCommission()
        {
            commissionController.CalculateAndCreateCommission(SelectedEmployee, NewStartDate, NewEndDate);
        }


        #endregion


    }
}
