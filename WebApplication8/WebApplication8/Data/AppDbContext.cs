using Microsoft.EntityFrameworkCore;
using WebApplication8.Models;

namespace WebApplication8.Data;

public class AppDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<EventPrelegent> EventPrelegents { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Registration> Registrations { get; set; }
    public DbSet<Prelegent> Prelegents { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var participant = new Participant
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "jondoe@gmail.com"
        };

        var participant2 = new Participant
        {
            Id = 2,
            FirstName = "Jan",
            LastName = "Do",
            Email = "2332@gmail.com"
        };
        
        var prelegent = new Prelegent
        {
            Id = 1,
            FirstName = "John",
            LastName = "Pork",
            Email = "johnpork@gmail.com"
        };
        var event1 = new Event
        {
            Id = 1,
            Title = "BestEvent",
            Description = "BestEvent description",
            Date = new DateTime(2025, 06, 19),
            MaxParticipants = 20
        };
        var registration = new Registration
        {
            Id = 1,
            ParticipantId = participant.Id,
            EventId = event1.Id,
        };
        var eventPrelegent = new EventPrelegent
        {
            Id = 1,
            PrelegentId = prelegent.Id,
            EventId = event1.Id,
        };
        
        modelBuilder.Entity<EventPrelegent>().HasData([eventPrelegent]);
        modelBuilder.Entity<Event>().HasData([event1]);
        modelBuilder.Entity<Registration>().HasData([registration]);
        modelBuilder.Entity<Participant>().HasData([participant]);
        modelBuilder.Entity<Participant>().HasData([participant2]);
        modelBuilder.Entity<Prelegent>().HasData([prelegent]);

    }
}