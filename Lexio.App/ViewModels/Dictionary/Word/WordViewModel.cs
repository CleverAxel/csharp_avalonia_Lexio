using CommunityToolkit.Mvvm.ComponentModel;

namespace Lexio.App.ViewModels.Dictionary.Word;

public partial class WordViewModel : ViewModelBase {
    [ObservableProperty]
    private int _id = 0;

    [ObservableProperty]
    private string _name = null!;
    
    [ObservableProperty]
    private string _definition = null!;
}