﻿using DomeGym.Domain.Common.Interfaces;

namespace DomeGym.Domain.Tests.TestUtils.Services;

public class TestDateTimeProvider : IDateTimeProvider
{
    private readonly DateTime? _fixedDateTime;

    public TestDateTimeProvider (DateTime? fixedDateTime = null)
    {
        _fixedDateTime = fixedDateTime;
    }

    public DateTime UtcNow => _fixedDateTime ?? DateTime.UtcNow;
}