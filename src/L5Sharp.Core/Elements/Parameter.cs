using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A component of the <see cref="AddOnInstruction"/> that makes up the structure of the instruction type.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class Parameter : LogixObject
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
        DataType = nameof(DINT);
        Dimension = Dimensions.Empty;
        Usage = TagUsage.Input;
        Radix = Radix.Decimal;
        Required = false;
        Visible = false;
        ExternalAccess = ExternalAccess.ReadWrite;
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
    public Parameter(string name, AtomicData value, TagUsage? usage = default) : this()
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
        get => GetRequiredValue<string>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// The description of the <c>Parameter</c>.
    /// </summary>
    /// <value>A <see cref="string"/> containing the component description if it exists; Otherwise, <c>null</c>.</value>
    public string? Description
    {
        get => GetProperty<string>();
        set => SetDescription(value);
    }

    /// <summary>
    /// The name of the data type of the <c>Parameter</c>.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> containing the data type name of the <c>Parameter</c>.
    /// Default is <see cref="string.Empty"/>.
    /// Valid value is required for valid import.
    /// </value>
    public string? DataType
    {
        get => GetValue<string>();
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
    public Dimensions? Dimension
    {
        get => GetValue<Dimensions>();
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
        get => GetValue<Radix>();
        set => SetValue(value?.Value);
    }

    /// <summary>
    /// The external access of the <c>Parameter</c>. 
    /// </summary>
    /// <value>
    /// A <see cref="Core.ExternalAccess"/> representing read/write access of the <c>Parameter</c>.
    /// Default is <see cref="L5Sharp.Core.ExternalAccess.ReadWrite"/>.
    /// </value>
    public ExternalAccess? ExternalAccess
    {
        get => GetValue<ExternalAccess>();
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
        get => GetValue<TagType>();
        set => SetValue(value);
    }

    /// <summary>
    /// The usage option indicating the scope in which the <c>Parameter</c> is visible or usable from.
    /// </summary>
    /// <value>
    /// A <see cref="TagUsage"/> option representing the <c>Parameter</c> scope.
    /// Default for AoiBlock is <see cref="TagUsage.Input"/>. Only valid options for AoiBlock are Input, Output,
    /// and InOut.
    /// </value>
    public TagUsage Usage
    {
        get => GetRequiredValue<TagUsage>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Indicates whether the <c>Parameter</c> is required form the instruction code clock.
    /// </summary>
    /// <value>
    /// <c>true</c> if the <c>Parameter</c> is required; otherwise, false. Default is <c>false</c>.</value>
    public bool? Required
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Indicates whether the <c>Parameter</c> is visible from the instruction code block.
    /// </summary>
    /// <value><c>true</c> if the <c>Parameter</c> is visible; otherwise, false. Default is <c>false</c>.</value>
    public bool? Visible
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// The tag name of the tag that is the alias of the current <c>Parameter</c>.
    /// </summary>
    /// <value>A <see cref="TagName"/> string representing the full tag name of the alias tag.</value>
    public TagName? AliasFor
    {
        get => GetValue<TagName>();
        set => SetValue(value);
    }

    /// <summary>
    /// A default value of the <c>Parameter</c> when instantiated.
    /// </summary>
    /// <value>An <see cref="AtomicData"/> representing the default value/data. Default is <c>null</c>.</value>
    public LogixData Default
    {
        get => GetData();
        set => SetData(value);
    }

    /// <summary>
    /// Indicates whether the <c>Parameter</c> is a constant.
    /// </summary>
    /// <value><c>true</c> if the <c>Parameter</c> is constant; otherwise, <c>false</c>.</value>
    /// <remarks>Only value type tags have the ability to be set as a constant. Default is <c>false</c>.</remarks>
    public bool? Constant
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Creates a new <see cref="Tag"/> instance from this <see cref="Parameter"/> configuration.
    /// </summary>
    /// <param name="tagName">The name of the tag.</param>
    /// <returns>A <see cref="Tag"/> instance with the specified name and value populated using this parameter's configuration.</returns>
    /// <exception cref="InvalidOperationException"><see cref="DataType"/> is null or emprty.</exception>
    /// <remarks>
    /// This is a helper to allow us to generate default tag instance from a parameter element. This will internally
    /// generate a default <see cref="LogixData"/> instance using the configured data typ name of the parameter.
    /// </remarks>
    public Tag ToTag(string tagName)
    {
        if (string.IsNullOrEmpty(DataType))
            throw new InvalidOperationException("Can not generate Tag with null or empty DataType name.");

        var isArray = Dimension is not null && Dimension.Length > 0;
        var data = Default is not NullData ? Default : LogixData.Create(DataType);
        var value = !isArray ? data.As<LogixData>() : new ArrayData<LogixData>(data, Dimension!);
        return new Tag(tagName, value);
    }

    /// <summary>
    /// Creates a new <see cref="Member"/> from the given <see cref="Parameter"/> configuration, which uses the
    /// <see cref="DataType"/> <see cref="Default"/> and <see cref="Name"/> in order to generate a new member instance.
    /// </summary>
    /// <exception cref="InvalidOperationException"><see cref="DataType"/> is null or empty -or- <see cref="Usage"/>
    /// is not configured as Input or Output.</exception>
    /// <returns>A <see cref="Member"/> instance with the name and default value of the current parameter.</returns>
    /// <remarks>
    /// This is helper to allow us to generate default tag members from a given parameter, so that we
    /// can easily build up an in memory instance of an AOI tag component. Note that this method is intended to only be
    /// called on Input or Output parameters which Logix requires to be atomic type parameters. these are the only parameter
    /// types that are available on a AOI tag instance.
    /// </remarks>
    public Member ToMember()
    {
        if (string.IsNullOrEmpty(DataType))
            throw new InvalidOperationException("Can not generate Member with null or empty DataType name.");

        if (Usage != TagUsage.Input && Usage != TagUsage.Output)
            throw new InvalidOperationException("Can only generate Member for Input or Output type parameters.");

        var value = Default is not NullData ? Default : AtomicData.Default(DataType);
        return new Member(Name, value);
    }
}