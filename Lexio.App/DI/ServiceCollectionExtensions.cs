using Lexio.App.Dialog;
using Lexio.App.Routing;
using Lexio.App.Services;
using Lexio.App.ViewModels;
using Lexio.App.ViewModels.Dictionary.Language;
using Lexio.App.ViewModels.Dictionary;
using Lexio.App.ViewModels.Dictionary.Word;
using Lexio.Core.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Lexio.App.DI;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection collection) {
        AddViewModels(collection);
        AddServices(collection);
        collection.AddSingleton<RoutingService>();
        collection.AddSingleton<DialogService>();
        collection.AddSingleton<AppDbContext>();
    }

    private static void AddServices(this IServiceCollection collection) {
        collection.AddTransient<LanguageService>();
        collection.AddTransient<WordService>();
    }

    private static void AddViewModels(IServiceCollection collection) {
        collection.AddTransient<MainWindowViewModel>();
        
        collection.AddTransient<HomeViewModel>();
        
        collection.AddTransient<DictionaryViewModel>();
        collection.AddTransient<LanguageManagementViewModel>();
        collection.AddTransient<WordManagementViewModel>();
    }
}