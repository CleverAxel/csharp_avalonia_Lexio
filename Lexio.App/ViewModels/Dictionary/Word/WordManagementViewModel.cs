using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexio.App.Dialog;
using Lexio.App.Routing;

namespace Lexio.App.ViewModels.Dictionary.Word;

public partial class WordManagementViewModel : ViewModelBase {
    [ObservableProperty]
    private RoutingService _routingService;

    private DialogService _dialogService;
    
    public WordManagementViewModel(
        RoutingService routingService,
        DialogService dialogService
    ) {
        _routingService = routingService;
        _dialogService = dialogService;
        routingService.SetPath(
            routingService.HomeBreadcrumb(),
            routingService.DictionaryBreadcrumb(),
            routingService.WordManagementBreadcrumb(true)
        );
    }


    [RelayCommand]
    public async Task Test() {
        string? test = await _dialogService.ShowPromptAsync("Ceci est mon message", "input");
        Console.WriteLine(test ?? "NULL");
    }
}