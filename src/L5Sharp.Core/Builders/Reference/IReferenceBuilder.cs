namespace L5Sharp.Core;

/// <summary>
/// Defines a contract for building and configuring <see cref="Reference"/> objects in a structured and customizable manner.
/// </summary>
public interface IReferenceBuilder
{
    /// <summary>
    /// Builds the reference based on the previously defined parameters and configurations.
    /// Instantiates a complete <see cref="Reference"/> object reflecting the specified details,
    /// such as type, name, container, and other associated attributes.
    /// </summary>
    /// <returns>A newly constructed <see cref="Reference"/> representing the defined reference configuration.</returns>
    Reference Build();
}