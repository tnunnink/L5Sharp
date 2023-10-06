using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Predefined;
using L5Sharp.Utilities;

namespace L5Sharp.Components;

/// <summary>
/// A logix <c>Tag</c> component. Contains the properties that comprise the L5X Tag element.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.Tag)]
[L5XType(L5XName.LocalTag, false)]
[L5XType(L5XName.ConfigTag, false)]
[L5XType(L5XName.InputTag, false)]
[L5XType(L5XName.OutputTag, false)]
public class Tag : LogixComponent<Tag>
{
    private readonly LogixMember _member;

    /// <summary>
    /// Creates a new <see cref="Tag"/> with default values.
    /// </summary>
    public Tag()
    {
        _member = new LogixMember(Element);
        _member.DataChanged += OnDataChanged;

        Root = this;
        TagType = TagType.Base;
        ExternalAccess = ExternalAccess.ReadWrite;
        Constant = false;
    }

    /// <summary>
    /// Creates a new <see cref="Tag"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Tag(XElement element) : base(element)
    {
        _member = new LogixMember(Element);
        _member.DataChanged += OnDataChanged;

        Root = this;
    }

    /// <summary>
    /// Creates a new nested member <see cref="Tag"/> initialized with the root tag, underlying member,
    /// and optional parent tag.
    /// </summary>
    /// <param name="root">The root or base tag of this tag member.</param>
    /// <param name="member">The underlying member that this tag wraps.</param>
    /// <param name="parent">The parent tag of this tag member.</param>
    /// <remarks>
    /// This constructor is used internally for methods like <see cref="Member"/> to return new
    /// tag member objects.
    /// </remarks>
    private Tag(Tag root, LogixMember member, Tag parent) : base(root.Element)
    {
        _member = member ?? throw new ArgumentNullException(nameof(member));
        Root = root ?? throw new ArgumentNullException(nameof(root));
        Parent = parent ?? throw new ArgumentNullException(nameof(parent));
    }

    /// <inheritdoc />
    /// <remarks>
    /// <para>
    /// The name of a <c>Tag</c> component is unique in that not all elements that we consider <c>Tag</c> objects
    /// have the <c>Name</c> attribute (namely module tags like config, input, and output).
    /// For this reason, the <c>Tag</c> component contains special code that will detect and determine a module tag name.
    /// If the underlying element represents a normal tag element, this simply returns the <c>Name</c> attribute,
    /// similar to other components. Setting the <c>Name</c> property will always update the name attribute of the
    /// underlying element.
    /// </para>
    /// <para>Note that this property will always represent the name of the root tag component. This is true even for
    /// nested tag objects. To get the full tag name for any given tag object, use the <see cref="TagName"/> property.
    /// </para>
    /// </remarks>
    public override string Name
    {
        get => GetTagName();
        set => SetValue(value);
    }

    /// <summary>
    /// The description (either root, comment, or parent) for the tag or tag member.
    /// </summary>
    /// <value>A <see cref="string"/> containing the text description for the tag.</value>
    /// <remarks>
    /// <para>
    /// If this is the root tag, this will return the root/base description.
    /// If this is a nested tag member, this will look for a configured comment (as comments are stored in a different
    /// element in the L5X), and return the value if found. If the comment is not found for the tag member,
    /// this will return the parent description, which mimics the pass through feature of logix tag documentation.
    /// </para>
    /// <para>
    /// Setting this value for a nested tag member will update the underlying comments element for the tag.
    /// Setting this value for the root tag will simply update the root tag description element.
    /// </para>
    /// </remarks>
    public override string? Description
    {
        get => GetTagDescription();
        set => SetTagDescription(value);
    }

    /// <summary>
    /// The name of the data type the tag represents. 
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the tag data type.</value>
    /// <remarks>
    /// This property simply points to the name property of <see cref="Value"/>.
    /// This keeps the properties in sync. By initializing value, you are setting the data type name.
    /// Once initialized, the data type won't change. To change the tag's type, use <see cref="With"/>.
    /// </remarks>
    public string DataType => Value.Name;

