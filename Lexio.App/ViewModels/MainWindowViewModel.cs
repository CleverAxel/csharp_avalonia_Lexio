using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexio.App.Routing;
using Lexio.App.ViewModels.Dictionary;
using Microsoft.Extensions.DependencyInjection;

namespace Lexio.App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase {
    [ObservableProperty]
    private ObservableObject _currentPage;
    
    public MainWindowViewModel(
        RoutingService routingService
    ) {
        routingService.GoDictionaryCommand = GoToDictionaryCommand;
        _currentPage = App.ServiceProvider.GetRequiredService<LanguageViewModel>();
    }
    
    [RelayCommand]
    private void GoToDictionary() => CurrentPage = new DictionaryViewModel();
}