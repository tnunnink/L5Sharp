using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Components;

/// <summary>
/// A component of the <see cref="AddOnInstruction"/> that makes up the structure of the instruction type.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public sealed class Parameter : LogixComponent<Parameter>
{
    /// <summary>
    /// Creates a new <see cref="Parameter"/> with default values.
    /// </summary>
    public Parameter()
    {
        TagType = TagType.Base;
        DataType = string.Empty;
        Dimension = Dimensions.Empty;
        Usage = TagUsage.Input;
        Radix = Radix.Null;
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
    /// The name of the data type of the parameter.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> containing the data type name of the parameter.
    /// Default is <see cref="string.Empty"/>.
    /// Valid value is required for valid import.
    /// </value>
    public string? DataType
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// The array dimension of the parameter.
    /// </summary>
    /// <value>
    /// A <see cref="Core.Dimensions"/> representing the array dimensions of the parameter.
    /// Default is <see cref="Core.Dimensions.Empty"/>.
    /// Members should not have multidimensional arrays.
    /// </value>
    public Dimensions? Dimension
    {
        get => GetValue<Dimensions>();
        set => SetValue(value);
    }

    /// <summary>
    /// The radix value format of the parameter.
    /// </summary>
    /// <value>
    /// A <see cref="Enums.Radix"/> representing the value type format of the parameter.
    /// Default is <see cref="Enums.Radix.Null"/>.
    /// Should be <see cref="Enums.Radix.Null"/> for all non atomic types.
    /// </value>
    public Radix? Radix
    {
        get => GetValue<Radix>();
        set => SetValue(value?.Value);
    }

    /// <summary>
    /// The external access of the parameter. 
    /// </summary>
    /// <value>
    /// A <see cref="Enums.ExternalAccess"/> representing read/write access of the parameter.
    /// Default is <see cref="Enums.ExternalAccess.ReadWrite"/>.
    /// </value>
    public ExternalAccess? ExternalAccess
    {
        get => GetValue<ExternalAccess>();
        set => SetValue(value);
    }
        
    /// <summary>
    /// A type indicating whether the current parameter is a base tag, or alias for another tag instance.
    /// </summary>
    /// <value>
    /// A <see cref="Enums.TagType"/> option representing the type of parameter.
    /// Default is <see cref="Enums.TagType.Base"/>.
    /// </value>
    public TagType? TagType
    {
        get => GetValue<TagType>();
        set => SetValue(value);
    }
        
    /// <summary>
    /// The usage option indicating the scope in which the parameter is visible or usable from.
    /// </summary>
    /// <value>
    /// A <see cref="Enums.TagUsage"/> option representing the parameter scope.
    /// Default for AOI is <see cref="Enums.TagUsage.Input"/>. Only valid options for AOI are Input, Output,
    /// and InOut.
    /// </value>
    public TagUsage Usage
    {
        get => GetRequiredValue<TagUsage>();
        set => SetRequiredValue(value);
    }
        
    /// <summary>
    /// Indicates whether the parameter is required form the instruction code clock.
    /// </summary>
    /// <value>
    /// <c>true</c> if the parameter is required; otherwise, false. Default is <c>false</c>.</value>
    public bool? Required
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }
        
    /// <summary>
    /// Indicates whether the parameter is visible from the instruction code block.
    /// </summary>
    /// <value><c>true</c> if the parameter is visible; otherwise, false. Default is <c>false</c>.</value>
    public bool? Visible 
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }
        
    /// <summary>
    /// The tag name of the tag that is the alias of the current parameter.
    /// </summary>
    /// <value>A <see cref="Core.TagName"/> string representing the full tag name of the alias tag.</value>
    public TagName? AliasFor
    {
        get => GetValue<TagName>();
        set => SetValue(value);
    }
        
    /// <summary>
    /// A default value of the parameter when instantiated.
    /// </summary>
    /// <value>An <see cref="AtomicType"/> representing the default value/data. Default is <c>null</c>.</value>
    public AtomicType? Default { get; set; } //todo figure this out
        
    /// <summary>
    /// Indicates whether the parameter is a constant.
    /// </summary>
    /// <value><c>true</c> if the parameter is constant; otherwise, <c>false</c>.</value>
    /// <remarks>Only value type tags have the ability to be set as a constant. Default is <c>false</c>.</remarks>
    public bool? Constant
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }
}