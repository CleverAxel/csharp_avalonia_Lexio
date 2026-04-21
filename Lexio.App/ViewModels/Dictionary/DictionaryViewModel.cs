using CommunityToolkit.Mvvm.ComponentModel;
using Lexio.App.Routing;

namespace Lexio.App.ViewModels.Dictionary;

public partial class DictionaryViewModel : ViewModelBase {
    [ObservableProperty]
    private RoutingService _routingService;
    
    public DictionaryViewModel(
        RoutingService routingService
    ) {
        _routingService = routingService;
    }
}