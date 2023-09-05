using ErrorOr;

namespace DomeGym.Domain;

public class Room
{
    private readonly int _maxDailySession;
    private readonly Schedule _schedule;
    private readonly List<Guid> _sessionIds = new ();

    public Room (int maxDailySession, Schedule? schedule = null, Guid? id = null)
    {
        _maxDailySession = maxDailySession;
        _schedule = schedule ?? Schedule.Empty();
        Id = id ?? Guid.NewGuid();
    }

    public Guid Id { get; }

    public ErrorOr<Success> ScheduleSession (Session session)
    {
        if (_sessionIds.Contains(session.Id))
            return Error.Conflict(description: "Session already exists in room");

        if (_sessionIds.Count >= _maxDailySession)
        {
            return RoomErrors.CannotHaveMoreSessionThanSubscriptionAllows;
        }

        var addEventResult = _schedule.BookTimeSlot(session.Date, session.Time);

        if (addEventResult is { IsError: true, FirstError.Type: ErrorType.Conflict })
        {
            return RoomErrors.CannotHaveTwoOrMoreOverlappingSessions;
        }
            

        _sessionIds.Add(session.Id);

        return Result.Success;
    }
}