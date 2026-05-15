using CommunityToolkit.Mvvm.ComponentModel;

namespace Lexio.App.ViewModels.Dictionary.Word;

public partial class FilterCharViewModel : ViewModelBase {

    [ObservableProperty]
    private char _char;

    [ObservableProperty]
    private bool _isSelected;

    public FilterCharViewModel(char c, bool isSelected) {
        Char = c;
        IsSelected = isSelected;
    }
    
}