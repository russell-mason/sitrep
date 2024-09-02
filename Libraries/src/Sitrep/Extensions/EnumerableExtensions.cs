namespace Sitrep.Extensions;

/// <summary>
/// Extension methods for IEnumerable.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Replaces the first item in the list that matches the predicate with the specified item.
    /// If no item is found, the new item is added to the end of the list.
    /// This function is immutable and does not modify the original list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">The source list of items.</param>
    /// <param name="predicate">A predicate to find the existing item.</param>
    /// <param name="newItem">The item to replace or add.</param>
    /// <returns>A new list with the new item inserted, or added.</returns>
    public static IEnumerable<T> ReplaceFirstOrAppend<T>(this IEnumerable<T> source, Predicate<T> predicate, T newItem)
    {
        var list = source.ToList();
        var index = list.FindIndex(predicate);

        if (index == -1)
        {
            return list.Append(newItem).ToList();
        }

        list[index] = newItem;
        
        return list;
    }
}
