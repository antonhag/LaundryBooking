using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LaundryBooking.Application.Interfaces;
using LaundryBooking.Application.Services;
using LaundryBooking.Domain.Entities;
using LaundryBooking.Domain.Enums;

namespace LaundryBooking.Maui.ViewModels;

public class IssueReportViewModel : INotifyPropertyChanged
{
    private readonly IIssueService _issueService;
    private readonly SessionService _sessionService;
    public event PropertyChangedEventHandler? PropertyChanged;
    public ICommand CreateIssueReportCommand { get; }
    
    private string _title = string.Empty;
    public string Title
    {
        get { return _title; }
        set
        {
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
    
    private string _description = string.Empty;
    public string Description
    {
        get { return _description; }
        set
        {
            _description = value;
            OnPropertyChanged(nameof(Description));
        }
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public IssueReportViewModel(IIssueService issueService, SessionService sessionService)
    {
        _issueService = issueService;
        _sessionService = sessionService;
        CreateIssueReportCommand = new Command(CreateIssueReportAsync);
    }

    private async void CreateIssueReportAsync()
    {
        if (string.IsNullOrWhiteSpace(_title) || string.IsNullOrWhiteSpace(_description)) 
        {
            await Shell.Current.DisplayAlertAsync("Fel", "Varken rubrik eller beskrivning kan vara tom.", "OK");                                   
            return;  
        }
        
        var newIssueReport = new IssueReport()
        {
            ApartmentNumber = _sessionService.ApartmentNumber,
            HousingCooperativeId = _sessionService.HousingCooperativeId,
            Title = _title,
            Description = _description,
            CreatedAt = DateTime.UtcNow,
            Status = IssueStatus.Open
        };
        
        await _issueService.CreateIssueAsync(newIssueReport);
        await Shell.Current.DisplayAlertAsync("Klart", "Felanmälan skickad!", "OK");
        await Shell.Current.Navigation.PopAsync();                                                                           
    }
    
    
   
}