    /// <summary>
    /// The dimensions of the tag, indicating the length and dimensions of it's array.
    /// </summary>
    /// <value>A <see cref="Common.Dimensions"/> value representing the array dimensions of the tag.</value>
    /// <remarks>
    /// This value will always point to the dimensions property of <see cref="Value"/>, assuming it is an
    /// <see cref="ArrayType"/>.
    /// If <c>Value</c> is not an array type, this property will always return <see cref="Common.Dimensions.Empty"/>.
    /// </remarks>
    public Dimensions Dimensions => Value is ArrayType array ? array.Dimensions : Dimensions.Empty;

    /// <summary>
    /// The radix format of <c>Value</c>. Only applies if the tag is an <see cref="AtomicType"/>.
    /// </summary>
    /// <value>A <see cref="Enums.Radix"/> option representing data format of the tag value.</value>
    /// <remarks>
    /// This value will always point to the radix of <see cref="Value"/>, assuming it is an <see cref="AtomicType"/>.
    /// If <c>Value</c> is not an atomic type, this property will always return <see cref="Enums.Radix.Null"/>.
    /// </remarks>
    public Radix Radix => Value is AtomicType atomic ? atomic.Radix : Radix.Null;

    /// <summary>
    /// The value or data of the <see cref="Tag"/>.
    /// </summary>
    /// <value>A <see cref="LogixType"/> containing the tag data.</value>
    /// <remarks>
    /// <para>
    /// The <see cref="LogixType"/> is the basis for all tag data types. This property may represent the atomic
    /// value (bool, integer, float), string, complex structure, or array. <c>LogixType</c> has built in implicit operators
    /// to convert .NET types to <c>LogixType</c> objects so to make setting <c>Value</c> more concise.
    /// </para>
    /// <para>
    /// Since the type can not be known at compile time when deserializing, we treat it as the abstract base class.
    /// However, the <see cref="LogixSerializer"/> will attempt to create concrete instances of types that are available,
    /// allowing the user to cast <c>Value</c> down to more derived types.
    /// </para>
    /// </remarks>
    public LogixType Value
    {
        get => _member.DataType;
        set => _member.DataType = value;
    }

    /// <summary>
    /// The external access option indicating the read/write access of the tag.
    /// </summary>
    /// <value>A <see cref="Enums.ExternalAccess"/> option representing read/write access of the tag.</value>
    public ExternalAccess? ExternalAccess
    {
        get => GetValue<ExternalAccess>();
        set => SetValue(value);
    }

