using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication8.Models;

public class EventPrelegent
{
    
    [Key]
    public int Id { get; set; }
    
    [Column("EventId")]
    public int EventId { get; set; }
    [Column("PrelegentId")]
    public int PrelegentId { get; set; }
    
    [ForeignKey(nameof(EventId))]
    public virtual Event Event { get; set; } = null!;
    [ForeignKey(nameof(PrelegentId))]
    public virtual Prelegent Prelegent { get; set; } = null!;
    
    
}