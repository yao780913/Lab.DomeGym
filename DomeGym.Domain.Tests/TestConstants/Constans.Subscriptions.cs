namespace DomeGym.Domain.Tests.TestConstants;


public partial class Constants
{
    public static class Subscriptions
    {
        public static SubscriptionType DefaultSubscriptionType = SubscriptionType.Free;
        public const int MaxRoomFreeTier = 1;
        public static readonly Guid Id = Guid.NewGuid();

    }
}
