using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;

namespace Lexio.App.ViewModels.Dictionary.Dialog;

public partial class LanguageDeleteDialogViewModel : ViewModelBase {
    private readonly Window _dialog;

    public string Message { get; }

    public LanguageDeleteDialogViewModel(Window dialog, string message)
    {
        _dialog = dialog;
        Message = message;
    }

    [RelayCommand]
    private void Confirm() => _dialog.Close(true);

    [RelayCommand]
    private void Cancel() => _dialog.Close(false);
}