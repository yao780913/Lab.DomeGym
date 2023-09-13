using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Entities;
using DomeGym.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Domain.RoomAggregate;

public class Room : AggregateRoot
{

    private readonly int _maxDailySession;
    private readonly Schedule _schedule;
    private readonly List<Guid> _sessionIds = new ();
    
    public string Name { get; }
    public Guid GymId { get; }

    public Room (string name, Guid gymId, int maxDailySession, Schedule? schedule = null, Guid? id = null)
        : base (id ?? Guid.NewGuid())
    {
        Name = name;
        GymId = gymId;
        _maxDailySession = maxDailySession;
        _schedule = schedule ?? Schedule.Empty();
    }

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