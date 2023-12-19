// <copyright file="Tag.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Domain.Tags;

using Ardalis.GuardClauses;
using System.Net;
using UrlShortener.ApplicationCore.Result;
using UrlShortener.Domain.TagDetails;
using UrlShortener.DomainCore.Primitives;

public sealed class Tag : AggregateRoot<TagId>, ISoftDelete
{
    private Tag(
        ShortUrl shortUrl,
        LongUrl longUrl,
        Ip ip,
        Description description,
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
    public ShortUrl ShortUrl { get; private set; }
    public LongUrl LongUrl { get; private set; }
    public Ip Ip { get; private set; }
    public Description Description { get; private set; }
    public bool IsPublic { get; private set; }

    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }

    void ISoftDelete.SetDeleted(DateTime deletedAt)
    {
        this.IsDeleted = true;
        this.DeletedAt = deletedAt;
    }

    public static Result<Tag> Create(
        ShortUrl shortUrl,
        LongUrl longUrl,
        Ip ip,
        Description description,
        bool isPublic)
    {
        Guard.Against.Null(shortUrl);
        Guard.Against.Null(longUrl);
        Guard.Against.Null(ip);
        Guard.Against.Null(description);

        Tag tag = new (
            shortUrl: shortUrl,
            longUrl: longUrl,
            ip: ip,
            description: description,
            isPublic: isPublic);

        tag.TagDetail = TagDetail.Create(tag.Id).Value;
        return Result<Tag>.Success(tag);
    }
}
