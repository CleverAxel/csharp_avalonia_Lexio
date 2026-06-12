using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Lexio.App.ViewModels.Dictionary.Traduction;
using Lexio.App.ViewModels.Dictionary.Word;
using Lexio.App.ViewModels.Serie;
using Lexio.Core.Database;
using Lexio.Core.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Lexio.App.Services;

public class SerieService {
    private AppDbContext _context;

    public SerieService(AppDbContext context) {
        _context = context;
    }

    public async Task<List<SerieDetailViewModel>> GetSeriesAvailablesAsync()
    {
        return await _context.Series
            .OrderBy(s => s.Language.Name)
            .Select(s => new SerieDetailViewModel
            {
                Id = s.Id,
                Name = s.Name,
                LanguageId = s.LanguageId,
                LanguageName = s.Language.Name,
                LanguageFlag = s.Language.Flag ?? "",
                WordCount = s.SeriesWords.Count()
            })
            .ToListAsync();
    }

    public async Task<bool> AddNewAsync(int languageId, string name) {
        var serie = await _context.Series.FirstOrDefaultAsync(t => EF.Functions.Like(t.Name, name));
        
        if (serie is not null)
            return false;
        await _context.Series.AddAsync(new Serie() {
            Name = name.Trim(),
            LanguageId = languageId
        });

        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task DeleteAsync(int serieId) {
        await _context.Series.Where(s => s.Id == serieId).ExecuteDeleteAsync();
    }

    public async Task AddNewWordToSerie(int wordId, int serieId) {
        await _context.SeriesWords.AddAsync(new SerieWord() {
            WordId = wordId,
            SeriesId = serieId
        });

        await _context.SaveChangesAsync();
    }
    
    public async Task RemoveNewWordToSerie(int wordId, int serieId) {
        await _context.SeriesWords.Where(sw => sw.WordId == wordId && sw.SeriesId == serieId).ExecuteDeleteAsync();
    }
    
    
    public async Task<List<TraductionViewModel>> GetWordListStartingBy(string c, int targetLanguageId) {
        int? languageFrenchId = (await _context.Languages.FirstOrDefaultAsync(l => l.Code == "fr"))?.Id;

        if (languageFrenchId is null)
            throw new Exception("The french language is not added in the added language");
        
        var stuff = await _context.Words
            .Where(w => EF.Functions.Like(w.Name, $"{c}%") && w.LanguageId == languageFrenchId)
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

    public async Task<List<TraductionViewModel>> RetrieveTranslationFromSerieId(int serieId) {
        var serieWordIds = await _context.SeriesWords
            .Where(sw => sw.SeriesId == serieId)
            .Select(sw => sw.WordId)
            .ToListAsync();

        var sourceWords = await _context.WordTranslations
            .Where(wt => serieWordIds.Contains(wt.TargetWordId))
            .Select(wt => wt.SourceWordId)
            .Distinct()
            .ToListAsync();

        var stuff = await _context.Words
            .Where(w => sourceWords.Contains(w.Id))
            .Select(w => new TraductionViewModel
            {
                SourceWord = new WordViewModel
                {
                    Id = w.Id,
                    Name = w.Name,
                    Definition = w.Definition
                },
                TargetWords = new ObservableCollection<WordViewModel>(
                    w.SourceTranslations.Select(t => new WordViewModel
                    {
                        Id = t.TargetWord.Id,
                        Name = t.TargetWord.Name,
                        Definition = t.TargetWord.Definition,
                        IsAdded = t.TargetWord.SeriesWords.FirstOrDefault(sw => sw.SeriesId == serieId) != null
                    }).ToList()
                )
            })
            .ToListAsync();

        return stuff;
    }

    public async Task AddSerieResult(int serieId, int questionCount, int correctAnswerCount, bool isAReplay) {
        await _context.SeriesResults.AddAsync(new SerieResult() {
            CorrectAnswerCount = correctAnswerCount,
            IsAReplay = isAReplay,
            QuestionCount = questionCount,
            SeriesId = serieId,
            CreatedAt = DateTime.Now
        });

        await _context.SaveChangesAsync();
    }

    public async Task<List<SerieResultDetailViewModel>> GetResults() {
        return await _context.SeriesResults.OrderByDescending(r => r.CreatedAt).Select(r => new SerieResultDetailViewModel() {
            Id = r.Id,
            CorrectAnswerCount = r.CorrectAnswerCount,
            CreatedAt = r.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),
            IsAReplay = r.IsAReplay ? "VRAI" : "FAUX",
            QuestionCount = r.QuestionCount,
            SerieName = $"{r.Serie.Name} en {r.Serie.Language.Name}{r.Serie.Language.Flag}",
            Percentage = Math.Round((double)(r.CorrectAnswerCount / r.QuestionCount * 100), 2)
        }).ToListAsync();
    }
}