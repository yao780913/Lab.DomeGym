﻿using DomeGym.Domain.GymAggregate;

namespace DomeGym.Application.Common.Interfaces;

public interface IGymsRepository
{
    Task AddGymAsync (Gym gym);

    Task<Gym?> GetByIAsync (Guid id);

    Task<bool> ExistsAsync (Guid id);

    Task<List<Gym>> ListSubscriptionGymsAsync (Guid subscriptionId);

    Task UpdateAsync (Gym gym);
}