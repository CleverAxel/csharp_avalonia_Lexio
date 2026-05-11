using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lexio.Core.Database.Models;

[Table("available_languages")]
public class AvailableLanguage
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(64)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(16)]
    [Column("code")]
    public string Code { get; set; } = string.Empty;

    [MaxLength(16)]
    [Column("flag")]
    public string? Flag { get; set; } = null;
}