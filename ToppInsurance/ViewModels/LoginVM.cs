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
                UserContext.Instance.SetUser(user);

                MessageBox.Show($"Inloggad som {user.FirstName} {user.LastName}");

                switch (user.EmployeeRole)
                {
                    case EmployeeRole.Säljare:
                        MenuWindowSP menuSP = new MenuWindowSP();
                        MenuSalespersonVM menuSalesperson = new MenuSalespersonVM(user);
                        menuSP.DataContext = menuSalesperson;
                        menuSP.ShowDialog();
                        ClearFields();
                        break;

                    case EmployeeRole.Försäljningschef:
                    case EmployeeRole.VD:
                        MenuWindowRP menuRP = new MenuWindowRP();
                        MenuResponsibleVM menuResponsible = new MenuResponsibleVM(user);
                        menuRP.DataContext = menuResponsible;
                        menuRP.ShowDialog();
                        ClearFields();
                        break;

                    case EmployeeRole.Ekonomiassistent:
                        MenuWindowEA menuEA = new MenuWindowEA();
                        MenuEconomicAssistantVM menuEconomicAssistant = new MenuEconomicAssistantVM(user);
                        menuEA.DataContext = menuEconomicAssistant;
                        menuEA.ShowDialog();
                        ClearFields();
                        break;

                    case EmployeeRole.Försäljningsassistent:
                        MenuWindowSA menuSA = new MenuWindowSA();
                        MenuSalesAssistantVM menuSalesAssistant = new MenuSalesAssistantVM(user);
                        menuSA.DataContext = menuSalesAssistant;
                        menuSA.ShowDialog();
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
    }




}
