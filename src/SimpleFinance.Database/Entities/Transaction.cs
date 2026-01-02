using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFinance.Database.Entities;

public sealed class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [Required] 
    public int AccountId { get; set; }
    
    [Column(TypeName = "decimal(18,4)")] 
    public decimal Amount { get; set; }

    [Required]
    public int TransactionTypeId { get; set; }

    public DateTime TransactionDate { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal BalanceAfterTransaction { get; set; }

    [MaxLength(3)]
    public string Currency { get; set; } = "EUR";

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    
    
}