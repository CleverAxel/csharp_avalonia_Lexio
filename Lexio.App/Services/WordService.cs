using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lexio.App.ViewModels.Dictionary.Word;
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
        if (language is null)
            throw new Exception("Le language français n'est pas ajouté, impossible de rajouter le mot.");
        
        int frenchId = language.Id;

        Word? word = await _context.Words
            .Where(w => EF.Functions.Like(w.Name, $"{newWord}"))
            .FirstOrDefaultAsync();

        if (word is not null)
            throw new Exception($"Le mot : \"{newWord}\", existe déjà dans la base de données.");
        
        await _context.Words.AddAsync(new Word() {
            Definition = "",
            Name = newWord.ToLower(),
            LanguageId = frenchId
        });

        await _context.SaveChangesAsync();

    }

    public async Task EditWordAsync(int wordId, string newName) {
        await _context
            .Words
            .Where(w => w.Id == wordId)
            .ExecuteUpdateAsync(w => w.SetProperty(n => n.Name, newName.ToLower()));
    }
    
    public async Task DeleteWordAsync(int wordId) {
        await _context
            .Words
            .Where(w => w.Id == wordId).ExecuteDeleteAsync();
    }

    public async Task<List<WordViewModel>> GetWordListStartingBy(string c) {
        return await _context.Words
            .Where(w => EF.Functions.Like(w.Name, $"{c}%"))
            .OrderBy(w => w.Name)
            .Select(w => new WordViewModel() {
                Id = w.Id,
                Name = char.ToUpper(w.Name[0]) + w.Name.Substring(1),
                Definition = w.Definition
            }).ToListAsync();
    }
}