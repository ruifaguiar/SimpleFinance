using System.ComponentModel.DataAnnotations;

namespace SimpleFinance.WebApi.Model;

public class InstitutionModel
{
    public Guid? Id { get; set; }
    
    [Required]
    [MaxLength (100)]
    public required string Name { get; set; }
}