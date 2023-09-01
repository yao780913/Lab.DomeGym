using DomeGym.Domain.Tests.TestConstants;
using DomeGym.Domain.Tests.TestUtils.Participants;
using DomeGym.Domain.Tests.TestUtils.Services;
using DomeGym.Domain.Tests.TestUtils.Sessions;
using FluentAssertions;

namespace DomeGym.Domain.Tests;

public class SessionTests
{
    [Fact]
    public void ReserveSpot_WhenNoMoreRoom_ShouldFailReservation ()
    {
        var session = SessionFactory.CreateSession();
        var participant1 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());
        var participant2 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());
        
        session.ReserveSpot(participant1);
        
        var action = () => session.ReserveSpot(participant2);
        
        action.Should().Throw<Exception>();
    }

    [Fact]
    public void CancelReservation_WhenCancellationInTooCloseToSession_ShouldFailCancellation ()
    {
        var session = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            startTime: Constants.Session.StartTime,
            endTime: Constants.Session.EndTime);

        var participant = ParticipantFactory.CreateParticipant();
        
        session.ReserveSpot(participant);

        var cancellationDateTime = Constants.Session.Date.ToDateTime(TimeOnly.MinValue);
        var action = () => session.CancelReservation(
            participant, 
            new TestDateTimeProvider(fixedDateTime: cancellationDateTime));

        action.Should().ThrowExactly<Exception>();
    }
}