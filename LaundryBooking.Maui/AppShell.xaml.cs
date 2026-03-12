using LaundryBooking.Maui.Views;

namespace LaundryBooking.Maui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(BookingPage), typeof(BookingPage));
        Routing.RegisterRoute(nameof(ManageBookingPage), typeof(ManageBookingPage));
        Routing.RegisterRoute(nameof(IssueReportPage), typeof(IssueReportPage));
        Routing.RegisterRoute(nameof(NewsPage), typeof(NewsPage));
    }
}