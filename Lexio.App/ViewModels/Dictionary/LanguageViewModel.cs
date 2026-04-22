using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Lexio.App.ViewModels.Dictionary;

public partial class LanguageViewModel : ViewModelBase {
        [ObservableProperty]
        private int _id;
        
        [ObservableProperty]
        private string _flag = String.Empty;
    
        [ObservableProperty]
        private string _name = String.Empty;

        [ObservableProperty]
        private string _code = String.Empty;
        
        public override string ToString() => Flag + " " + Name;
}