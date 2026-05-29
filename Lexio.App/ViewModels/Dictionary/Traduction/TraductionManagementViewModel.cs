using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Lexio.App.Helpers;
using Lexio.App.Routing;
using Lexio.App.Services;
using Lexio.App.ViewModels.Dictionary.Language;
using Lexio.App.ViewModels.Dictionary.Word;

namespace Lexio.App.ViewModels.Dictionary.Traduction;

public partial class TraductionManagementViewModel : ViewModelBase {

    public string LanguageCode { get; set; }

    private TraductionService _traductionService;
    private char _selectedChar = 'O';
    public ObservableCollection<FilterCharViewModel> FilterChars {
        get;
        set => SetProperty(ref field, value);
    } = new ObservableCollection<FilterCharViewModel>();
    
    public TraductionManagementViewModel(RoutingService routingService, TraductionService traductionService) {
        _traductionService = traductionService;
        UpdateUiFilterChar(_selectedChar);
        _ = Task.Run(async () => {
            await _traductionService.GetWordListStartingBy(_selectedChar.ToString());
        });
    }
    
    private void UpdateUiFilterChar(char c = 'A') {
        _selectedChar = c.ToString().ToAscii().ToUpper()[0];
        FilterChars =
            new ObservableCollection<FilterCharViewModel>(
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Select(curr => new FilterCharViewModel(curr, _selectedChar == curr))
            );
    }
    
    [RelayCommand]
    public async Task ApplyCharFilterAsync(char c) {
        if(c == _selectedChar)
            return;
        
        UpdateUiFilterChar(c);
    }
}