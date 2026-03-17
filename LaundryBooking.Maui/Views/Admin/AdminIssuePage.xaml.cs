using LaundryBooking.Maui.ViewModels.Admin;

namespace LaundryBooking.Maui.Views.Admin;

public partial class AdminIssuePage : ContentPage
{
    public AdminIssuePage(AdminIssueViewModel adminIssueViewModel)
    {
        InitializeComponent();
        BindingContext = adminIssueViewModel;
    }

    private async void OnStatusChanged(object? sender, EventArgs e)
    {
        if (sender is Picker picker && picker.BindingContext is IssueReportDisplay display)
        {
            await ((AdminIssueViewModel)BindingContext).UpdateStatusAsync(display);
        }
    }

    private async void OnDeleteClicked(object? sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is IssueReportDisplay display)
        {
            await ((AdminIssueViewModel)BindingContext).DeleteIssueAsync(display);
        }
    }
}