using DomeGym.Domain.Common;
using DomeGym.Domain.GymAggregate;
using ErrorOr;

namespace DomeGym.Domain.SubscriptionAggregate;

public class Subscription : AggregateRoot
{
    private readonly Guid _adminId;
    private readonly List<Guid> _gymIds = new ();
    private readonly SubscriptionType _subscriptionType;
    private readonly int _maxGyms;

    public Subscription (SubscriptionType subscriptionType, Guid adminId, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        _subscriptionType = subscriptionType;
        _maxGyms = GetMaxGyms();
        _adminId = adminId;
    }

    public int GetMaxGyms () =>
        _subscriptionType.Name switch
        {
            nameof(SubscriptionType.Free)    => 1,
            nameof(SubscriptionType.Starter) => 1,
            nameof(SubscriptionType.Pro)     => 3,
            _                                => throw new InvalidOperationException()
        };

    public ErrorOr<Success> AddGym (Gym gym)
    {
        if (_gymIds.Contains(gym.Id))
            return Error.Conflict(description: "Gym already exists");

        if (_gymIds.Count >= _maxGyms)
            return SubscriptionErrors.CannotHaveMoreGymsThanSubscriptionAllows;

        _gymIds.Add(gym.Id);

        return Result.Success;
    }
}