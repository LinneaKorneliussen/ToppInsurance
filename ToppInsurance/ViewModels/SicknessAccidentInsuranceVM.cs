using TopInsuranceWPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceWPF.ViewModels
{
    public class SicknessAccidentInsuranceVM : ObservableObject
    {
        private bool isAdultOptionSelected;

        public bool IsAdultOptionSelected
        {
            get { return isAdultOptionSelected; }
            set
            {
                if (isAdultOptionSelected != value)
                {
                    isAdultOptionSelected = value;
                    OnPropertyChanged(nameof(IsAdultOptionSelected));
                }
            }
        }

        private bool isChildInsuranceSelected;

        public bool IsChildInsuranceSelected
        {
            get { return isChildInsuranceSelected; }
            set
            {
                if (isChildInsuranceSelected != value)
                {
                    isChildInsuranceSelected = value;
                    OnPropertyChanged(nameof(IsChildInsuranceSelected));
                }
            }
        }
    }

}
