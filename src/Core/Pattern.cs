namespace L5Sharp.Core;

/// <summary>
/// A static class containing reusable regex pattern strings that are helpful for parsing textual data such as
/// <see cref="NeutralText"/> and <see cref="Core.TagName"/>.
/// </summary>
public static class Pattern
{
    /// <summary>
    /// A regex pattern for a Logix tag name with starting and ending anchors.
    /// Use this pattern to match a string and ensure it is only a tag name an nothing else.
    /// </summary>
    public const string AnchoredTagName =
        @"^[A-Za-z_][\w+:]{1,39}(?:(?:\[\d+\]|\[\d+,\d+\]|\[\d+,\d+,\d+\])?(?:\.[A-Za-z_]\w{1,39})?)+$";

    /// <summary>
    /// A regex pattern for identifying/ensuring a string is a number and nothing else. 
    /// </summary>
    public const string AnchoredNumber = @"^[+-]?(?:[0-9]*[.])?[0-9]+$";

    /// <summary>
    /// A regex pattern for identifying/ensuring a string either a tag name or an immediate number value. 
    /// </summary>
    public const string AnchoredTagNameOrNumber =
        @"^(?:[A-Za-z_][\w+:]{1,39}(?:(?:\[\d+\]|\[\d+,\d+\]|\[\d+,\d+,\d+\])?(?:\.[A-Za-z_]\w{1,39})?)+|[+-]?(?:[0-9]*[.])?[0-9]+)$";

    
    /*/// <summary>
    /// The regex pattern for Logix tag names without starting and ending anchors.
    /// This pattern also includes a negative lookahead for removing text prior to parenthesis (i.e. instruction keys)
    /// Use this patter for tag names within text, such as longer
    /// </summary>
    public const string TagName =
        @"(?!\w*\()[A-Za-z_][\w+:]{1,39}(?:(?:\[\d+\]|\[\d+,\d+\]|\[\d+,\d+,\d+\])?(?:\.[A-Za-z_]\w{1,39})?)+";*/
}