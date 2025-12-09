using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace L5Sharp.Core;

/// <summary>
/// A logix <c>Tag</c> component. Contains the properties that comprise the L5X Tag element.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[LogixElement(L5XName.Tag)]
[LogixElement(L5XName.ConfigTag)]
[LogixElement(L5XName.InputTag)]
[LogixElement(L5XName.OutputTag)]
[LogixElement(L5XName.InAliasTag)]
[LogixElement(L5XName.OutAliasTag)]
public class Tag : LogixComponent<Tag>
{
    /// <inheritdoc />
    protected override List<string> ElementOrder =>
    [
        L5XName.ConsumeInfo,
        L5XName.ProduceInfo,
        L5XName.Description,
        L5XName.Comments,
        L5XName.EngineeringUnits,
        L5XName.Mins,
        L5XName.Maxes,
        L5XName.State0s,
        L5XName.State1s,
        L5XName.Data,
        L5XName.ForceData
    ];

    /// <summary>
    /// Creates a new <see cref="Tag"/> with default values.
    /// </summary>
    public Tag() : base(L5XName.Tag)
    {
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
    }

    /// <summary>
    /// Creates a new <see cref="Tag"/> initialized with the provided name and value.
    /// </summary>
    /// <param name="name">The name of the tag.</param>
    /// <param name="value">The <see cref="LogixData"/> value of the tag.</param>
    /// <param name="description">The optional description of the tag.</param>
    public Tag(string name, LogixData value, string? description = null) : this()
    {
        Element.SetAttributeValue(L5XName.Name, name);
        Value = value;
        SetProperty(description, nameof(Description));
    }

    /// <summary>
    /// Creates a new <see cref="Tag"/> initialized with the provided name, data type, and optional description.
    /// </summary>
    /// <param name="name">The name of the tag.</param>
    /// <param name="dataType">The name of the data type of the tag.</param>
    /// <param name="description">The optional description of the tag.</param>
    /// <remarks>
    /// This constructor will use the <see cref="LogixType.CreateOrDefault(string)"/> factory method to instantiate
    /// the <see cref="Value"/> data for the tag. If <paramref name="dataType"/> represents a complex type that is
    /// not registered, it will default to creating a <see cref="StructureData"/> instance having the provided name.
    /// </remarks>
    public Tag(string name, string dataType, string? description = null) : this()
    {
        Element.SetAttributeValue(L5XName.Name, name);
        Value = LogixType.CreateOrDefault(dataType);
        SetProperty(description, nameof(Description));
    }

    /// <summary>
    /// Creates a new <see cref="Tag"/> initialized with default value and having an element with the provided name.
    /// </summary>
    /// <param name="element">the name of the tag element.</param>
    protected Tag(string element) : base(element)
    {
        TagType = TagType.Base;
        ExternalAccess = ExternalAccess.ReadWrite;
        Constant = false;
    }

