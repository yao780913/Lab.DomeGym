using ErrorOr;

namespace DomeGym.Domain.SubscriptionAggregate;

public class SubscriptionErrors
{
    public static Error CannotHaveMoreGymsThanSubscriptionAllows =>
        Error.Validation(code: "Subscription.CannotHaveMoreGymsThanSubscriptionAllows",
            description: "Cannot have more gyms than subscription allows");
}