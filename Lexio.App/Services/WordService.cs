using System.Linq;
using System.Threading.Tasks;
using Lexio.Core.Database;
using Lexio.Core.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Lexio.App.Services;

public class WordService {
    private AppDbContext _context;

    public WordService(AppDbContext context) {
        _context = context;
    }

    public async Task AddNewWord(string newWord) {
        Language? language = await _context.Languages.Where(l => l.Code == "fr").FirstOrDefaultAsync();
        if(language is null)
            return;

        int frenchId = language.Id;

        await _context.Words.AddAsync(new Word() {
            Definition = "",
            Name = newWord,
            LanguageId = frenchId
        });

        await _context.SaveChangesAsync();

    }
}