using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Lexio.App.ViewModels.Dialog;

public partial class PromptDialogViewModel : ViewModelBase {
    private readonly Window _dialog;

    [ObservableProperty]
    private string _input;
    
    public string Message { get; }

    public PromptDialogViewModel(Window dialog, string message, string input = "")
    {
        _dialog = dialog;
        Message = message;
        _input = input;
    }

    [RelayCommand]
    private void Confirm() => _dialog.Close(Input);

    [RelayCommand]
    private void Cancel() => _dialog.Close(null);
}