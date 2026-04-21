using Lexio.App.Dialog;
using Lexio.App.Routing;
using Lexio.App.ViewModels;
using Lexio.App.ViewModels.Dictionary;
using Microsoft.Extensions.DependencyInjection;

namespace Lexio.App.DI;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection collection) {
        AddViewModels(collection);
        collection.AddSingleton<RoutingService>();
        collection.AddSingleton<DialogService>();
    }

    private static void AddViewModels(IServiceCollection collection) {
        collection.AddTransient<MainWindowViewModel>();
        
        collection.AddTransient<HomeViewModel>();
        
        collection.AddTransient<DictionaryViewModel>();
        collection.AddTransient<LanguageViewModel>();
    }
}