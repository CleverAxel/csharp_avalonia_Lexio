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
        routingService.GoLanguageManagementCommand = GoToLanguageManagementCommand;
        _currentPage = App.ServiceProvider.GetRequiredService<HomeViewModel>();
    }

    [RelayCommand]
    private void GoToDictionary() => CurrentPage = App.ServiceProvider.GetRequiredService<DictionaryViewModel>();

    [RelayCommand]
    private void GoToLanguageManagement() => CurrentPage = App.ServiceProvider.GetRequiredService<LanguageViewModel>();
}