using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Lexio.App.ViewModels.Dictionary.Traduction;
using Lexio.App.ViewModels.Dictionary.Word;
using Lexio.Core.Database;
using Lexio.Core.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Lexio.App.Services;

public class TraductionService {
    private AppDbContext _context;
    public TraductionService(AppDbContext context) {
        _context = context;
    }
    
    public async Task<List<TraductionViewModel>> GetWordListStartingBy(string c, int targetLanguageId)
    {
        var stuff = await _context.Words
            .Where(w => EF.Functions.Like(w.Name, $"{c}%") && w.LanguageId == 12)
            .OrderBy(w => w.Name)
            .Select(w => new TraductionViewModel
            {
                SourceWord = new WordViewModel
                {
                    Id = w.Id,
                    Definition = w.Definition,
                    Name = w.Name
                },
                TargetWords = new ObservableCollection<WordViewModel>(
                    w.SourceTranslations
                        .Where(t => t.TargetWord.LanguageId == targetLanguageId)
                        .Select(s => new WordViewModel
                        {
                            Id = s.TargetWord.Id,
                            Name = s.TargetWord.Name,
                            Definition = s.TargetWord.Definition
                        })
                        .ToList()
                )
            })
            .ToListAsync();

        return stuff;
    }

    public async Task<WordViewModel> AddNewTraduction(WordViewModel source,int languageId, string traduction) {
        var stuff = await _context.Words.AddAsync(new Word() {
            Definition = "",
            LanguageId = languageId,
            Name = traduction
        });
        
     
        await _context.SaveChangesAsync();
        await _context.WordTranslations.AddAsync(new WordTranslation() {
            SourceWordId = source.Id,
            TargetWordId = stuff.Entity.Id
        });
        await _context.SaveChangesAsync();

        return new WordViewModel() {
            Definition = "",
            Id = stuff.Entity.Id,
            Name = stuff.Entity.Name
        };
    }

    public async Task DeleteWord(int wordId) {
        await _context.Words.Where(w => w.Id == wordId).ExecuteDeleteAsync();
    }

    public async Task EditWord(int wordId, string newTrad) {
        await _context.Words.Where(w => w.Id == wordId)
            .ExecuteUpdateAsync(w => 
                w.SetProperty(x => x.Name, newTrad));
        
    }
    
    
    public async Task EditDefinition(int wordId, string newDef) {
        await _context.Words.Where(w => w.Id == wordId)
            .ExecuteUpdateAsync(w => 
                w.SetProperty(x => x.Definition, newDef));
        
    }
}