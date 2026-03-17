using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace LaundryBooking.Maui.ViewModels.Admin;

public class AdminHomeViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public string TodayDate { get; set; } = DateTime.Now.ToString("dddd d MMMM", new CultureInfo("sv-SE"));


    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}