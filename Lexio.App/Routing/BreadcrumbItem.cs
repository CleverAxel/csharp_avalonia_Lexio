using CommunityToolkit.Mvvm.Input;

namespace Lexio.App.Routing;

public class BreadcrumbItem {
    public IRelayCommand Command { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
}