using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication8.Models;


[Table("Participant")]
public class Participant
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)] public string FirstName { get; set; } = null!;

    [MaxLength(100)] public string LastName { get; set; } = null!;
    
    [MaxLength(50)]
    public string? Email { get; set; }
    
    public virtual ICollection<Registration> Registration { get; set; } = null!;

}