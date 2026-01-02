using System.ComponentModel.DataAnnotations;

namespace SimpleFinance.WebApi.Model;

public class InstitutionModel
{
    public int? Id { get; set; }
    
    [Required]
    [MaxLength (100)]
    public required string Name { get; set; }
}