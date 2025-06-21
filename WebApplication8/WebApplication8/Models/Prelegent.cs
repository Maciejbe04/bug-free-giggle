using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models;

public class Prelegent
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [MaxLength(100)]
    public string LastName { get; set; } = null!;

    [MaxLength(50)]
    public string Email { get; set; } = null!;
    
    
    public virtual ICollection<EventPrelegent> EventPrelegent { get; set; } = null!;
}