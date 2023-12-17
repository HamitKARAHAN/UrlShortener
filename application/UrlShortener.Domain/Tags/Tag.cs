// <copyright file="Tag.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Domain.Tags;

using Ardalis.GuardClauses;
using UrlShortener.Domain.TagDetails;
using UrlShortener.DomainCore.Primitives;

public sealed class Tag : AggregateRoot<TagId>, ISoftDelete
{
    private Tag(
        string shortUrl,
        string longUrl,
        string ip,
        string description,
        bool isPublic)
        : base(TagId.NewId())
    {
        this.ShortUrl = shortUrl;
        this.LongUrl = longUrl;
        this.Ip = ip;
        this.Description = description;
        this.IsPublic = isPublic;
    }

    private Tag() { }

    public TagDetail TagDetail { get; private set; }
    public string ShortUrl { get; private set; }
    public string LongUrl { get; private set; }
    public string Ip { get; private set; }
    public string Description { get; private set; }
    public bool IsPublic { get; private set; }

    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }

    void ISoftDelete.SetDeleted(DateTime deletedAt)
    {
        this.IsDeleted = true;
        this.DeletedAt = deletedAt;
    }

    public static Tag Create(
        string shortUrl,
        string longUrl,
        string ip,
        string description,
        bool isPublic)
    {
        Guard.Against.NullOrEmpty(shortUrl);
        Guard.Against.NullOrEmpty(longUrl);
        Guard.Against.NullOrEmpty(ip);
        Guard.Against.NullOrEmpty(description);

        Tag tag = new (
            shortUrl: shortUrl,
            longUrl: longUrl,
            ip: ip,
            description: description,
            isPublic: isPublic);

        tag.TagDetail = TagDetail.Create(tag.Id);
        return tag;
    }
}
