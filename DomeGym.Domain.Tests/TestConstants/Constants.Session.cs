﻿using DomeGym.Domain.Common.ValueObjects;

namespace DomeGym.Domain.Tests.TestConstants;

public static partial class Constants
{
    public static class Session
    {
        public const int MaxParticipants = 1;
        public static readonly Guid Id = Guid.NewGuid();
        public static DateOnly Date { get; set; }
        public static TimeOnly StartTime { get; set; }
        public static TimeOnly EndTime { get; set; }
        public static readonly TimeRange Time = new(
            TimeOnly.MinValue.AddHours(8),
            TimeOnly.MinValue.AddHours(9));
    }
}