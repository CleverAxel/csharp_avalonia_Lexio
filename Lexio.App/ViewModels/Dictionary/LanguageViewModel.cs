using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Lexio.App.Dialog;
using Lexio.App.Models.Dictionary;

namespace Lexio.App.ViewModels.Dictionary;

public partial class LanguageViewModel : ViewModelBase {
    private ObservableCollection<LanguageModel> _languageModels = null!;
    public ObservableCollection<LanguageModel> LanguageModels {
        get => _languageModels;
        set => SetProperty(ref _languageModels, value);
    }

    private readonly DialogService _dialogService;
    public LanguageViewModel(DialogService dialogService) {
        LoadDummyLanguages();
        _dialogService = dialogService;
    }
    
    [RelayCommand]
    private async Task Delete(LanguageModel languageModel) {
        string message = $"Supprimer le language : {languageModel.Name} ?\nCette action est irréversible.";
        bool shouldDelete = await _dialogService.ShowConfirmAsync(message, "Confirmer la suppression du language");
        if (shouldDelete)
            LanguageModels.Remove(languageModel);
    }

    private void LoadDummyLanguages() {
        LanguageModels = new ObservableCollection<LanguageModel> {
            new() { Id = 1, Name = "🇫🇷Français", Code = "fr" },
            new() { Id = 2, Name = "🇬🇧Anglais", Code = "en" },
            new() { Id = 3, Name = "🇳🇱Néerlandais", Code = "nl" },
            new() { Id = 4, Name = "🇪🇸Espagnol", Code = "es" },
            new() { Id = 5, Name = "🇩🇪Allemand", Code = "de" },
            new() { Id = 6, Name = "🇮🇹Italien", Code = "it" },
            new() { Id = 7, Name = "Portugais", Code = "pt" },
            new() { Id = 8, Name = "Russe", Code = "ru" },
            new() { Id = 9, Name = "Japonais", Code = "ja" },
            new() { Id = 10, Name = "Chinois", Code = "zh" },
            new() { Id = 11, Name = "Coréen", Code = "ko" },
            new() { Id = 12, Name = "Arabe", Code = "ar" },
            new() { Id = 13, Name = "Turc", Code = "tr" },
            new() { Id = 14, Name = "Polonais", Code = "pl" },
            new() { Id = 15, Name = "Suédois", Code = "sv" },
            new() { Id = 16, Name = "Norvégien", Code = "no" },
            new() { Id = 17, Name = "Danois", Code = "da" },
            new() { Id = 18, Name = "Finnois", Code = "fi" },
            new() { Id = 19, Name = "Grec", Code = "el" },
            new() { Id = 20, Name = "Hébreu", Code = "he" },
        };
    }
}