using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFinance.Database.Entities;

public class Account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(50)]
    public string? AccountNumber { get; set; }

    public required int AccountTypeId { get; set; }
    public AccountType? AccountType { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance { get; set; }

    [MaxLength(3)]
    public string Currency { get; set; } = "EUR";

    public int InstitutionId { get; set; }

    public bool IsActive { get; set; } = true;

    public DateOnly? OpenedAt { get; set; }

    public DateOnly? ClosedAt { get; set; }

    public DateOnly CreatedAt { get; set; }

    public DateOnly? ModifiedAt { get; set; }
}
