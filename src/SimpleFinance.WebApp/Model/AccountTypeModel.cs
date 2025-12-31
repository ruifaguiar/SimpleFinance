using System.ComponentModel.DataAnnotations;

namespace SimpleFinance.WebApp.Model;

public class AccountTypeModel
{
    public int? Id { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    [MaxLength(200)]
    public string? Description { get; set; }
}
