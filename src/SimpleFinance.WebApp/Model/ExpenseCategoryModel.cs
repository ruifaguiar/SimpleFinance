using System.ComponentModel.DataAnnotations;

namespace SimpleFinance.WebApp.Model;

public class ExpenseCategoryModel
{
    public int? Id { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
}
