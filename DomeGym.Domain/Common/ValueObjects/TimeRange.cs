using ErrorOr;
using Throw;

namespace DomeGym.Domain.Common.ValueObjects;

public class TimeRange : ValueObject
{
    public TimeOnly Start { get; init; }
    
    public TimeOnly End { get; init; }
    
    public TimeRange (TimeOnly start, TimeOnly end)
    {
        Start = start.Throw().IfGreaterThanOrEqualTo(end);
        End = end;
    }

    public static ErrorOr<TimeRange> FromDateTimes (DateTime start, DateTime end)
    {
        if (start.Date != end.Date)
        {
            return Error.Validation(description: "Start and end date must be the same");
        }

        if (start >= end)
        {
            return Error.Validation(description: "End time must be greater than the start time");
        }
        
        return new TimeRange(
            TimeOnly.FromDateTime(start), 
            TimeOnly.FromDateTime(end));
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