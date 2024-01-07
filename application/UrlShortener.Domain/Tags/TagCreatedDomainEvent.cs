// <copyright file="TagCreatedDomainEvent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Ardalis.GuardClauses;
using UrlShortener.DomainCore.Abstractions;

namespace UrlShortener.Domain.Tags;

public record TagCreatedDomainEvent : IDomainEvent
{
    public TagId TagId { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    private TagCreatedDomainEvent(TagId id, DateTime createdOnUtc)
    {
        this.TagId = id;
        this.CreatedOnUtc = createdOnUtc;
    }

    internal static TagCreatedDomainEvent Create(TagId id, DateTime createdOnUtc)
    {
        Guard.Against.Null(id);
        Guard.Against.NullOrWhiteSpace(id.Value);
        Guard.Against.Default(createdOnUtc);
        return new TagCreatedDomainEvent(id, createdOnUtc);
    }
}
