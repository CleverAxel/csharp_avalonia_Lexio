using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Lexio.App.Views.Dialog;

public partial class AlertDialogView : Window {
    public AlertDialogView() {
        InitializeComponent();
    }
    
    protected override void OnOpened(EventArgs e) {
        base.OnOpened(e);
        ButtonConfirm.Focus();
    }
}