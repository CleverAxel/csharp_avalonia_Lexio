using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Lexio.App.Routing;
using Lexio.App.Services;
using Lexio.App.ViewModels.Dictionary.Traduction;

namespace Lexio.App.ViewModels.Serie;

public partial class SeriePlayViewModel : ViewModelBase {
    public SerieDetailViewModel SerieDetailViewModel { get; set; } = null!;
    private List<TraductionViewModel> _traductionViewModels;
    private RoutingService _routingService;
    private SerieService _serieService;
    
    [ObservableProperty]
    private string _serieLanguage = "";

    [ObservableProperty]
    private string _traductionWayFromLanguage = "";

    [ObservableProperty]
    private string _traductionWayFromFrench = "";
    
    
    public SeriePlayViewModel(RoutingService routingService, SerieService serieService) {
        _routingService = routingService;
        _serieService = serieService;
        _ = Task.Run(async () => {
            SerieLanguage = $"en {SerieDetailViewModel.LanguageName}";
            TraductionWayFromLanguage = $"De {SerieDetailViewModel.LanguageName.ToLower()} en français";
            TraductionWayFromFrench = $"De français en {SerieDetailViewModel.LanguageName.ToLower()}";
            _traductionViewModels = await serieService.RetrieveTranslationFromSerieId(SerieDetailViewModel.Id);
        });
    }
}