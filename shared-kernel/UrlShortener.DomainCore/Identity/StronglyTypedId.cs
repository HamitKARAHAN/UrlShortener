namespace UrlShortener.DomainCore.Identity;
public abstract record StronglyTypedId<TValue, T>(TValue Value) : IComparable<StronglyTypedId<TValue, T>>
    where T : StronglyTypedId<TValue, T>
    where TValue : IComparable<TValue>
{
    public int CompareTo(StronglyTypedId<TValue, T> other) => other == null ? 1 : Value.CompareTo(other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public abstract bool TryParse(TValue value, out T result);

    public virtual bool Equals(StronglyTypedId<TValue, T> other) => other != null && Value.Equals(other.Value);

    public static implicit operator TValue(StronglyTypedId<TValue, T> stronglyTypedId) => stronglyTypedId.Value;
}
