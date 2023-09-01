using DomeGym.Domain.Tests.TestUtils.Participants;
using DomeGym.Domain.Tests.TestUtils.Sessions;
using FluentAssertions;

namespace DomeGym.Domain.Tests;

public class SessionTests
{
    [Fact]
    public void ReserveSpot_WhenNoMoreRoom_ShouldFailReservation ()
    {
        var session = SessionFactory.CreateSession(maxParticipants: 1);
        var participant1 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());
        var participant2 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());
        
        session.ReserveSpot(participant1);
        
        var action = () => session.ReserveSpot(participant2);
        
        action.Should().Throw<Exception>();
    }
}