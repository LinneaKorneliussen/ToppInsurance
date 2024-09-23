using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TopInsuranceWPF.Commands
{
    public abstract class ObservableObject : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected readonly Dictionary<string, Func<object, string>> _validations = new Dictionary<string, Func<object, string>>();

        protected void AddValidation(string propertyName, Func<object, string> validation)
        {
            _validations[propertyName] = validation;
        }

        public abstract string this[string columnName] { get; }

        public virtual string Error => null;

        public virtual bool IsValid()
        {
            foreach (var validation in _validations.Values)
            {
                if (validation != null && !string.IsNullOrEmpty(validation.Invoke(null)))
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsValidPersonalNumber(string personalNumber)
        {
            return Regex.IsMatch(personalNumber, @"^\d{4}-\d{2}-\d{2}-\d{4}$");
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\d{3}-\d{7}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

    }
}
