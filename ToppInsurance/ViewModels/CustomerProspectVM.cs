using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TopInsuranceBL;
using TopInsuranceEntities;
using TopInsuranceWPF.Commands;

namespace TopInsuranceWPF.ViewModels
{
    public class CustomerProspectVM : ObservableObject
    {
        private BusinessController businessController;
        private PrivateController privateController;
        private ProspectController prospectController;
        private Employee employee;

        public ObservableCollection<Insurance> ActiveInsurances { get; set; }
        public ICollection<ProspectInformation> ProspectInformationList { get; set; }

        public CustomerProspectVM()
        {
            
            employee = UserContext.Instance.LoggedInUser;
            businessController = new BusinessController();
            privateController = new PrivateController();
            prospectController = new ProspectController();

            
            FindPcustomersCommand = new RelayCommand(FindPcustomers);
            FindBCcustomersCommand = new RelayCommand(FindBCcustomers);
            AddPCNoteCommand = new RelayCommand(AddPCNote);
            AddBCNoteCommand = new RelayCommand(AddBCNote);

            FindPrivatCustomerProspect();
            FindBusinessCustomerProspect();

        }

        #region Properties for inlogged user

        private string userFirstName;
        public string UserFirstName
        {
            get { return userFirstName; }
            set
            {
                userFirstName = value;
                OnPropertyChanged(nameof(UserFirstName));
                OnPropertyChanged(nameof(UserInfo));
            }
        }
        private string userLastName;
        public string UserLastName
        {
            get { return userLastName; }
            set
            {
                userLastName = value;
                OnPropertyChanged(nameof(UserLastName));
                OnPropertyChanged(nameof(UserInfo));
            }
        }

        private int agencyNumber;
        public int AgencyNumber
        {
            get { return agencyNumber; }
            set
            {
                agencyNumber = value;
                OnPropertyChanged(nameof(AgencyNumber));
            }
        }

        public string UserInfo
        {
            get { return $"{UserFirstName} {UserLastName}"; }
        }
        #endregion

        #region Properties

        private BusinessCustomer? _selectBusinessCustomer;
        public BusinessCustomer? SelectBusinessCustomer
        {
            get { return _selectBusinessCustomer; }
            set
            {
                if (_selectBusinessCustomer != value)
                {
                    _selectBusinessCustomer = value;
                    OnPropertyChanged(nameof(SelectBusinessCustomer));
                }
            }
        }

        private PrivateCustomer? _selectPrivateCustomer;
        public PrivateCustomer? SelectPrivateCustomer
        {
            get { return _selectPrivateCustomer; }
            set
            {
                if (_selectPrivateCustomer != value)
                {
                    _selectPrivateCustomer = value;
                    OnPropertyChanged(nameof(SelectPrivateCustomer));
                }
            }
        }

        private string _note;
        public string Note
        {
            get { return _note; }
            set
            {
                if (_note != value)
                {
                    _note = value;
                    OnPropertyChanged(nameof(Note));
                }
            }
        }
        #endregion

        #region Add note method
        private void AddPCNote()
        {
            try
            {
                if (SelectPrivateCustomer == null)
                {
                    MessageBox.Show("Vänligen välj en kund från listan.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(Note))
                {
                    MessageBox.Show("Noteringen får inte vara tom.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                prospectController.AddPCNote(Note, employee, SelectPrivateCustomer, null); 
                MessageBox.Show("Notering lagd framgångsrikt.");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Varning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ett oväntat fel inträffade: {ex.Message}\nStack Trace: {ex.StackTrace}", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddBCNote()
        {
            try
            {
                if (SelectBusinessCustomer == null)
                {
                    MessageBox.Show("Vänligen välj en företagskund från listan.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(Note))
                {
                    MessageBox.Show("Noteringen får inte vara tom.", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                prospectController.AddBCNote(Note, employee, null, SelectBusinessCustomer); 
                MessageBox.Show("Notering lagd framgångsrikt.");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Varning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ett oväntat fel inträffade: {ex.Message}\nStack Trace: {ex.StackTrace}", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Search
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

        private void FindBCcustomers()
        {
            var filteredBusinessCustomers = businessController.SearchBusinessCustomers(SearchBusinessCustomer);

            BCcustomers = new ObservableCollection<BusinessCustomer>(filteredBusinessCustomers);
            SearchBusinessCustomer = string.Empty;
        }

        private void FindPcustomers()
        {
            var filteredPrivateCustomers = privateController.SearchPrivateCustomers(SearchPrivateCustomer);

            Pcustomers = new ObservableCollection<PrivateCustomer>(filteredPrivateCustomers);
            SearchPrivateCustomer = string.Empty;
        }

        #endregion

        #region Get all business and private customers
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
        #endregion

        #region Find method 
        private void FindPrivatCustomerProspect()
        {
            var privateProspects = privateController.GetPrivateCustomerProspects();
            Pcustomers = new ObservableCollection<PrivateCustomer>(privateProspects);
        }

        private void FindBusinessCustomerProspect()
        {
            var businessProspects = businessController.GetBusinessCustomerProspects();
            BCcustomers = new ObservableCollection<BusinessCustomer>(businessProspects);
        }

        #endregion

        #region Commands
        public ICommand ClearCommand { get; }
        public ICommand FindBCcustomersCommand { get; }
        public ICommand FindPcustomersCommand { get; }
        public ICommand AddPCNoteCommand { get; }
        public ICommand AddBCNoteCommand { get; }

        #endregion


    }
}
