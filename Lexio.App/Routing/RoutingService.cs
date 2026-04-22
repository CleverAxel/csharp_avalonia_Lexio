using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;

namespace Lexio.App.Routing;

public class RoutingService {
    public event Action<IEnumerable<BreadcrumbItem>>? BreadcrumbChanged;
    public void SetPath(params BreadcrumbItem[] items)
    {
        BreadcrumbChanged?.Invoke(items);
    }

    public BreadcrumbItem HomeBreadcrumb() {
        return new BreadcrumbItem() {
            Command = GoHomeCommand,
            Name = "Accueil"
        };
    }

    public BreadcrumbItem DictionaryBreadcrumb() {
        return new BreadcrumbItem() {
            Command = GoDictionaryCommand,
            Name = "Dictionnaire"
        };
    }

    public BreadcrumbItem LanguageManagementBreadcrumb() {
        return new BreadcrumbItem() {
            Command = GoLanguageManagementCommand,
            Name = "Gestion des langues"
        };
    }

    public IRelayCommand GoHomeCommand { get; set; } = null!;
    public IRelayCommand GoDictionaryCommand { get; set; } = null!;
    public IRelayCommand GoLanguageManagementCommand { get; set; } = null!;
}