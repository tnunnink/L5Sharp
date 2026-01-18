using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A component of the <see cref="AddOnInstruction"/> that makes up the structure of the instruction type.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[LogixElement(L5XName.Parameter)]
public class Parameter : LogixObject<Parameter>
{
    /// <inheritdoc />
    protected override List<string> ElementOrder =>
    [
        L5XName.Description,
        L5XName.DefaultData
    ];

    /// <summary>
    /// Creates a new <see cref="Parameter"/> with default values.
    /// </summary>
    public Parameter() : base(L5XName.Parameter)
    {
        Name = string.Empty;
        TagType = TagType.Base;
        DataType = string.Empty;
        Dimension = Dimensions.Empty;
        Usage = TagUsage.Input;
        Radix = Radix.Null;
        Required = false;
        Visible = false;
        ExternalAccess = Access.ReadWrite;
        Constant = false;
    }

    /// <summary>
    /// Creates a new <see cref="Parameter"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Parameter(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new <see cref="Parameter"/> with the provided name, default atomic value, and optional usage.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="value">The default <see cref="AtomicData"/> of the parameter.</param>
    /// <param name="usage">The <see cref="TagUsage"/> type of the parameter (this should be Input or Output).</param>
    /// <remarks>
    /// This constructor if meant to create simple atomic Input/Output parameters which can be used as members
    /// of the complex tag data structure for an AOI component.
    /// </remarks>
    public Parameter(string name, AtomicData value, TagUsage? usage = null) : this()
    {
        Element.SetAttributeValue(L5XName.Name, name);
        DataType = value.Name;
        Radix = value.Radix;
        Default = value;
        Usage = usage ?? TagUsage.Input;
    }

