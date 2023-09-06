using DomeGym.Domain.Common;
using ErrorOr;
using Throw;

namespace DomeGym.Domain;

public class TimeRange : ValueObject
{
    public TimeOnly Start { get; init; }
    
    public TimeOnly End { get; init; }
    
    public TimeRange (TimeOnly start, TimeOnly end)
    {
        Start = start;
        End = end;
    }

    public static ErrorOr<TimeRange> FromDateTimes (DateTime start, DateTime end)
    {
        if (start.Date != end.Date || start > end)
        {
            return Error.Validation();
        }
        
        return new TimeRange(TimeOnly.FromDateTime(start), TimeOnly.FromDateTime(end));
    }

    public static TimeRange CreateFromHours (int startHour, int endHour)
    {
        startHour.Throw()
            .IfGreaterThanOrEqualTo(endHour)
            .IfNegative()
            .IfGreaterThan(23);

        endHour.Throw()
            .IfLessThan(1)
            .IfGreaterThan(24);
            
        return new TimeRange(
            start: TimeOnly.MinValue.AddHours(startHour),
            end: TimeOnly.MinValue.AddHours(endHour));
    }

    public bool OverlapsWith (TimeRange other)
    {
        if (Start >= other.End)
            return false;

        if (other.Start >= End)
            return false;

        return true;
    }

    public override IEnumerable<object?> GetEqualityComponents ()
    {
        yield return Start;
        yield return End;
    }
}