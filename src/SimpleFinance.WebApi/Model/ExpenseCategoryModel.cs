using System.ComponentModel.DataAnnotations;

namespace SimpleFinance.WebApi.Model;

public class ExpenseCategoryModel
{
    public int? Id { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    [MaxLength(100)]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
}

