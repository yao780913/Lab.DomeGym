using DomeGym.Domain.Common.ValueObjects;
using ErrorOr;

namespace DomeGym.Domain.Common.Entities;

public class Schedule : Entity
{
    private readonly Dictionary<DateOnly, List<TimeRange>> _calendar;

    public Schedule (
        Dictionary<DateOnly, List<TimeRange>>? calendar = null,
        Guid? id = null)
        :base (id ?? Guid.NewGuid())
    {
        _calendar = calendar ?? new ();
    }

    public static Schedule Empty ()
    {
        return new Schedule(id: Guid.NewGuid());
    }

    public ErrorOr<Success> BookTimeSlot (DateOnly date, TimeRange time)
    {
        if (!_calendar.TryGetValue(date, out var timeSlots))
        {
            _calendar[date] = new () { time };
            return Result.Success;
        }

        if (timeSlots.Any(timeSlot => timeSlot.OverlapsWith(time)))
        {
            return Error.Conflict();
        }
        
        timeSlots.Add(time);
        
        return Result.Success;
    }
}