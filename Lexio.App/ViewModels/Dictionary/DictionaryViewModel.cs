using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexio.App.Routing;
using Lexio.App.Services;
using Lexio.App.ViewModels.Dictionary.Language;

namespace Lexio.App.ViewModels.Dictionary;

public partial class DictionaryViewModel : ViewModelBase {
    [ObservableProperty]
    private RoutingService _routingService;
    
    public ObservableCollection<LanguageViewModel> TraductionsAvailable {
        get;
        set => SetProperty(ref field, value);
    } = new ObservableCollection<LanguageViewModel>();
    
    public DictionaryViewModel(
        RoutingService routingService,
        LanguageService languageService
    ) {
        _routingService = routingService;
        
        routingService.SetPath(
            routingService.HomeBreadcrumb(),
            routingService.DictionaryBreadcrumb(true)
        );

        _ = Task.Run(async () => {
            TraductionsAvailable =
                new ObservableCollection<LanguageViewModel>(await languageService.GetAvailableTraductions());
        });
    }

    [RelayCommand]
    public void Test(LanguageViewModel languageViewModel) {
        RoutingService.GoTraductionManagementCommand.Execute(languageViewModel);
    }
}