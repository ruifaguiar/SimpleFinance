using System.ComponentModel.DataAnnotations;

namespace SimpleFinance.WebApi.Model;

public class AccountModel
{
    public int? Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(50)]
    public string? AccountNumber { get; set; }

    [Required]
    public required int AccountTypeId { get; set; }

    public decimal Balance { get; set; }

    [Required]
    [MaxLength(3)]
    public string Currency { get; set; } = "EUR";

    [Required]
    public required int InstitutionId { get; set; }

    public bool IsActive { get; set; } = true;

    public DateOnly? OpenedAt { get; set; }

    public DateOnly? ClosedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }
}

