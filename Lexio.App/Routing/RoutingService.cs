using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.Input;

namespace Lexio.App.Routing;

public class RoutingService {
    public event Action<IEnumerable<BreadcrumbItem>>? BreadcrumbChanged;

    public void SetPath(params BreadcrumbItem[] items) {
        BreadcrumbChanged?.Invoke(
            items.Select((item, index) => new BreadcrumbItem() {
                Name = item.Name,
                Command = item.Command,
                Active = item.Active,
                IsLast = index == items.Length - 1
            })
        );
    }

    public BreadcrumbItem HomeBreadcrumb(bool active = false) {
        return new BreadcrumbItem() {
            Command = GoHomeCommand,
            Name = "🏠Accueil",
            Active = active
        };
    }

    public BreadcrumbItem DictionaryBreadcrumb(bool active = false) {
        return new BreadcrumbItem() {
            Command = GoDictionaryCommand,
            Name = "📚Dictionnaire",
            Active = active
        };
    }

    public BreadcrumbItem LanguageManagementBreadcrumb(bool active = false) {
        return new BreadcrumbItem() {
            Command = GoLanguageManagementCommand,
            Name = "👅Gestion des langues",
            Active = active
        };
    }

    public IRelayCommand GoHomeCommand { get; set; } = null!;
    public IRelayCommand GoDictionaryCommand { get; set; } = null!;
    public IRelayCommand GoLanguageManagementCommand { get; set; } = null!;
}