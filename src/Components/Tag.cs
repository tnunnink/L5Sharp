using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Components;

/// <summary>
/// A logix <c>Tag</c> component. Contains the properties that comprise the L5X Tag element.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class Tag : TagMember, ILogixComponent
{
    /// <inheritdoc />
    public Tag() : base(new Member(GenerateElement()))
    {
        Name = string.Empty;
        TagType = TagType.Base;
        ExternalAccess = ExternalAccess.ReadWrite;
        Constant = false;
    }

    /// <inheritdoc />
    public Tag(XElement element) : base(new Member(element))
    {
    }

    /// <inheritdoc />
    public Use? Use
    {
        get => GetValue<Use>();
        set => SetValue(value);
    }

    /// <inheritdoc />
    public new string Name
    {
        get => GetValue<string>() ?? throw new L5XException(Element);
        set => SetValue(value);
    }

    /// <inheritdoc cref="ILogixComponent.Description" />
    public override string Description
    {
        get => GetProperty<string>() ?? string.Empty;
        set => SetProperty(value);
    }

    /// <summary>
    /// The external access option indicating the read/write access of the tag.
    /// </summary>
    /// <value>A <see cref="Enums.ExternalAccess"/> option representing read/write access of the tag.</value>
    public ExternalAccess ExternalAccess
    {
        get => GetValue<ExternalAccess>() ?? throw new L5XException(Element);
        set => SetValue(value);
    }

    /// <summary>
    /// A type indicating whether the current tag component is a base tag, or alias for another tag instance.
    /// </summary>
    /// <value>A <see cref="Enums.TagType"/> option representing the type of tag component.</value>
    public TagType TagType
    {
        get => GetValue<TagType>() ?? throw new L5XException(Element);
        set => SetValue(value);
    }

    /// <summary>
    /// The usage option indicating the scope in which the tag is visible or usable from.
    /// </summary>
    /// <value>A <see cref="Enums.TagUsage"/> option representing the tag scope.</value>
    public TagUsage? Usage
    {
        get => GetValue<TagUsage>();
        set => SetValue(value);
    }

    /// <summary>
    /// The tag name of the tag that is the alias of the current tag object.
    /// </summary>
    /// <value>A <see cref="Core.TagName"/> string representing the full tag name of the alias tag.</value>
    public TagName? AliasFor
    {
        get => GetValue<TagName>();
        set => SetValue(value);
    }

    /// <summary>
    /// Indicates whether the tag is a constant.
    /// </summary>
    /// <value><c>true</c> if the tag is constant; otherwise, <c>false</c>.</value>
    /// <remarks>Only value type tags have the ability to be set as a constant. Default is <c>false</c>.</remarks>
    public bool Constant
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    private static XElement GenerateElement() => new(L5XName.Tag);
}