using DomeGym.Domain.Tests.TestUtils.Rooms;
using DomeGym.Domain.Tests.TestUtils.Sessions;
using FluentAssertions;

namespace DomeGym.Domain.Tests;

public class RoomTests
{
    [Fact]
    public void ScheduledSession_WhenMoreThanSubscriptionAllows_ShouldBeFail ()
    {
        var room = RoomFactory.CreateRoom(1);

        var session1 = SessionFactory.CreateSession(id: Guid.NewGuid());
        var session2 = SessionFactory.CreateSession(id: Guid.NewGuid());

        var scheduleSession1Result = room.ScheduleSession(session1);
        var scheduleSession2Result = room.ScheduleSession(session2);

        scheduleSession1Result.IsError.Should().BeFalse();

        scheduleSession2Result.IsError.Should().BeTrue();
        scheduleSession2Result.FirstError.Should().Be(RoomErrors.CannotHaveMoreSessionThanSubscriptionAllows);
    }
}