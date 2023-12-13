// <copyright file="IEventHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.ApplicationCore.CQRS;
using System.Diagnostics.CodeAnalysis;
using MediatR;
[SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix")]
[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1404:Code analysis suppression should have justification", Justification = "<Pending>")]
public interface IEventHandler<in TNotification> : INotificationHandler<TNotification>
    where TNotification : IEvent
{
}
