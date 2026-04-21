using System;
using CommunityToolkit.Mvvm.Input;

namespace Lexio.App.Routing;

public class RoutingService {
    public IRelayCommand GoDictionaryCommand { get; set; } = null!;
    public IRelayCommand GoLanguageManagementCommand { get; set; } = null!;
}