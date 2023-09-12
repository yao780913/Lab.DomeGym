﻿using System.Reflection;
using DomeGym.Domain.AdminAggregate;
using DomeGym.Domain.GymAggregate;
using DomeGym.Domain.ParticipantAggregate;
using DomeGym.Domain.RoomAggregate;
using DomeGym.Domain.SessionAggregate;
using DomeGym.Domain.SubscriptionAggregate;
using DomeGym.Domain.TrainerAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DomeGym.Infrastructure.Persistence;

public class DomeGymDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public DbSet<Subscription> Subscriptions { get; set; } = null!;
    public DbSet<Gym> Gyms { get; set; } = null!;
    public DbSet<Room> Rooms { get; set; } = null!;
    public DbSet<Session> Sessions { get; set; } = null!;
    public DbSet<Trainer> Trainers { get; set; } = null!;
    public DbSet<Participant> Participants { get; set; } = null!;
    public DbSet<Admin> Admins { get; set; } = null!;

    public DomeGymDbContext (DbContextOptions options, IHttpContextAccessor httpContextAccessor)
        : base (options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
}