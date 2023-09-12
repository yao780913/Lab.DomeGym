using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.GymAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Gyms.Commands.Queries.ListGyms;

public class ListGymsQueryHandler : IRequestHandler<ListGymsQuery, ErrorOr<List<Gym>>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepo;
    private readonly IGymsRepository _gymsRepo;

    public ListGymsQueryHandler (ISubscriptionsRepository subscriptionsRepo, IGymsRepository gymsRepo)
    {
        _subscriptionsRepo = subscriptionsRepo;
        _gymsRepo = gymsRepo;
    }
    
    public async Task<ErrorOr<List<Gym>>> Handle (ListGymsQuery query, CancellationToken cancellationToken)
    {
        if (!await _subscriptionsRepo.ExistsAsync(query.SubscriptionId))
        {
            return Error.NotFound(description: "subscription not found");
        }

        return await _gymsRepo.ListSubscriptionGymsAsync(query.SubscriptionId);
    }
}