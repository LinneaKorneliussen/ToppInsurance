using System.Windows.Input;
using System.Windows;
using TopInsuranceWPF.Commands;
using TopInsuranceEntities;
using TopInsuranceBL;
using System.Collections.ObjectModel;

namespace TopInsuranceWPF.ViewModels
{
    public class LifeInsuranceVM : ObservableObject

    {
        private LifeInsuranceController lifeInsuranceController;
        public IEnumerable<Paymentform> Paymentforms {  get; }

        public LifeInsuranceVM()
        {
            lifeInsuranceController = new LifeInsuranceController();
            List<PrivateCustomer> customers = lifeInsuranceController.GetAllPrivateCustomers();
            List<int> baseamounts = lifeInsuranceController.GetBaseAmounts();
            BaseAmount = new ObservableCollection<int>(baseamounts); 
            Paymentforms = Enum.GetValues(typeof(Paymentform)) as IEnumerable<Paymentform>;

        }


        #region Properties

        private DateTime _newStartDate;
        public DateTime NewStartDate 
        { 
            get { return _newStartDate; } 
            set 
            { 
                if (_newStartDate != value)
                {
                    NewStartDate = value;
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
                    NewStartDate = value;
                    OnPropertyChanged(nameof(NewEndDate));
                }
            }
        }

        private Paymentform _selectedPaymentForm;
        public Paymentform SelectedPaymentForm
        {
            get { return _selectedPaymentForm; }
            set
            {
                if (_selectedPaymentForm != value)
                {
                    _selectedPaymentForm = value;
                    OnPropertyChanged(nameof(SelectedPaymentForm));
                }
            }
        }

        private Paymentform _selectedBaseAmount;
        public Paymentform SelectedBaseAmount
        {
            get { return _selectedBaseAmount; }
            set
            {
                if (_selectedBaseAmount != value)
                {
                    _selectedBaseAmount = value;
                    OnPropertyChanged(nameof(SelectedBaseAmount));
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

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                }
            }
        }





        #endregion




        private ObservableCollection<int> _baseAmounts;
        public ObservableCollection<int> BaseAmount
        {
            get { return _baseAmounts; }
            set
            {
                if (_baseAmounts != value)
                {
                    _baseAmounts = value;
                    OnPropertyChanged(nameof(BaseAmount));
                }
            }
        }



    }
}
