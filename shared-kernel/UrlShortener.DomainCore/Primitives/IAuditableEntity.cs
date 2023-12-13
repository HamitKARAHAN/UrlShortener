// <copyright file="IAuditableEntity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.DomainCore.Primitives;
public interface IAuditableEntity
{
    DateTime CreatedAt { get; }

    DateTime? ModifiedAt { get; }

    void UpdateModifiedAt(DateTime? modifiedAt);
}
