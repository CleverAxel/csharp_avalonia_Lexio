using CommunityToolkit.Mvvm.ComponentModel;

namespace Lexio.App.ViewModels.Serie;

public partial class SerieResultDetailViewModel : ViewModelBase {

    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private string _createdAt = "";

    [ObservableProperty]
    private int _correctAnswerCount;

    [ObservableProperty]
    private int _questionCount;

    [ObservableProperty]
    private string _isAReplay;

    [ObservableProperty]
    private string _serieName;

    [ObservableProperty]
    private double _percentage;

}