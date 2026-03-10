using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaundryBooking.Maui.ViewModels;

namespace LaundryBooking.Maui.Views;

public partial class IssueReportPage : ContentPage
{
    public IssueReportPage(IssueReportViewModel issueReportViewModel)
    {
        InitializeComponent();
        BindingContext = issueReportViewModel;
    }
}