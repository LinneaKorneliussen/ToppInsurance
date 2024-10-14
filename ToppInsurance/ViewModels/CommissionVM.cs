using System.Collections.ObjectModel;
using System.Windows.Input;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    public class CommissionVM : ObservableObject
    {
        public CommissionVM()
        {
            //commissionController = new CommissionController();
            //FindEmployeeCommand = new RelayCommand(FindEmployee);


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
                    //LoadSalesDataForEmployee();
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
        //private void FindEmployee()
        //{
        //    if (!string.IsNullOrEmpty(SearchEmployee))
        //    {
        //        var filteredEmployees = commissionController.GetSalespersonsByLastNameOrAgencyNumber(SearchEmployee);
        //        Employees = new ObservableCollection<Employee>(filteredEmployees);
        //        SearchEmployee = string.Empty;
        //    }
        //}
        #endregion
    }
}
