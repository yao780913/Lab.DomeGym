using ErrorOr;

namespace DomeGym.Domain;

public class Room
{
    private readonly int _maxDailySession;
    private readonly Guid _id;
    private readonly List<Guid> _sessionIds = new();

    public Room (int maxDailySession, Guid id)
    {
        _maxDailySession = maxDailySession;
        _id = id;
    }

    public Guid Id => _id;

    public ErrorOr<Success> ScheduleSession (Session session)
    {
        if (_sessionIds.Contains(session.Id))
            return Error.Conflict(description: "Session already exists in room");

        if (_sessionIds.Count >= _maxDailySession)
        {
            return RoomErrors.CannotHaveMoreSessionThanSubscriptionAllows;
        }
        
        _sessionIds.Add(session.Id);
        
        return Result.Success;
    }
}