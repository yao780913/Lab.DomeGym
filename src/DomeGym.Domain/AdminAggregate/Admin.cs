using DomeGym.Domain.Common;

namespace DomeGym.Domain.AdminAggregate;

public class Admin : AggregateRoot
{
    public Guid UserId { get; }
    public Guid? SubscriptionId { get; }

    protected Admin (Guid userId, Guid? id = null, Guid? subscriptionId = null) 
        : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
    }
}