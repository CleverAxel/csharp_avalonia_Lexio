using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Lexio.App.Models.Dictionary;
using Lexio.App.ViewModels.Dictionary;
using Lexio.App.ViewModels.Dictionary.Dialog;
using Lexio.App.Views.Dictionary.Dialog;

namespace Lexio.App.Views.Dictionary;

public partial class LanguageView : UserControl {
    public LanguageView() {
        InitializeComponent();
    }
    
    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        if (DataContext is LanguageViewModel vm)
            vm.DeleteRequested += OnDeleteRequested;
    }

    private async void OnDeleteRequested(LanguageModel languageModel)
    {
        var topLevel = TopLevel.GetTopLevel(this) as Window;
        var dialog = new LanguageDeleteDialogView();
        var vm = new LanguageDeleteDialogViewModel(dialog, "Delete this item?");
        dialog.DataContext = vm;
        bool? result = await dialog.ShowDialog<bool?>(topLevel);
        if (result == true && DataContext is LanguageViewModel lvm)
            lvm.LanguageModels.Remove(languageModel);
    }
}