using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaundryBooking.Maui.ViewModels.Admin;

namespace LaundryBooking.Maui.Views.Admin;

public partial class AdminLoginPage : ContentPage
{
    public AdminLoginPage(AdminLoginViewModel adminLoginViewModel)
    {
        InitializeComponent();
        BindingContext = adminLoginViewModel;
    }
}