namespace UrlShortener.DomainCore.Primitives;
public abstract class Entity<T>(T id) : IEquatable<Entity<T>>
    where T : IComparable<T>
{
    public T Id { get; private set; } = id;
    public virtual bool Equals(Entity<T> other) => 
        !ReferenceEquals(null, other) &&
        (ReferenceEquals(this, other) || EqualityComparer<T>.Default.Equals(Id, other.Id));

    public override bool Equals(object obj) => Equals(obj as Entity<T>);

    public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(Id);

    public static bool operator ==(Entity<T> a, Entity<T> b) => a?.Equals(b) == true;

    public static bool operator !=(Entity<T> a, Entity<T> b) => !(a == b);
}
