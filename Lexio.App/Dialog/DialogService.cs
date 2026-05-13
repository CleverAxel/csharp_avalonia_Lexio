using System.Threading.Tasks;
using Avalonia.Controls;
using Lexio.App.ViewModels;
using Lexio.App.ViewModels.Dialog;
using Lexio.App.Views;
using Lexio.App.Views.Dialog;

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

    public async Task<string?> ShowPromptAsync(string message, string input, string title = "Entrer une valeur") {
        PromptDialogView dialog = new PromptDialogView();
        dialog.Title = title;
        dialog.DataContext = new PromptDialogViewModel(dialog, message, input);
        return await dialog.ShowDialog<string?>(MainWindow);
    }
}