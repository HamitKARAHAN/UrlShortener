namespace UrlShortener.DomainCore.Identity;
using System;
using System.Reflection;

public abstract record StronglyTypedUlid<T>(string Value) : StronglyTypedId<string, T>(Value)
    where T : StronglyTypedUlid<T>
{
    public static T NewId() => 
        GenerateIdInstance(Ulid.NewUlid());

    public override bool TryParse(string value, out T? result)
    {
        bool success = Ulid.TryParse(value, out Ulid parsedValue);
        result = success ? GenerateIdInstance(id: parsedValue) : null;
        return success;
    }

    private static T GenerateIdInstance(Ulid id) => 
        (T)Activator.CreateInstance(typeof(T), BindingFlags.Instance | BindingFlags.Public, null, new object[] { id.ToString() }, null)!;
}
