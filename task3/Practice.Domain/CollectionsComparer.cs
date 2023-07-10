namespace Practice.Domain;

public class CollectionsComparer<T> : IEqualityComparer<T> 
{
    private static CollectionsComparer<T>? _instance;
    
    private CollectionsComparer() { }
    
    public static CollectionsComparer<T> Instance =>
        _instance ??= new CollectionsComparer<T>();

    public bool Equals(T? x, T? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Equals(y);
    }

    public int GetHashCode(T? obj)
    {
        return obj.GetHashCode();
    }
}
