using DomeGym.Domain.Common.ValueObjects;
using DomeGym.Domain.RoomAggregate;
using DomeGym.Domain.Tests.TestConstants;
using DomeGym.Domain.Tests.TestUtils.Rooms;
using DomeGym.Domain.Tests.TestUtils.Sessions;
using FluentAssertions;

namespace DomeGym.Domain.Tests;

public class RoomTests
{
    [Fact]
    public void ScheduledSession_WhenMoreThanSubscriptionAllows_ShouldBeFail ()
    {
        var room = RoomFactory.CreateRoom();

        var session1 = SessionFactory.CreateSession(id: Guid.NewGuid());
        var session2 = SessionFactory.CreateSession(id: Guid.NewGuid());

        var scheduleSession1Result = room.ScheduleSession(session1);
        var scheduleSession2Result = room.ScheduleSession(session2);

        scheduleSession1Result.IsError.Should().BeFalse();

        scheduleSession2Result.IsError.Should().BeTrue();
        scheduleSession2Result.FirstError.Should().Be(RoomErrors.CannotHaveMoreSessionThanSubscriptionAllows);
    }

    [Theory]
    [InlineData(1, 3, 1, 3)]
    [InlineData(1, 3, 2, 3)]
    [InlineData(1, 3, 2, 4)]
    [InlineData(1, 3, 0, 2)]
    public void ScheduleSession_WhenSessionOverlappingWithAnotherSession_ShouldFail (
        int startHourSession1,
        int endHourSession1,
        int startHourSession2,
        int endHourSession2)
    {
        var room = RoomFactory.CreateRoom(2);

        var session1 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRange.CreateFromHours(startHourSession1, endHourSession1),
            id: Guid.NewGuid());

        var session2 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRange.CreateFromHours(startHourSession2, endHourSession2),
            id: Guid.NewGuid());

        var scheduleSession1Result = room.ScheduleSession(session1);
        var scheduleSession2Result = room.ScheduleSession(session2);

        scheduleSession1Result.IsError.Should().BeFalse();

        scheduleSession2Result.IsError.Should().BeTrue();
        scheduleSession2Result.FirstError.Should().Be(RoomErrors.CannotHaveTwoOrMoreOverlappingSessions);
    }
}