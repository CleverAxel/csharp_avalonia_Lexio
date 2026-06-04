using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lexio.Core.Database.Models;

[Table("series")]
public class Serie
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("language_id")]
    public int LanguageId { get; set; }

    [ForeignKey(nameof(LanguageId))]
    public Language Language { get; set; } = null!;

    public ICollection<SerieWord> SeriesWords { get; set; } = new List<SerieWord>();
    public ICollection<SerieResult> Results { get; set; } = new List<SerieResult>();
}