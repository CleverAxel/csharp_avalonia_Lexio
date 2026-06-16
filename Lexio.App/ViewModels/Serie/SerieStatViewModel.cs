using CommunityToolkit.Mvvm.ComponentModel;

namespace Lexio.App.ViewModels.Serie;

public partial class SerieStatViewModel : ViewModelBase {
    [ObservableProperty]
    private string _serieName;

    [ObservableProperty]
    private int _playedCount;

    [ObservableProperty]
    private int _totalErrors;
}