using System.ComponentModel.DataAnnotations;

namespace SimpleFinance.Database.Entities;

public class Institution
{
    public required Guid Id { get; set; }
    
    [MaxLength(100)]
    public required string Name { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}

