using System.Windows.Input;
using System.Windows;
using TopInsuranceWPF.Commands;
using TopInsuranceEntities;
using TopInsuranceBL;

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
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
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

        private bool _isPasswordVisible;
        public bool IsPasswordVisible
        {
            get => _isPasswordVisible;
            set
            {
                _isPasswordVisible = value;
                OnPropertyChanged(nameof(IsPasswordVisible));
            }
        }


        #endregion

        #region Login Commands
        public ICommand LoginCommand { get; }
        #endregion

        #region Login Methods 
        private void Login()
        {

            Employee user = loginController.AuthorizeUser(Username, Password);

            if (user != null) // Typa om här
            {
                switch (user.EmployeeRole) // Använd den typade instansen
                {
                    case EmployeeRole.Säljare:
                        // Hantera inloggning för SalesPerson
                        MessageBox.Show($"Inloggad som {user.Name}");
                        MenuWindow menu = new MenuWindow();
                        MenuVM menuVM = new MenuVM(user);
                        menu.DataContext = menuVM;
                        menu.ShowDialog();
                        break;
                    case EmployeeRole.Försäljningschef:
                        // Hantera inloggning för SalesPerson
                        MessageBox.Show($"Inloggad som {user.Name}");
                        MenuWindow menu1 = new MenuWindow();
                        menu1.ShowDialog(); 
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
                case "Username": 
                    if (string.IsNullOrWhiteSpace(Username))
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
