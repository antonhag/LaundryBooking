using System.Collections.ObjectModel;
using System.ComponentModel;
using LaundryBooking.Application.Interfaces;
using LaundryBooking.Domain.Entities;
using LaundryBooking.Domain.Enums;

namespace LaundryBooking.Maui.ViewModels.Admin;

// Wrapper som kombinerar IssueReport med BRF-namn för visning i UI
public class IssueReportDisplay
{
    public IssueReport Issue { get; set; } = new();
    public string HousingCooperativeName { get; set; } = string.Empty;

    public string Title => Issue.Title;
    public string ApartmentNumber => Issue.ApartmentNumber;
    public string Description => Issue.Description;
    public string Id => Issue.Id;
    public IssueStatus Status
    {
        get => Issue.Status;
        set => Issue.Status = value;
    }

    public string StatusDisplay
    {
        get => Issue.Status switch
        {
            IssueStatus.Open => "Öppen",
            IssueStatus.Ongoing => "Pågående",
            IssueStatus.Resolved => "Löst",
            _ => "Öppen"
        };
        set => Issue.Status = value switch
        {
            "Öppen" => IssueStatus.Open,
            "Pågående" => IssueStatus.Ongoing,
            "Löst" => IssueStatus.Resolved,
            _ => IssueStatus.Open
        };
    }
}

public class AdminIssueViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly IIssueService _issueService;
    private readonly IHousingCooperativeService _cooperativeService;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private ObservableCollection<IssueReportDisplay> _issueReports = new();
    public ObservableCollection<IssueReportDisplay> IssueReports
    {
        get { return _issueReports; }
        set
        {
            _issueReports = value;
            OnPropertyChanged(nameof(IssueReports));
        }
    }

    public List<string> StatusOptions { get; } = new() { "Öppen", "Pågående", "Löst" };

    public AdminIssueViewModel(IIssueService issueService, IHousingCooperativeService cooperativeService)
    {
        _issueService = issueService;
        _cooperativeService = cooperativeService;
        LoadReports();
    }

    private async void LoadReports()
    {
        var reports = await _issueService.GetAllIssuesAsync();
        var cooperatives = await _cooperativeService.GetAllHousingCooperativesAsync();

        IssueReports.Clear();
        foreach (var report in reports)
        {
            var name = cooperatives
                .FirstOrDefault(c => c.Id == report.HousingCooperativeId)?.Name ?? "Okänd";
            IssueReports.Add(new IssueReportDisplay { Issue = report, HousingCooperativeName = name });
        }
    }

    public async Task UpdateStatusAsync(IssueReportDisplay display)
    {
        try
        {
            await _issueService.UpdateIssueStatusAsync(display.Id, display.Issue.Status);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Fel", $"Kunde inte uppdatera status: {ex.Message}", "OK");
        }
    }

    public async Task DeleteIssueAsync(IssueReportDisplay display)
    {
        try
        {
            await _issueService.DeleteIssueAsync(display.Id);
            IssueReports.Remove(display);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Fel", $"Kunde inte ta bort felanmälan: {ex.Message}", "OK");
        }
    }
}
