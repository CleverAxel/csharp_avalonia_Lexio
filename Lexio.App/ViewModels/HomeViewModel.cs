using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexio.App.DI;
using Lexio.App.Routing;

namespace Lexio.App.ViewModels;

public partial class HomeViewModel : ViewModelBase {
    [ObservableProperty]
    private RoutingService _routingService;

 

    public HomeViewModel(
        RoutingService routingService
    ) {
        _routingService = routingService;
    }
    
}