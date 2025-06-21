using System.ComponentModel.DataAnnotations;

namespace WebApplication8.DTOs;

public class CreateEventDto
{
    [MaxLength(50)]
    public required string Title { get; set; }
    [MaxLength(150)]
    public required string Description { get; set; }
    public DateTime Date { get; set; }
    public required int MaxParticipants { get; set; }
}