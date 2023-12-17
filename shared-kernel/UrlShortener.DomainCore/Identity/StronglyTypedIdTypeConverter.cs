// <copyright file="StronglyTypedIdTypeConverter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.DomainCore.Identity;

using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

public abstract class StronglyTypedIdTypeConverter<TValue, T> : TypeConverter
    where T : StronglyTypedId<TValue, T>
    where TValue : IComparable<TValue>
{
    private static readonly MethodInfo TryParseMethod = typeof(T).GetMethod(name: nameof(StronglyTypedId<TValue, T>.TryParse));

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is not string valueAsString || TryParseMethod == null)
        {
            return base.ConvertFrom(context, culture, value);
        }

        object[] parameters = [valueAsString, null];

        if ((bool)TryParseMethod.Invoke(obj: null, parameters)!)
        {
            return (T)parameters[1];
        }

        return base.ConvertFrom(context, culture, value);
    }
}
