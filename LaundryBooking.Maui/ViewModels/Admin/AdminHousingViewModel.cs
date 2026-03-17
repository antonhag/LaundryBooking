using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using LaundryBooking.Application.Interfaces;
using LaundryBooking.Domain.Entities;

namespace LaundryBooking.Maui.ViewModels.Admin;

public class AdminHousingViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly IHousingCooperativeService _housingCooperativeService;

    private ObservableCollection<HousingCooperative> _cooperatives = new();
    public ObservableCollection<HousingCooperative> Cooperatives
    {
        get { return _cooperatives; }
        set
        {
            _cooperatives = value;
            OnPropertyChanged(nameof(Cooperatives));
        }
    }

    private string _newName = string.Empty;
    public string NewName
    {
        get { return _newName; }
        set
        {
            _newName = value;
            OnPropertyChanged(nameof(NewName));
        }
    }

    private string _newApartmentNumbers = string.Empty;
    public string NewApartmentNumbers
    {
        get { return _newApartmentNumbers; }
        set
        {
            _newApartmentNumbers = value;
            OnPropertyChanged(nameof(NewApartmentNumbers));
        }
    }

    public ICommand CreateCommand { get; }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public AdminHousingViewModel(IHousingCooperativeService housingCooperativeService)
    {
        _housingCooperativeService = housingCooperativeService;
        CreateCommand = new Command(CreateAsync);
        LoadAsync();
    }

    private async void LoadAsync()
    {
        var cooperatives = await _housingCooperativeService.GetAllHousingCooperativesAsync();
        cooperatives = cooperatives.OrderBy(c => c.Name).ToList();
        Cooperatives.Clear();
        foreach (var c in cooperatives)
            Cooperatives.Add(c);
    }

    private async void CreateAsync()
    {
        if (string.IsNullOrWhiteSpace(NewName)) return;

        var numbers = NewApartmentNumbers
            .Split(',')
            .Select(n => n.Trim())
            .Where(n => !string.IsNullOrEmpty(n))
            .ToList();

        var cooperative = new HousingCooperative
        {
            Name = NewName,
            ApartmentNumbers = numbers
        };

        try
        {
            await _housingCooperativeService.CreateHousingCooperativeAsync(cooperative);
            Cooperatives.Add(cooperative);
            NewName = string.Empty;
            NewApartmentNumbers = string.Empty;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Fel", $"Kunde inte skapa förening: {ex.Message}", "OK");
        }
    }

    public async Task DeleteHousingCooperativeAsync(HousingCooperative cooperative)
    {
        try
        {
            await _housingCooperativeService.DeleteHousingCooperativeAsync(cooperative.Id);
            Cooperatives.Remove(cooperative);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Fel", $"Kunde inte ta bort förening: {ex.Message}", "OK");
        }
    }
}
