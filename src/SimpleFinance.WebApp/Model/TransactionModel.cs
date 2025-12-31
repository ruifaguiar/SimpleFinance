using System.ComponentModel.DataAnnotations;

namespace SimpleFinance.WebApp.Model;

public class TransactionModel
{
    public int? Id { get; set; }

    [Required]
    public required int AccountId { get; set; }

    [Required]
    public required decimal Amount { get; set; }

    [Required]
    public required int TransactionTypeId { get; set; }

    [Required]
    public required DateTime TransactionDate { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    public decimal BalanceAfterTransaction { get; set; }

    [Required]
    [MaxLength(3)]
    public string Currency { get; set; } = "EUR";

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

