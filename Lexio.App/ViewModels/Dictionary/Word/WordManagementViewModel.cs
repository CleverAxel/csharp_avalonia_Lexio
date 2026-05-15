using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexio.App.Dialog;
using Lexio.App.Helpers;
using Lexio.App.Routing;
using Lexio.App.Services;

namespace Lexio.App.ViewModels.Dictionary.Word;

public partial class WordManagementViewModel : ViewModelBase {
    [ObservableProperty]
    private RoutingService _routingService;

    private DialogService _dialogService;
    private WordService _wordService;

    /*public List<char> FilterChars { get; set; } = new List<char>() {
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
    };*/

    private char _selectedChar = 'A';

    public ObservableCollection<FilterCharViewModel> FilterChars {
        get;
        set => SetProperty(ref field, value);
    } = new ObservableCollection<FilterCharViewModel>();
    
    public ObservableCollection<WordViewModel> WordList {
        get;
        set => SetProperty(ref field, value);
    } = new ObservableCollection<WordViewModel>();

    public WordManagementViewModel(
        RoutingService routingService,
        DialogService dialogService,
        WordService wordService
    ) {
        _routingService = routingService;
        _dialogService = dialogService;
        _wordService = wordService;
        routingService.SetPath(
            routingService.HomeBreadcrumb(),
            routingService.DictionaryBreadcrumb(),
            routingService.WordManagementBreadcrumb(true)
        );

        UpdateUIFilterChar();
        _ = RefreshWordList();
    }


    private void UpdateUIFilterChar(char c = 'A') {
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
        
        UpdateUIFilterChar(c);
        await RefreshWordList();
    }

    public async Task RefreshWordList() {
        WordList = new ObservableCollection<WordViewModel>(await _wordService.GetWordListStartingBy(_selectedChar.ToString()));
        
    }

    [RelayCommand]
    public async Task AddNewWord() {
        string? newWord =
            await _dialogService.ShowPromptAsync("Quel est le nouveau mot à ajouter ?", "", "Nouveau mot");
        if (String.IsNullOrWhiteSpace(newWord))
            return;

        _ = Task.Run(async () => {
            try {
                await _wordService.AddNewWord(newWord);
                await _dialogService.ShowAlertAsync($"Le mot : \"{newWord}\", a été correctement ajouté.", "Mot ajouté.");
                UpdateUIFilterChar(newWord[0]);
                await RefreshWordList();
            }
            catch (Exception e) {
                await _dialogService.ShowAlertAsync(e.Message);
            }
        });
    }


    [RelayCommand]
    public async Task Test() {
        string? test = await _dialogService.ShowPromptAsync("Ceci est mon message", "input");
        Console.WriteLine(test ?? "NULL");
    }


    [RelayCommand]
    public async Task DeleteWordAsync() {
        
    }
}