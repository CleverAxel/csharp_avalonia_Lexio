using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lexio.App.Dialog;
using Lexio.App.Helpers;

namespace Lexio.App.ViewModels.Dictionary;

public partial class LanguageManagementViewModel : ViewModelBase {
    private ObservableCollection<LanguageViewModel> _languageModels = null!;
    
    [ObservableProperty]
    private LanguageViewModel? _selectedLanguage;

    public ObservableCollection<LanguageViewModel> LanguageModels {
        get => _languageModels;
        set => SetProperty(ref _languageModels, value);
    }

    [ObservableProperty]
    private string _newLanguageName;

    [ObservableProperty]
    private string _newLanguageCode;

    private readonly DialogService _dialogService;

    public LanguageManagementViewModel(DialogService dialogService) {
        LoadDummyLanguages();
        _dialogService = dialogService;
    }

    [RelayCommand]
    private async Task Delete(LanguageViewModel languageViewModel) {
        string message = $"Supprimer le language : {languageViewModel.Name} ?\nCette action est irréversible.";
        bool shouldDelete = await _dialogService.ShowConfirmAsync(message, "Confirmer la suppression du language");
        if (shouldDelete)
            LanguageModels.Remove(languageViewModel);
    }

    [RelayCommand]
    private async Task AddNewLanguage() {
        if(SelectedLanguage is null)
            return;

        var lang = _languageModels.FirstOrDefault((t) => t.Id == SelectedLanguage.Id);
        if(lang is null)
            return;
        
        WeakReferenceMessenger.Default.Send(lang);
    }

