namespace Practice.Domain;

public static class EnumerableExtensions
{
    public static IEnumerable<IEnumerable<T>> 
        GetCombinations<T>(
            this IEnumerable<T> collection, 
            int length, 
            CollectionsComparer<T> comparer) // комбинации
        where T : IComparable 
    {
        CheckCollections(collection, comparer);

        if (length == 1) return collection.Select(t => new T[] { t });
        return GetCombinations(collection, length - 1, comparer)
            .SelectMany(t => collection.Where(o => o.CompareTo(t.Last()) >= 0), 
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }
    
    public static IEnumerable<IEnumerable<T>> 
        GetSubset<T>(
            this IEnumerable<T> collection, 
            CollectionsComparer<T> comparer) // подмножества
    {
        CheckCollections(collection, comparer);
        
        var set = new List<IEnumerable<T>>() { Enumerable.Empty<T>() };
        return collection.Aggregate((IEnumerable<IEnumerable<T>>)set, 
            (x, y) => x.Concat(x.Select(z 
                => z.Concat(new List<T>() { y }))));
    }
    
    public static IEnumerable<IEnumerable<T>>
        GetPermutations<T>(
            this IEnumerable<T> collection, 
            int length, 
            CollectionsComparer<T> comparer) // перестановки
    {
        CheckCollections(collection, comparer);

        if (length == 1) return collection.Select(t => new T[] { t });
        return GetPermutations(collection, length - 1, comparer)
            .SelectMany(t => collection.Where(o => !t.Contains(o)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    private static void CheckCollections<T>(IEnumerable<T> collection, CollectionsComparer<T> comparer)
    {
        if (collection is null || comparer is null)
            throw new ArgumentNullException("Null аргумент в компораторе", nameof(collection) + nameof(comparer));
        
        if (collection.Distinct(comparer).Count() != collection.Count())
            throw new ArgumentException("Повтор значений в коллекции", nameof(collection));
    }
}
