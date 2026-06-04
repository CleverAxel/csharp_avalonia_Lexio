using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Lexio.App.ViewModels.Dictionary.Language;
using Lexio.App.ViewModels.Serie;

namespace Lexio.App.Views.Serie;

public partial class SerieView : UserControl {
    private TextBox? _newSerieName;
    private SerieViewModel? _vm;
    public SerieView() {
        InitializeComponent();
        
        _vm = DataContext as SerieViewModel;
        _newSerieName = this.FindControl<TextBox>("NewSerieTextInput");
    }
    
    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        _vm = DataContext as SerieViewModel;
        _vm?.ClearNewSerieNameInput = ClearNewSerieNameInput;
    }

    private void ClearNewSerieNameInput() {
        _newSerieName?.Clear();
    }
}