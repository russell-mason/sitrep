namespace Sitrep;

/// <summary>
/// Creates a GUID using the comb algorithm designed to make the use of GUIDs as Primary Keys, Foreign Keys, 
/// and Indexes, nearly as efficient as numeric values. Also allows sorting in the order of creation.
/// </summary>
public static class CombGuid
{
    private static readonly SqlCombProvider _sqlCombProvider =
        new(new UnixDateTimeStrategy(), new UtcNoRepeatTimestampProvider().GetTimestamp);

    /// <summary>
    /// Gets or sets a function that generates a new GUID. By default, this is set to a comb algorithm
    /// compatible with SQL Server, but can be changed.
    /// </summary>
    public static Func<Guid> GuidGeneratorFunction { get; set; } = _sqlCombProvider.Create;

    /// <summary>
    /// Creates a new GUID using the comb algorithm.
    /// </summary>
    /// <returns>A compatible GUID.</returns>
    public static Guid NewGuid() => GuidGeneratorFunction();

    /// <summary>
    /// Orders the sequence of comb based GUIDs in the order they were created.
    /// Non-comb based GUIDs will not be ordered correctly.
    /// </summary>
    /// <param name="values">A list of comb-based guids to order.</param>
    /// <returns>A sequence of GUIDs in the order they were created.</returns>
    public static IEnumerable<Guid> Order(IEnumerable<Guid> values)
    {
        var copy = new List<Guid>(values);

        copy.Sort(Compare);

        return copy;
    }

    /// <summary>
    /// Resets the GUID generator function to the default comb algorithm compatible with SQL Server
    /// </summary>
    public static void ResetGuidGeneratorFunction()
    {
        GuidGeneratorFunction = _sqlCombProvider.Create;
    }

    // https://stackoverflow.com/questions/29674395/how-to-sort-sequential-guids-in-c
    private static int Compare(Guid first, Guid second)
    {
        const int bytesInGuid = 16;

        var firstAsBytes = new byte[bytesInGuid];
        var secondAsBytes = new byte[bytesInGuid];

        first.ToByteArray().CopyTo(firstAsBytes, 0);
        second.ToByteArray().CopyTo(secondAsBytes, 0);

        // 16 Bytes = 128 Bit 
        int[] byteOrder = [10, 11, 12, 13, 14, 15, 8, 9, 6, 7, 4, 5, 0, 1, 2, 3];

        // Swap to the correct order to be compared
        for (var index = 0; index < bytesInGuid; index++)
        {
            var byte1 = firstAsBytes[byteOrder[index]];
            var byte2 = secondAsBytes[byteOrder[index]];

            if (byte1 != byte2)
            {
                return byte1 < byte2 ? -1 : 1;
            }
        }

        return 0;
    }
}