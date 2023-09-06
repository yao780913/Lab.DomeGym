using ErrorOr;

namespace DomeGym.Domain;

public class Session
{
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds = new ();
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
        if (_participantIds.Count >= _maxParticipants)
        {
            return SessionErrors.CannotHaveMoreReservationThanParticipants;
        }

        _participantIds.Add(participant.Id);

        return Result.Success;
    }

    public ErrorOr<Success> CancelReservation (Participant participant, IDateTimeProvider dateTimeProvider)
    {
        if (IsTooCloseToSession(dateTimeProvider.UtcNow))
            return SessionErrors.CannotCancelReservationTooCloseToSession;

        if (_participantIds.Remove(participant.Id))
            return Error.NotFound(description: "Participant is not found");

        return Result.Success;
    }

    private bool IsTooCloseToSession (DateTime utcNow)
    {
        const int MinHours = 24;

        return (Date.ToDateTime(Time.Start) - utcNow).TotalHours < MinHours;
    }
}