    /// <summary>
    /// The unique name of the <c>Parameter</c>.
    /// </summary>
    /// <value>A <see cref="string"/> representing the component name. This property is required for valid elements.</value>
    public string Name
    {
        get => GetRequiredValue();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// The description of the <c>Parameter</c>.
    /// </summary>
    /// <value>A <see cref="string"/> containing the component description if it exists; Otherwise, <c>null</c>.</value>
    public string? Description
    {
        get => GetProperty();
        set => SetProperty(value);
    }

    /// <summary>
    /// The name of the data type of the <c>Parameter</c>.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> containing the data type name of the <c>Parameter</c>.
    /// Default is <see cref="string.Empty"/>.
    /// Valid value is required for valid import.
    /// </value>
    public string DataType
    {
        get => GetValue() ?? Alias?.DataType ?? throw Element.L5XError(L5XName.DataType);
        set => SetValue(value);
    }

    /// <summary>
    /// The array dimension of the <c>Parameter</c>.
    /// </summary>
    /// <value>
    /// A <see cref="Dimensions"/> representing the array dimensions of the <c>Parameter</c>.
    /// Default is <see cref="Dimensions.Empty"/>.
    /// Members should not have multidimensional arrays.
    /// </value>
    public Dimensions Dimension
    {
        get => GetValue(Dimensions.Parse) ?? Dimensions.Empty;
        set => SetValue(value);
    }

    /// <summary>
    /// The radix value format of the <c>Parameter</c>.
    /// </summary>
    /// <value>
    /// A <see cref="Core.Radix"/> representing the value type format of the <c>Parameter</c>.
    /// Default is <see cref="L5Sharp.Core.Radix.Null"/>.
    /// Should be <see cref="L5Sharp.Core.Radix.Null"/> for all non-atomic types.
    /// </value>
    public Radix? Radix
    {
        get => GetValue(Radix.Parse);
        set => SetValue(value?.Value);
    }

    /// <summary>
    /// The external access of the <c>Parameter</c>. 
    /// </summary>
    /// <value>
    /// A <see cref="Access"/> representing read/write access of the <c>Parameter</c>.
    /// Default is <see cref="Access.ReadWrite"/>.
    /// </value>
    public Access? ExternalAccess
    {
        get => GetValue(Access.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// A type indicating whether the current <c>Parameter</c> is a base tag, or alias for another tag instance.
    /// </summary>
    /// <value>
    /// A <see cref="Core.TagType"/> option representing the type of <c>Parameter</c>.
    /// Default is <see cref="L5Sharp.Core.TagType.Base"/>.
    /// </value>
    public TagType? TagType
    {
        get => GetValue(TagType.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// The <see cref="TagUsage"/> option indicating the scope in which the parameter is visible or usable from.
    /// </summary>
    /// <remarks>
    /// Default is <see cref="TagUsage.Input"/>. The only valid options for AoiBlock are Input, Output, and InOut.
    /// </remarks>
    public TagUsage Usage
    {
        get => GetRequiredValue(TagUsage.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Indicates whether the <see cref="Parameter"/> is required from the instruction code clock.
    /// </summary>
    public bool? Required
    {
        get => GetOptionalBool();
        set => SetValue(value);
    }

    /// <summary>
    /// Indicates whether the <see cref="Parameter"/> is visible from the instruction code block.
    /// </summary>
    public bool? Visible
    {
        get => GetOptionalBool();
        set => SetValue(value);
    }

    /// <summary>
    /// The tag name of the tag that is the alias of the current <c>Parameter</c>.
    /// </summary>
    /// <value>A <see cref="TagName"/> string representing the full tag name of the alias tag.</value>
    public TagName? AliasFor
    {
        get => GetValue()?.ToTagName();
        set => SetValue(value);
    }

    /// <summary>
    /// A default value of the <c>Parameter</c> when instantiated.
    /// </summary>
    /// <value>An <see cref="AtomicData"/> representing the default value/data. Default is <c>null</c>.</value>
    public AtomicData? Default
    {
        get => Element.TryGetFormattedData(out var data) ? data.As<AtomicData>() : null;
        set => SetElement(DataFormat.Format(value, GetType()));
    }

    /// <summary>
    /// Indicates whether the <c>Parameter</c> is a constant.
    /// </summary>
    /// <value><c>true</c> if the <c>Parameter</c> is constant; otherwise, <c>false</c>.</value>
    /// <remarks>Only value type tags can be set as a constant. Default is <c>false</c>.</remarks>
    public bool? Constant
    {
        get => GetOptionalBool();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the parent <see cref="AddOnInstruction"/> of the current <see cref="Parameter"/> instance.
    /// This property navigates the hierarchy to find the nearest ancestor of type <see cref="AddOnInstruction"/>,
    /// or returns null if no such ancestor exists.
    /// </summary>
    public AddOnInstruction? Instruction => GetAncestor<AddOnInstruction>();

    /// <summary>
    /// Represents the associated alias of the parameter if one is defined.
    /// Provides a way to reference a <see cref="LocalTag"/> that serves as an alias for this parameter.
    /// </summary>
    public LocalTag? Alias => GetAncestor<AddOnInstruction>()?.LocalTags.SingleOrDefault(t => t.Name == AliasFor);

    /// <summary>
    /// Creates a new <see cref="Tag"/> instance from this <see cref="Parameter"/> configuration.
    /// </summary>
    /// <param name="tagName">The name of the tag.</param>
    /// <returns>A <see cref="Tag"/> instance with the specified name and value populated using this parameter's configuration.</returns>
    /// <exception cref="InvalidOperationException"><see cref="DataType"/> is null or empty.</exception>
    /// <remarks>
    /// This is a helper to allow us to generate a default tag instance from a parameter element. This will internally
    /// generate a default <see cref="LogixData"/> instance using the configured data typ name of the parameter.
    /// </remarks>
    public Tag ToTag(string tagName)
    {
        var isArray = Dimension.Length > 0;

        var data = Default ?? (LogixType.TryCreate(DataType, out var registered)
            ? registered
            : new StructureData(DataType));

        var value = !isArray ? data : ArrayData.New(data, Dimension);

        return new Tag(tagName, value);
    }

    /// <summary>
    /// Creates a new <see cref="LogixMember"/> from the given <see cref="Parameter"/> configuration, which uses the
    /// <see cref="DataType"/> <see cref="Default"/> and <see cref="Name"/> in order to generate a new member instance.
    /// </summary>
    /// <exception cref="InvalidOperationException"><see cref="DataType"/> is null or empty -or- <see cref="Usage"/>
    /// is not configured as Input or Output.</exception>
    /// <returns>A <see cref="LogixMember"/> instance with the name and default value of the current parameter.</returns>
    /// <remarks>
    /// This is a helper to allow us to generate default tag members from a given parameter so that we
    /// can easily build up an in memory instance of an AOI tag component. Note that this method is intended to only be
    /// called on Input or Output parameters, which Logix requires to be atomic type parameters.
    /// These are the only parameter types that are available on an AOI tag instance.
    /// </remarks>
    public LogixMember ToMember()
    {
        if (Usage != TagUsage.Input && Usage != TagUsage.Output)
            throw new InvalidOperationException("Can only generate Member for Input or Output type parameters.");

        var isArray = Dimension.Length > 0;

        //If the parameter has default data, we can opt to return a member with the correcly initialized data value.
        if (Default is not null)
        {
            LogixData defaultValue = isArray ? ArrayData.New(Default, Dimension) : Default;
            return new LogixMember(Name, defaultValue);
        }

        //If the type is registered, we can create the instance using the registered factory.
        if (LogixType.TryCreate(DataType, out var registered))
        {
            var value = isArray ? ArrayData.New(registered, Dimension) : registered;
            return new LogixMember(Name, value);
        }

        //If not, we can try to get the data type definition from the l5X if attached
        //and use that to recursively build up the complex type.
        var data = TryGetDocument(out var doc) && doc.TryGet<DataType>(DataType, out var definition)
            ? definition.ToData()
            : new StructureData(DataType);

        var structure = isArray ? ArrayData.New(data, Dimension) : data;
        return new LogixMember(Name, structure);
    }

    /// <inheritdoc />
    public override string ToString() => Name;
}