using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using LaundryBooking.Application.Interfaces;
using LaundryBooking.Domain.Entities;

namespace LaundryBooking.Maui.ViewModels.Admin;

// Wrapper som kombinerar NewsPost med BRF-namn för visning i UI
public class NewsPostDisplay
{
    public NewsPost Post { get; set; } = new();
    public string HousingCooperativeName { get; set; } = string.Empty;

    public string Title => Post.Title;
    public string Content => Post.Content;
    public string Id => Post.Id;
    public DateTime CreatedAt => Post.CreatedAt;
}

public class AdminNewsViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly INewsService _newsService;
    private readonly IHousingCooperativeService _cooperativeService;

    private ObservableCollection<NewsPostDisplay> _newsPosts = new();
    public ObservableCollection<NewsPostDisplay> NewsPosts
    {
        get { return _newsPosts; }
        set
        {
            _newsPosts = value;
            OnPropertyChanged(nameof(NewsPosts));
        }
    }

    private List<HousingCooperative> _cooperatives = new();
    public List<HousingCooperative> Cooperatives
    {
        get { return _cooperatives; }
        set
        {
            _cooperatives = value;
            OnPropertyChanged(nameof(Cooperatives));
        }
    }

    private HousingCooperative? _selectedCooperative;
    public HousingCooperative? SelectedCooperative
    {
        get { return _selectedCooperative; }
        set
        {
            _selectedCooperative = value;
            OnPropertyChanged(nameof(SelectedCooperative));
        }
    }

    private string _newTitle = string.Empty;
    public string NewTitle
    {
        get { return _newTitle; }
        set
        {
            _newTitle = value;
            OnPropertyChanged(nameof(NewTitle));
        }
    }

    private string _newContent = string.Empty;
    public string NewContent
    {
        get { return _newContent; }
        set
        {
            _newContent = value;
            OnPropertyChanged(nameof(NewContent));
        }
    }

    public ICommand CreatePostCommand { get; }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public AdminNewsViewModel(INewsService newsService, IHousingCooperativeService cooperativeService)
    {
        _newsService = newsService;
        _cooperativeService = cooperativeService;
        CreatePostCommand = new Command(CreatePostAsync);
        LoadNewsPostsAsync();
    }

    private async void LoadNewsPostsAsync()
    {
        var cooperatives = await _cooperativeService.GetAllHousingCooperativesAsync();
        Cooperatives = cooperatives;
        await ReloadPostsAsync(cooperatives);
    }

    private async Task ReloadPostsAsync(List<HousingCooperative>? cooperatives = null)
    {
        var posts = await _newsService.GetAllPostsAsync();
        posts = posts.OrderByDescending(p => p.CreatedAt).ToList();

        if (cooperatives == null)
        {
            cooperatives = await _cooperativeService.GetAllHousingCooperativesAsync();
        }                                            
            
        NewsPosts.Clear();
        foreach (var post in posts)
        {
            var name = cooperatives
                .FirstOrDefault(c => c.Id == post.HousingCooperativeId)?.Name ?? "Okänd";
            NewsPosts.Add(new NewsPostDisplay { Post = post, HousingCooperativeName = name });
        }
    }

    private async void CreatePostAsync()
    {
        if (SelectedCooperative == null || string.IsNullOrWhiteSpace(NewTitle))
            return;

        var post = new NewsPost
        {
            HousingCooperativeId = SelectedCooperative.Id,
            Title = NewTitle,
            Content = NewContent
        };

        try
        {
            await _newsService.CreatePostAsync(post);
            await ReloadPostsAsync();
            NewTitle = string.Empty;
            NewContent = string.Empty;
            SelectedCooperative = null;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Fel", $"Kunde inte publicera nyhet: {ex.Message}", "OK");
        }
    }

    public async Task DeletePostAsync(NewsPostDisplay display)
    {
        try
        {
            await _newsService.DeletePostAsync(display.Id);
            NewsPosts.Remove(display);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Fel", $"Kunde inte ta bort nyhet: {ex.Message}", "OK");
        }
    }
}
