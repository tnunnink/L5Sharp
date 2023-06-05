using L5Sharp.Core;

namespace L5Sharp;

/// <summary>
/// A base interface for objects that represent code within a <see cref="Components.Routine"/> component. 
/// </summary>
public interface ILogixCode : ILogixScoped
{
    /// <summary>
    /// The routine name that this <see cref="ILogixCode"/> is contained within.
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the containing routine.</value>
    /// <remarks>
    /// This is only used in deserialization of a <see cref="ILogixCode"/> component.
    /// This helper property makes it easier to filter code objects, such as rungs, structured text, and sheets.
    /// This property is not serialized back to an L5X file, so setting it effectively does nothing useful.
    /// </remarks>
    string Routine { get; }

    /// <summary>
    /// The number or index of the <see cref="ILogixCode"/> position within it's containing <c>Routine</c>.
    /// </summary>
    /// <value>An <see cref="int"/> representing the index number of the code (rung, line, or sheet).</value>
    /// <remarks></remarks>
    int Number { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    NeutralText Text { get; set; }
}