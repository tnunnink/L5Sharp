namespace L5Sharp;

/// <summary>
/// A base interface for objects that represent code within a <see cref="Components.Routine"/> component. 
/// </summary>
public interface ILogixCode
{
    /// <summary>
    /// The container name of the component that this <see cref="ILogixCode"/> is contained within.
    /// This will be the <c>Program</c> or <c>AddOnInstruction</c> name.
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the containing component (Program or AOI).</value>
    /// <remarks>
    /// This is only used in deserialization of a <see cref="ILogixCode"/> component.
    /// This helper property makes it easier to filter code objects, such as rungs, structured text, and sheets.
    /// This property is not serialized back to an L5X file, so setting it effectively does nothing useful.
    /// </remarks>
    string Container { get; set; }

    /// <summary>
    /// The routine name that this <see cref="ILogixCode"/> is contained within.
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the containing routine.</value>
    /// <remarks>
    /// This is only used in deserialization of a <see cref="ILogixCode"/> component.
    /// This helper property makes it easier to filter code objects, such as rungs, structured text, and sheets.
    /// This property is not serialized back to an L5X file, so setting it effectively does nothing useful.
    /// </remarks>
    string Routine { get; set; }

    /// <summary>
    /// The number or index of the <see cref="ILogixCode"/> position within it's containing <c>Routine</c>.
    /// </summary>
    public int Number { get; set; }
}