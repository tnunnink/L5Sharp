using System.Xml.Linq;
using L5Sharp.Types;

namespace L5Sharp.Components;

/// <summary>
/// A generic tag member ...
/// </summary>
/// <typeparam name="TLogixType"></typeparam>
public class TagMember<TLogixType> : TagMember where TLogixType : LogixType
{
    /// <inheritdoc />
    internal TagMember(Member member, XElement? root = null, TagMember? parent = null) : base(member, root, parent)
    {
    }

    /// <summary>
    /// The value of the <c>TagMember</c> data.
    /// </summary>
    /// <value>A <see cref="LogixType"/> representing the value of the <c>Tag</c>.</value>
    public new TLogixType Value
    {
        get => base.Value.To<TLogixType>();
        set => base.Value = value;
    }
}