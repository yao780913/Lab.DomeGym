﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomeGym.Infrastructure.Persistence.Converters;

public static class FluentApiExtensions
{
    public static PropertyBuilder<T> HasValueJsonConverter<T>(this PropertyBuilder<T> propertyBuilder)
    {
        return propertyBuilder.HasConversion(
            new ValueJsonConverter<T>(),
            new ValueJsonComparer<T>());
    }

    
    public static PropertyBuilder<T> HasListOfIdsConverter<T> (this PropertyBuilder<T> propertyBuilder)
    {
        return propertyBuilder.HasConversion(
            new ListOfIdsConverter(),
            new ListOfIdsComparer());
    }
}