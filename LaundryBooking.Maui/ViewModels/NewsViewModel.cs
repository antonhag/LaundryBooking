using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using LaundryBooking.Application.Interfaces;
using LaundryBooking.Application.Services;
using LaundryBooking.Domain.Entities;

namespace LaundryBooking.Maui.ViewModels;

public class NewsViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly INewsService _newsService;
    private readonly SessionService _sessionService;

    private ObservableCollection<NewsPost> _newsPosts;
    public ObservableCollection<NewsPost> NewsPosts
    {
        get { return _newsPosts; }
        set
        {
            _newsPosts = value;
            OnPropertyChanged(nameof(NewsPosts));
        }
    }
    
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public NewsViewModel(INewsService newsService, SessionService sessionService)
    {
        _newsService = newsService;
        _sessionService = sessionService;
        LoadNewsPosts();
    }

    private async void LoadNewsPosts()
    {
        var newsPosts = await _newsService.GetPostsByHousingCooperativeAsync(_sessionService.HousingCooperativeId);
        newsPosts = newsPosts.OrderByDescending(n => n.CreatedAt).ToList();
        NewsPosts = new ObservableCollection<NewsPost>(newsPosts);
    }
}