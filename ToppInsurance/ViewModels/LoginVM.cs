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

        #endregion

        #region Login Commands
        public ICommand LoginCommand { get; }
        #endregion

        #region Login Methods 
        private void Login()
        {

            Employee user = loginController.AuthorizeUser(Username, Password);

            if (user != null)
            {
                switch (user.EmployeeRole)
                {
                    case EmployeeRole.Säljare:
                        MessageBox.Show($"Inloggad som {user.Name}");
                        MenuWindowSP menuSP = new MenuWindowSP();
                        MenuSalespersonVM menuSalesperson = new MenuSalespersonVM(user);
                        menuSP.DataContext = menuSalesperson;
                        menuSP.ShowDialog();
                        ClearFields();
                        break;

                    case EmployeeRole.Försäljningschef:
                    case EmployeeRole.VD:
                    case EmployeeRole.Ekonomiassistent:
                        MessageBox.Show($"Inloggad som {user.Name}");
                        MenuWindowRP menuRP = new MenuWindowRP();
                        MenuResponsibleVM menuResponsible = new MenuResponsibleVM(user);
                        menuRP.DataContext = menuResponsible;
                        menuRP.ShowDialog();
                        ClearFields();
                        break;

                    default:
                        MessageBox.Show("Okänd roll");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Misslyckades med inloggning. Försök igen!");
            }

        }

        #endregion

        #region Clear fields 
        private void ClearFields()
        {
            Username = string.Empty;
            Password = string.Empty;
        }

        #endregion

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
