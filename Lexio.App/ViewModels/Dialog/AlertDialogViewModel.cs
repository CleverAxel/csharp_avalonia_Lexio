using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;

namespace Lexio.App.ViewModels.Dialog;

public partial class AlertDialogViewModel : ViewModelBase {
    private readonly Window _dialog;

    public string Message { get; }

    public AlertDialogViewModel(Window dialog, string message)
    {
        _dialog = dialog;
        Message = message;
    }

    [RelayCommand]
    private void Confirm() => _dialog.Close();
}