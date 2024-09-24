using System.Windows.Input;
using System.Windows;
using TopInsuranceWPF.Commands;
using ToppInsuranceBL;
using TopInsuranceEntities;

namespace TopInsuranceWPF.ViewModels
{
    public class LoginVM : ObservableObject
    {
        private LoginController loginController;

        public LoginVM()
        {
            loginController = new LoginController();
            LoginCommand = new RelayCommand(Login);
        }

        #region Login properties 
        private int _employeeId;
        public int EmployeeId
        {
            get { return _employeeId; }
            set
            {
                _employeeId = value;
                OnPropertyChanged(nameof(EmployeeId));
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        #endregion

        #region Login Commands
        public ICommand LoginCommand { get; }
        #endregion

        #region Login Methods 
        private void Login()
        {
            Employee user = loginController.AuthorizeUser(EmployeeId, Password);

            if (user != null)
            {
                switch (user.EmployeeRole)
                {
                    case EmployeeRole.SalesPerson:
                        // Hantera inloggning för SalesPerson
                        MessageBox.Show("Inloggad som SalesPerson!");
                        MenuWindow menu = new MenuWindow();
                        menu.Show();
                        break;

                    case EmployeeRole.SalesAssistant:
                        // Hantera inloggning för SalesAssistant
                        MessageBox.Show("Inloggad som SalesAssistant!");
                        // Navigera till SalesAssistant-vyn eller logik
                        break;

                    case EmployeeRole.VD:
                        // Hantera inloggning för VD
                        MessageBox.Show("Inloggad som VD!");
                        // Navigera till VD-vyn eller logik
                        break;

                    case EmployeeRole.EconomicAssistant:
                        // Hantera inloggning för EconomicAssistant
                        MessageBox.Show("Inloggad som EconomicAssistant!");
                        // Navigera till EconomicAssistant-vyn eller logik
                        break;

                    case EmployeeRole.SalesManager:
                        // Hantera inloggning för SalesManager
                        MessageBox.Show("Inloggad som SalesManager!");
                        // Navigera till SalesManager-vyn eller logik
                        break;

                    default:
                        MessageBox.Show("Ogiltig roll!");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Misslyckades med inloggning. Försök igen!");
            }
        }

        #endregion

        // Validation using the IDataErrorInfo interface
        #region Validation 
        private string ValidateField(string columnName)
        {
            string errorMessage = null;

            switch (columnName)
            {
                case "EmployeeId": 
                    if (EmployeeId < 0)
                    {
                        errorMessage = "Agency number must be a positive integer.";
                    }
                    break;
                case "Password":
                    if (string.IsNullOrWhiteSpace(Password))
                    {
                        errorMessage = "Field is required.";
                    }
                    break;
            }

            return errorMessage;
        }


        public override string this[string columnName]
        {
            get
            {
                return ValidateField(columnName);
            }
        }
        #endregion

    }
}
