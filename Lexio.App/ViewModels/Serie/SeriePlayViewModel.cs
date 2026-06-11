using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    private List<TraductionViewModel> _copyTraductions;
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

    [ObservableProperty]
    private bool _submitButtonEnabled = true;

    [ObservableProperty]
    private bool _nextButtonEnabled = false;


    private WordViewModel _currentSource;
    private WordViewModel _currentTarget;

    [ObservableProperty]
    private string _answer = "";

    public SeriePlayViewModel(RoutingService routingService, SerieService serieService) {
        _routingService = routingService;
        _serieService = serieService;
        _ = Task.Run(async () => {
            SerieLanguage = $"en {SerieDetailViewModel.LanguageName} {SerieDetailViewModel.LanguageFlag}";
            TraductionWayFromLanguage = $"Traduire de {SerieDetailViewModel.LanguageName.ToLower()} en français";
            TraductionWayFromFrench = $"Traduire de français en {SerieDetailViewModel.LanguageName.ToLower()}";

            _traductionViewModels = await serieService.RetrieveTranslationFromSerieId(SerieDetailViewModel.Id);
            _copyTraductions = _traductionViewModels;
            
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
            // donne un mot anglais à traduire en français
            var tempTarget = traduction.TargetWords.RandomElement();

            if (tempTarget is null)
                return;

            WordToTranslate = tempTarget;
            DefinitionOfWord = WordToTranslate.Definition;
            _currentTarget = tempTarget;
        }
        else {
            // donne un mot français à traduire en anglais
            WordToTranslate = traduction.SourceWord;
            if (CanBeAnyTraduction) {
                var definitions = 
                    _copyTraductions
                        .First(t => t.SourceWord.Id == traduction.SourceWord.Id).TargetWords
                        .Select(t => t.Definition);
                DefinitionOfWord = string.Join("\n", definitions);
            }
            else {
                var tempTarget = traduction.TargetWords.RandomElement();
                if (tempTarget is null)
                    return;

                _currentTarget = tempTarget;
                DefinitionOfWord = _currentTarget.Definition;
            }
        }
    }

    private void ToggleVisibilityStartQuizz() {
        IsPlayMenuVisible = !IsPlayMenuVisible;
        IsQuizzVisible = !IsQuizzVisible;
    }

    private void ToggleSubmitNextButton() {
        NextButtonEnabled = !NextButtonEnabled;
        SubmitButtonEnabled = !SubmitButtonEnabled;
    }

    [RelayCommand]
    private void SubmitAnswer() {
        if(string.IsNullOrWhiteSpace(Answer))
            return;

        bool hasAnsweredCorrectly = true;
        
        if (FromLanguage) {
            //donne un mot anglais à traduire en français
            if (Answer.TrimAndReduce().ToLower() == _currentSource.Name.TrimAndReduce().ToLower()) {
                Console.WriteLine("bonne réponse yay 1");

                var tradRemove = _traductionViewModels
                    .First(t => t.SourceWord.Id == _currentSource.Id);

                tradRemove.TargetWords.Remove(_currentTarget);
                if (tradRemove.TargetWords.Count == 0) {
                    _traductionViewModels.Remove(tradRemove);
                }
            }
            else {
                var tradRemove = _traductionViewModels
                    .First(t => t.SourceWord.Id == _currentSource.Id);

                tradRemove.TargetWords.Remove(_currentTarget);
                if (tradRemove.TargetWords.Count == 0) {
                    _traductionViewModels.Remove(tradRemove);
                }


                Console.WriteLine("nyay 1");
                hasAnsweredCorrectly = false;
                
                var trad = _traductionViewModelsNotCorrectlyAnswered.FirstOrDefault(t =>
                    t.SourceWord.Id == _currentSource.Id);
                if (trad is null) {
                    _traductionViewModelsNotCorrectlyAnswered.Add(
                        new TraductionViewModel() {
                            SourceWord = _currentSource,
                            TargetWords = new ObservableCollection<WordViewModel>() {
                                _currentTarget
                            }
                        }
                    );
                }
                else {
                    if (!trad.TargetWords.Contains(_currentTarget)) {
                        trad.TargetWords.Add(_currentTarget);
                    }
                }
            }
        }
        else {
            //donne un mot français à traduire en anglais
            if (CanBeAnyTraduction) {
                var trad = _traductionViewModels.First(t => t.SourceWord.Id == _currentSource.Id);
                var allPossibleAnswers = trad.TargetWords
                    .Select(w => w.Name.TrimAndReduce().ToLower());

                if (allPossibleAnswers.Contains(Answer.TrimAndReduce().ToLower())) {
                    _traductionViewModels.Remove(trad);
                    Console.WriteLine("bonne réponse yay 2");
                }
                else {
                    _traductionViewModels.Remove(trad);

                    if (_traductionViewModelsNotCorrectlyAnswered.FirstOrDefault(t =>
                            t.SourceWord.Id == _currentSource.Id) != null) {
                        _traductionViewModelsNotCorrectlyAnswered.Add(trad);
                    }

                    Console.WriteLine("nyay 2");
                    hasAnsweredCorrectly = false;
                }
            }
            else {
                if (Answer.TrimAndReduce().ToLower() == _currentTarget.Name.TrimAndReduce().ToLower()) {
                    Console.WriteLine("bonne réponse yay 3");
                    var tradRemove = _traductionViewModels
                        .First(t => t.SourceWord.Id == _currentSource.Id);

                    tradRemove.TargetWords.Remove(_currentTarget);
                    if (tradRemove.TargetWords.Count == 0) {
                        _traductionViewModels.Remove(tradRemove);
                    }
                }
                else {
                    var tradRemove = _traductionViewModels
                        .First(t => t.SourceWord.Id == _currentSource.Id);

                    tradRemove.TargetWords.Remove(_currentTarget);
                    if (tradRemove.TargetWords.Count == 0) {
                        _traductionViewModels.Remove(tradRemove);
                    }

                    Console.WriteLine("nyay 3");
                    hasAnsweredCorrectly = false;

                    var trad = _traductionViewModelsNotCorrectlyAnswered.FirstOrDefault(t =>
                        t.SourceWord.Id == _currentSource.Id);
                    if (trad is null) {
                        _traductionViewModelsNotCorrectlyAnswered.Add(
                            new TraductionViewModel() {
                                SourceWord = _currentSource,
                                TargetWords = new ObservableCollection<WordViewModel>() {
                                    _currentTarget
                                }
                            }
                        );
                    }
                    else {
                        if (!trad.TargetWords.Contains(_currentTarget)) {
                            trad.TargetWords.Add(_currentTarget);
                        }
                    }
                }
            }
        }
        
        ToggleSubmitNextButton();
    }
}