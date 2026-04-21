using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Lexio.App.DI;
using Lexio.App.ViewModels;
using Lexio.App.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Lexio.App;

public partial class App : Application {

    public static ServiceProvider ServiceProvider { get; private set; } = null!;

    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted() {
        // If you use CommunityToolkit, line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        //BindingPlugins.DataValidators.RemoveAt(0);
        var toRemove = BindingPlugins.DataValidators
            .OfType<DataAnnotationsValidationPlugin>()
            .ToArray();

        foreach (var plugin in toRemove)
            BindingPlugins.DataValidators.Remove(plugin);

        // Register all the services needed for the application to run
        var collection = new ServiceCollection();
        collection.AddCommonServices();

        // Creates a ServiceProvider containing services from the provided IServiceCollection
        ServiceProvider = collection.BuildServiceProvider();

        var vm = ServiceProvider.GetRequiredService<MainWindowViewModel>();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            desktop.MainWindow = new MainWindow {
                DataContext = vm
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform) {
            singleViewPlatform.MainView = new MainWindow {
                DataContext = vm
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}