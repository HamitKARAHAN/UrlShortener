namespace UrlShortener.DomainCore.Guards;

using Ardalis.GuardClauses;
using System;
using System.Runtime.CompilerServices;
using UrlShortener.DomainCore.Primitives;

public static class AuditableEntityGuard
{
    public static void UnSetCreatedAt(this IGuardClause guardClause, IAuditableEntity auditableEntity, [CallerArgumentExpression(nameof(auditableEntity))] string parameterName = null)
    {
        if(auditableEntity.CreatedAt == default) 
        { 
            throw new ArgumentNullException(parameterName, message: "CreatedAt must be set before saving."); 
        }
    }
}
