using LaundryBooking.Maui.Views;
using LaundryBooking.Maui.Views.Admin;

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
        Routing.RegisterRoute(nameof(AdminLoginPage), typeof(AdminLoginPage));
        Routing.RegisterRoute(nameof(AdminHomePage), typeof(AdminHomePage));
        Routing.RegisterRoute(nameof(AdminIssuePage), typeof(AdminIssuePage));
        Routing.RegisterRoute(nameof(AdminNewsPage), typeof(AdminNewsPage));
        Routing.RegisterRoute(nameof(AdminHousingPage), typeof(AdminHousingPage));
    }
}