using System.Threading.Tasks;
using Avalonia.Controls;
using Lexio.App.ViewModels;
using Lexio.App.Views;

namespace Lexio.App.Dialog;

public class DialogService {
    public Window MainWindow { get; set; } = null!;
    
    public async Task<bool> ShowConfirmAsync(string message, string title = "Confirmer")
    {
        ConfirmDialogView dialog = new ConfirmDialogView();
        dialog.Title = title;
        dialog.DataContext = new ConfirmDialogViewModel(dialog, message);
        bool? result = await dialog.ShowDialog<bool?>(MainWindow);
        return result == true;
    }
}