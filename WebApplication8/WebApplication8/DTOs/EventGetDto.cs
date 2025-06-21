using WebApplication8.DTOs;


namespace WebApplication8.DTOs;

public class EventGetDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public int MaxParticipants { get; set; }
    
}