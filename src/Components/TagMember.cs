using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;

namespace L5Sharp.Components;

/// <summary>
/// 
/// </summary>
public class TagMember : LogixEntity<TagMember>, ILogixScoped
{
    private readonly Member _member;
    private readonly XElement _root;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="member"></param>
    /// <param name="root"></param>
    /// <param name="parent"></param>
    /// <exception cref="ArgumentNullException"></exception>
    protected TagMember(Member member, XElement? root = null, TagMember? parent = null) : base(member.Serialize())
    {
        _member = member ?? throw new ArgumentNullException(nameof(member));
        _root = root ?? Element;
        Parent = parent;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Name => Element.MemberName() ?? throw new L5XException(Element);

    /// <summary>
    /// The description of the tag which is either the root tag description or the inherited (pass through)
    /// description of the descendent member.
    /// </summary>
    /// <value>A <see cref="string"/> containing the text description for the tag.</value>
    public virtual string Description
    {
        get => GetComment();
        set => SetComment(value);
    }

    /// <summary>
    /// The full tag name path of the tag or tag member.
    /// </summary>
    /// <value>A <see cref="Core.TagName"/> type representing the tag name of the member.</value>
    public TagName TagName => Parent is not null ? TagName.Combine(Parent.TagName, Name) : new TagName(Name);

    /// <summary>
    /// The name of the data type that the tag data contains. 
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the tag data type.</value>
    /// <remarks>
    /// This property simply points to the name property of <see cref="Value"/>.
    /// This keeps the properties in sync. By setting value, you are setting the data type name.</remarks>
    public string DataType => Value.Name;

    /// <summary>
    /// The dimensions of the tag, indicating the length and dimensions of it's array.
    /// </summary>
    /// <value>A <see cref="Core.Dimensions"/> value representing the array dimensions of the tag.</value>
    /// <remarks>
    /// This value will always point to the dimensions property of <see cref="Value"/>, assuming it is an
    /// <see cref="ArrayType"/>.
    /// If <c>Data</c> is not an array type, this property will always return <see cref="Core.Dimensions.Empty"/>.
    /// </remarks>
    public Dimensions Dimensions => Value is ArrayType<LogixType> array ? array.Dimensions : Dimensions.Empty;

    /// <summary>
    /// The radix format of the tags data value. Only applies if the tag is an <see cref="AtomicType"/>.
    /// </summary>
    /// <value>A <see cref="Enums.Radix"/> option representing data format of the tag value.</value>
    /// <remarks>
    /// This value will always point to the radix of <see cref="Value"/>, assuming it is an <see cref="AtomicType"/>.
    /// If <c>Value</c> is not an atomic type, this property will always return <see cref="Enums.Radix.Null"/>.
    /// </remarks>
    public Radix Radix => Value is AtomicType atomic ? atomic.Radix : Radix.Null;

    /// <summary>
    /// The value of the <c>TagMember</c> data.
    /// </summary>
    /// <value>A <see cref="LogixType"/> representing the value of the <c>Tag</c>.</value>
    public LogixType Value
    {
        get => _member.DataType;
        set => _member.DataType = value;
    }

    /// <summary>
    /// The root tag of the current <see cref="TagMember"/> member.
    /// </summary>
    /// <value>A <see cref="TagMember"/> representing the root tag of the current tag member.</value>
    /// <remarks>This is here to assist in navigating back up the hierarchical data structure of the tag.</remarks>
    public TagMember Root => new Tag(_root);

    /// <summary>
    /// The parent tag or tag member of the current <see cref="TagMember"/> member.
    /// </summary>
    /// <value>A <see cref="TagMember"/> representing the immediate parent tag of the current tag member.</value>
    /// <remarks>This is here to assist in navigating back up the hierarchical data structure of the tag.</remarks>
    public TagMember? Parent { get; }

    /// <inheritdoc />
    public Scope Scope => Scope.FromElement(_root);

    /// <inheritdoc />
    public string Container => Scope != Scope.Null ? _root.Ancestors(Scope.XName).First().LogixName() : string.Empty;

    /// <summary>
    /// The units of the tag member. This appears to only apply to module defined tags...
    /// </summary>
    /// <value>A <see cref="string"/> representing the scaled units of the tag member.</value>
    public string Unit => throw new NotImplementedException();

    /// <summary>
    /// Gets an element of the tag array.
    /// </summary>
    /// <param name="index">The index of the element to retrieve.</param>
    /// <exception cref="ArgumentException"><c>index</c> does not represent a valid member for the tag member data structure.</exception>
    public TagMember this[ushort index]
    {
        get
        {
            var name = $"[{index}]";
            var member = Value.Members
                .FirstOrDefault(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));

            if (member is null)
                throw new ArgumentException(
                    $"No element with index '{name}' exists in the tag data structure for type {DataType}.");

            return new TagMember(member, _root, this);
        }
    }

