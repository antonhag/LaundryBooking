using LaundryBooking.Domain.Entities;
using LaundryBooking.Maui.ViewModels.Admin;

namespace LaundryBooking.Maui.Views.Admin;

public partial class AdminHousingPage : ContentPage
{
    public AdminHousingPage(AdminHousingViewModel adminHousingViewModel)
    {
        InitializeComponent();
        BindingContext = adminHousingViewModel;
    }

    private async void OnClickedDelete(object? sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is HousingCooperative cooperative)
            await ((AdminHousingViewModel)BindingContext).DeleteHousingCooperativeAsync(cooperative);
    }
}
