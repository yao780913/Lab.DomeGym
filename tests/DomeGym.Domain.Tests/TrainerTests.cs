using DomeGym.Domain.Common.ValueObjects;
using DomeGym.Domain.Tests.TestConstants;
using DomeGym.Domain.Tests.TestUtils.Sessions;
using DomeGym.Domain.Tests.TestUtils.Trainers;
using DomeGym.Domain.TrainerAggregate;
using FluentAssertions;

namespace DomeGym.Domain.Tests;

public class TrainerTests
{
    [Theory]
    [InlineData(1, 3, 1, 3)]
    [InlineData(1, 3, 2, 3)]
    [InlineData(1, 3, 2, 4)]
    [InlineData(1, 3, 0, 2)]
    public void AddSessionToSchedule_WhenSessionOverlappingWithAnotherSession_ShouldFail (
        int startHourSession1,
        int endHourSession1,
        int startHourSession2,
        int endHourSession2)
    {
        var trainer = TrainerFactory.CreateTrainer();
        
        var session1 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRange.CreateFromHours(startHourSession1, endHourSession1), 
            id: Guid.NewGuid());
        
        var session2 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRange.CreateFromHours(startHourSession2, endHourSession2), 
            id: Guid.NewGuid());
        
        var addSession1Result = trainer.AddSessionToSchedule(session1);
        var addSession2Result = trainer.AddSessionToSchedule(session2);
        
        addSession1Result.IsError.Should().BeFalse();
        
        addSession2Result.IsError.Should().BeTrue();
        addSession2Result.FirstError.Should().Be(TrainerErrors.CannotHaveTwoOrMoreOverlappingSessions);
    }
}