namespace Lexio.Core.Database.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("word_translations")]
public class WordTranslation {
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("source_word_id")]
    public int SourceWordId { get; set; }

    [Column("target_word_id")]
    public int TargetWordId { get; set; }

    [ForeignKey(nameof(SourceWordId))]
    public Word SourceWord { get; set; } = null!;

    [ForeignKey(nameof(TargetWordId))]
    public Word TargetWord { get; set; } = null!;
}