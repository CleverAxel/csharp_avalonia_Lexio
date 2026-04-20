using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Lexio.App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase {
    [ObservableProperty]
    private ObservableObject _currentPage;

    public MainWindowViewModel() {
        _currentPage = App.ServiceProvider.GetRequiredService<HomeViewModel>();;
    }
    
    [RelayCommand]
    private void GoToDictionary() => CurrentPage = new DictionaryViewModel();
}