using DomeGym.Domain.Tests.TestConstants;
using DomeGym.Domain.Tests.TestUtils.Participants;
using DomeGym.Domain.Tests.TestUtils.Sessions;
using FluentAssertions;

namespace DomeGym.Domain.Tests;

public class ParticipantTests
{
    [Theory]
    [InlineData(1, 3, 1, 3)]
    [InlineData(1, 3, 2, 3)]
    [InlineData(1, 3, 2, 4)]
    [InlineData(1, 3, 0, 2)]
    public void AddSessionToSchedule_WhenSessionOverlappingWithAnotherSession_ShouldBeFail (
        int startHourSession1,
        int endHourSession1,
        int startHourSession2,
        int endHourSession2)
    {
        var participant = ParticipantFactory.CreateParticipant();
        
        var session1 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRange.CreateFromHours(startHourSession1, endHourSession1),
            id: Guid.NewGuid());
        
        var session2 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRange.CreateFromHours(startHourSession2, endHourSession2),
            id: Guid.NewGuid());

        var addSession1Result = participant.AddSessionToSchedule(session1);
        var addSession2Result = participant.AddSessionToSchedule(session2);

        addSession1Result.IsError.Should().BeFalse();
        addSession2Result.IsError.Should().BeTrue();
        addSession2Result.FirstError.Should().Be(ParticipantErrors.CannotHaveTwoOrMoreOverlappingSessions);
    }
}