    /// <summary>
    /// Gets a descendent tag member with the provided tag name value.
    /// </summary>
    /// <param name="tagName">The tag name relative to the current tag member for which to retrieve.</param>
    /// <exception cref="ArgumentNullException"><c>tagName</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>tagName</c> does not represent a valid member for the tag member data structure.</exception>
    public TagMember this[TagName tagName]
    {
        get
        {
            if (tagName is null)
                throw new ArgumentNullException(nameof(tagName));

            var memberName = tagName.Members.FirstOrDefault() ?? string.Empty;

            var member = Value.Members
                .FirstOrDefault(m => string.Equals(m.Name, memberName, StringComparison.OrdinalIgnoreCase));

            if (member is null)
                throw new ArgumentException(
                    $"No member with name '{memberName}' exists in the tag data structure for type {DataType}.");

            var tagMember = new TagMember(member, _root, this);

            var remaining = TagName.Combine(tagName.Members.Skip(1));

            return remaining.IsEmpty ? tagMember : tagMember[remaining];
        }
    }

    public void Add(string name, LogixType type)
    {
        if (Value is not ComplexType complexType)
            throw new InvalidOperationException();
        
        complexType.Add(new Member(name, type));
    }
    
    public void Remove(string name)
    {
        if (Value is not ComplexType complexType)
            throw new InvalidOperationException();
        
        complexType.Remove(name);
    }

    /// <summary>
    /// Gets a descendent tag member relative to the current tag member.
    /// </summary>
    /// <param name="tagName">The full <see cref="Core.TagName"/> path of the member to get.</param>
    /// <returns>A <see cref="TagMember"/> representing the child member instance.</returns>
    /// <remarks>
    /// Note that <c>tagName</c> can be a path to a member more than one layer down the hierarchical structure
    /// of the tag or tag member. However, it must start with a member of the current tag or tag member, and not the
    /// actual name of the current tag or tag member.
    /// </remarks>
    /// <example>
    /// <c>var member = tag.Member("Array[1].SubType.Member.0");</c>
    /// </example>
    public TagMember? Member(TagName tagName)
    {
        if (tagName is null)
            throw new ArgumentNullException(nameof(tagName));

        var memberName = tagName.Members.FirstOrDefault() ?? string.Empty;

        var member = Value.Members
            .FirstOrDefault(m => string.Equals(m.Name, memberName, StringComparison.OrdinalIgnoreCase));

        if (member is null) return default;

        var tagMember = new TagMember(member, _root, this);

        var remaining = TagName.Combine(tagName.Members.Skip(1));

        return remaining.IsEmpty ? tagMember : tagMember.Member(remaining);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    /// <typeparam name="TLogixType"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public TagMember<TLogixType>? Member<TLogixType>(TagName tagName) where TLogixType : LogixType
    {
        if (tagName is null)
            throw new ArgumentNullException(nameof(tagName));

        var memberName = tagName.Members.FirstOrDefault() ?? string.Empty;

        var member = Value.Members
            .FirstOrDefault(m => string.Equals(m.Name, memberName, StringComparison.OrdinalIgnoreCase));

        if (member is null) return default;

        var remaining = TagName.Combine(tagName.Members.Skip(1));

        return remaining.IsEmpty
            ? new TagMember<TLogixType>(member, _root, this)
            : new TagMember(member, _root, this).Member<TLogixType>(remaining);
    }

    /// <summary>
    /// Gets all descendent tag members relative to the current tag member.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="TagMember"/> objects.</returns>
    /// <remarks>This recursively traverses the hierarchical data structure of the tag and returns all
    /// child/descendant members.</remarks>
    public IEnumerable<TagMember> Members()
    {
        var members = new List<TagMember>();

        foreach (var member in Value.Members)
        {
            var tagMember = new TagMember(member, _root, this);
            members.Add(tagMember);
            members.AddRange(tagMember.Members());
        }

        return members;
    }

    /// <summary>
    /// Gets all descendent tag members relative to the current tag member including the current tag member.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="TagMember"/> objects.</returns>
    /// <remarks>This recursively traverses the hierarchical data structure of the tag and returns all
    /// child/descendant members.</remarks>
    public IEnumerable<TagMember> MembersAndSelf()
    {
        var members = new List<TagMember> { this };

        foreach (var member in Value.Members)
        {
            var tagMember = new TagMember(member, _root, this);
            members.Add(tagMember);
            members.AddRange(tagMember.Members());
        }

        return members;
    }

    /// <summary>
    /// Gets all descendent tag members that satisfy the specified tag name predicate expression.
    /// </summary>
    /// <param name="predicate">A predicate expression specifying the tag name filter.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="TagMember"/> objects that satisfy the predicate.</returns>
    /// <remarks>This recursively traverses the hierarchical data structure of the tag and returns all
    /// child/descendant members that satisfy the specified predicate.</remarks>
    public IEnumerable<TagMember> Members(Predicate<TagName> predicate)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        var members = new List<TagMember>();

        foreach (var member in Value.Members)
        {
            var tagMember = new TagMember(member, _root, this);

            if (predicate.Invoke(tagMember.TagName))
                members.Add(tagMember);

            members.AddRange(tagMember.Members(predicate));
        }

        return members;
    }

