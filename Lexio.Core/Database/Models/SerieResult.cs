using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lexio.Core.Database.Models;

[Table("series_results")]
public class SerieResult
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("series_id")]
    public int SeriesId { get; set; }

    [Column("correct_answer_count")]
    public int CorrectAnswerCount { get; set; }

    [Column("question_count")]
    public int QuestionCount { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("is_a_replay")]
    public bool IsAReplay { get; set; }

    [ForeignKey(nameof(SeriesId))]
    public Serie Serie { get; set; } = null!;
}