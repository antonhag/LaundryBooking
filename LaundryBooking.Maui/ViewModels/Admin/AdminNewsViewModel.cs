using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LaundryBooking.Application.Interfaces;
using LaundryBooking.Domain.Entities;

namespace LaundryBooking.Maui.ViewModels.Admin;

public class AdminNewsViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly INewsService _newsService;

    public ObservableCollection<NewsPost> _newsPosts = new();
    private ObservableCollection<NewsPost> NewsPosts
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

    public AdminNewsViewModel(INewsService newsService)
    {
        _newsService = newsService;
    }
    
    public async Task LoadNewsPostsAsync()
    {
        NewsPosts = new ObservableCollection<NewsPost>(await _newsService.GetAllPostsAsync());
    }
}