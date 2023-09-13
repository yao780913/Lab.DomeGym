using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Entities;
using DomeGym.Domain.Common.Interfaces;
using DomeGym.Domain.Common.ValueObjects;
using DomeGym.Domain.ParticipantAggregate;
using ErrorOr;

namespace DomeGym.Domain.SessionAggregate;

public class Session : AggregateRoot
{
    private readonly List<Reservation> _reservations = new ();

    public string Name { get; }
    public string Description { get; }
    public DateOnly Date { get; }
    public TimeRange Time { get; set; }
    public List<SessionCategory> Categories { get; }

    public Guid TrainerId { get; }

    public int MaxParticipants { get; }
    public Guid RoomId { get; }

    public Session (
        string name,
        string description,
        short maxParticipants,
        Guid roomId,
        Guid trainerId,
        DateOnly date,
        TimeRange time,
        List<SessionCategory> categories,
        Guid? id = null)
        :base(id ?? Guid.NewGuid())
    {
        Name = name;
        Description = description;
        Date = date;
        MaxParticipants = maxParticipants;
        RoomId = roomId;
        TrainerId = trainerId;
        Time = time;
        Categories = categories;
    }


    public ErrorOr<Success> ReserveSpot (Participant participant)
    {
        if (_reservations.Count >= MaxParticipants)
        {
            return SessionErrors.CannotHaveMoreReservationThanParticipants;
        }
        
        if (_reservations.Any(r => r.ParticipantId == participant.Id))
        {
            return Error.Conflict(description: "Participant already has a reservation");
        }

        _reservations.Add(
            new Reservation(participant.Id));

        return Result.Success;
    }

    public ErrorOr<Success> CancelReservation (Participant participant, IDateTimeProvider dateTimeProvider)
    {
        if (IsTooCloseToSession(dateTimeProvider.UtcNow))
            return SessionErrors.CannotCancelReservationTooCloseToSession;

        var reservation = _reservations.FirstOrDefault(r => r.ParticipantId == participant.Id);
        
        if (reservation is null)
        {
            return Error.NotFound(description: "Participant is not found");
        }

        _reservations.Remove(reservation);

        return Result.Success;
    }

    private bool IsTooCloseToSession (DateTime utcNow)
    {
        const int MinHours = 24;

        return (Date.ToDateTime(Time.Start) - utcNow).TotalHours < MinHours;
    }
}