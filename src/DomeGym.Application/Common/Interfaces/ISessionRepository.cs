using DomeGym.Domain.SessionAggregate;

namespace DomeGym.Application.Common.Interfaces;

public interface ISessionRepository
{
    Task AddSessionAsync (Session session);
    Task UpdateSessionAsync (Session session);
    
}