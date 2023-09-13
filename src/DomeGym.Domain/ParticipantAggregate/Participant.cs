using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Entities;
using DomeGym.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Domain.ParticipantAggregate;

public class Participant : AggregateRoot
{
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly List<Guid> _sessionIds = new ();

    public Participant (Guid userId, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
    }

    public Guid UserId { get; }

    public ErrorOr<Success> AddSessionToSchedule (Session session)
    {
        if (_sessionIds.Contains(session.Id))
            return Error.Conflict(description: "Session already exists in participant's schedule");

        var bookTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);

        if (bookTimeSlotResult.IsError
            && bookTimeSlotResult.FirstError.Type == ErrorType.Conflict)
            return ParticipantErrors.CannotHaveTwoOrMoreOverlappingSessions;

        _sessionIds.Add(session.Id);

        return Result.Success;
    }
}