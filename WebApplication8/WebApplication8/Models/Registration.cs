using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication8.Models;


[Table("Registration")]
public class Registration
{
    [Key]
    public int Id { get; set; }
    
    [Column("EventId")]
    public int EventId { get; set; }
    [Column("ParticipantId")]
    public int ParticipantId { get; set; }
    
    
    
    [ForeignKey(nameof(EventId))]
    public virtual Event Event { get; set; } = null!;
    
    [ForeignKey(nameof(ParticipantId))]
    public virtual Participant Participant { get; set; } = null!;
}