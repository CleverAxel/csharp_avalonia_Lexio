using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using Lexio.App.ViewModels.Dictionary;

namespace Lexio.App.Views.Dictionary;

public partial class LanguageManagementView : UserControl {
    private ListBox? _listLanguages;

    public LanguageManagementView() {
        WeakReferenceMessenger.Default.Register<LanguageViewModel>(this,
            async void (recipient, stuff) => {
   
                await Dispatcher.UIThread.InvokeAsync(() => { _listLanguages?.ScrollIntoView(stuff); });
                
                await Dispatcher.UIThread.InvokeAsync(() => {
                    if (_listLanguages?.ContainerFromItem(stuff) is ListBoxItem container) {
                        container.Classes.Remove("blink");
                    }
                });

                await Dispatcher.UIThread.InvokeAsync(async void () => {
                    if (_listLanguages?.ContainerFromItem(stuff) is ListBoxItem container) {
                        container.Classes.Add("blink");

                        await Dispatcher.UIThread.InvokeAsync(async void () => {
                            container.ClearValue(BorderBrushProperty);
                        });
                    }
                });
                
            });


        InitializeComponent();
        _listLanguages = this.FindControl<ListBox>("ListLanguages");
    }
}