using DomeGym.Domain.Tests.TestConstants;

namespace DomeGym.Domain.Tests.TestUtils.Gyms;

public static class GymFactory
{
    public static Gym CreateGym (
        int maxRooms = Constants.Subscriptions.MaxRoomFreeTier,
        Guid? id = null)
    {
        return new Gym(
            maxRooms,
            Constants.Subscriptions.Id,
            id ?? Constants.Gyms.Id);
    }
}