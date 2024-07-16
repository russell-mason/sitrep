namespace SitRep;

// From NHibernate's GuidCombGenerator.cs
// https://github.com/nhibernate/nhibernate-core/blob/5e71e83ac45439239b9028e6e87d1a8466aba551/src/NHibernate/Id/GuidCombGenerator.cs

/// <summary>
/// Creates a GUID using the comb algorithm designed to make the use of GUIDs as Primary Keys, Foreign Keys, 
/// and Indexes, nearly as efficient as numeric values. Also allows sorting in the order of creation.
/// </summary>
public static class CombGuid
{
    /// <summary>
    /// Creates a new GUID using the comb algorithm.
    /// </summary>
    /// <returns>A compatible GUID.</returns>
    public static Guid NewGuid()
    {
        var guidArray = Guid.NewGuid().ToByteArray();

        var baseDate = new DateTime(1900, 1, 1);
        var now = DateTime.Now;

        // Get the days and milliseconds which will be used to build the byte string
        var days = new TimeSpan(now.Ticks - baseDate.Ticks);
        var milliseconds = now.TimeOfDay;

        // Convert to a byte array 
        // Note that SQL Server is accurate to 1/300th of a millisecond, so we divide by 3.333333 
        var daysArray = BitConverter.GetBytes(days.Days);
        var millisecondsArray = BitConverter.GetBytes((long) (milliseconds.TotalMilliseconds / 3.333333));

        // Reverse the bytes to match SQL Servers ordering 
        Array.Reverse(daysArray);
        Array.Reverse(millisecondsArray);

        // Copy the bytes into the guid 
        Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
        Array.Copy(millisecondsArray, millisecondsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

        return new Guid(guidArray);
    }
}
