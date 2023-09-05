using ErrorOr;

namespace DomeGym.Domain;

public class Participant
{
    private readonly Guid _userId;
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly List<Guid> _sessionIds = new ();

    public Participant (Guid userId, Guid? id = null)
    {
        _userId = userId;
        Id = id ?? Guid.NewGuid();
    }

    public Guid Id { get; }

    public ErrorOr<Success> AddSessionToSchedule (Session session)
    {
        if (_sessionIds.Contains(session.Id))
            return Error.Conflict(description: "Session already exists in participant's schedule");
        
        var bookTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);
        
        if (bookTimeSlotResult.IsError
            && bookTimeSlotResult.FirstError.Type == ErrorType.Conflict)
            return ParticipantErrors.CannotHaveTwoOrMoreOverlappingSessions;

        return Result.Success;
    }
}