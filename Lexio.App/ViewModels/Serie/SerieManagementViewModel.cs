using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Lexio.App.Dialog;
using Lexio.App.Helpers;
using Lexio.App.Routing;
using Lexio.App.Services;
using Lexio.App.ViewModels.Dictionary.Traduction;
using Lexio.App.ViewModels.Dictionary.Word;

namespace Lexio.App.ViewModels.Serie;

public partial class SerieManagementViewModel : ViewModelBase {
    private char _selectedChar = 'A';
    private DialogService _dialogService;
    private TraductionService _traductionService;
    private SerieService _serieService;
    public string LanguageCode { get; set; } = string.Empty;
    public int LanguageId { get; set; }
    public string LanguageFlag { get; set; } = string.Empty;
    public int SerieId { get; set; }

    public ObservableCollection<FilterCharViewModel> FilterChars {
        get;
        set => SetProperty(ref field, value);
    } = new ObservableCollection<FilterCharViewModel>();

    public ObservableCollection<TraductionViewModel> TraductionList {
        get;
        set => SetProperty(ref field, value);
    } = new ObservableCollection<TraductionViewModel>();

    public SerieManagementViewModel(RoutingService routingService, TraductionService traductionService,
        DialogService dialogService, SerieService serieService) {
        _dialogService = dialogService;
        _traductionService = traductionService;
        _serieService = serieService;
        UpdateUiFilterChar(_selectedChar);
        _ = Task.Run(async () => {
            TraductionList = new ObservableCollection<TraductionViewModel>(
                await _traductionService.GetWordListStartingBy(_selectedChar.ToString(), LanguageId, SerieId));
        });
    }

    private void UpdateUiFilterChar(char c = 'A') {
        _selectedChar = c.ToString().ToAscii().ToUpper()[0];
        FilterChars =
            new ObservableCollection<FilterCharViewModel>(
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Select(curr => new FilterCharViewModel(curr, _selectedChar == curr))
            );
    }

    [RelayCommand]
    public async Task ApplyCharFilterAsync(char c) {
        if (c == _selectedChar)
            return;

        UpdateUiFilterChar(c);
        TraductionList = new ObservableCollection<TraductionViewModel>(
            await _traductionService.GetWordListStartingBy(_selectedChar.ToString(), LanguageId, SerieId));
    }

    [RelayCommand]
    public async Task ToggleWordChoice(WordViewModel wordViewModel) {
        bool toAdd = wordViewModel.IsAdded;

        if (toAdd) {
            await _serieService.AddNewWordToSerie(wordViewModel.Id, SerieId);
        }
        else {
            await _serieService.RemoveNewWordToSerie(wordViewModel.Id, SerieId);
        }
    }
}