using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Lexio.App.Services;

namespace Lexio.App.ViewModels.Serie;

public partial class SerieResultViewModel : ViewModelBase {
    public ObservableCollection<SerieResultDetailViewModel> SerieResultDetailViewModels {
        get;
        set => SetProperty(ref field, value);
    } = new ObservableCollection<SerieResultDetailViewModel>();
    
    public ObservableCollection<SerieStatViewModel> SerieStatViewModels {
        get;
        set => SetProperty(ref field, value);
    } = new ObservableCollection<SerieStatViewModel>();

    public SerieResultViewModel(SerieService serieService) {
        _ = Task.Run(async () => {
            SerieResultDetailViewModels =
                new ObservableCollection<SerieResultDetailViewModel>(await serieService.GetResults());

            SerieStatViewModels = new ObservableCollection<SerieStatViewModel>(await serieService.GetSeriesStats());
        });
    }
}