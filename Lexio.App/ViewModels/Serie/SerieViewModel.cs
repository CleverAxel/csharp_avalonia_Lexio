using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexio.App.Services;
using Lexio.App.ViewModels.Dictionary.Language;

namespace Lexio.App.ViewModels.Serie;

public partial class SerieViewModel : ViewModelBase {
    private LanguageService _languageService;

    public Action ClearNewSerieNameInput { get; set; }

    [ObservableProperty]
    private string _newSerieName = "";

    [ObservableProperty]
    private LanguageViewModel _selectedSerieLanguage;
    
    public ObservableCollection<LanguageViewModel> AvailableLanguageModels {
        get;
        set => SetProperty(ref field, value);
    } = null!;
    
    public SerieViewModel(LanguageService languageService) {
        _languageService = languageService;

        _ = Task.Run(async () => {
            AvailableLanguageModels = new ObservableCollection<LanguageViewModel>(await _languageService.GetAvailableTraductions());
        });
    }

    [RelayCommand]
    public void AddNewSerie() {
        Console.WriteLine(NewSerieName);
        Console.WriteLine(SelectedSerieLanguage.Name);
    }
    
    
}