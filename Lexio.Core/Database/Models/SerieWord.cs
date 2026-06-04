using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lexio.Core.Database.Models;

[Table("series_words")]
public class SerieWord
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("series_id")]
    public int SeriesId { get; set; }

    [Column("word_id")]
    public int WordId { get; set; }

    [ForeignKey(nameof(SeriesId))]
    public Serie Serie { get; set; } = null!;

    [ForeignKey(nameof(WordId))]
    public Word Word { get; set; } = null!;
}