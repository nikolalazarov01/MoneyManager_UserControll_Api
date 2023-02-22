namespace Utilities;

public static class CollectionExtensions
{
    public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> collection)
        => collection ?? Enumerable.Empty<T>();
    
    public static IEnumerable<T> IgnoreNullValues<T>(this IEnumerable<T> collection)
        where T : class
    {
        if (collection is null)
            throw new ArgumentNullException(nameof(collection));

        return collection.Where(el => el is not null);
    }
}