    private void LoadDummyLanguages() {
        LanguageModels = new ObservableCollection<LanguageViewModel> {
            new() { Id = 1, Name = "Afrikaans", Code = "af", Flag = "🇿🇦" },
            new() { Id = 2, Name = "Albanais", Code = "sq", Flag = "🇦🇱" },
            new() { Id = 3, Name = "Allemand", Code = "de", Flag = "🇩🇪" },
            new() { Id = 4, Name = "Amharique", Code = "am", Flag = "🇪🇹" },
            new() { Id = 5, Name = "Anglais", Code = "en", Flag = "🇬🇧" },
            new() { Id = 6, Name = "Arabe", Code = "ar", Flag = "🇸🇦" },
            new() { Id = 7, Name = "Arménien", Code = "hy", Flag = "🇦🇲" },
            new() { Id = 8, Name = "Azerbaïdjanais", Code = "az", Flag = "🇦🇿" },
            new() { Id = 9, Name = "Basque", Code = "eu", Flag = "🇪🇸" },
            new() { Id = 10, Name = "Biélorusse", Code = "be", Flag = "🇧🇾" },
            new() { Id = 11, Name = "Bengali", Code = "bn", Flag = "🇧🇩" },
            new() { Id = 12, Name = "Bosniaque", Code = "bs", Flag = "🇧🇦" },
            new() { Id = 13, Name = "Bulgare", Code = "bg", Flag = "🇧🇬" },
            new() { Id = 14, Name = "Catalan", Code = "ca", Flag = "🇪🇸" },
            new() { Id = 15, Name = "Chinois simplifié", Code = "zh-CN", Flag = "🇨🇳" },
            new() { Id = 16, Name = "Chinois traditionnel", Code = "zh-TW", Flag = "🇹🇼" },
            new() { Id = 17, Name = "Croate", Code = "hr", Flag = "🇭🇷" },
            new() { Id = 18, Name = "Danois", Code = "da", Flag = "🇩🇰" },
            new() { Id = 19, Name = "Espagnol", Code = "es", Flag = "🇪🇸" },
            new() { Id = 20, Name = "Estonien", Code = "et", Flag = "🇪🇪" },
            new() { Id = 21, Name = "Finnois", Code = "fi", Flag = "🇫🇮" },
            new() { Id = 22, Name = "Français", Code = "fr", Flag = "🇫🇷" },
            new() { Id = 23, Name = "Galicien", Code = "gl", Flag = "🇪🇸" },
            new() { Id = 24, Name = "Géorgien", Code = "ka", Flag = "🇬🇪" },
            new() { Id = 25, Name = "Grec", Code = "el", Flag = "🇬🇷" },
            new() { Id = 26, Name = "Gujarati", Code = "gu", Flag = "🇮🇳" },
            new() { Id = 27, Name = "Haïtien créole", Code = "ht", Flag = "🇭🇹" },
            new() { Id = 28, Name = "Haoussa", Code = "ha", Flag = "🇳🇬" },
            new() { Id = 29, Name = "Hébreu", Code = "he", Flag = "🇮🇱" },
            new() { Id = 30, Name = "Hindi", Code = "hi", Flag = "🇮🇳" },
            new() { Id = 31, Name = "Hongrois", Code = "hu", Flag = "🇭🇺" },
            new() { Id = 32, Name = "Igbo", Code = "ig", Flag = "🇳🇬" },
            new() { Id = 33, Name = "Indonésien", Code = "id", Flag = "🇮🇩" },
            new() { Id = 34, Name = "Irlandais", Code = "ga", Flag = "🇮🇪" },
            new() { Id = 35, Name = "Islandais", Code = "is", Flag = "🇮🇸" },
            new() { Id = 36, Name = "Italien", Code = "it", Flag = "🇮🇹" },
            new() { Id = 37, Name = "Japonais", Code = "ja", Flag = "🇯🇵" },
            new() { Id = 38, Name = "Javanais", Code = "jv", Flag = "🇮🇩" },
            new() { Id = 39, Name = "Kannada", Code = "kn", Flag = "🇮🇳" },
            new() { Id = 40, Name = "Kazakh", Code = "kk", Flag = "🇰🇿" },
            new() { Id = 41, Name = "Khmer", Code = "km", Flag = "🇰🇭" },
            new() { Id = 42, Name = "Coréen", Code = "ko", Flag = "🇰🇷" },
            new() { Id = 43, Name = "Letton", Code = "lv", Flag = "🇱🇻" },
            new() { Id = 44, Name = "Lituanien", Code = "lt", Flag = "🇱🇹" },
            new() { Id = 45, Name = "Macédonien", Code = "mk", Flag = "🇲🇰" },
            new() { Id = 46, Name = "Malais", Code = "ms", Flag = "🇲🇾" },
            new() { Id = 47, Name = "Malayalam", Code = "ml", Flag = "🇮🇳" },
            new() { Id = 48, Name = "Maltais", Code = "mt", Flag = "🇲🇹" },
            new() { Id = 49, Name = "Marathi", Code = "mr", Flag = "🇮🇳" },
            new() { Id = 50, Name = "Mongol", Code = "mn", Flag = "🇲🇳" },
            new() { Id = 51, Name = "Népalais", Code = "ne", Flag = "🇳🇵" },
            new() { Id = 52, Name = "Norvégien", Code = "no", Flag = "🇳🇴" },
            new() { Id = 53, Name = "Ouzbek", Code = "uz", Flag = "🇺🇿" },
            new() { Id = 54, Name = "Pachto", Code = "ps", Flag = "🇦🇫" },
            new() { Id = 55, Name = "Persan", Code = "fa", Flag = "🇮🇷" },
            new() { Id = 56, Name = "Polonais", Code = "pl", Flag = "🇵🇱" },
            new() { Id = 57, Name = "Portugais (Brésil)", Code = "pt-BR", Flag = "🇧🇷" },
            new() { Id = 58, Name = "Portugais (Portugal)", Code = "pt-PT", Flag = "🇵🇹" },
            new() { Id = 59, Name = "Punjabi", Code = "pa", Flag = "🇮🇳" },
            new() { Id = 60, Name = "Roumain", Code = "ro", Flag = "🇷🇴" },
            new() { Id = 61, Name = "Russe", Code = "ru", Flag = "🇷🇺" },
            new() { Id = 62, Name = "Serbe", Code = "sr", Flag = "🇷🇸" },
            new() { Id = 63, Name = "Cingalais", Code = "si", Flag = "🇱🇰" },
            new() { Id = 64, Name = "Slovaque", Code = "sk", Flag = "🇸🇰" },
            new() { Id = 65, Name = "Slovène", Code = "sl", Flag = "🇸🇮" },
            new() { Id = 66, Name = "Somali", Code = "so", Flag = "🇸🇴" },
            new() { Id = 67, Name = "Soundanais", Code = "su", Flag = "🇮🇩" },
            new() { Id = 68, Name = "Suédois", Code = "sv", Flag = "🇸🇪" },
            new() { Id = 69, Name = "Swahili", Code = "sw", Flag = "🇰🇪" },
            new() { Id = 70, Name = "Tamoul", Code = "ta", Flag = "🇮🇳" },
            new() { Id = 71, Name = "Télougou", Code = "te", Flag = "🇮🇳" },
            new() { Id = 72, Name = "Thaï", Code = "th", Flag = "🇹🇭" },
            new() { Id = 73, Name = "Tchèque", Code = "cs", Flag = "🇨🇿" },
            new() { Id = 74, Name = "Turc", Code = "tr", Flag = "🇹🇷" },
            new() { Id = 75, Name = "Ukrainien", Code = "uk", Flag = "🇺🇦" },
            new() { Id = 76, Name = "Ourdou", Code = "ur", Flag = "🇵🇰" },
            new() { Id = 77, Name = "Vietnamien", Code = "vi", Flag = "🇻🇳" },
            new() { Id = 78, Name = "Yoruba", Code = "yo", Flag = "🇳🇬" },
            new() { Id = 79, Name = "Zoulou", Code = "zu", Flag = "🇿🇦" },
        };
    }
}