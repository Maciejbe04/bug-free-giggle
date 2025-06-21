using Microsoft.EntityFrameworkCore;
using WebApplication8.Data;
using WebApplication8.DTOs;
using WebApplication8.Exceptions;
using WebApplication8.Models;
using WebApplication8.DTOs;

namespace WebApplication8.Services;


public interface IDbService
{
    public Task<EventGetDto> CreateNewEventAsync(CreateEventDto ewent);
    public Task RegisterPrelegentToEventAsync(int prelegentId, int eventId);
    
    public Task RegisterParticipantToEventAsync(int participantId, int eventId);
    
    public Task DeleteRegistrationAsync(int participantId);
    
    public Task<IEnumerable<EventGetDto>> GetAllEventsAsync();
    
    
}


public class DbService(AppDbContext data) : IDbService
{
    public async Task<EventGetDto> CreateNewEventAsync(CreateEventDto ewent)
    {
        var today = DateTime.Today;

        if (ewent.Date < today)
        {
            throw new DueDateException("You cannot add new event for past date");
        }

        var newEwent = new Event
        {
            Title = ewent.Title,
            Description = ewent.Description,
            Date = ewent.Date,
            MaxParticipants = ewent.MaxParticipants,
        };
        
        await data.AddAsync(newEwent);
        await data.SaveChangesAsync();

        return new EventGetDto()
        {
            Id = newEwent.Id,
            Title = newEwent.Title,
            Description = newEwent.Description,
            Date = newEwent.Date,
            MaxParticipants = newEwent.MaxParticipants,
        };


    }

    public async Task RegisterPrelegentToEventAsync(int prelegentId, int eventId)
    {
        
        var ewent = await data.Events.FirstOrDefaultAsync(e => e.Id == eventId);

        var prelegent = await data.Prelegents.FirstOrDefaultAsync(p => p.Id == prelegentId);
        
        
        var alreadyAssigned = await data.EventPrelegents.FirstOrDefaultAsync(p => p.Id == prelegentId);
        
        var ewentPrelegent = await data.EventPrelegents.FirstOrDefaultAsync(ep => ep.EventId == eventId && ep.PrelegentId == prelegentId);
        
        
        if (ewent is null)
        {
            throw new EventNotFoundException("Event not found");
        }

        if (prelegent is null)
        {
            throw new PrelegentNotFoundException("Prelegent not found");
        }


        if (alreadyAssigned is not null)
        {
            throw new PrelegentRegisteredException("Prelegent already assigned cant assign another one");
        }

        if (ewentPrelegent is not null)
        {
            throw new PrelegentRegisteredException("Prelegent already registered to this event");
        }

        var newAssignment = new EventPrelegent()
        {
            PrelegentId = prelegentId,
            EventId = eventId,
        };


        await data.EventPrelegents.AddAsync(newAssignment);
        
        await data.SaveChangesAsync();

    }

    public async Task RegisterParticipantToEventAsync(int participantId, int eventId)
    {
        var ewent = await data.Events.FirstOrDefaultAsync(e => e.Id == eventId);
        
        var participant = await data.Participants.FirstOrDefaultAsync(p => p.Id == participantId);
        
        var alreadyRegistered = await data.Registrations.FirstOrDefaultAsync(r => r.EventId == eventId && r.ParticipantId == participantId);
        
        
        if (alreadyRegistered is not null)
        {
            throw new ParticipantRegisteredException("Participant already registered");
        }
        
        var registrations = await data.Registrations.CountAsync(r => r.EventId == eventId);

        if (registrations >= ewent.MaxParticipants)
        {
            throw new MaxParticipantsException("Max participants exceeded");
        }

        var registration = new Registration()
        {
            EventId = eventId,
            ParticipantId = participantId,
        };
        
        await data.Registrations.AddAsync(registration);
        await data.SaveChangesAsync();

    }

    public async Task DeleteRegistrationAsync(int participantId)
    {
        
        var date = DateTime.Now;
        
        var registration = await data.Registrations.Include(r => r.Event)
            .FirstOrDefaultAsync(r => r.ParticipantId == participantId);

        if (registration is null)
        {
            throw new RegistrationNotFoundException("Registration not found");
        }
        

        if (registration.Event.Date - date < TimeSpan.FromHours(24))
        {
            throw new TimePassedException("You cannot delete the event because 24 hour mark between cancelling and event has passed");
        }
        
        data.Registrations.Remove(registration);
        await data.SaveChangesAsync();
        
    }

    public async Task<IEnumerable<EventGetDto>> GetAllEventsAsync()
    {
        return await data.Events.Select(e => new EventGetDto
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            Date = e.Date,
            MaxParticipants = e.MaxParticipants,
            Participants = e.Registration.Select(r => new ParticipantGetDto
            {
                Id = r.Id,
                FirstName = r.Participant.FirstName,
                LastName = r.Participant.LastName,
                Email = r.Participant.Email,
            }).ToList(),
            Prelegents = e.EventPrelegent.Select( ep => new PrelegentGetDto
            {
                Id = ep.Prelegent.Id,
                FirstName = ep.Prelegent.FirstName,
                LastName = ep.Prelegent.LastName,
                Email = ep.Prelegent.Email
            }).ToList()
        }).ToListAsync();
    }
    
}