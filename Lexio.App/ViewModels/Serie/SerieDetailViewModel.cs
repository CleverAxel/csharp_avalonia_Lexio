using CommunityToolkit.Mvvm.ComponentModel;

namespace Lexio.App.ViewModels.Serie;

public partial class SerieDetailViewModel : ViewModelBase {
    [ObservableProperty]
    private int _languageId;
    [ObservableProperty]
    private string _languageName = string.Empty;
    [ObservableProperty]
    private string _languageFlag = string.Empty;

    [ObservableProperty]
    private int _id;
    [ObservableProperty]
    private string _name = string.Empty;
    [ObservableProperty]
    private int _wordCount;
}