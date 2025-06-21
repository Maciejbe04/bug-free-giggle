using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication8.Models;

[Table("Event")]
public class Event
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)] 
    public string Title { get; set; } = null!;
    
    [MaxLength(150)]
    public string Description { get; set; } = null!;
    
    public DateTime Date { get; set; }
    public int MaxParticipants { get; set; }
    
    public virtual ICollection<Registration> Registration { get; set; } = null!;
    public virtual ICollection<EventPrelegent> EventPrelegent { get; set; } = null!;
    
}