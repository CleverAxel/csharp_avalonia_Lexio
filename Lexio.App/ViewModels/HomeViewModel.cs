using CommunityToolkit.Mvvm.Input;
using Lexio.App.DI;

namespace Lexio.App.ViewModels;

public partial class HomeViewModel : ViewModelBase {
    private TestInjection _testInjection;
    public HomeViewModel(TestInjection testInjection) {
        _testInjection = testInjection;
    }
    
    [RelayCommand]
    private void Save()
    {
        // Save logic
    }
}