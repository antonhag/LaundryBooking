using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using LaundryBooking.Application.Interfaces;
using LaundryBooking.Application.Services;
using LaundryBooking.Domain.Entities;

namespace LaundryBooking.Maui.ViewModels;

public class HomeViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly INewsService _newsService;
    private readonly IHousingCooperativeService _housingCooperativeService;
    private readonly SessionService _sessionService;

    public string TodayDate { get; set; } = DateTime.Now.ToString("dddd d MMMM", new CultureInfo("sv-SE"));
    public string ApartmentNumber => _sessionService.ApartmentNumber;

    private string _housingCooperativeName = string.Empty;
    public string HousingCooperativeName
    {
        get { return _housingCooperativeName; }
        set
        {
            _housingCooperativeName = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HousingCooperativeName)));
        }
    }

    private ObservableCollection<NewsPost> _newsPosts = new();
    public ObservableCollection<NewsPost> NewsPosts
    {
        get { return _newsPosts; }
        set
        {
            _newsPosts = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewsPosts)));
        }
    }

    public HomeViewModel(INewsService newsService, IHousingCooperativeService housingCooperativeService, SessionService sessionService)
    {
        _newsService = newsService;
        _housingCooperativeService = housingCooperativeService;
        _sessionService = sessionService;
        LoadNewsPostsAsync();
    }

    private async void LoadNewsPostsAsync()
    {
        var posts = await _newsService.GetPostsByHousingCooperativeAsync(_sessionService.HousingCooperativeId);
        posts = posts.OrderByDescending(p => p.CreatedAt).Take(3).ToList();
        NewsPosts = new ObservableCollection<NewsPost>(posts);
    }
    
    public async void ReloadNewsAsync()
    {
        var posts = await _newsService.GetPostsByHousingCooperativeAsync(_sessionService.HousingCooperativeId);
        posts = posts.OrderByDescending(p => p.CreatedAt).Take(3).ToList();
        NewsPosts = new ObservableCollection<NewsPost>(posts);

        var cooperative = await _housingCooperativeService.GetHousingCooperativeByIdAsync(_sessionService.HousingCooperativeId);
        HousingCooperativeName = cooperative?.Name ?? string.Empty;
    }
}
