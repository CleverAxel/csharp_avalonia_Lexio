using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Lexio.App.Models.Dictionary;

public partial class LanguageModel : ObservableObject {
    [ObservableProperty]
    private int _id;
    
    [ObservableProperty]
    private string _name = String.Empty;

    [ObservableProperty]
    private string _code = String.Empty;
}