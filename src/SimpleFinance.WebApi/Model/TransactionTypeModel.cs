using System.ComponentModel.DataAnnotations;

namespace SimpleFinance.WebApi.Model;

public class TransactionTypeModel
{
    public int? Id { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    [Required]
    public required int BalanceImpact { get; set; }

    [MaxLength(50)]
    public string? Category { get; set; }

    public bool IsActive { get; set; } = true;

    [MaxLength(500)]
    public string? Description { get; set; }

    public int ExpenseCategoryId { get; set; }
}

