namespace WebApplication8.DTOs;

public class EventDetailsGetDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public int MaxParticipants { get; set; }
    public int ParticipantCount { get; set; }
    public int Remaining { get; set; }
    public ICollection<ParticipantGetDto>? Participants { get; set; }
    public ICollection<PrelegentGetDto>? Prelegents { get; set; }
}