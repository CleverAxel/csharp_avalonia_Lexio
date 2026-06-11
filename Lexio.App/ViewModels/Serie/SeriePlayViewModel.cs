using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexio.App.Helpers;
using Lexio.App.Routing;
using Lexio.App.Services;
using Lexio.App.ViewModels.Dictionary.Traduction;
using Lexio.App.ViewModels.Dictionary.Word;

namespace Lexio.App.ViewModels.Serie;

public partial class SeriePlayViewModel : ViewModelBase {
    public SerieDetailViewModel SerieDetailViewModel { get; set; } = null!;
    private List<TraductionViewModel> _traductionViewModels;
    private List<TraductionViewModel> _traductionViewModelsNotCorrectlyAnswered = new List<TraductionViewModel>();
    private RoutingService _routingService;
    private SerieService _serieService;

    [ObservableProperty]
    private string _serieLanguage = "";


    [ObservableProperty]
    private string _traductionWayFromLanguage = "";

    [ObservableProperty]
    private bool _fromLanguage = false;

    [ObservableProperty]
    private string _traductionWayFromFrench = "";

    [ObservableProperty]
    private bool _fromFrench = true;


    [ObservableProperty]
    private string _anyTraductionLabel = "N'importe quelle traduction convient";

    [ObservableProperty]
    private string _traductionMustMatchDefinitionLabel = "La traduction doit correspondre à la définition donnée";

    [ObservableProperty]
    private bool _canBeAnyTraduction = true;

    [ObservableProperty]
    private bool _traductionMustMatchDefinition = false;


    private int _countWithDefinition = 0;
    private int _countWithoutDefinition = 0;


    [ObservableProperty]
    private int _totalWordsCount = 0;

    [ObservableProperty]
    private int _remainingWordCount = 0;

    [ObservableProperty]
    private int _correctTranslatedWordCount = 0;

    [ObservableProperty]
    private int _incorrectTranslatedWordCount = 0;


    [ObservableProperty]
    private WordViewModel _wordToTranslate;

    [ObservableProperty]
    private string _definitionOfWord = "definition placeholder";

    [ObservableProperty]
    private string _labelAnswer = "Donnez votre réponse en ";


    [ObservableProperty]
    private bool _isPlayMenuVisible = true;

    [ObservableProperty]
    private bool _isQuizzVisible = false;


    private WordViewModel _currentSource;
    private WordViewModel _currentTarget;

    public SeriePlayViewModel(RoutingService routingService, SerieService serieService) {
        _routingService = routingService;
        _serieService = serieService;
        _ = Task.Run(async () => {
            SerieLanguage = $"en {SerieDetailViewModel.LanguageName} {SerieDetailViewModel.LanguageFlag}";
            TraductionWayFromLanguage = $"Traduire de {SerieDetailViewModel.LanguageName.ToLower()} en français";
            TraductionWayFromFrench = $"Traduire de français en {SerieDetailViewModel.LanguageName.ToLower()}";

            _traductionViewModels = await serieService.RetrieveTranslationFromSerieId(SerieDetailViewModel.Id);

            _countWithDefinition = _traductionViewModels
                .Select(t => t.TargetWords.Count(w => w.IsAdded))
                .Sum();

            _countWithoutDefinition = _traductionViewModels.Count;

            AnyTraductionLabel += $" ({_countWithoutDefinition} mot(s))";
            TraductionMustMatchDefinitionLabel += $" ({_countWithDefinition} mot(s))";
        });
    }

    [RelayCommand]
    public void StartPlay() {
        if (FromFrench) {
            LabelAnswer += SerieDetailViewModel.LanguageName.ToLower() + " :";
        }
        else {
            LabelAnswer += "français :";
        }

        if (TraductionMustMatchDefinition || FromLanguage) {
            TotalWordsCount = _countWithDefinition;
        }
        else {
            TotalWordsCount = _countWithoutDefinition;
        }


        ToggleVisibilityStartQuizz();
        Pickword();
    }

    [RelayCommand]
    private void Pickword() {
        var traduction = _traductionViewModels.RandomElement();

        if (traduction is null)
            return;

        _currentSource = traduction.SourceWord;

        if (FromLanguage) {
            var tempTarget = traduction.TargetWords.RandomElement();

            if (tempTarget is null)
                return;
            
            WordToTranslate = tempTarget;
            DefinitionOfWord = WordToTranslate.Definition;
            _currentTarget = tempTarget;
        }
        else {
            // De français en anglais
            WordToTranslate = traduction.SourceWord;
            if (CanBeAnyTraduction) {
                var definitions = traduction.TargetWords.Select(t => t.Definition);
                DefinitionOfWord = string.Join(" | ", definitions);
            }
            else {
                var definition = traduction.TargetWords.RandomElement()?.Definition;
                if (definition != null)
                    DefinitionOfWord = definition;
            }
        }
    }

    private void ToggleVisibilityStartQuizz() {
        IsPlayMenuVisible = !IsPlayMenuVisible;
        IsQuizzVisible = !IsQuizzVisible;
    }
}