    /// <summary>
    /// A type indicating whether the current tag component is a base tag, or alias for another tag instance.
    /// </summary>
    /// <value>A <see cref="Enums.TagType"/> option representing the type of tag component.</value>
    public TagType? TagType
    {
        get => GetValue<TagType>();
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
    /// <value>A <see cref="Common.TagName"/> string representing the full tag name of the alias tag.</value>
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
    public bool? Constant
    {
        get => GetValue<bool?>();
        set => SetValue(value);
    }

    /// <summary>
    /// The configured unit value of the tag.
    /// </summary>
    /// <value>A <see cref="string"/> representing the defined units of the tag.</value>
    /// <remarks>This appears only used for module defined tags.</remarks>
    public string? Unit
    {
        get => GetUnit();
        set => SetUnit(value);
    }

    /// <summary>
    /// The parent tag of this <see cref="Tag"/> member.
    /// </summary>
    /// <value>
    /// A <see cref="Tag"/> representing the immediate parent tag of the this tag member. Will be <c>null</c>
    /// for all root tag objects.
    /// </value>
    /// <remarks>
    /// This property helps model the hierarchical structure of a tag object. Tags has a <see cref="Value"/>
    /// which can represent a nested complex data type. This class models this by keeping references to the <c>Root</c>
    /// and <c>Parent</c> tags for each tag object. Only nested tag members should have a <c>Parent</c>.
    /// </remarks>
    /// <seealso cref="Root"/>
    public Tag? Parent { get; }

    /// <summary>
    /// The root tag of this <see cref="Tag"/> member.
    /// </summary>
    /// <value>A <see cref="Tag"/> representing the root tag.</value>
    /// <remarks>
    /// This property helps model the hierarchical structure of a tag object. Tags has a <see cref="Value"/>
    /// which can represent a nested complex data type. This class models this by keeping references to the <c>Root</c>
    /// and <c>Parent</c> tags for each tag object. All tags should have a <c>Root</c>.
    /// </remarks>
    /// <seealso cref="Parent"/>
    public Tag Root { get; }

    /// <summary>
    /// The full tag name path of the <see cref="Tag"/>.
    /// </summary>
    /// <value>A <see cref="Common.TagName"/> containing the full dot-down path of the tag member name.</value>
    /// <remarks>
    /// <para>
    /// This property will always represent the fully qualified tag name path, which includes nested tag
    /// member object. This property is determined using the hierarchical structure of the tag component.
    /// </para>
    /// </remarks>
    public TagName TagName => Parent is not null ? TagName.Concat(Parent.TagName, _member.Name) : new TagName(Name);

    /// <summary>
    /// The scope type of the tag component, indicating whether the tag is a global controller tag, a local program tag.
    /// or neither.
    /// </summary>
    /// <value>A <see cref="Enums.Scope"/> option indicating the container type for the tag.</value>
    /// <remarks>
    /// <para>
    /// The scope of a tag component is determined from the ancestors of the underlying element. If the first parent is
    /// program element, the tag is program scoped. If the first element is a controller element, the tag is controller
    /// scoped. If ancestors of these types are not found, we assume the tag has no/null scope.
    /// </para>
    /// <para>
    /// This property is not inherent in the L5X of the tag component, but one that adds a lot of value as it
    /// helps identify tags within the L5X file. This is why it is part of the core component model.
    /// </para>
    /// </remarks>
    public Scope Scope => Scope.FromElement(Element);

    /// <summary>
    /// Gets the tag member having the provided tag name value. The tag name can represent either an immediate member
    /// or a nested member in the tag hierarchy.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> relative to the current tag member for which to retrieve.</param>
    /// <exception cref="ArgumentNullException"><c>tagName</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>tagName</c> does not represent a valid member for the tag member data structure.</exception>
    /// <returns>A child <see cref="Tag"/> component represented by the provided tag name value.</returns>
    /// <remarks>
    /// Note that <c>tagName</c> can be a path to a member more than one layer down the hierarchical structure
    /// of the tag. However, it must start with a member of the current tag, and not the
    /// actual name of this tag object. The difference between this indexer property and
    /// <see cref="Member"/> is that this will throw and exception if a member with <c>tagName</c> is not found
    /// (i.e. returns non-nullable reference type).
    /// </remarks>
    public Tag this[TagName tagName]
    {
        get
        {
            if (tagName is null) throw new ArgumentNullException(nameof(tagName));
            if (tagName.IsEmpty) throw new ArgumentException("Can not retrieve member from empty tag name.");

            var member = Value.Member(tagName.Root);
            if (member is null)
                throw new ArgumentException(
                    $"No member with name '{tagName.Root}' exists in the tag data structure for type {DataType}.");

            var tag = new Tag(Root, member, this);
            var remaining = TagName.Combine(tagName.Members.Skip(1));
            return remaining.IsEmpty ? tag : tag[remaining];
        }
    }

    /// <summary>
    /// Gets a descendent tag member relative to the current tag member.
    /// </summary>
    /// <param name="tagName">The full <see cref="Common.TagName"/> path of the member to get.</param>
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
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));
        if (tagName.IsEmpty) throw new ArgumentException("Can not retrieve member from empty tag name.");

        var member = Value.Member(tagName.Root);
        if (member is null) return default;

        var tag = new Tag(Root, member, this);
        var remaining = TagName.Combine(tagName.Members.Skip(1));
        return remaining.IsEmpty ? tag : tag.Member(remaining);
    }

    /// <summary>
    /// Gets this and all descendent tag members of the tag data structure.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Tag"/> objects.</returns>
    /// <remarks>
    /// This recursively traverses the hierarchical data structure of tag's <see cref="Value"/> and returns all
    /// descendant tags, as well as this tag.
    /// </remarks>
    public IEnumerable<Tag> Members()
    {
        var members = Parent is null ? new List<Tag> {this} : new List<Tag>();

        foreach (var member in Value.Members)
        {
            var tagMember = new Tag(Root, member, this);
            members.Add(tagMember);
            members.AddRange(tagMember.Members());
        }

        return members;
    }

    /// <summary>
    /// Gets this and all descendent tag members of the tag data structure that satisfy the specified tag name predicate.
    /// </summary>
    /// <param name="predicate">A predicate expression specifying the tag name filter.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Tag"/> objects that satisfy the predicate.</returns>
    /// <remarks>
    /// This recursively traverses the hierarchical data structure of tag's <see cref="Value"/> and returns all
    /// tags that satisfy the specified predicate.
    /// </remarks>
    public IEnumerable<Tag> Members(Predicate<TagName> predicate)
    {
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));

        var members = Parent is null && predicate.Invoke(TagName) ? new List<Tag> {this} : new List<Tag>();

        foreach (var member in Value.Members)
        {
            var tag = new Tag(Root, member, this);

            if (predicate.Invoke(tag.TagName))
                members.Add(tag);

            members.AddRange(tag.Members(predicate));
        }

        return members;
    }

    /// <summary>
    /// Gets this and all descendent tag members of the tag data structure that that satisfy the specified tag predicate.
    /// </summary>
    /// <param name="predicate">A predicate expression specifying the tag filter.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Tag"/> objects that satisfy the predicate.</returns>
    /// <remarks>
    /// This recursively traverses the hierarchical data structure of tag's <see cref="Value"/> and returns all
    /// tags that satisfy the specified predicate.
    /// </remarks>
    public IEnumerable<Tag> Members(Predicate<Tag> predicate)
    {
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));

        var members = Parent is null && predicate.Invoke(this) ? new List<Tag> {this} : new List<Tag>();

        foreach (var member in Value.Members)
        {
            var tag = new Tag(Root, member, this);

            if (predicate.Invoke(tag))
                members.Add(tag);

            members.AddRange(tag.Members(predicate));
        }

        return members;
    }

    /// <summary>
    /// Gets all descendent tags of the tag specified by the provided tag name.
    /// </summary>
    /// <param name="tagName">A tag name path to the tag member for which to get members of.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Tag"/> objects.</returns>
    /// <remarks>This recursively traverses the hierarchical data structure of the tag and returns all
    /// child/descendant members.</remarks>
    public IEnumerable<Tag> MembersOf(TagName tagName)
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));
        if (tagName.IsEmpty) throw new ArgumentException("Can not retrieve members from empty tag name.");

        var member = Value.Member(tagName.Root);
        if (member is null) return Enumerable.Empty<Tag>();

        var tag = new Tag(Root, member, this);
        var remaining = TagName.Combine(tagName.Members.Skip(1));
        return remaining.IsEmpty ? tag.Members() : tag.MembersOf(remaining);
    }

    /// <summary>
    /// Creates a new <see cref="Components.Tag"/> with the provided name and specified type parameter.
    /// </summary>
    /// <param name="name">The name of the tag.</param>
    /// <typeparam name="TLogixType">The logix data type of the tag. Type must have parameterless constructor to create.</typeparam>
    /// <returns>A new <see cref="Tag"/> object with specified parameters.</returns>
    public static Tag New<TLogixType>(string name) where TLogixType : LogixType, new() =>
        new() {Name = name, Value = new TLogixType()};

    /// <inheritdoc />
    public override string ToString() => TagName;

    #region Extensions

    /// <summary>
    /// Gets the name of the scoped container for the current logix element.
    /// </summary>
    /// <value> A <see cref="string"/> containing the name of the containing program or controller for which this tag
    /// belongs. If this tag has not container (i.e. <c>Null</c> scope), then returns <c>null</c>.</value>
    /// <remarks>
    /// This value is retrieved from the ancestors of the underlying element. If not ancestors exists, meaning this
    /// tag has no scope, then this property will be <c>null</c>.
    /// </remarks>
    /// <seealso cref="ScopeName"/>
    public string? ContainerName => Element.Ancestors(Scope.Name).FirstOrDefault()?.LogixName();

    /// <summary>
    /// Returns a collection of all descendent tag names of the current <c>Tag</c>, including the tag name of the
    /// this <c>Tag</c>. 
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> of <see cref="TagName"/> containing the this tag name and all child tag names.
    /// </returns>
    public IEnumerable<TagName> TagNames() => Members().Select(t => t.TagName);

    /// <summary>
    /// Returns the <c>TagName</c> and the tag's program name prepended with the format <i>Program:{ProgramName}.{TagName}</i>
    /// if the tag is a program tag. If the tag is a controller tag, then returns the tag's <c>TagName</c>. This forms a
    /// globally unique tag identifier for the given tag object.
    /// </summary>
    /// <value>
    /// If this tag is a controller tag, then <see cref="TagName"/>. If this tag is a program tag,
    /// then <see cref="ContainerName"/> and <see cref="Common.TagName"/> in the format <i>Program:{ProgramName}.{TagName}</i>.
    /// </value>
    /// <remarks>
    /// This value is determined based on the <see cref="Scope"/> of the tag. This name provides a consistent
    /// global unique identified for a given tag object.
    /// </remarks>
    /// <seealso cref="ContainerName"/>
    public TagName ScopeName => Scope == Scope.Program ? $"Program:{ContainerName}.{TagName}" : TagName;
    
    /// <summary>
    /// Adds a new member to the tag's complex data structure.
    /// </summary>
    /// <param name="name">The name of the member to add to the tag's data structure.</param>
    /// <param name="value">The <see cref="LogixType"/> of the member to add to the tag's data structure.</param>
    /// <exception cref="InvalidOperationException">The current tag does not contain a mutable complex logix type.</exception>
    /// <remarks>
    /// This will operate relative to the current tag member object, and is simply a call to the underlying
    /// <see cref="ComplexType"/> <c>Add</c> method. Therefore this is simply a helper to make mutating tag structures
    /// more concise.
    /// </remarks>
    public void Add(string name, LogixType value)
    {
        var member = new LogixMember(name, value);
        if (Value is not ComplexType complexType)
            throw new InvalidOperationException("Can only mutate ComplexType tags.");
        complexType.Add(member);
    }

    /// <summary>
    /// Removes a member with the specified name from the tag's complex data structure.
    /// </summary>
    /// <param name="name">The name of the member to remove.</param>
    /// <exception cref="InvalidOperationException">The current tag does not contain a mutable complex logix type.</exception>
    /// <remarks>
    /// This will operate relative to the current tag member object, and is simply a call to the underlying
    /// <see cref="ComplexType"/> <c>Remove</c> method. Therefore this is simply a helper to make mutating tag structures
    /// more concise.
    /// </remarks>
    public void Remove(string name)
    {
        if (Value is not ComplexType complexType)
            throw new InvalidOperationException("Can only mutate ComplexType tags.");
        complexType.Remove(name);
    }
    
    /// <summary>
    /// Returns as new <see cref="Tag"/> with the updated data type value provided. 
    /// </summary>
    /// <param name="value">The <see cref="LogixType"/> value to change to.</param>
    /// <returns>
    /// A <see cref="Tag"/> with the same underlying <see cref="XElement"/> and corresponding properties with
    /// <see cref="Value"/> changed to the provided <see cref="LogixType"/>.
    /// </returns>
    /// <exception cref="InvalidOperationException">When this tag is a nested tag member and it's parent tag's
    /// <see cref="Value"/> property is not a <see cref="ComplexType"/> object.</exception>
    /// <remarks>
    /// <para>
    /// This is meant to be a concise way to change the data type of tag while leaving all else the same, since setting
    /// <see cref="Value"/> should only ever update the value and not change the data type.
    /// </para>
    /// <para>
    /// If this is called for the <c>Root</c> tag object, then the entire data element is replaced and a new instance
    /// is returned. The Tag will still be attached as we are mutating the underlying element object in place.
    /// If this is called for a nested tag member, then this method checks if the parent tag is a complex type, and if so,
    /// calls the underlying Replace method for the current member name. Therefore, calls to this method for nested tags
    /// will fail if <see cref="Value"/> for the parent tag is not a complex type object.
    /// </para>
    /// </remarks>
    public Tag With(LogixType value)
    {
        if (Parent is null)
        {
            SetData(value);
            return new Tag(Element);
        }

        if (Parent.Value is not ComplexType complexType)
            throw new InvalidOperationException(
                $"Can not mutate tag data for parent type {Parent.DataType} as it is not a complex type instance.");

        complexType.Replace(TagName.Member, value);
        return Root[TagName.Path];
    }

    #endregion

    #region Internal

    /// <summary>
    /// Triggers <see cref="SetData"/> when a nested data type value of this tag's <see cref="Value"/> changes.
    /// </summary>
    private void OnDataChanged(object sender, EventArgs e) => SetData(Root.Value);

    /// <summary>
    /// Handles setting the data of the root tag and updating the root properties
    /// </summary>
    private void SetData(LogixType value)
    {
        var data = Element.Elements().FirstOrDefault(e =>
            DataFormat.Supported.Any(f => f == e.Attribute(L5XName.Format)?.Value));

        if (data is null)
            Element.Add(GenerateData(value));
        else
            data.ReplaceWith(GenerateData(value));

        SetTagAttributes(value);
    }

    /// <summary>
    /// Handles setting the <see cref="DataType"/>, <see cref="Radix"/>, and <see cref="Dimensions"/> of the underlying
    /// element for the tag.
    /// </summary>
    private void SetTagAttributes(LogixType value)
    {
        Element.SetAttributeValue(L5XName.DataType, value.Name);

        var radix = value is AtomicType atomicType ? atomicType.Radix
            : value is ArrayType arrayType && arrayType.Radix != Radix.Null ? arrayType.Radix
            : null;
        Element.SetAttributeValue(L5XName.Radix, radix);

        var dimensions = value is ArrayType array ? array.Dimensions : null;
        Element.SetAttributeValue(L5XName.Dimensions, dimensions);
    }

    /// <summary>
    /// Generates the root data element for a tag component provided a logix type.
    /// </summary>
    private static XElement GenerateData(LogixType type)
    {
        return type switch
        {
            StringType stringType => stringType.Serialize(),
            ALARM_ANALOG alarmAnalog => GenerateFormatted(alarmAnalog, DataFormat.Alarm),
            ALARM_DIGITAL alarmDigital => GenerateFormatted(alarmDigital, DataFormat.Alarm),
            MESSAGE message => GenerateFormatted(message, DataFormat.Message),
            ILogixSerializable serializable => GenerateFormatted(serializable, DataFormat.Decorated)
        };
    }

    /// <summary>
    /// Generates data element with provided format value and serializable type.
    /// </summary>
    private static XElement GenerateFormatted(ILogixSerializable type, DataFormat format)
    {
        var data = new XElement(L5XName.Data, new XAttribute(L5XName.Format, format));
        data.Add(type.Serialize());
        return data;
    }

    /// <summary>
    /// Handles determining the tag name of the current object from the underlying XElement. This handles module
    /// tag elements (ConfigTag, InputTag, OutputTag) as well as normal component elements (Tag, LocalTag).
    /// </summary>
    private string GetTagName()
    {
        var xName = Element.Name;

        if (xName == L5XName.ConfigTag || xName == L5XName.InputTag || xName == L5XName.OutputTag)
            return ModuleTagName(Element);

        return Element.Attribute(L5XName.Name)?.Value ?? string.Empty;
    }

    /// <summary>
    /// A helper for determining a module tag name for an input, output, or config tag element. This involves getting
    /// the tag suffix, module name, parent module name, and configured slot number.
    /// </summary>
    private static string ModuleTagName(XElement element)
    {
        var suffix = DetermineModuleSuffix(element);

        var moduleName = element.Ancestors(L5XName.Module)
            .FirstOrDefault()?.Attribute(L5XName.Name)?.Value;

        var parentName = element.Ancestors(L5XName.Module)
            .FirstOrDefault()?.Attribute(L5XName.ParentModule)?.Value;

        var slot = element
            .Ancestors(L5XName.Module)
            .Descendants(L5XName.Port)
            .Where(p => bool.TryParse(p.Attribute(L5XName.Upstream)?.Value!, out _)
                        && p.Attribute(L5XName.Type)?.Value != "Ethernet"
                        && int.TryParse(p.Attribute(L5XName.Address)?.Value, out _))
            .Select(p => p.Attribute(L5XName.Address)?.Value)
            .FirstOrDefault();

        return slot is not null ? $"{parentName}:{slot}:{suffix}" : $"{moduleName}:{suffix}";
    }

    private static string DetermineModuleSuffix(XElement element)
    {
        if (element.Name == L5XName.ConfigTag) return "C";

        if (element.Name == L5XName.InputTag)
            return element.Parent?.Attribute(L5XName.InputTagSuffix)?.Value ?? "I";

        if (element.Name == L5XName.OutputTag)
            return element.Parent?.Attribute(L5XName.OutputTagSuffix)?.Value ?? "O";

        throw new ArgumentException($"Module tag element name {element.Name} not valid.");
    }

    /// <summary>
    /// Handles getting a comment value for the current tag. 
    /// </summary>
    private string? GetTagDescription()
    {
        if (Parent is null) return Element.Element(L5XName.Description)?.Value;

        var comment = Element.Descendants(L5XName.Comment)
            .FirstOrDefault(e => string.Equals(e.Attribute(L5XName.Operand)?.Value, TagName.Operand,
                StringComparison.OrdinalIgnoreCase));

        //logix descriptions propagates to their children when not overriden. This mimics that.
        return comment is not null ? comment.Value : Parent.Description;
    }

    /// <summary>
    /// Handles setting a comment element of the root tag structure for the current tag name operand. 
    /// </summary>
    private void SetTagDescription(string? value)
    {
        //If the parent is null forward set to base description implementation which is essentially
        //setting the description element of the root tag component.
        if (Parent is null)
        {
            base.Description = value;
            return;
        }

        //Child descriptions are set in the comments element of a tag.
        if (string.IsNullOrEmpty(value))
        {
            Element.Descendants(L5XName.Comment)
                .FirstOrDefault(e => string.Equals(e.Attribute(L5XName.Operand)?.Value, TagName.Operand,
                    StringComparison.OrdinalIgnoreCase))?.Remove();
            return;
        }

        var comments = Element.Element(L5XName.Comments);
        if (comments is null)
        {
            comments = new XElement(L5XName.Comments);

            //This is to place comments right after description if it exists, otherwise as the first element.
            if (Element.FirstNode is XElement element && element.Name == L5XName.Description)
                Element.FirstNode.AddAfterSelf(comments);
            else
                Element.AddFirst(comments);
        }

        var comment = comments.Elements(L5XName.Comment)
            .FirstOrDefault(e => string.Equals(e.Attribute(L5XName.Operand)?.Value, TagName.Operand,
                StringComparison.OrdinalIgnoreCase));

        if (comment is not null)
        {
            comment.Value = value;
            return;
        }

        comments.Add(GenerateDescriptor(value, L5XName.Comment));
    }

    /// <summary>
    /// Handles getting a unit value for the current tag name operand. 
    /// </summary>
    private string? GetUnit()
    {
        return Element.Descendants(L5XName.EngineeringUnit)
            .FirstOrDefault(e => string.Equals(e.Attribute(L5XName.Operand)?.Value, TagName.Operand,
                StringComparison.OrdinalIgnoreCase))?.Value;
    }

    /// <summary>
    /// Handles setting a unit element of the root tag structure for the current tag name operand. 
    /// </summary>
    private void SetUnit(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Element.Descendants(L5XName.EngineeringUnit)
                .FirstOrDefault(e => string.Equals(e.Attribute(L5XName.Operand)?.Value, TagName.Operand,
                    StringComparison.OrdinalIgnoreCase))?.Remove();
            return;
        }

        var units = Element.Element(L5XName.EngineeringUnits);
        if (units is null)
        {
            units = new XElement(L5XName.EngineeringUnits);
            Element.Add(units);
        }

        var unit = units.Elements(L5XName.EngineeringUnit)
            .FirstOrDefault(e => string.Equals(e.Attribute(L5XName.Operand)?.Value, TagName.Operand,
                StringComparison.OrdinalIgnoreCase));

        if (unit is not null)
        {
            unit.Value = value;
            return;
        }

        units.Add(GenerateDescriptor(value, L5XName.EngineeringUnit));
    }

    /// <summary>
    /// Generates a new comment/unit descriptor element with the provided value and name.
    /// </summary>
    private XElement GenerateDescriptor(string value, string name)
    {
        var element = new XElement(name);
        element.Add(new XAttribute(L5XName.Operand, TagName.Operand.ToUpper()));
        element.Add(new XCData(value));
        return element;
    }

    #endregion
}