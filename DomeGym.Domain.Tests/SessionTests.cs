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

        var reserveParticipant1Result = session.ReserveSpot(participant1);
        var reserveParticipant2Result = session.ReserveSpot(participant2);

        reserveParticipant1Result.IsError.Should().BeFalse();
        
        reserveParticipant2Result.IsError.Should().BeTrue();
        reserveParticipant2Result.FirstError.Should().Be(SessionError.CannotHaveMoreReservationThanParticipants);
    }

    [Fact]
    public void CancelReservation_WhenCancellationInTooCloseToSession_ShouldFailCancellation ()
    {
        var session = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            startTime: Constants.Session.StartTime,
            endTime: Constants.Session.EndTime);

        var participant = ParticipantFactory.CreateParticipant();

        var reserveSpot = session.ReserveSpot(participant);

        var cancellationDateTime = Constants.Session.Date.ToDateTime(TimeOnly.MinValue);
        var cancelReservationResult = session.CancelReservation(
            participant, 
            new TestDateTimeProvider(fixedDateTime: cancellationDateTime));

        reserveSpot.IsError.Should().BeFalse();

        cancelReservationResult.IsError.Should().BeTrue();
        cancelReservationResult.FirstError.Should().Be(SessionError.CannotCancelReservationTooCloseToSession);
    }
}