    /// <summary>
    /// Creates a new nested member <see cref="Tag"/> initialized with the provided <see cref="LogixMember"/> element
    /// and parent tag.
    /// </summary>
    /// <param name="member">The underlying member that this tag wraps.</param>
    /// <param name="parent">The parent tag of this tag member.</param>
    /// <remarks>
    /// This constructor is used internally for methods like <see cref="Member"/> to return new
    /// wrapped members as Tag objects.
    /// </remarks>
    private Tag(LogixMember member, Tag parent) : base(member.Serialize())
    {
        Parent = parent;
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
    /// </remarks>
    public override string Name
    {
        get => Element.IsModuleTagElement() ? Element.ModuleTagName() : Element.MemberName();
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
    /// element in the L5X) and return the value if found. If the comment is not found for the tag member,
    /// this will return the parent description, which mimics the pass-through feature of logix tag documentation.
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

    /// <inheritdoc />
    /// <remarks>
    /// Tag overrides the default implementation so that each nested tag member will include the
    /// full dot down path to it's instance/reference (not just the root tag name).
    /// </remarks>
    public override Reference Reference => Parent is null ? base.Reference : Base.Reference.ToTag(TagName);

    /// <summary>
    /// The full tag name path of the <see cref="Tag"/>.
    /// </summary>
    /// <value>A <see cref="Core.TagName"/> containing the full dot-down path of the tag member name.</value>
    /// <remarks>
    /// <para>
    /// This property will always represent the fully qualified tag name path, which includes a nested tag
    /// member object. This property is determined by the hierarchical structure of the tag component.
    /// </para>
    /// </remarks>
    public TagName TagName => Parent is null ? new TagName(Name) : TagName.Concat(Parent.TagName, Element.MemberName());

    /// <summary>
    /// The name of the data type the tag represents. 
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the tag data type.</value>
    /// <remarks>
    /// This property simply points to the name property of <see cref="Value"/>.
    /// This keeps the properties in sync. By initializing value, you are setting the data type name.
    /// </remarks>
    public string DataType => Value.Name;

    /// <summary>
    /// The dimensions of the tag, indicating the length and dimensions of its array.
    /// </summary>
    /// <value>A <see cref="Core.Dimensions"/> value representing the array dimensions of the tag.</value>
    /// <remarks>
    /// This value will always point to the <see cref="Dimensions"/> property of <see cref="Value"/>.
    /// If <c>Value</c> is not an array type, this property will always
    /// return <see cref="L5Sharp.Core.Dimensions.Empty"/>.
    /// </remarks>
    public Dimensions Dimensions => Value is ArrayData array ? array.Dimensions : Dimensions.Empty;

    /// <summary>
    /// The radix format of <c>Value</c>. Only applies if the tag is an <see cref="AtomicData"/>.
    /// </summary>
    /// <value>A <see cref="Core.Radix"/> option representing data format of the tag value.</value>
    /// <remarks>
    /// This value will always point to the radix of <see cref="Value"/>.
    /// If <c>Value</c> is not an atomic type or array type, this property will always
    /// return <see cref="L5Sharp.Core.Radix.Null"/>.
    /// </remarks>
    public Radix Radix => Value is AtomicData atomic ? atomic.Radix : Radix.Null;

    /// <summary>
    /// The value or data of the <see cref="Tag"/>.
    /// </summary>
    /// <value>A <see cref="LogixData"/> containing the tag data.</value>
    /// <remarks>
    /// <para>
    /// The <see cref="LogixData"/> is the basis for all tag data types. This property may represent the atomic
    /// value (bool, integer, float), string, complex structure, or array. <c>LogixData</c> has built in implicit operators
    /// to convert .NET types to <c>LogixData</c> objects so to make setting <c>Value</c> more concise.
    /// </para>
    /// <para>
    /// Since the type cannot be known at compile time when deserializing, we treat it as the abstract base class.
    /// However, the <see cref="LogixSerializer"/> will attempt to create concrete instances of types that are available,
    /// allowing the user to cast <c>Value</c> down to more derived types.
    /// </para>
    /// </remarks>
    public LogixData Value
    {
        get => GetData();
        set => SetData(value);
    }

    /// <summary>
    /// The external access option indicating the read/write access of the tag.
    /// </summary>
    /// <value>A <see cref="Core.ExternalAccess"/> option representing read/write access of the tag.</value>
    public ExternalAccess? ExternalAccess
    {
        get => Base.GetValue(ExternalAccess.Parse);
        set => Base.SetValue(value);
    }

    /// <summary>
    /// The external access option indicating the read/write access of the tag from OPC UA.
    /// </summary>
    /// <value>A <see cref="Core.OpcUAAccess"/> option representing read/write access of the tag from OPC UA.</value>
    // ReSharper disable once InconsistentNaming we need the name to match.
    public OpcUAAccess? OpcUAAccess
    {
        get => Base.GetValue(OpcUAAccess.Parse);
        set => Base.SetValue(value);
    }

    /// <summary>
    /// The <see cref="ComponentClass"/> value indicating whether this component is a standard or safety type component.
    /// </summary>
    /// <value>A <see cref="Core.ComponentClass"/> option representing class of the component.</value>
    /// <remarks>Specify the class of the tag. This attribute applies only to safety controller projects.</remarks>
    public ComponentClass? Class
    {
        get => Base.GetValue(ComponentClass.Parse);
        set => Base.SetValue(value);
    }

    /// <summary>
    /// A type indicating whether the current tag component is a base tag or alias for another tag instance.
    /// </summary>
    /// <value>A <see cref="Core.TagType"/> option representing the type of tag component.</value>
    public TagType? TagType
    {
        get => Base.GetValue(TagType.Parse);
        set => Base.SetValue(value);
    }

    /// <summary>
    /// The usage option indicating the scope in which the tag is visible or usable from.
    /// </summary>
    /// <value>A <see cref="TagUsage"/> option representing the tag scope.</value>
    public TagUsage? Usage
    {
        get => Base.GetValue(TagUsage.Parse);
        set => Base.SetValue(value);
    }

    /// <summary>
    /// The tag name of the tag that is the alias of the current tag object.
    /// </summary>
    /// <value>A <see cref="Core.TagName"/> string representing the full tag name of the alias tag.</value>
    public TagName? AliasFor
    {
        get => Base.GetValue(TagName.Parse);
        set => Base.SetValue(value);
    }

    /// <summary>
    /// Indicates whether the tag is a constant.
    /// </summary>
    /// <value><c>true</c> if the tag is constant; otherwise, <c>false</c>.</value>
    /// <remarks>Only value type tags can be set as a constant. Default is <c>false</c>.</remarks>
    public bool? Constant
    {
        get => Base.GetOptionalBool();
        set => Base.SetValue(value);
    }

    /// <summary>
    /// The <see cref="Core.ProduceInfo"/> defining the configuration for a produced tag.
    /// </summary>
    /// <value>An instance of the <see cref="Core.ProduceInfo"/> sub element containing produced tag configuration.</value>
    /// <remarks>
    /// If a tag contains configuration for <see cref="ProduceInfo"/> make sure to set the <see cref="TagType"/>
    /// property accordingly.
    /// </remarks>
    public ProduceInfo? ProduceInfo
    {
        get => Base.GetComplex<ProduceInfo>();
        set => Base.SetComplex(value);
    }

    /// <summary>
    /// The <see cref="Core.ConsumeInfo"/> defining the configuration for a consumed tag.
    /// </summary>
    /// <value>An instance of the <see cref="Core.ConsumeInfo"/> sub element containing consumed tag configuration.</value>
    /// <remarks>
    /// If a tag contains configuration for <see cref="ConsumeInfo"/> make sure to set the <see cref="TagType"/>
    /// property accordingly.
    /// </remarks>
    public ConsumeInfo? ConsumeInfo
    {
        get => Base.GetComplex<ConsumeInfo>();
        set => Base.SetComplex(value);
    }

    /// <summary>
    /// The configured unit value of the tag.
    /// </summary>
    /// <value>A <see cref="string"/> representing the defined units of the tag.</value>
    /// <remarks>This appears only used for module-defined tags.</remarks>
    public string? Unit
    {
        get => Base.GetUnit();
        set => Base.SetUnit(value);
    }

    /// <summary>
    /// The parent tag of this <see cref="Tag"/> member.
    /// </summary>
    /// <value>
    /// A <see cref="Tag"/> representing the immediate parent tag of this tag member. Will be <c>null</c>
    /// for all root tag objects.
    /// </value>
    /// <remarks>
    /// This property helps model the hierarchical structure of a tag object. Tags have a <see cref="Value"/>
    /// which can represent a nested complex data type. This class models this by keeping references to the <c>Root</c>
    /// and <c>Parent</c> tags for each tag object. Only nested tag members should have a <c>Parent</c>.
    /// </remarks>
    /// <seealso cref="Base"/>
    public Tag? Parent { get; }

    /// <summary>
    /// The root tag of this <see cref="Tag"/> member.
    /// </summary>
    /// <value>A <see cref="Tag"/> representing the root tag.</value>
    /// <remarks>
    /// This property helps model the hierarchical structure of a tag object. Tags have a <see cref="Value"/>
    /// which can represent a nested complex data type. This class models this by keeping references to the <c>Root</c>
    /// and <c>Parent</c> tags for each tag object. All tags should have a <c>Root</c>.
    /// </remarks>
    /// <seealso cref="Parent"/>
    public Tag Base => GetBaseTag();

    /// <summary>
    /// Gets the <see cref="Tag"/> object from the L5X that is the alias for this tag object.
    /// </summary>
    /// <value>The <see cref="Tag"/> object that represents the alias if found. If this object is not
    /// attached, or <see cref="AliasFor"/> is not set, then this will return <c>null</c>.</value>
    public Tag? Alias => GetAliasTag();


    /// <summary>
    /// The collection of <see cref="Comment"/> configured for this tag.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TObject}"/> wrapping the root collection of tag units.</value>
    /// <remarks>
    /// <para>
    /// This will always operate over the root Comments element, regardless of which tag member object
    /// this is called from. The caller is responsible for ensuring proper configuration of the comment collection.
    /// </para>
    /// <para>
    /// Note that setting <see cref="Description"/> from a given tag or nested tag member will update this collection
    /// using that tag's operand value, which simplifies updating this collection, as you don't need to specify the
    /// operand, you can instead set the value.
    /// </para>
    /// </remarks>
    public LogixContainer<Comment>? Comments
    {
        get => Base.TryGetContainer<Comment>();
        set => Base.SetContainer(value);
    }

    /// <summary>
    /// The collection of <see cref="Unit"/> configured for this tag.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TObject}"/> wrapping the root collection of tag units.</value>
    /// <remarks>
    /// <para>
    /// This will always operate over the root EngineeringUnits element, regardless of which tag member object
    /// this is called from. The caller is responsible for ensuring proper configuration of the units' collection.
    /// </para>
    /// <para>
    /// Note that setting <see cref="Unit"/> from a given tag or nested tag member will update this collection
    /// using that tag's operand value. This simplifies updating the collection.
    /// </para>
    /// </remarks>
    public LogixContainer<Unit>? Units
    {
        // ReSharper disable once ExplicitCallerInfoArgument would like Units instead of EngineeringUnits
        get => Base.TryGetContainer<Unit>(L5XName.EngineeringUnits);
        set => Base.SetContainer(value);
    }

    /// <inheritdoc />
    public override IEnumerable<ILogixEntity> Dependencies()
    {
        var dependencies = new List<ILogixEntity>();

        if (TagType is not null && TagType == TagType.Alias && Alias is not null)
        {
            dependencies.Add(Alias);
            dependencies.AddRange(Alias.Dependencies());
        }

        if (TryResolveType(DataType, out var type))
        {
            dependencies.Add(type);
            dependencies.AddRange(type.Dependencies());
        }

        return dependencies;
    }

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
    /// (i.e., returns a non-nullable reference type).
    /// </remarks>
    public Tag this[TagName tagName]
    {
        get
        {
            if (tagName is null) throw new ArgumentNullException(nameof(tagName));
            if (tagName.IsEmpty) return this;

            var member = Value.Member(tagName.Root);

            if (member is null)
                throw new ArgumentException(
                    $"No member with name '{tagName.Root}' exists in the tag data structure for type {DataType}.");

            var tag = new Tag(member, this);
            var remaining = TagName.Combine(tagName.Members.Skip(1));
            return remaining.IsEmpty ? tag : tag[remaining];
        }
    }

    /// <summary>
    /// Adds a new member to the tag's complex data structure.
    /// </summary>
    /// <param name="name">The name of the member to add to the tag's data structure.</param>
    /// <param name="value">The <see cref="LogixData"/> of the member to add to the tag's data structure.</param>
    /// <exception cref="InvalidOperationException">The current tag does not contain a mutable complex logix type.</exception>
    /// <remarks>
    /// This will operate relative to the current tag member object, and is simply a call to the underlying
    /// <see cref="StructureData"/> <c>Add</c> method. Therefore, this is simply a helper to make mutating tag structures
    /// more concise.
    /// </remarks>
    public void AddMember(string name, LogixData value)
    {
        if (Value is not StructureData structure)
            throw new InvalidOperationException($"The type {Value.GetType()} is not a mutable data structure.");

        structure.Add(name, value);
    }

    /// <summary>
    /// Removes a member with the specified name from the tag's complex data structure.
    /// </summary>
    /// <param name="name">The name of the member to remove.</param>
    /// <exception cref="InvalidOperationException">The current tag does not contain a mutable complex logix type.</exception>
    /// <remarks>
    /// This will operate relative to the current tag member object, and is simply a call to the underlying
    /// <see cref="StructureData"/> <c>Remove</c> method. Therefore, this is simply a helper to make mutating tag structures
    /// more concise.
    /// </remarks>
    public void RemoveMember(string name)
    {
        if (Value is not StructureData structure)
            throw new InvalidOperationException($"The type {Value.GetType()} is not a mutable data structure.");

        structure.Remove(name);
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
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));
        if (tagName.IsEmpty) return this;

        var member = Value.Member(tagName.Root);
        if (member is null) return null;

        var tag = new Tag(member, this);
        var remaining = TagName.Combine(tagName.Members.Skip(1));
        return remaining.IsEmpty ? tag : tag.Member(remaining);
    }

