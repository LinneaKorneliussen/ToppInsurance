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
        public ObservableCollection<ProspectInformation> PrivateCustomerProspectList { get; set; }
        private BusinessController businessController;
        private PrivateController privateController;
        private ProspectController prospectController;
        private Employee employee;

        public ObservableCollection<Insurance> ActiveInsurances { get; set; }

        public CustomerProspectVM()
        {

            employee = UserContext.Instance.LoggedInUser;
            businessController = new BusinessController();
            privateController = new PrivateController();
            prospectController = new ProspectController();
            PrivateCustomerProspectList = new ObservableCollection<ProspectInformation>();


            FindPcustomersCommand = new RelayCommand();
            FindBCcustomersCommand = new RelayCommand(FindBCcustomers);
            AddPCNoteCommand = new RelayCommand(AddPCNote);
            AddBCNoteCommand = new RelayCommand(AddBCNote);
            ClearCommand = new RelayCommand(ClearFields);

            FindPrivatCustomerProspect();
            FindBusinessCustomerProspect();

        }

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
            if (string.IsNullOrWhiteSpace(SearchBusinessCustomer))
            {
                MessageBox.Show("Sökning misslyckades. Ange söktext.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var filteredBusinessCustomers = businessController.SearchBusinessCustomers(SearchBusinessCustomer);

            if (filteredBusinessCustomers == null || !filteredBusinessCustomers.Any())
            {
                MessageBox.Show("Inga resultat hittades för den angivna söktexten.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                BCcustomers = new ObservableCollection<BusinessCustomer>(filteredBusinessCustomers);
            }
        }

        private void FindPcustomers()
        {
            if (string.IsNullOrWhiteSpace(SearchPrivateCustomer))
            {
                MessageBox.Show("Sökning misslyckades. Ange söktext.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var filteredPrivateCustomers = privateController.SearchPrivateCustomers(SearchPrivateCustomer);

            if (filteredPrivateCustomers == null || !filteredPrivateCustomers.Any())
            {
                MessageBox.Show("Inga resultat hittades för den angivna söktexten.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Pcustomers = new ObservableCollection<PrivateCustomer>(filteredPrivateCustomers);
            }
        }

        #endregion

        #region Note property
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

        #region Properties for selected customer

        private BusinessCustomer _selectBusinessCustomer;
        public BusinessCustomer SelectBusinessCustomer
        {
            get { return _selectBusinessCustomer; }
            set
            {
                if (_selectBusinessCustomer != value)
                {
                    _selectBusinessCustomer = value;
                    OnPropertyChanged(nameof(SelectBusinessCustomer));

                    if (_selectBusinessCustomer != null)
                    {
                        BusinessCustomerProspect();
                    }
                }

            }
        }

        private PrivateCustomer _selectPrivateCustomer;
        public PrivateCustomer SelectPrivateCustomer
        {
            get { return _selectPrivateCustomer; }
            set
            {
                if (_selectPrivateCustomer != value)
                {
                    _selectPrivateCustomer = value;
                    OnPropertyChanged(nameof(SelectPrivateCustomer));

                    if (_selectPrivateCustomer != null)
                    {
                        PrivateCustomerProspect();
                    }
                }
            }
        }

        #endregion

        #region Observable Collections 
        private ObservableCollection<BusinessCustomer> _bccustomers;
        public ObservableCollection<BusinessCustomer> BCcustomers
        {
            get { return _bccustomers; }
            set
            {
                _bccustomers = value;
                OnPropertyChanged(nameof(BCcustomers));
            }
        }

        private ObservableCollection<PrivateCustomer> _pcustomers;
        public ObservableCollection<PrivateCustomer> Pcustomers
        {
            get { return _pcustomers; }
            set
            {
                _pcustomers = value;
                OnPropertyChanged(nameof(Pcustomers));
            }
        }

        private ObservableCollection<ProspectInformation> _prospectInformtionB;
        public ObservableCollection<ProspectInformation> ProspectInformtionB
        {
            get { return _prospectInformtionB; }
            set
            {
                _prospectInformtionB = value;
                OnPropertyChanged(nameof(ProspectInformtionB));
            }
        }

        private ObservableCollection<ProspectInformation> _prospectInformtionP;
        public ObservableCollection<ProspectInformation> ProspectInformtionP
        {
            get { return _prospectInformtionP; }
            set
            {
                _prospectInformtionP = value;
                OnPropertyChanged(nameof(ProspectInformtionP));
            }
        }
        #endregion

        #region Commands
        public ICommand ClearCommand { get; }
        public ICommand FindBCcustomersCommand { get; }
        public ICommand FindPcustomersCommand { get; }
        public ICommand AddPCNoteCommand { get; }
        public ICommand AddBCNoteCommand { get; }

        #endregion

        #region Find customers Methods
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

        #region Add note Methods
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
                PrivateCustomerProspect();
                
                MessageBox.Show($"Notering lagd framgångsrikt för {SelectPrivateCustomer.FirstName} {SelectPrivateCustomer.LastName}.");

               
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

                BusinessCustomerProspect();
               

                MessageBox.Show($"Notering lagd framgångsrikt för {SelectBusinessCustomer.FirstName} {SelectBusinessCustomer.LastName}." );


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

        #region Load notes method
        public void PrivateCustomerProspect()
        {
            var privateProspects = prospectController.PrivateCustomerProspect(SelectPrivateCustomer);
            ProspectInformtionP = new ObservableCollection<ProspectInformation>(privateProspects);
        }

        public void BusinessCustomerProspect()
        {
            var businessProspects = prospectController.BusinessCustomerProspect(SelectBusinessCustomer);
            ProspectInformtionB = new ObservableCollection<ProspectInformation>(businessProspects);
        }

        #endregion

        #region Clear fields method
        private void ClearFields()
        {
            SearchBusinessCustomer = string.Empty;
            SearchPrivateCustomer = string.Empty;
            Note = string.Empty;
            Pcustomers = new ObservableCollection<PrivateCustomer>();
            BCcustomers = new ObservableCollection<BusinessCustomer>();
            ProspectInformtionB = new ObservableCollection<ProspectInformation>();
            ProspectInformtionP = new ObservableCollection<ProspectInformation>();

        }
        #endregion

    }
}
