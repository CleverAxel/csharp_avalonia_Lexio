using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Lexio.App.Dialog;
using Lexio.App.Helpers;
using Lexio.App.Routing;
using Lexio.App.Services;
using Lexio.App.ViewModels.Dictionary.Language;
using Lexio.App.ViewModels.Dictionary.Word;

namespace Lexio.App.ViewModels.Dictionary.Traduction;

public partial class TraductionManagementViewModel : ViewModelBase {

    public string LanguageCode { get; set; }
    public int LanguageId { get; set; }
    private DialogService _dialogService;

    private TraductionService _traductionService;
    private char _selectedChar = 'O';
    public ObservableCollection<FilterCharViewModel> FilterChars {
        get;
        set => SetProperty(ref field, value);
    } = new ObservableCollection<FilterCharViewModel>();
    
    
    public ObservableCollection<TraductionViewModel> TraductionList {
        get;
        set => SetProperty(ref field, value);
    } = new ObservableCollection<TraductionViewModel>();
    
    public TraductionManagementViewModel(RoutingService routingService, TraductionService traductionService, DialogService dialogService) {
        _dialogService = dialogService;
        _traductionService = traductionService;
        UpdateUiFilterChar(_selectedChar);
        _ = Task.Run(async () => {
            TraductionList = new ObservableCollection<TraductionViewModel>(await _traductionService.GetWordListStartingBy(_selectedChar.ToString()));
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
        if(c == _selectedChar)
            return;
        
        UpdateUiFilterChar(c);
    }

    [RelayCommand]
    public async Task AddNewTraductionAsync(WordViewModel wordViewModel) {
        string? newTrad = await _dialogService.ShowPromptAsync($"Nouvelle traduction du mot :\n{wordViewModel.Name} :", "");
        if(string.IsNullOrWhiteSpace(newTrad))
            return;
        _ = Task.Run(async () => {
            TraductionList.First(w => w.SourceWord.Id == wordViewModel.Id)
                .TargetWords
                .Add(
                    await _traductionService.AddNewTraduction(wordViewModel, LanguageId, newTrad)
                );
        });
    }

    [RelayCommand]
    public async Task DeleteTraductionAsync(WordViewModel wordViewModel) {
        _ = Task.Run(async () => {
            await _traductionService.DeleteWord(wordViewModel.Id);
            TraductionList.First(w => w.TargetWords.Contains(wordViewModel)).TargetWords.Remove(wordViewModel);
        });
    }
    
    [RelayCommand]
    public async Task EditTraductionAsync(WordViewModel wordViewModel) {
        string? newTrad = await _dialogService.ShowPromptAsync($"Modification du mot :\n{wordViewModel.Name}", "");
        if(string.IsNullOrWhiteSpace(newTrad))
            return;
        
        _ = Task.Run(async () => {
            await _traductionService.EditWord(wordViewModel.Id, newTrad);
            TraductionList.First(w => w.TargetWords.Contains(wordViewModel))
                .TargetWords.First(w => w.Id == wordViewModel.Id).Name = newTrad;
        });
            
    }
}