    /// <summary>
    /// Gets this and all descendent tag members of the tag data structure.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Tag"/> objects.</returns>
    /// <remarks>
    /// This recursively traverses the hierarchical data structure of tag's <see cref="Value"/> and returns all
    /// descendant tags, as well as this tag, in a flat collection.
    /// </remarks>
    public IEnumerable<Tag> Members()
    {
        var members = Parent is null ? [this] : new List<Tag>();

        foreach (var member in Value.Members)
        {
            var tagMember = new Tag(member, this);
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

        var members = Parent is null && predicate.Invoke(TagName) ? [this] : new List<Tag>();

        foreach (var member in Value.Members)
        {
            var tag = new Tag(member, this);

            if (predicate.Invoke(tag.TagName))
                members.Add(tag);

            members.AddRange(tag.Members(predicate));
        }

        return members;
    }

    /// <summary>
    /// Gets this and all descendent tag members of the tag data structure that satisfy the specified tag predicate.
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

        var members = Parent is null && predicate.Invoke(this) ? [this] : new List<Tag>();

        foreach (var member in Value.Members)
        {
            var tag = new Tag(member, this);

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
        if (tagName.IsEmpty) return Members();

        var member = Value.Member(tagName.Root);
        if (member is null) return [];

        var tag = new Tag(member, this);
        var remaining = TagName.Combine(tagName.Members.Skip(1));
        return remaining.IsEmpty ? tag.Members() : tag.MembersOf(remaining);
    }

    /// <summary>
    /// Returns a collection of all descendent tag names of the current <c>Tag</c>, including the tag name of the
    /// this <c>Tag</c>. 
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> of <see cref="TagName"/> containing the tag name and all child tag names.
    /// </returns>
    public IEnumerable<TagName> TagNames() => Members().Select(t => t.TagName);

    /// <inheritdoc />
    public override string ToString() => TagName;

    /// <summary>
    /// Creates a new <see cref="Tag"/> with the provided name and specified type parameter.
    /// </summary>
    /// <param name="name">The name of the tag.</param>
    /// <typeparam name="TData">The logix data type of the tag. Type must have a parameterless constructor to create.</typeparam>
    /// <returns>A new <see cref="Tag"/> object with specified parameters.</returns>
    public static Tag New<TData>(string name) where TData : LogixData, new()
    {
        return new Tag { Name = name, Value = new TData() };
    }

    /// <summary>
    /// Builds a tag using the fluent tag builder API to intuitively construct simple or complex tag objects. 
    /// </summary>
    /// <param name="name">The name of the tag to build.</param>
    /// <returns>An instance of <see cref="ITagBaseTypeBuilder"/> to enable tag configuration.</returns>
    public static ITagBaseTypeBuilder New(string name)
    {
        return new TagBaseTypeBuilder(name);
    }

    #region Internal

    /// <summary>
    /// Retrieves the root tag by traversing the hierarchy of parent tags.
    /// </summary>
    /// <returns>The root <see cref="Tag"/> representing the top-most tag in the hierarchy.</returns>
    private Tag GetBaseTag()
    {
        var current = this;

        while (current.Parent is not null)
        {
            current = current.Parent;
        }

        return current;
    }

    /// <summary>
    /// Retrieves and deserializes the data associated with the current element.
    /// This element can be either the base tag element or a child data member element.
    /// </summary>
    private LogixData GetData()
    {
        //This is a member tag if we have a parent.
        if (Parent is not null)
        {
            return Element.Deserialize<LogixData>();
        }

        //Handle module-defined tags that have alias data in the parent module element.
        if (Element.Name.LocalName is L5XName.InAliasTag or L5XName.OutAliasTag)
        {
            return GetModuleAliasData();
        }

        //If we get here, we should be at the base tag element.
        if (Element.TryGetFormattedData(out var data))
        {
            return data;
        }

        return LogixType.Null;
    }

    /// <remarks>
    /// After setting the data element, we need to also update the tag attributes to keep them in with
    /// the currently assigned value.
    /// </remarks>
    private void SetData(LogixData? value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        //This is a member tag if we have a parent. Forward to the setter of the LogixMember.
        if (Parent is not null)
        {
            Element.Deserialize<LogixData>().Update(value);
            return;
        }

        //Handle module-defined tags that have alias data in the parent module element.
        if (Element.Name.LocalName is L5XName.InAliasTag or L5XName.OutAliasTag)
        {
            var aliasData = GetModuleAliasData();
            aliasData.Update(value);
            return;
        }

        //If we get here, we should be at the base tag element.
        if (Element.TryGetFormattedData(out var formatted))
        {
            formatted.Update(value);
            return;
        }

        Element.Add(L5XName.Data, DataFormat.Format(value, GetType()));
        EnsureOrder();
        UpdateDataAttributes(value);
    }

    /// <summary>
    /// handles updating the root tag element DataType, Radix, and Dimensions when we replace the root
    /// data element.
    /// </summary>
    private void UpdateDataAttributes(LogixData value)
    {
        Element.SetAttributeValue(L5XName.DataType, value.Name);

        var radix = value is AtomicData atomicType ? atomicType.Radix
            : value is ArrayData arrayType && arrayType.Radix != Radix.Null ? arrayType.Radix
            : null;
        Element.SetAttributeValue(L5XName.Radix, radix);

        var dimensions = value is ArrayData array ? array.Dimensions : null;
        Element.SetAttributeValue(L5XName.Dimensions, dimensions);
    }

    /// <summary>
    /// Retrieves the alias <see cref="Tag"/> associated with the current tag, if it exists.
    /// </summary>
    /// <returns>
    /// The alias <see cref="Tag"/> if the current tag is an alias and the document contains the alias tag;
    /// otherwise, <c>null</c>.
    /// </returns>
    private Tag? GetAliasTag()
    {
        if (AliasFor is null || !TryGetDocument(out var doc))
            return null;

        return doc.TryGet<Tag>(AliasFor, out var alias) ? alias : null;
    }

    /// <summary>
    /// Handles getting a comment value for the current tag.
    /// This method emulates pass-through documentation by attempting to find the corresponding
    /// user-defined type from the L5X content. If found, we will append that description to the parent (based on the
    /// configured pass-through options for the project)
    /// </summary>
    private string? GetTagDescription()
    {
        if (Parent is null)
            return Element.Element(L5XName.Description)?.Value;

        //Local member comments always override pass-through and inherited descriptions
        var comment = Comments?.FirstOrDefault(c => TagName.HasOperand(c.Operand));
        if (comment is not null) return comment.Value;

        //If there is no attached document or the corresponding data type is not available, default to the inherited description.
        //Note that we need to always use the parent type. If we are here, we know we have a parent and could
        //potentially be some user-defined type.
        if (!TryGetDocument(out var doc) || !doc.TryGet<DataType>(Parent.DataType, out var type))
            return Parent.Description;

        //Here we have the corresponding type definition and can use the description to emulate pass-through.
        //Enable means returning the definition description.
        if (Equals(doc.Controller.PassThroughConfiguration, PassThroughOption.Enabled))
        {
            return type.Description;
        }

        //EnableWithAppend means append the definition description to the parent.
        if (Equals(doc.Controller.PassThroughConfiguration, PassThroughOption.EnabledWithAppend))
        {
            var description = type.Members.FirstOrDefault(m => m.Name == Name)?.Description;
            return Parent.Description + " " + description;
        }

        //Disable means we don't use pass-through. Default to Inherited description.
        return Parent.Description;
    }

    /// <summary>
    /// Handles setting a comment element of the root tag structure for the current tag name operand. 
    /// </summary>
    private void SetTagDescription(string? value)
    {
        //If the parent is null, forward the call to base implementation (which is essentially setting the description
        //element of the root tag component).
        if (Parent is null)
        {
            base.Description = value;
            return;
        }

        //Child descriptions are set in the 'Comments' element of a tag.
        if (value is null || value.IsEmpty())
        {
            Comments?.RemoveAll(c => TagName.HasOperand(c.Operand));
            return;
        }

        //Comments are only initialized in the XML element when a comment exists.
        Comments ??= [];

        if (Comments!.Any(c => TagName.HasOperand(c.Operand)))
        {
            Comments!.Update(c => c.Value = value, c => TagName.HasOperand(c.Operand));
            return;
        }

        Comments!.Add(new Comment(TagName.Operand, value));
    }

    /// <summary>
    /// Handles getting a unit value for the current tag name operand. 
    /// </summary>
    private string? GetUnit()
    {
        return Units?.FirstOrDefault(x => TagName.HasOperand(x.Operand))?.Value;
    }

    /// <summary>
    /// Handles setting a unit element of the root tag structure for the current tag name operand. 
    /// </summary>
    private void SetUnit(string? value)
    {
        if (value is null || value.IsEmpty())
        {
            Units?.RemoveAll(x => TagName.HasOperand(x.Operand));
            return;
        }

        Units ??= [];

        if (Units!.Any(c => TagName.HasOperand(c.Operand)))
        {
            Units!.Update(c => c.Value = value, c => TagName.HasOperand(c.Operand));
            return;
        }

        Units!.Add(new Unit(TagName.Operand, value));
    }

    /// <summary>
    /// Attempts to retrieve the In/Out alias tag member for the parent rack-connected module. This will allow the
    /// caller to directly interact with the alias data element that a given In/out alias tag represents (this data is
    /// stored in a separate module element). If not found, then we are going to return a null data instance.
    /// </summary>
    private LogixData GetModuleAliasData()
    {
        //We can use this name to determine which parent module tag to retrieve.
        var parts = Name.Split(':', StringSplitOptions.RemoveEmptyEntries);

        //We have to have all 3 parts (module name, I/O suffix, and slot number) to find the correct member.
        //If not, then we will default to null data instance;
        if (parts.Length != 3 || !TryGetDocument(out var doc))
            return LogixType.Null;

        var name = parts[0];
        var slot = parts[1];
        var suffix = parts[2];

        var tag = doc.Query<Module>()
            .FirstOrDefault(m => m.Name == name)
            ?.Tags.FirstOrDefault(t => t.Name.EndsWith(suffix))
            ?.Member($"Slot[{slot}]"); //Not sure if this is all rack connections tag structure, but for now will hard code

        return tag is not null ? tag.Value : LogixType.Null;
    }

    #endregion
}