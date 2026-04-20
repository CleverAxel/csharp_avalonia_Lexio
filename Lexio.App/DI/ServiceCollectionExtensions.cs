using Lexio.App.Routing;
using Lexio.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Lexio.App.DI;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection collection) {
        collection.AddTransient<TestInjection>();
        collection.AddTransient<MainWindowViewModel>();
        collection.AddTransient<HomeViewModel>();
        
        collection.AddSingleton<RoutingService>();
    }
}