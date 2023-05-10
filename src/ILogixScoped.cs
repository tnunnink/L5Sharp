using L5Sharp.Enums;

namespace L5Sharp;

/// <summary>
/// An interface providing members for scoped components that assist with identifying where within the L5X file they
/// exist (i.e. controller, program, routine).
/// </summary>
public interface ILogixScoped
{
    /// <summary>
    /// The scope type of the component.
    /// </summary>
    /// <value>A <see cref="Enums.Scope"/> option indicating the container type for the scoped component.</value>
    /// <remarks>
    /// Note that these properties are only used on deserialization. When serializing component, these properties
    /// are neither considered or written to the resulting L5X file. These properties assist with filtering and identifying
    /// components that could have duplicate names across the entire scope of the L5X.
    /// </remarks>
    Scope Scope { get; }

    /// <summary>
    /// The scope name of the component.
    /// </summary>
    /// <value>A <see cref="string"/> representing the container (program, controller, routine) name of the component.</value>
    /// <remarks>
    /// Note that these properties are only used on deserialization. When serializing component, these properties
    /// are neither considered or written to the resulting L5X file. These properties assist with filtering and identifying
    /// components that could have duplicate names across the entire scope of the L5X.
    /// </remarks>
    string Container { get; }
}