using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexio.App.Dialog;
using Lexio.App.Routing;
using Lexio.App.Services;
using Lexio.App.ViewModels.Dictionary.Language;

namespace Lexio.App.ViewModels.Serie;

public partial class SerieViewModel : ViewModelBase {
    private LanguageService _languageService;
    private SerieService _serieService;
    private DialogService _dialogService;

    public Action? ClearNewSerieNameInput { get; set; }

    [ObservableProperty]
    private string _newSerieName = "";

    [ObservableProperty]
    private LanguageViewModel? _selectedSerieLanguage = null;

    [ObservableProperty]
    private RoutingService _routingService;

    public ObservableCollection<LanguageViewModel> AvailableLanguageModels {
        get;
        set => SetProperty(ref field, value);
    } = null!;


    public ObservableCollection<SerieDetailViewModel> AvailableSeries {
        get;
        set => SetProperty(ref field, value);
    } = null!;

    public SerieViewModel(RoutingService routingService, LanguageService languageService, SerieService serieService, DialogService dialogService) {
        _languageService = languageService;
        _serieService = serieService;
        _dialogService = dialogService;
        RoutingService = routingService;
        _ = Task.Run(async () => {
            
            AvailableLanguageModels =
                new ObservableCollection<LanguageViewModel>(await _languageService.GetAvailableTraductions());
            
            AvailableSeries =
                new ObservableCollection<SerieDetailViewModel>(await _serieService.GetSeriesAvailablesAsync());
        });
    }

    [RelayCommand]
    public async Task AddNewSerie() {
        if (string.IsNullOrWhiteSpace(NewSerieName) || SelectedSerieLanguage is null) {
            await _dialogService.ShowAlertAsync("Le nom de la série ne peut pas être composé uniquement d'espace, et un language doit être sélectionné.");
            return;
        }
        bool resultAdded = await _serieService.AddNewAsync(SelectedSerieLanguage.Id, NewSerieName);
        if (!resultAdded) {
            await _dialogService.ShowAlertAsync("Une série avec ce nom existe déjà :(");
        }
        
        ClearNewSerieNameInput?.Invoke();
        AvailableSeries =
            new ObservableCollection<SerieDetailViewModel>(await _serieService.GetSeriesAvailablesAsync());
    }
    
    [RelayCommand]
    public void Test(SerieDetailViewModel serieDetailViewModel) {
        RoutingService.GoSerieManagementCommand.Execute(serieDetailViewModel);
    }

    [RelayCommand]
    public void Play(SerieDetailViewModel serieDetailViewModel) {
        RoutingService.GoSeriePlayCommand.Execute(serieDetailViewModel);
    }

    [RelayCommand]
    public async Task Delete(SerieDetailViewModel serieDetailViewModel) {
        await _serieService.DeleteAsync(serieDetailViewModel.Id);
        AvailableSeries =
            new ObservableCollection<SerieDetailViewModel>(await _serieService.GetSeriesAvailablesAsync());
    }
    
}