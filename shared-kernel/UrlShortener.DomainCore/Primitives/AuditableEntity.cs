﻿// <copyright file="AuditableEntity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.DomainCore.Primitives;

using System;

public class AuditableEntity<T> : Entity<T>, IAuditableEntity
    where T : IComparable<T>
{
    protected AuditableEntity(T id)
        : base(id)
    {
    }

    protected AuditableEntity() { }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public DateTime? ModifiedAt { get; private set; }

    void IAuditableEntity.UpdateModifiedAt(DateTime? modifiedAt) => this.ModifiedAt = modifiedAt;
}