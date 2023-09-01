using DomeGym.Domain.Tests.TestConstants;

namespace DomeGym.Domain.Tests.TestUtils.Sessions;

public static class SessionFactory
{
    public static Session CreateSession(int maxParticipants, Guid? id = null)
    {
        return new Session(
            maxParticipants: maxParticipants,
            trainerId: Constants.Trainer.Id,
            id: id ?? Constants.Session.Id);
    }
}