using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lexio.Core.Database.Models;

[Table("words")]
public class Word {
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(512)]
    [Column("definition")]
    public string Definition { get; set; } = string.Empty;

    [Column("language_id")]
    public int LanguageId { get; set; }

    // Navigation
    [ForeignKey(nameof(LanguageId))]
    public Language Language { get; set; } = null!;

    public ICollection<WordTranslation> SourceTranslations { get; set; } = new List<WordTranslation>();
    public ICollection<WordTranslation> TargetTranslations { get; set; } = new List<WordTranslation>();
}