using DomeGym.Domain.GymAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Gyms.Commands.Queries.ListGyms;

public record ListGymsQuery(Guid SubscriptionId): IRequest<ErrorOr<List<Gym>>>;