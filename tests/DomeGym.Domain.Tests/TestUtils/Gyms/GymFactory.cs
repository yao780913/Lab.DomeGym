using DomeGym.Domain.GymAggregate;
using DomeGym.Domain.Tests.TestConstants;

namespace DomeGym.Domain.Tests.TestUtils.Gyms;

public static class GymFactory
{
    public static Gym CreateGym (
        string name = Constants.Gyms.Name,
        int maxRooms = Constants.Subscriptions.MaxRoomFreeTier,
        Guid? id = null)
    {
        return new Gym(
            name,
            maxRooms,
            Constants.Subscriptions.Id,
            id ?? Constants.Gyms.Id);
    }
}