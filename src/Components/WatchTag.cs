namespace L5Sharp.Components;

/// <summary>
/// An individual watch tag within a <see cref="WatchList"/> component.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class WatchTag
{
    /// <summary>
    /// Specify the tag or part of a tag to watch.
    /// </summary>
    /// <value>A <see cref="string"/> representing the tag specifier.</value>
    public string Specifier { get; set; } = string.Empty;

    /// <summary>
    /// Specify the name of program, equipment phase, or Add-On Instruction that contains the watch tag.
    /// </summary>
    /// <value>A <see cref="string"/> representing the scope of the tag. If controller scope set to empty.</value>
    public string Scope { get; set; } = string.Empty;
}