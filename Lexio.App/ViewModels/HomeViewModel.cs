using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexio.App.DI;
using Lexio.App.Routing;
using Lexio.Core.Database;

namespace Lexio.App.ViewModels;

public partial class HomeViewModel : ViewModelBase {
    [ObservableProperty]
    private RoutingService _routingService;

 

    public HomeViewModel(
        RoutingService routingService,
        AppDbContext context
    ) {
        _routingService = routingService;
        routingService.SetPath(
            routingService.HomeBreadcrumb(true)
        );

        Console.WriteLine(context.Database.CanConnect());
    }
    
}