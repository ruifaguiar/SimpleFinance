using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFinance.Database.Entities;

public sealed class TransactionType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    
    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    public BalanceImpact BalanceImpact { get; set; }

    [MaxLength(50)]
    public string? Category { get; set; }

    public bool IsActive { get; set; } = true;

    [MaxLength(500)]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    
    public int ExpenseCategoryId { get; set; }
    
}