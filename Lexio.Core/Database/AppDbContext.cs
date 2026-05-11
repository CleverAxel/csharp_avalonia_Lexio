using Lexio.Core.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Lexio.Core.Database;

public class AppDbContext : DbContext {
    public DbSet<Language> Languages { get; set; }
    public DbSet<AvailableLanguage> AvailableLanguages { get; set; }
    public DbSet<Word> Words { get; set; }
    public DbSet<WordTranslation> WordTranslations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        
        // Disambiguate the two FKs from WordTranslation → Word
        modelBuilder.Entity<WordTranslation>()
            .HasOne(wt => wt.SourceWord)
            .WithMany(w => w.SourceTranslations)
            .HasForeignKey(wt => wt.SourceWordId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WordTranslation>()
            .HasOne(wt => wt.TargetWord)
            .WithMany(w => w.TargetTranslations)
            .HasForeignKey(wt => wt.TargetWordId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options) {
        const string server = "localhost";
        const string database = "lexio";
        const string user = "root";
        const string password = "root";

        const string connectionString = $"Server={server};Database={database};User={user};Password={password};"; 
        options.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString)
        ).UseSeeding((context, _) => {
            SeedAvailableLanguages(context);
        });
    }

    private void SeedAvailableLanguages(DbContext ctx) {
        var dbset = ctx.Set<AvailableLanguage>();
        if(dbset.Any())
            return;
        
        List<AvailableLanguage> availableLanguages = new() {
            new AvailableLanguage { Id = 1, Name = "Afrikaans", Code = "af", Flag = "🇿🇦" },
            new AvailableLanguage { Id = 2, Name = "Albanais", Code = "sq", Flag = "🇦🇱" },
            new AvailableLanguage { Id = 3, Name = "Allemand", Code = "de", Flag = "🇩🇪" },
            new AvailableLanguage { Id = 4, Name = "Amharique", Code = "am", Flag = "🇪🇹" },
            new AvailableLanguage { Id = 5, Name = "Anglais", Code = "en", Flag = "🇬🇧" },
            new AvailableLanguage { Id = 6, Name = "Arabe", Code = "ar", Flag = "🇸🇦" },
            new AvailableLanguage { Id = 7, Name = "Arménien", Code = "hy", Flag = "🇦🇲" },
            new AvailableLanguage { Id = 8, Name = "Azerbaïdjanais", Code = "az", Flag = "🇦🇿" },
            new AvailableLanguage { Id = 9, Name = "Basque", Code = "eu", Flag = "🇪🇸" },
            new AvailableLanguage { Id = 10, Name = "Biélorusse", Code = "be", Flag = "🇧🇾" },
            new AvailableLanguage { Id = 11, Name = "Bengali", Code = "bn", Flag = "🇧🇩" },
            new AvailableLanguage { Id = 12, Name = "Bosniaque", Code = "bs", Flag = "🇧🇦" },
            new AvailableLanguage { Id = 13, Name = "Bulgare", Code = "bg", Flag = "🇧🇬" },
            new AvailableLanguage { Id = 14, Name = "Catalan", Code = "ca", Flag = "🇪🇸" },
            new AvailableLanguage { Id = 15, Name = "Chinois simplifié", Code = "zh-CN", Flag = "🇨🇳" },
            new AvailableLanguage { Id = 16, Name = "Chinois traditionnel", Code = "zh-TW", Flag = "🇹🇼" },
            new AvailableLanguage { Id = 17, Name = "Croate", Code = "hr", Flag = "🇭🇷" },
            new AvailableLanguage { Id = 18, Name = "Danois", Code = "da", Flag = "🇩🇰" },
            new AvailableLanguage { Id = 19, Name = "Espagnol", Code = "es", Flag = "🇪🇸" },
            new AvailableLanguage { Id = 20, Name = "Estonien", Code = "et", Flag = "🇪🇪" },
            new AvailableLanguage { Id = 21, Name = "Finnois", Code = "fi", Flag = "🇫🇮" },
            new AvailableLanguage { Id = 22, Name = "Français", Code = "fr", Flag = "🇫🇷" },
            new AvailableLanguage { Id = 23, Name = "Galicien", Code = "gl", Flag = "🇪🇸" },
            new AvailableLanguage { Id = 24, Name = "Géorgien", Code = "ka", Flag = "🇬🇪" },
            new AvailableLanguage { Id = 25, Name = "Grec", Code = "el", Flag = "🇬🇷" },
            new AvailableLanguage { Id = 26, Name = "Gujarati", Code = "gu", Flag = "🇮🇳" },
            new AvailableLanguage { Id = 27, Name = "Haïtien créole", Code = "ht", Flag = "🇭🇹" },
            new AvailableLanguage { Id = 28, Name = "Haoussa", Code = "ha", Flag = "🇳🇬" },
            new AvailableLanguage { Id = 29, Name = "Hébreu", Code = "he", Flag = "🇮🇱" },
            new AvailableLanguage { Id = 30, Name = "Hindi", Code = "hi", Flag = "🇮🇳" },
            new AvailableLanguage { Id = 31, Name = "Hongrois", Code = "hu", Flag = "🇭🇺" },
            new AvailableLanguage { Id = 32, Name = "Igbo", Code = "ig", Flag = "🇳🇬" },
            new AvailableLanguage { Id = 33, Name = "Indonésien", Code = "id", Flag = "🇮🇩" },
            new AvailableLanguage { Id = 34, Name = "Irlandais", Code = "ga", Flag = "🇮🇪" },
            new AvailableLanguage { Id = 35, Name = "Islandais", Code = "is", Flag = "🇮🇸" },
            new AvailableLanguage { Id = 36, Name = "Italien", Code = "it", Flag = "🇮🇹" },
            new AvailableLanguage { Id = 37, Name = "Japonais", Code = "ja", Flag = "🇯🇵" },
            new AvailableLanguage { Id = 38, Name = "Javanais", Code = "jv", Flag = "🇮🇩" },
            new AvailableLanguage { Id = 39, Name = "Kannada", Code = "kn", Flag = "🇮🇳" },
            new AvailableLanguage { Id = 40, Name = "Kazakh", Code = "kk", Flag = "🇰🇿" },
            new AvailableLanguage { Id = 41, Name = "Khmer", Code = "km", Flag = "🇰🇭" },
            new AvailableLanguage { Id = 42, Name = "Coréen", Code = "ko", Flag = "🇰🇷" },
            new AvailableLanguage { Id = 43, Name = "Letton", Code = "lv", Flag = "🇱🇻" },
            new AvailableLanguage { Id = 44, Name = "Lituanien", Code = "lt", Flag = "🇱🇹" },
            new AvailableLanguage { Id = 45, Name = "Macédonien", Code = "mk", Flag = "🇲🇰" },
            new AvailableLanguage { Id = 46, Name = "Malais", Code = "ms", Flag = "🇲🇾" },
            new AvailableLanguage { Id = 47, Name = "Malayalam", Code = "ml", Flag = "🇮🇳" },
            new AvailableLanguage { Id = 48, Name = "Maltais", Code = "mt", Flag = "🇲🇹" },
            new AvailableLanguage { Id = 49, Name = "Marathi", Code = "mr", Flag = "🇮🇳" },
            new AvailableLanguage { Id = 50, Name = "Mongol", Code = "mn", Flag = "🇲🇳" },
            new AvailableLanguage { Id = 51, Name = "Népalais", Code = "ne", Flag = "🇳🇵" },
            new AvailableLanguage { Id = 52, Name = "Norvégien", Code = "no", Flag = "🇳🇴" },
            new AvailableLanguage { Id = 53, Name = "Ouzbek", Code = "uz", Flag = "🇺🇿" },
            new AvailableLanguage { Id = 54, Name = "Pachto", Code = "ps", Flag = "🇦🇫" },
            new AvailableLanguage { Id = 55, Name = "Persan", Code = "fa", Flag = "🇮🇷" },
            new AvailableLanguage { Id = 56, Name = "Polonais", Code = "pl", Flag = "🇵🇱" },
            new AvailableLanguage { Id = 57, Name = "Portugais (Brésil)", Code = "pt-BR", Flag = "🇧🇷" },
            new AvailableLanguage { Id = 58, Name = "Portugais (Portugal)", Code = "pt-PT", Flag = "🇵🇹" },
            new AvailableLanguage { Id = 59, Name = "Punjabi", Code = "pa", Flag = "🇮🇳" },
            new AvailableLanguage { Id = 60, Name = "Roumain", Code = "ro", Flag = "🇷🇴" },
            new AvailableLanguage { Id = 61, Name = "Russe", Code = "ru", Flag = "🇷🇺" },
            new AvailableLanguage { Id = 62, Name = "Serbe", Code = "sr", Flag = "🇷🇸" },
            new AvailableLanguage { Id = 63, Name = "Cingalais", Code = "si", Flag = "🇱🇰" },
            new AvailableLanguage { Id = 64, Name = "Slovaque", Code = "sk", Flag = "🇸🇰" },
            new AvailableLanguage { Id = 65, Name = "Slovène", Code = "sl", Flag = "🇸🇮" },
            new AvailableLanguage { Id = 66, Name = "Somali", Code = "so", Flag = "🇸🇴" },
            new AvailableLanguage { Id = 67, Name = "Soundanais", Code = "su", Flag = "🇮🇩" },
            new AvailableLanguage { Id = 68, Name = "Suédois", Code = "sv", Flag = "🇸🇪" },
            new AvailableLanguage { Id = 69, Name = "Swahili", Code = "sw", Flag = "🇰🇪" },
            new AvailableLanguage { Id = 70, Name = "Tamoul", Code = "ta", Flag = "🇮🇳" },
            new AvailableLanguage { Id = 71, Name = "Télougou", Code = "te", Flag = "🇮🇳" },
            new AvailableLanguage { Id = 72, Name = "Thaï", Code = "th", Flag = "🇹🇭" },
            new AvailableLanguage { Id = 73, Name = "Tchèque", Code = "cs", Flag = "🇨🇿" },
            new AvailableLanguage { Id = 74, Name = "Turc", Code = "tr", Flag = "🇹🇷" },
            new AvailableLanguage { Id = 75, Name = "Ukrainien", Code = "uk", Flag = "🇺🇦" },
            new AvailableLanguage { Id = 76, Name = "Ourdou", Code = "ur", Flag = "🇵🇰" },
            new AvailableLanguage { Id = 77, Name = "Vietnamien", Code = "vi", Flag = "🇻🇳" },
            new AvailableLanguage { Id = 78, Name = "Yoruba", Code = "yo", Flag = "🇳🇬" },
            new AvailableLanguage { Id = 79, Name = "Zoulou", Code = "zu", Flag = "🇿🇦" },
        };
        foreach (var lng in availableLanguages) {
            dbset.Add(lng);
        }
        ctx.SaveChanges();
    }
}