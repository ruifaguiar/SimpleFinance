using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFinance.Database.Entities;

public sealed class Institution
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required int Id { get; init; }
    
    [MaxLength(100)]
    public required string Name { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}

