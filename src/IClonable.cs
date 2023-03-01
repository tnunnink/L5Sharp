namespace L5Sharp;

/// <summary>
/// An interface that provided tha contract for performing cloning of objects.
/// </summary>
/// <typeparam name="T">The type to clone.</typeparam>
/// <remarks>
/// This is useful for us so we don't have to perform the complex object initialization if we already have a copy of
/// a component in memory. Some of the Logix lots of properties and the initialization can be painful.
/// </remarks>
public interface ICloneable<out T>
{
    /// <summary>
    /// Clones the current object by creating a new instance and copying all data into the new instance.
    /// </summary>
    /// <returns>A new object of the specified type.</returns>
    T Clone();
}