using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.SessionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Gyms.Commands.Queries.ListSessions;

public class ListSessionsQueryHandler : IRequestHandler<ListSessionsQuery, ErrorOr<List<Session>>>
{
    private readonly IGymsRepository _gymsRepo;
    private readonly ISubscriptionsRepository _subscriptionsRepo;
    private readonly ISessionsRepository _sessionsRepo;

    public ListSessionsQueryHandler (IGymsRepository gymsRepo, ISubscriptionsRepository subscriptionsRepo, ISessionsRepository sessionsRepo)
    {
        _gymsRepo = gymsRepo;
        _subscriptionsRepo = subscriptionsRepo;
        _sessionsRepo = sessionsRepo;
    }
    
    public async Task<ErrorOr<List<Session>>> Handle (ListSessionsQuery query, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionsRepo.GetByIdAsync(query.SubscriptionId);

        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }
        
        if (!subscription.HasGym(query.GymId))
        {
            Error.NotFound(description: "Gym not found");
        }

        if (!await _gymsRepo.ExistsAsync(query.GymId))
        {
            return Error.NotFound(description: "Gym not found");
        }

        return await _sessionsRepo.ListByGymIdAsync(
            query.GymId,
            query.StartDateTime, 
            query.EndDateTime,
            query.Categories);
    }
}