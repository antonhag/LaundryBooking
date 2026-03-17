using LaundryBooking.Maui.ViewModels.Admin;

namespace LaundryBooking.Maui.Views.Admin;

public partial class AdminNewsPage : ContentPage
{
    public AdminNewsPage(AdminNewsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnDeleteClicked(object? sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is NewsPostDisplay display)
            await ((AdminNewsViewModel)BindingContext).DeletePostAsync(display);
    }
}
