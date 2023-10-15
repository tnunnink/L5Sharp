// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
namespace L5Sharp.Enums;

/// <summary>
/// An enumeration of known Logix <c>Keywords</c> used for the <c>StructuredText</c> programming language.
/// </summary>
public class Keyword : LogixEnum<Keyword, string>
{
    private Keyword(string name, string value) : base(name, value)
    {
    }
    
    /// <summary>
    /// Represents the By Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword By = new(nameof(By), "by");
    
    /// <summary>
    /// Represents the Case Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword Case = new(nameof(Case), "case");
    
    /// <summary>
    /// Represents the Comma Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword Comma = new(nameof(Comma), ",");
    
    /// <summary>
    /// Represents the Do Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword Do = new(nameof(Do), "do");
    
    /// <summary>
    /// Represents the Dot Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword Period = new(nameof(Period), ".");
    
    /// <summary>
    /// Represents the Else Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword Else = new(nameof(Else), "else");
    
    /// <summary>
    /// Represents the ElsIf Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword ElsIf = new(nameof(ElsIf), "elseif");
    
    /// <summary>
    /// Represents the EndCase Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword EndCase = new(nameof(EndCase), "end_case");
    
    /// <summary>
    /// Represents the EndFor Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword EndFor = new(nameof(EndFor), "end_for");
    
    /// <summary>
    /// Represents the EndIf Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword EndIf = new(nameof(EndIf), "end_if");

    /// <summary>
    /// Represents the EndRepeat Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword EndRepeat = new(nameof(EndRepeat), "end_repeat");
    
    /// <summary>
    /// Represents the EndWhile Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword EndWhile = new(nameof(EndWhile), "end_while");
    
    /// <summary>
    /// Represents the Exit Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword Exit = new(nameof(Exit), "exit");
    
    /// <summary>
    /// Represents the For Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword For = new(nameof(For), "for");
    
    /// <summary>
    /// Represents the Goto Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword Goto = new(nameof(Goto), "goto");
    
    /// <summary>
    /// Represents the If Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword If = new(nameof(If), "if");
    
    /// <summary>
    /// Represents the LeftBracket Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword LeftBracket = new(nameof(LeftBracket), "[");
    
    /// <summary>
    /// Represents the LeftParenthesis Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword LeftParenthesis = new(nameof(LeftParenthesis), ")");
    
    /// <summary>
    /// Represents the Of Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword Of = new(nameof(Of), "of");
    
    /// <summary>
    /// Represents the Repeat Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword Repeat = new(nameof(Repeat), "repeat");
    
    /// <summary>
    /// Represents the Return Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword Return = new(nameof(Return), "return");
    
    /// <summary>
    /// Represents the RightBracket Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword RightBracket = new(nameof(RightBracket), "]");
    
    /// <summary>
    /// Represents the RightParenthesis Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword RightParenthesis = new(nameof(RightParenthesis), ")");
    
    /// <summary>
    /// Represents the Then Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword Then = new(nameof(Then), "then");
    
    /// <summary>
    /// Represents the This Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword This = new(nameof(This), "this");
    
    /// <summary>
    /// Represents the To Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword To = new(nameof(To), "to");
    
    /// <summary>
    /// Represents the Until Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword Until = new(nameof(Until), "until");
    
    /// <summary>
    /// Represents the While Logix <see cref="Keyword"/>.
    /// </summary>
    public static readonly Keyword While = new(nameof(While), "while");
}