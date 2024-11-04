using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TopInsuranceWPF.Commands
{
    /// <summary>
    ///  The ObservableObject class is an abstract implementation of the INotifyPropertyChanged interface, designed to support property change notifications in WPF applications.
    ///  It provides a mechanism for notifying subscribers when a property value has changed, facilitating data binding in the user interface. 
    ///  The class includes an event, PropertyChanged, and a protected method, OnPropertyChanged, which raises the event with the name of the changed property. 
    ///  This structure simplifies the implementation of the Model-View-ViewModel (MVVM) pattern by enabling automatic updates of UI elements when the underlying data changes.
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
