using System.Globalization;

namespace LaundryBooking.Maui.ViewModels;

public class HomeViewModel
{
    public string TodayDate { get; set; } = DateTime.Now.ToString("dddd d MMMM", new CultureInfo("sv-SE"));
}