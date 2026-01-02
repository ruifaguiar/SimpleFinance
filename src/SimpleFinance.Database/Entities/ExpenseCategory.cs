using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFinance.Database.Entities;

public sealed class ExpenseCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    [MaxLength(100)]
    public string? Description { get; set; }
    
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; init; }
}

