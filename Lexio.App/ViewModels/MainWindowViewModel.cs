using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexio.App.Routing;
using Lexio.App.ViewModels.Dictionary;
using Microsoft.Extensions.DependencyInjection;

namespace Lexio.App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase {
    [ObservableProperty]
    private ObservableObject _currentPage;

    private ObservableCollection<BreadcrumbItem> _breadcrumbItems = new ObservableCollection<BreadcrumbItem>();

    public ObservableCollection<BreadcrumbItem> Breadcrumbs {
        get => _breadcrumbItems;
        set => SetProperty(ref _breadcrumbItems, value);
    }


    [ObservableProperty]
    private RoutingService _routingService;

    public MainWindowViewModel(
        RoutingService routingService
    ) {
        _routingService = routingService;
        routingService.GoDictionaryCommand = GoToDictionaryCommand;
        routingService.GoLanguageManagementCommand = GoToLanguageManagementCommand;
        routingService.GoHomeCommand = GoToHomeCommand;

        // TODO Cleanup
        routingService.BreadcrumbChanged += OnBreadcrumbChanged;

        _currentPage = App.ServiceProvider.GetRequiredService<HomeViewModel>();

    }

    private void OnBreadcrumbChanged(IEnumerable<BreadcrumbItem> items) {
        Breadcrumbs.Clear();
        foreach (var item in items)
            Breadcrumbs.Add(item);
    }


    [RelayCommand]
    private void GoToHome() => CurrentPage = App.ServiceProvider.GetRequiredService<HomeViewModel>();

    [RelayCommand]
    private void GoToDictionary() => CurrentPage = App.ServiceProvider.GetRequiredService<DictionaryViewModel>();

    [RelayCommand]
    private void GoToLanguageManagement(string? query)  {
        Console.WriteLine(query);
        CurrentPage = App.ServiceProvider.GetRequiredService<LanguageManagementViewModel>();
    }
}