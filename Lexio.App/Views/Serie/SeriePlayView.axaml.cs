using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Lexio.App.ViewModels.Serie;

namespace Lexio.App.Views.Serie;

public partial class SeriePlayView : UserControl {
    
    private TextBox? _answer;
    private Button? _submitButton;
    private SeriePlayViewModel? _vm;
    public SeriePlayView() {
        
        InitializeComponent();
        
        _vm = DataContext as SeriePlayViewModel;
        _answer = this.FindControl<TextBox>("Answer");
        _submitButton = this.FindControl<Button>("SubmitButton");
    }
    
    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        _vm = DataContext as SeriePlayViewModel;
        _vm?.ClearAnswerInput = ClearAnswerInput;
        _vm?.FocusAnswerInput = FocusAnswerInput;
        _vm?.FocusSubmitButton = FocusSubmitButton;
    }

    private void ClearAnswerInput() {
        _answer?.Clear();
    }

    private void FocusAnswerInput() {
        _answer?.Focus();
    }

    private void FocusSubmitButton() {
        _submitButton?.Focus();
    }
}