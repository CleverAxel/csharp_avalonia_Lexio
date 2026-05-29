using System.Collections.ObjectModel;
using Lexio.App.ViewModels.Dictionary.Word;

namespace Lexio.App.ViewModels.Dictionary.Traduction;

public partial class TraductionViewModel : ViewModelBase {
    public WordViewModel SourceWord { get; set; }
    public ObservableCollection<WordViewModel> TargetWords {
        get;
        set => SetProperty(ref field, value);
    } = new ObservableCollection<WordViewModel>();
    
}