using ErrorOr;

namespace DomeGym.Domain;

public class Schedule
{
    private readonly Dictionary<DateOnly, List<TimeRange>> _calendar;
    private readonly Guid? _id;

    public Schedule (
        Dictionary<DateOnly, List<TimeRange>>? calendar = null,
        Guid? id = null)
    {
        _calendar = calendar ?? new ();
        _id = id ?? Guid.NewGuid();
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