    /// <summary>
    /// Gets all descendent tag members that satisfy the specified tag member predicate expression.
    /// </summary>
    /// <param name="predicate">A predicate expression specifying the tag member filter.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="TagMember"/> objects that satisfy the predicate.</returns>
    /// <remarks>This recursively traverses the hierarchical data structure of the tag and returns all
    /// child/descendant members that satisfy the specified predicate.</remarks>
    public IEnumerable<TagMember> Members(Predicate<TagMember> predicate)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        var members = new List<TagMember>();

        foreach (var element in Value.Members)
        {
            var tagMember = new TagMember(element, _root, this);

            if (predicate.Invoke(tagMember))
                members.Add(tagMember);

            members.AddRange(tagMember.Members(predicate));
        }

        return members;
    }

    /// <summary>
    /// Gets all descendent tag members of the tag member specified by the provided tag name path.
    /// </summary>
    /// <param name="tagName">A tag name path to the tag member for which to get members of.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="TagMember"/> objects.</returns>
    /// <remarks>This recursively traverses the hierarchical data structure of the tag and returns all
    /// child/descendant members.</remarks>
    public IEnumerable<TagMember> MembersOf(TagName tagName)
    {
        if (tagName is null)
            throw new ArgumentNullException(nameof(tagName));

        var memberName = tagName.Members.FirstOrDefault() ?? string.Empty;

        var member = Value.Members.FirstOrDefault(m =>
            string.Equals(m.Name, memberName, StringComparison.OrdinalIgnoreCase));

        if (member is null) return Enumerable.Empty<TagMember>();

        var tagMember = new TagMember(member, _root, this);

        var remaining = TagName.Combine(tagName.Members.Skip(1));

        return remaining.IsEmpty ? tagMember.Members() : tagMember.MembersOf(remaining);
    }

    /// <summary>
    /// Returns a collection of all descendent tag names of the current <c>TagMember</c>, including the tag name of the
    /// current <c>TagMember</c>. 
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> of <see cref="TagName"/> containing the current <c>TagMember</c> tag name and
    /// all child tag names.
    /// </returns>
    public IEnumerable<TagName> Names()
    {
        var names = new List<TagName> { TagName };
        names.AddRange(Names(TagName, Value));
        return names;
    }

    #region Internal

    private static IEnumerable<TagName> Names(TagName root, LogixType type)
    {
        var names = new List<TagName>();

        foreach (var member in type.Members)
        {
            var name = TagName.Combine(root, member.Name);
            names.Add(name);
            names.AddRange(Names(name, member.DataType));
        }

        return names;
    }

    private string GetComment()
    {
        if (_root.Name != L5XName.Tag)
            throw new InvalidOperationException();

        var comment = _root.Descendants(L5XName.Comment)
            .FirstOrDefault(e => e.Attribute(L5XName.Operand)?.Value == TagName.Operand);

        return comment is not null ? comment.Value : Parent is not null ? Parent.Description : string.Empty;
    }

    private void SetComment(string value)
    {
        if (_root.Name != L5XName.Tag)
            throw new InvalidOperationException();

        if (string.IsNullOrEmpty(value))
        {
            _root.Descendants(L5XName.Comment)
                .FirstOrDefault(e => e.Attribute(L5XName.Operand)?.Value == TagName.Operand)?.Remove();
            return;
        }

        var comments = _root.Element(L5XName.Comments);

        if (comments is null)
        {
            var container = new XElement(L5XName.Comments);
            container.Add(GenerateComment(value));
            _root.Add(container);
            return;
        }

        var comment = comments.Elements(L5XName.Comment)
            .FirstOrDefault(e => e.Attribute(L5XName.Operand)?.Value == TagName.Operand);

        if (comment is not null)
        {
            comment.Value = value;
            return;
        }

        comments.Add(GenerateComment(value));
    }

    private XElement GenerateComment(string value)
    {
        var comment = new XElement(L5XName.Comment);
        comment.Add(new XAttribute(L5XName.Operand, TagName.Operand));
        comment.Add(new XCData(value));
        return comment;
    }

    #endregion
}