using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexio.App.Dialog;
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
    }


    private void UpdateUIFilterChar(char c = 'A') {
        _selectedChar = c;
        FilterChars = 
            new ObservableCollection<FilterCharViewModel>(
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Select(c => new FilterCharViewModel(c, _selectedChar == c))
            );
    }


    [RelayCommand]
    public async Task ApplyCharFilterAsync(char c) {
        UpdateUIFilterChar(c);
    }

    [RelayCommand]
    public async Task AddNewWord() {
        string? newWord = await _dialogService.ShowPromptAsync("Quel est le nouveau mot à ajouter ?", "", "Nouveau mot");
        if(String.IsNullOrWhiteSpace(newWord))
            return;

        _ = Task.Run(async () => {
            await _wordService.AddNewWord(newWord);
        });
    }


    [RelayCommand]
    public async Task Test() {
        string? test = await _dialogService.ShowPromptAsync("Ceci est mon message", "input");
        Console.WriteLine(test ?? "NULL");
    }
}