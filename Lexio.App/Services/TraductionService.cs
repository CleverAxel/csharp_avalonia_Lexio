using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Lexio.App.ViewModels.Dictionary.Traduction;
using Lexio.App.ViewModels.Dictionary.Word;
using Lexio.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace Lexio.App.Services;

public class TraductionService {
    private AppDbContext _context;
    public TraductionService(AppDbContext context) {
        _context = context;
    }
    
    
    public async Task GetWordListStartingBy(string c) {
        var stuff = await _context.Words
            .Where(w => EF.Functions.Like(w.Name, $"{c}%") && w.LanguageId == 12)
            .Include(w => w.SourceTranslations)
            .ThenInclude(t => t.TargetWord)
            .OrderBy(w => w.Name)
            .Select(w => new TraductionViewModel {
                SourceWord = new WordViewModel {
                    Id = w.Id,
                    Definition = w.Definition,
                    Name = w.Name
                },
                TargetWords = new ObservableCollection<WordViewModel>(
                    w.SourceTranslations.Select(s => new WordViewModel() {
                        Id = s.TargetWord.Id,
                        Name = s.TargetWord.Name,
                        Definition = s.TargetWord.Definition
                    })
                )
            })
            .ToListAsync();
        

        var foo = stuff;
    }
}