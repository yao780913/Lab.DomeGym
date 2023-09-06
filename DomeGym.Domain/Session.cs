using ErrorOr;

namespace DomeGym.Domain;

public class Session
{
    private readonly Guid _trainerId;
    private readonly List<Reservation> _reservations = new ();
    private readonly int _maxParticipants;

    public Session (
        DateOnly date,
        TimeRange time,
        int maxParticipants,
        Guid trainerId,
        Guid? id = null)
    {
        Date = date;
        _maxParticipants = maxParticipants;
        _trainerId = trainerId;
        Time = time;

        Id = id ?? Guid.NewGuid();
    }

    public Guid Id { get; }
    public DateOnly Date { get; }
    public TimeRange Time { get; set; }

    public ErrorOr<Success> ReserveSpot (Participant participant)
    {
        if (_reservations.Count >= _maxParticipants)
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