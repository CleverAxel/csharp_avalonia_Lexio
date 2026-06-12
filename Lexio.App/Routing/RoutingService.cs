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
    
    public BreadcrumbItem WordManagementBreadcrumb(bool active = false) {
        return new BreadcrumbItem() {
            Command = GoWordManagementCommand,
            Name = "✒️Gestion des mots disponibles",
            Active = active
        };
    }

    public BreadcrumbItem TraductionManagementBreadcrumb(string flag, string language, bool active = false) {
        return new BreadcrumbItem() {
            Command = GoWordManagementCommand,
            Name = $"{flag}Gestion de la traduction : {language}",
            Active = active
        };
    }

    public BreadcrumbItem SerieBreadcrumb(bool active = false) {
        return new BreadcrumbItem() {
            Command = GoSerieCommand,
            Name = "📔Séries",
            Active = active
        };
    }
    
    public BreadcrumbItem SerieManagementBreadcrumb(string flag, string name, bool active = false) {
        return new BreadcrumbItem() {
            Command = GoSerieManagementCommand,
            Name = $"{flag}Gestion de la série : {name}",
            Active = active
        };
    }
    
    public BreadcrumbItem SeriePlayBreadcrumb(string serieName, string flag, bool active = false) {
        return new BreadcrumbItem() {
            Command = GoSeriePlayCommand,
            Name = $"🎮Jouer série : '{flag}{serieName}'",
            Active = active
        };
    }

    public BreadcrumbItem SerieResultBreadcrumb(bool active = false) {
        return new BreadcrumbItem() {
            Command = GoSerieResultCommand,
            Name = "📈Résultats des séries",
            Active = active
        };
    }

    public IRelayCommand GoHomeCommand { get; set; } = null!;
    public IRelayCommand GoDictionaryCommand { get; set; } = null!;
    public IRelayCommand GoLanguageManagementCommand { get; set; } = null!;
    public IRelayCommand GoWordManagementCommand { get; set; } = null!;
    public IRelayCommand GoTraductionManagementCommand { get; set; } = null!;
    public IRelayCommand GoSerieCommand { get; set; } = null!;
    public IRelayCommand GoSerieManagementCommand { get; set; } = null!;
    public IRelayCommand GoSeriePlayCommand { get; set; } = null!;
    public IRelayCommand GoSerieResultCommand { get; set; } = null!;
}