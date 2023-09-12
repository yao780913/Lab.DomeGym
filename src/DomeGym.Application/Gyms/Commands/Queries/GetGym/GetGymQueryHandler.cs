using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.GymAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Gyms.Commands.Queries.GetGym;

public class GetGymQueryHandler : IRequestHandler<GetGymQuery, ErrorOr<Gym>>
{
    private readonly IGymsRepository _gymsRepo;
    private readonly ISubscriptionsRepository _subscriptionsRepo;
    
    public GetGymQueryHandler (IGymsRepository gymsRepo, ISubscriptionsRepository subscriptionsRepo)
    {
        _gymsRepo = gymsRepo;
        _subscriptionsRepo = subscriptionsRepo;
    }

    public async Task<ErrorOr<Gym>> Handle (GetGymQuery request, CancellationToken cancellationToken)
    {
        if (await _subscriptionsRepo.ExistsAsync(request.SubscriptionId))
        {
            return Error.NotFound(description: "Subscription not found");
        }

        if (await _gymsRepo.GetByIAsync(request.GymId) is not { } gym)
        {
            return Error.NotFound(description: "Gym not found");
        }

        return gym;
    }
}