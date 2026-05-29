using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lexio.App.Dialog;
using Lexio.App.Routing;
using Lexio.App.Services;

namespace Lexio.App.ViewModels.Dictionary.Language;

public partial class LanguageManagementViewModel : ViewModelBase {
    public Action? ClearAutoCompleteTextInput;

    [ObservableProperty]
    private bool _showLanguageAlreadyExist = false;

    public ObservableCollection<LanguageViewModel> AvailableLanguageModels {
        get;
        set => SetProperty(ref field, value);
    } = null!;

    public ObservableCollection<LanguageViewModel> AddedLanguageModels {
        get;
        set => SetProperty(ref field, value);
    } = new ObservableCollection<LanguageViewModel>();

    [ObservableProperty]
    private LanguageViewModel? _selectedLanguage;

    private readonly DialogService _dialogService;
    private readonly LanguageService _languageService;

    public LanguageManagementViewModel(DialogService dialogService, RoutingService routingService, LanguageService languageService) {
        _dialogService = dialogService;
        _languageService = languageService;
        
        // my db context (singleton for now)
        // not thread safe https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/
        // possible solution : DbContextFactory<AppDbContext> _factory;
        _ = Task.Run(async () => {
            await LoadAddedLanguages();
            await LoadAvailableLanguages();
        });
        
        
        routingService.SetPath(
            routingService.HomeBreadcrumb(),
            routingService.DictionaryBreadcrumb(),
            routingService.LanguageManagementBreadcrumb(true)
        );
    }

    [RelayCommand]
    private async Task Delete(LanguageViewModel languageViewModel) {
        string message = $"Supprimer le language : {languageViewModel.Name} ?\nCette action est irréversible.";
        bool shouldDelete = await _dialogService.ShowConfirmAsync(message, "Confirmer la suppression du language");
        if (shouldDelete) {
            _ = Task.Run(async () => {
                await _languageService.RemoveLanguageAsync(name: languageViewModel.Name);
                AddedLanguageModels.Remove(languageViewModel);
            });
        }
    }

    [RelayCommand]
    private async Task AddNewLanguage() {
        if (SelectedLanguage is null)
            return;

        var lang = AddedLanguageModels.FirstOrDefault((t) => t.Name == SelectedLanguage.Name);
        if (lang is not null) {
            WeakReferenceMessenger.Default.Send(lang);
            ShowLanguageAlreadyExist = true;
            SelectedLanguage = null;
            ClearAutoCompleteTextInput?.Invoke();

            // it's blocking the thread, what the fuck
            _ = Task.Run(async () => {
                await Task.Delay(7500);
                ShowLanguageAlreadyExist = false;
            });
            return;
        }

        var clone = SelectedLanguage.Clone();
        _ =_languageService.AddLanguageAsync(
            name: clone.Name,
            code:clone.Code,
            flag:clone.Flag
        ).ContinueWith((task => {
            // fucking dirty as fuck for now
            AddedLanguageModels.Add(clone);
            var sorted = AddedLanguageModels.OrderBy(l => l.Name).ToList();
            AddedLanguageModels.Clear();
            foreach (var item in sorted) {
                AddedLanguageModels.Add(item);
            }

            WeakReferenceMessenger.Default.Send(clone);
            ClearAutoCompleteTextInput?.Invoke();
        }));
    }

    private async Task LoadAvailableLanguages() {
        AvailableLanguageModels = await _languageService.GetAvailableLanguagesAsync();
    }

    private async Task LoadAddedLanguages() {
        var addedLng = await _languageService.GetAddedLanguagesAsync();
        
        AddedLanguageModels = addedLng;
    }
    
    
}