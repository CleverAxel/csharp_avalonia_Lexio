using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Lexio.App.ViewModels.Dictionary;
using Lexio.Core.Database;
using Lexio.Core.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Lexio.App.Services;

public class LanguageService {
    private AppDbContext _context;

    public LanguageService(AppDbContext context) {
        _context = context;
    }

    public async Task<ObservableCollection<LanguageViewModel>> GetAvailableLanguagesAsync() {
        ObservableCollection<LanguageViewModel> collection = new ObservableCollection<LanguageViewModel>();
        var availableLanguage = await _context.AvailableLanguages.ToListAsync();
        foreach (var language in availableLanguage) {
            collection.Add(new LanguageViewModel() {
                Id = language.Id,
                Code = language.Code,
                Flag = language.Flag ?? "",
                Name = language.Name
            });
        }
        return collection;
    }
    
    public async Task<ObservableCollection<LanguageViewModel>> GetAddedLanguagesAsync() {
        ObservableCollection<LanguageViewModel> collection = new ObservableCollection<LanguageViewModel>();
        var availableLanguage = await _context.Languages.OrderBy(l => l.Name).ToListAsync();
        foreach (var language in availableLanguage) {
            collection.Add(new LanguageViewModel() {
                Id = language.Id,
                Code = language.Code,
                Flag = language.Flag ?? "",
                Name = language.Name
            });
        }
        return collection;
    }

    public async Task AddLanguageAsync(string code, string name, string flag) {
        await _context.Languages.AddAsync(new Language() {
            Code = code,
            Name = name,
            Flag = flag,
        });

        await _context.SaveChangesAsync();
    }

    public async Task RemoveLanguageAsync(string name) {
        await _context.Languages.Where(l => l.Name == name).ExecuteDeleteAsync();
    }
}