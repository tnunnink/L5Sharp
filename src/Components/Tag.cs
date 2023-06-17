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
/// A logix <c>Tag</c> component. Contains the properties that comprise the L5X Tag element.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class Tag : LogixComponent<Tag>, ILogixScoped
{
    private readonly Member _member;

    /// <summary>
    /// base cons
    /// </summary>
    private Tag(XElement element, Member member, Tag? parent = null) : base(element)
    {
        _member = member;
        Parent = parent;
    }

    public Tag(Member member) : base(GetRoot(member))
    {
        
    }

    private static XElement GetRoot(Member member)
    {
        return member.Serialize().Ancestors(L5XName.Tag).FirstOrDefault();
    }

    /// <inheritdoc />
    public Tag(XElement element) : this(element, new Member(element))
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public Tag() : this(new XElement(L5XName.Tag), new Member(new XElement(L5XName.Tag)))
    {
        TagType = TagType.Base;
        ExternalAccess = ExternalAccess.ReadWrite;
        Constant = false;
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

    /// <summary>
    /// The description of the tag which is either the root tag description or the inherited (pass through)
    /// description of the descendent member.
    /// </summary>
    /// <value>A <see cref="string"/> containing the text description for the tag.</value>
    public string? Comment
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
    /// The value of the <c>Tag</c> data.
    /// </summary>
    /// <value>A <see cref="LogixType"/> representing the value of the <c>Tag</c>.</value>
    public LogixType Value
    {
        get => _member.DataType;
        set => _member.DataType = value;
    }

    /// <summary>
    /// The root tag of the current <see cref="Tag"/> member.
    /// </summary>
    /// <value>A <see cref="Tag"/> representing the root tag of the current tag member.</value>
    /// <remarks>This is here to assist in navigating back up the hierarchical data structure of the tag.</remarks>
    public Tag Root => new(Element);

    /// <summary>
    /// The parent tag or tag member of the current <see cref="Tag"/> member.
    /// </summary>
    /// <value>A <see cref="Tag"/> representing the immediate parent tag of the current tag member.</value>
    /// <remarks>This is here to assist in navigating back up the hierarchical data structure of the tag.</remarks>
    public Tag? Parent { get; }

    /// <inheritdoc />
    public Scope Scope => Scope.FromElement(Element);

    /// <inheritdoc />
    public string Container => Scope != Scope.Null ? Element.Ancestors(Scope.XName).First().LogixName() : string.Empty;

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
    public Tag this[ushort index]
    {
        get
        {
            var name = $"[{index}]";
            var member = Value.Members
                .FirstOrDefault(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));

            if (member is null)
                throw new ArgumentException(
                    $"No element with index '{name}' exists in the tag data structure for type {DataType}.");

            return new Tag(Element, member, this);
        }
    }

    /// <summary>
    /// Gets a descendent tag member with the provided tag name value.
    /// </summary>
    /// <param name="tagName">The tag name relative to the current tag member for which to retrieve.</param>
    /// <exception cref="ArgumentNullException"><c>tagName</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>tagName</c> does not represent a valid member for the tag member data structure.</exception>
    public Tag this[TagName tagName]
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

            var tag = new Tag(Element, member, this);

            var remaining = TagName.Combine(tagName.Members.Skip(1));

            return remaining.IsEmpty ? tag : tag[remaining];
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="member"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void Add(Member member)
    {
        if (Value is not ComplexType complexType)
            throw new InvalidOperationException();
        
        complexType.Add(member);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void Remove(string name)
    {
        if (Value is not ComplexType complexType)
            throw new InvalidOperationException();

        complexType.Remove(name);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tag"></param>
    public override void Replace(Tag tag)
    {
        if (Parent is null)
        {
            base.Replace(tag);
            return;
        }
        
        //todo not sure what to do here
    }

    /// <summary>
    /// Gets a descendent tag member relative to the current tag member.
    /// </summary>
    /// <param name="tagName">The full <see cref="Core.TagName"/> path of the member to get.</param>
    /// <returns>A <see cref="Tag"/> representing the child member instance.</returns>
    /// <remarks>
    /// Note that <c>tagName</c> can be a path to a member more than one layer down the hierarchical structure
    /// of the tag or tag member. However, it must start with a member of the current tag or tag member, and not the
    /// actual name of the current tag or tag member.
    /// </remarks>
    /// <example>
    /// <c>var member = tag.Member("Array[1].SubType.Member.0");</c>
    /// </example>
    public Tag? Member(TagName tagName)
    {
        if (tagName is null)
            throw new ArgumentNullException(nameof(tagName));

        var memberName = tagName.Members.FirstOrDefault() ?? string.Empty;

        var member = Value.Members
            .FirstOrDefault(m => string.Equals(m.Name, memberName, StringComparison.OrdinalIgnoreCase));

        if (member is null) return default;

        var tag = new Tag(Element, member, this);

        var remaining = TagName.Combine(tagName.Members.Skip(1));

        return remaining.IsEmpty ? tag : tag.Member(remaining);
    }

    /// <summary>
    /// Gets all descendent tag members relative to the current tag member.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Tag"/> objects.</returns>
    /// <remarks>This recursively traverses the hierarchical data structure of the tag and returns all
    /// child/descendant members.</remarks>
    public IEnumerable<Tag> Members()
    {
        var members = new List<Tag>();

        foreach (var member in Value.Members)
        {
            var tag = new Tag(Element, member, this);
            members.Add(tag);
            members.AddRange(tag.Members());
        }

        return members;
    }

    /// <summary>
    /// Gets all descendent tag members relative to the current tag member including the current tag member.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Tag"/> objects.</returns>
    /// <remarks>This recursively traverses the hierarchical data structure of the tag and returns all
    /// child/descendant members.</remarks>
    public IEnumerable<Tag> MembersAndSelf()
    {
        var members = new List<Tag> { this };

        foreach (var member in Value.Members)
        {
            var tagMember = new Tag(Element, member, this);
            members.Add(tagMember);
            members.AddRange(tagMember.Members());
        }

        return members;
    }

    /// <summary>
    /// Gets all descendent tag members that satisfy the specified tag name predicate expression.
    /// </summary>
    /// <param name="predicate">A predicate expression specifying the tag name filter.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Tag"/> objects that satisfy the predicate.</returns>
    /// <remarks>This recursively traverses the hierarchical data structure of the tag and returns all
    /// child/descendant members that satisfy the specified predicate.</remarks>
    public IEnumerable<Tag> Members(Predicate<TagName> predicate)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        var members = new List<Tag>();

        foreach (var member in Value.Members)
        {
            var tagMember = new Tag(Element, member, this);

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
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Tag"/> objects that satisfy the predicate.</returns>
    /// <remarks>This recursively traverses the hierarchical data structure of the tag and returns all
    /// child/descendant members that satisfy the specified predicate.</remarks>
    public IEnumerable<Tag> Members(Predicate<Tag> predicate)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        var members = new List<Tag>();

        foreach (var member in Value.Members)
        {
            var tag = new Tag(Element, member, this);

            if (predicate.Invoke(tag))
                members.Add(tag);

            members.AddRange(tag.Members(predicate));
        }

        return members;
    }

    /// <summary>
    /// Gets all descendent tag members of the tag member specified by the provided tag name path.
    /// </summary>
    /// <param name="tagName">A tag name path to the tag member for which to get members of.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Tag"/> objects.</returns>
    /// <remarks>This recursively traverses the hierarchical data structure of the tag and returns all
    /// child/descendant members.</remarks>
    public IEnumerable<Tag> MembersOf(TagName tagName)
    {
        if (tagName is null)
            throw new ArgumentNullException(nameof(tagName));

        var memberName = tagName.Members.FirstOrDefault() ?? string.Empty;

        var member = Value.Members.FirstOrDefault(m =>
            string.Equals(m.Name, memberName, StringComparison.OrdinalIgnoreCase));

        if (member is null) return Enumerable.Empty<Tag>();

        var tag = new Tag(Element, member, this);

        var remaining = TagName.Combine(tagName.Members.Skip(1));

        return remaining.IsEmpty ? tag.Members() : tag.MembersOf(remaining);
    }

    /// <summary>
    /// Returns a collection of all descendent tag names of the current <c>Tag</c>, including the tag name of the
    /// current <c>Tag</c>. 
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> of <see cref="TagName"/> containing the current <c>Tag</c> tag name and
    /// all child tag names.
    /// </returns>
    public IEnumerable<TagName> Names()
    {
        var names = new List<TagName> { TagName };
        names.AddRange(Names(TagName, Value));
        return names;
    }

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

    private string? GetComment()
    {
        var comment = Element.Descendants(L5XName.Comment)
            .FirstOrDefault(e => e.Attribute(L5XName.Operand)?.Value == TagName.Operand);

        return comment is not null ? comment.Value : Parent is not null ? Parent.Description : Description;
    }

    /// <summary>
    /// Handles 
    /// </summary>
    private void SetComment(string? value)
    {
        //If the parent is null forward to description (because it's the root tag)
        if (Parent is null)
        {
            Description = value;
            return;
        }

        //If the value is null or empty clear the comment
        if (string.IsNullOrEmpty(value))
        {
            Element.Descendants(L5XName.Comment)
                .FirstOrDefault(e => e.Attribute(L5XName.Operand)?.Value == TagName.Operand)?.Remove();
            return;
        }
        
        var comments = Element.Element(L5XName.Comments);

        //If no comments, initialize the container.
        if (comments is null)
        {
            comments = new XElement(L5XName.Comments);
            Element.Add(comments);
            return;
        }
        
        var comment = comments.Elements(L5XName.Comment)
            .FirstOrDefault(e => e.Attribute(L5XName.Operand)?.Value == TagName.Operand);

        //If it already exists, just update.
        if (comment is not null)
        {
            comment.Value = value;
            return;
        }

        //Otherwise add it
        comments.Add(GenerateComment(value));
    }

    private XElement GenerateComment(string value)
    {
        var comment = new XElement(L5XName.Comment);
        comment.Add(new XAttribute(L5XName.Operand, TagName.Operand));
        comment.Add(new XCData(value));
        return comment;
    }
}