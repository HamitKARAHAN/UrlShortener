// <copyright file="TagDetail.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Domain.TagDetails;

using UrlShortener.Domain.Tags;
using UrlShortener.DomainCore.Primitives;

public class TagDetail : AuditableEntity<TagDetailId>, ISoftDelete
{
    internal TagDetail(TagId tagId, int clickedCount, DateTime? lastCallTime)
        : base(TagDetailId.NewId())
    {
        this.TagId = tagId;
        this.ClickedCount = clickedCount;
        this.LastCallTime = lastCallTime;
    }

    private TagDetail() { }

    public TagId TagId { get; private set; }
    public int ClickedCount { get; private set; }
    public DateTime? LastCallTime { get; private set; }
    public bool IsDeleted { get; private set; }

    public DateTimeOffset? DeletedAt { get; private set; }

    void ISoftDelete.SetDeleted(DateTime deletedAt)
    {
        this.IsDeleted = true;
        this.DeletedAt = deletedAt;
    }

    internal static TagDetail Create(TagId tagId)
        => new (
            tagId: tagId,
            clickedCount: default,
            lastCallTime: default);

    public void UpdateClickedCount() => this.ClickedCount++;

    public void UpdateLastCallTime(DateTime dateTime) => this.LastCallTime = dateTime;
}
