using CommunityToolkit.Mvvm.ComponentModel;
using Lexio.App.Routing;

namespace Lexio.App.ViewModels.Dictionary.Word;

public partial class WordManagementViewModel : ViewModelBase {
    [ObservableProperty]
    private RoutingService _routingService;
    
    public WordManagementViewModel(
        RoutingService routingService
    ) {
        _routingService = routingService;
        
        routingService.SetPath(
            routingService.HomeBreadcrumb(),
            routingService.DictionaryBreadcrumb(),
            routingService.WordManagementBreadcrumb(true)
        );
    }
}